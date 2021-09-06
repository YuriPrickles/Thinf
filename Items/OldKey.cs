using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class OldKey : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old Key");
			Tooltip.SetDefault("It's broken");
		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 62;
			item.maxStack = 999;
			item.value = 50000;
			item.rare = ItemRarityID.LightPurple;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
