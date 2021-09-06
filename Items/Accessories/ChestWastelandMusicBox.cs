using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	public class ChestWastelandMusicBox : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 40;
			item.accessory = true;
			item.useAnimation = 10;
			item.useTime = 15;
			item.value = Item.sellPrice(gold: 1, silver: 0);
			item.rare = ItemRarityID.LightPurple;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Blocks.ChestWastelandMusicBoxTile>();
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Storage Overflow)");
			Tooltip.SetDefault("This is a bop");
		}

	}
}