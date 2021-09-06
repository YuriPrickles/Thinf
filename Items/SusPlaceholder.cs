using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class SusPlaceholder : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SUS !");
			Tooltip.SetDefault("When tge placheolder is sus ! o//-//o");
		}

		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 64;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Expert;
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
