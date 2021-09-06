using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Thinf.MyPlayer;
namespace Thinf.Items
{
	public class NightmareFuel : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sometimes what scares us make us stronger");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.value = 50000;
			item.rare = ItemRarityID.Red;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
