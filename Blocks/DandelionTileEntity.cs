using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Blocks
{
    //A plant with 3 stages, planted, growing and grown.
    //Sadly, modded plants are unable to be grown by the flower boots
    public class DandelionTileEntity : ModTileEntity
    {
        bool spinachBoost = false;
        int spikeTimer = 0;
        private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<DandelionTile>();
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
                if (spikeTimer == 12)
                {
                    for (int i = 0; i < Main.maxNPCs; ++i)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && !target.friendly && !target.dontTakeDamage && target.Distance(Position.ToVector2().ToWorldCoordinates()) < 800 && target.type != NPCID.TargetDummy)
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
                                for (int f = 0; f < Main.rand.Next(5, 7); ++f)
                                {
                                    Projectile projectile = Projectile.NewProjectileDirect(Position.ToVector2().ToWorldCoordinates(), new Vector2(Main.rand.Next(-4, 4), -3), ModContent.ProjectileType<DandyBomb>(), 250, 0, player.whoAmI);
                                    projectile.friendly = true;
                                    projectile.hostile = false;
                                }
                            }
                            else
                            {
                                for (int f = 0; f < Main.rand.Next(3, 4); ++f)
                                {
                                    Projectile projectile = Projectile.NewProjectileDirect(Position.ToVector2().ToWorldCoordinates(), new Vector2(Main.rand.Next(-4, 4), -3), ModContent.ProjectileType<DandyBomb>(), 150, 0, player.whoAmI);
                                    projectile.friendly = true;
                                    projectile.hostile = false;
                                }
                            }
                            break;
                        }
                    }
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