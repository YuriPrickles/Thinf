using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Blocks
{
    //A plant with 3 stages, planted, growing and grown.
    //Sadly, modded plants are unable to be grown by the flower boots
    public class VacuucumberTileEntity : ModTileEntity
    {
        bool spinachBoost = false;
        int suckTimer = 0;
        private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<VacuucumberTile>();
        }
        public override void Update()
        {
            spinachBoost = false;
            PlantStage stage = GetStage(Position.X, Position.Y);
            Tile tile = Main.tile[Position.X, Position.Y];
            if (stage == PlantStage.Grown)
            {
                int dustSpawnAmount = 32;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Position.ToVector2().ToWorldCoordinates() + dustOffset * (16 * 16), DustID.Smoke, null, 0, default, 1.5f);
                    dust.noGravity = true;
                }
                suckTimer++;
                if (suckTimer == 90)
                {
                    for (int i = 0; i < Main.maxNPCs; ++i)
                    {
                        NPC target = Main.npc[i];
                        if (target.active && !target.friendly && !target.dontTakeDamage && target.Distance(Position.ToVector2().ToWorldCoordinates()) < (16 * 16) && target.type != NPCID.TargetDummy)
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
                                if (!target.boss)
                                {
                                    target.StrikeNPC(125, 0, 0);
                                    target.velocity = target.DirectionTo(Position.ToWorldCoordinates()) * 8f;
                                }
                            }
                            else
                            {
                                if (!target.boss)
                                {
                                    target.StrikeNPC(75, 0, 0);
                                    target.velocity = target.DirectionTo(Position.ToWorldCoordinates()) * 5f;
                                }
                            }
                        }
                    }
                    suckTimer = 0;
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