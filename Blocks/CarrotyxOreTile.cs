using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Blocks
{
	public class CarrotyxOreTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = false;
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileStone[Type] = true;
			mineResist = 4;
			minPick = 201;
			drop = ItemType<CarrotyxOre>();
			AddMapEntry(new Color(255, 128, 0));
		}
		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}