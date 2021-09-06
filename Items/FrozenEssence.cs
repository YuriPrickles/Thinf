using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class FrozenEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A very, very, cold source of power");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.maxStack = 99;
			item.value = 1000;
			item.rare = ItemRarityID.Cyan;
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
