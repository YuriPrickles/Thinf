//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.ObjectData;
//using Thinf.Items;
//using Thinf.Items.Placeables;
//using Thinf.NPCs;

//namespace Thinf.Blocks
//{
//	public class FakeLifeFruitTile : ModTile
//	{

//		public override void SetDefaults()
//		{
//			Main.tileSpelunker[Type] = true;
//			Main.tileFrameImportant[Type] = true;
//			Main.tileCut[Type] = false;
//			Main.tileNoFail[Type] = true;

//			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);

//			TileObjectData.newTile.AnchorValidTiles = new int[]
//			{
//				TileID.JungleGrass
//			};
//			TileObjectData.addTile(Type);

//		}

//		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
//		{
//			Tile tile = Framing.GetTileSafely(i, j);
//			tile.frameX = (short)((Main.rand.Next(3) + 1) * 18);
//		}

//        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
//		{
//			NPC.NewNPC(i * 16, (j - 75) * 16, ModContent.NPCType<StrawberryShifterLifeFruit>());
//		}
//        public override bool Drop(int i, int j)
//		{
//			Main.NewText("You don't need that extra HP.");
//			return false;
//		}

//		public override void RandomUpdate(int i, int j)
//		{
//			Tile tile = Framing.GetTileSafely(i, j);
//			tile.frameX = (short)((Main.rand.Next(3) + 1) * 18);
//		}
//	}
//}