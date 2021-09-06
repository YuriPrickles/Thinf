using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Blocks
{
	public class PotatiumiteOreTile : ModTile
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
			drop = ItemType<PotatiumiteOre>();
			AddMapEntry(new Color(94, 77, 48));
		}
		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}