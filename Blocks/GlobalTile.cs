using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf.NPCs;

namespace Thinf.Blocks
{
	internal sealed class GlobalTileThing : GlobalTile
	{
		public override bool Drop(int i, int j, int type)
		{
			Player player = Main.LocalPlayer;
			Tile tile = Framing.GetTileSafely(i, j);

			if (type == TileID.PalmTree && Main.rand.Next(5) == 0)
			{
				Item.NewItem(new Vector2(i * 16, j * 16), ModContent.ItemType<Coconut>(), Main.rand.Next(3) + 1);
			}

			if (type == TileID.PalmTree && Main.rand.Next(6) == 0)
			{
				Item.NewItem(new Vector2(i * 16, j * 16), ModContent.ItemType<PalmLeaf>(), Main.rand.Next(5) + 15);
			}

			if (type == TileID.Trees && tile.frameX/176 == 0 && Main.rand.Next(3) == 0)
			{
				Item.NewItem(new Vector2(i * 16, j * 16), ModContent.ItemType<Leaf>(), Main.rand.Next(5) + 15);
				MyPlayer.treesChopped++;
				if (MyPlayer.treesChopped >= 150)
                {
                    if (ModNameWorld.downedAcornSpirit)
                    {
                        Main.NewText("The Guardians of the Trees are too tired to put up with your nonsense.", Color.Green);
                    }
                    else
                    {
                        Main.NewText("The Guardians of the Trees are mad with your logging spree!", Color.Green);
                        NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AcornSpirit>());
                    }
                    MyPlayer.treesChopped = 0;
                }
			}

			return true;
		}
		public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			Tile isCrimstone = Framing.GetTileSafely(i, j + 1);
			if (type == TileID.ImmatureHerbs && tile.frameX == (18 * 3) && isCrimstone.type == TileID.Crimstone)
			{
				WorldGen.KillTile(i, j);
			}
			return true;
		}
		public override void RandomUpdate(int i, int j, int type)
		{
			if (type == TileID.Crimstone && WorldGen.TileEmpty(i, j - 1) && Main.rand.Next(240) == 0)
			{
				WorldGen.PlaceTile(i, j -1, ModContent.TileType<BloodclotTile>());
			}
			//if (Main.hardMode && type == TileID.JungleGrass && WorldGen.TileEmpty(i, j - 1) && Main.rand.Next(1) == 0)
			//{
			//	WorldGen.PlaceTile(i, j - 1, ModContent.TileType<FakeLifeFruitTile>());
			//}
			if (NPC.downedPlantBoss && (type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick) && WorldGen.TileEmpty(i, j - 1) && Main.rand.Next(240) == 0)
			{
				WorldGen.PlaceTile(i, j - 1, ModContent.TileType<GhostSproutTile>());
			}
			base.RandomUpdate(i, j, type);
		}
	}
}