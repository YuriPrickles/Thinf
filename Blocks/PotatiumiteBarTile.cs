using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Blocks
{
	public class PotatiumiteBarTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = ItemType<PotatiumiteBar>();
			AddMapEntry(new Color(120, 98, 76));
		}

        
    }
}