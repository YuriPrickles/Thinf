using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Blocks
{
    //A plant with 3 stages, planted, growing and grown.
    //Sadly, modded plants are unable to be grown by the flower boots
    public class PumpkitronTileEntity : ModTileEntity
    {
        bool spinachBoost = false;
        int spikeTimer = 0;
        int laserDelay = 0;
        private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<PumpkitronTile>();
        }
        public override void Update()
        {
            Player player = Thinf.FindNearestPlayer(256, Position.ToWorldCoordinates());
            spinachBoost = false;
            PlantStage stage = GetStage(Position.X, Position.Y);
            Tile tile = Main.tile[Position.X, Position.Y];
            if (stage == PlantStage.Grown)
            {
                spikeTimer++;
                if (spikeTimer >= 50 && spikeTimer % 10 == 0)
                {
                    for (int i = 0; i < Main.maxNPCs; ++i)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && !target.friendly && !target.dontTakeDamage && target.Distance(Position.ToVector2().ToWorldCoordinates()) < 1100 && target.type != NPCID.TargetDummy)
                        {
                            foreach (KeyValuePair<Point16, TileEntity> item in ByPosition)
                            {
                                if (item.Value.type == ModContent.TileEntityType<SpinachTileEntity>() && Vector2.Distance(item.Key.ToWorldCoordinates(), Position.ToWorldCoordinates()) < (16 * 16))
                                {
                                    spinachBoost = true;
                                }
                            }
                            if (spinachBoost)
                            {
                                Projectile projectile = Projectile.NewProjectileDirect(Position.ToVector2().ToWorldCoordinates(), Vector2.Zero, ProjectileID.DeathLaser, 24, 0, player.whoAmI);
                                projectile.friendly = true;
                                projectile.hostile = false;
                                projectile.magic = false;
                                projectile.minion = false;
                                projectile.melee = false;
                                projectile.thrown = false;
                                projectile.penetrate = 1;
                                projectile.ranged = false;
                                projectile.velocity = (projectile.DirectionTo(target.Center) * 27f).RotatedByRandom(MathHelper.ToRadians(1));
                            }
                            else
                            {
                                Projectile projectile = Projectile.NewProjectileDirect(Position.ToVector2().ToWorldCoordinates(), Vector2.Zero, ProjectileID.DeathLaser, 15, 0, player.whoAmI);
                                projectile.friendly = true;
                                projectile.hostile = false;
                                projectile.magic = false;
                                projectile.minion = false;
                                projectile.melee = false;
                                projectile.thrown = false;
                                projectile.penetrate = 1;
                                projectile.ranged = false;
                                projectile.velocity = (projectile.DirectionTo(target.Center) * 20f).RotatedByRandom(MathHelper.ToRadians(8));
                            }
                            break;
                        }
                    }
                }
                if (spikeTimer == 150)
                {
                    spikeTimer = 0;
                }
            }
        }
        public PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j); //Always use Framing.GetTileSafely instead of Main.tile as it prevents any errors caused from other mods
            return (PlantStage)(tile.frameX / FrameWidth);
        }
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
            //Main.NewText("i " + i + " j " + j + " t " + type + " s " + style + " d " + direction);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 1);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                return -1;
            }
            return Place(i, j);
        }
    }
}