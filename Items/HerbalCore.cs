using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class HerbalCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herbal Core");
			Tooltip.SetDefault("A powerful energy source made from mutated herbs");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 1000;
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
