using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Thinf.Items.Placeables;

namespace Thinf.Blocks
{

	//A plant with 3 stages, planted, growing and grown.
	//Sadly, modded plants are unable to be grown by the flower boots
	public class TomatoTile : ModTile
	{
		private const int FrameWidth = 18; //a field for readibilty and to kick out those magic numbers

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileCut[Type] = false;
			Main.tileNoFail[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);

			TileObjectData.newTile.AnchorValidTiles = new int[]
			{
				ModContent.TileType<TilledDirt>()
			};

			TileObjectData.addTile(Type);
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
				spriteEffects = SpriteEffects.FlipHorizontally;
		}

		public override bool Drop(int i, int j)
		{
			PlantStage stage = GetStage(i, j); //The current stage of the herb

			//Only drop items if the herb is grown
			if (stage == PlantStage.Grown)
				Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ModContent.ItemType<Tomato>(), Main.rand.Next(9) + 21);

			return false;
		}

		public override void RandomUpdate(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j); //Safe way of getting a tile instance
			PlantStage stage = GetStage(i, j); //The current stage of the herb

			//Only grow to the next stage if there is a next stage. We dont want our tile turning pink!
			if (Main.rand.Next(4) == 0)
			if (stage != PlantStage.Grown)
			{
				//Increase the x frame to change the stage
				tile.frameX += FrameWidth;

				//If in multiplayer, sync the frame change
				if (Main.netMode != NetmodeID.SinglePlayer)
					NetMessage.SendTileSquare(-1, i, j, 1);
			}
		}

		//A method to quickly get the current stage of the herb
		private PlantStage GetStage(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j); //Always use Framing.GetTileSafely instead of Main.tile as it prevents any errors caused from other mods
			return (PlantStage)(tile.frameX / FrameWidth);
		}
	}
}