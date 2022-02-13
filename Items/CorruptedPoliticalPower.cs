using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class CorruptedPoliticalPower : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It seems to make lots of selfish decisions.\nPerhaps we can purify it?");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 1000;
			item.rare = ItemRarityID.Purple;
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
