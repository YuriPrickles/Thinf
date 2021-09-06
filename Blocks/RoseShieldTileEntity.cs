using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Thinf.MyPlayer;

namespace Thinf.Blocks
{
    public class RoseShieldTileEntity : ModTileEntity
    {
        int spikeTimer = 0;
        private const int FrameWidth = 18;

        public override bool ValidTile(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            return tile.active() && tile.type == ModContent.TileType<RoseShieldTile>();
        }
        public override void Update()
        {
            Player player = Main.LocalPlayer;
            PlantStage stage = GetStage(Position.X, Position.Y);
            Tile tile = Main.tile[Position.X, Position.Y];
            if (stage == PlantStage.Grown)
            {
                if (player.Distance(Position.ToVector2().ToWorldCoordinates()) < 32 * 16)
                {
                    player.GetModPlayer<MyPlayer>().roseDefense = true;
                }
                int dustSpawnAmount = 32;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Position.ToVector2().ToWorldCoordinates() + dustOffset * (32 * 16), DustID.CrimtaneWeapons, null, 0, default, 1.5f);
                    dust.noGravity = true;
                }
            }
        }
        public PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (PlantStage)(tile.frameX / FrameWidth);
        }
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
        {
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