using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class EggYolk : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("How does this thing even stay rigid?");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 16;
			item.maxStack = 99;
			item.value = 1000;
			item.rare = ItemRarityID.Blue;
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
