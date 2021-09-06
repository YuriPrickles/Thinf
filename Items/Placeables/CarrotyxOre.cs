using Terraria;
using Terraria.ID;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class CarrotyxOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Unlike Potatiumite, this ore is edible and crunchy!");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Blocks.CarrotyxOreTile>();
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe;recipe.AddIngredient(ItemType<ExampleItem>());
			recipe.SetResult(this, 10);
			recipe.SetResult(this); recipe.AddRecipe();
		}*/
	}
}
