using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Blocks
{
	public class TilledDirt : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = false;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileMergeDirt[Type] = true;
			drop = ItemID.DirtBlock;
			AddMapEntry(new Color(94, 77, 48));
		}
	}
}