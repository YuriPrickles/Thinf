using Terraria;
using Terraria.ID;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
using Terraria.Localization;
using Terraria.ModLoader;
using Thinf.Blocks;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class AsteroidBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Instantly cause a third degree burn");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 32;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 15);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
