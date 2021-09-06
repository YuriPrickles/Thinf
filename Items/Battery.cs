using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class Battery : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Battery");
			Tooltip.SetDefault("The 12 things in the diet of a Thundercock");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 32;
			item.maxStack = 99;
			item.value = 5000;
			item.rare = ItemRarityID.Green;
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
