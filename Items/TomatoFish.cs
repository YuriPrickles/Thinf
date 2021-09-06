using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class TomatoFish : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 5000;
			item.rare = ItemRarityID.Orange;
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
