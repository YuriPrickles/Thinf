using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Placeables
{
	public class WandaPainting : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 40;
			item.autoReuse = true;
			item.maxStack = 999;
			item.useAnimation = 10;
			item.useTime = 15;
			item.value = Item.buyPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.LightPurple;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Blocks.WandaPaintingTile>();
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wanda from hit survival game dont starve together");
			Tooltip.SetDefault("Image sent by @Radeline__ (hi!!)\nFull high quality version available at the mod homepage!");
		}

	}
}