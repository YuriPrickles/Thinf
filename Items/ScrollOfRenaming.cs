using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class ScrollOfRenaming : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Press L while this item is in your Inventory to be able to rename items.\nPress ESC to close the UI.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
