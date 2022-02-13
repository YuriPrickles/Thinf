using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class PoliticalPower : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It seems to make selfless decisions for the good of the community.\nI think we can keep this.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 1000;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CorruptedPoliticalPower>());
			recipe.AddIngredient(ItemID.GreenSolution);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
