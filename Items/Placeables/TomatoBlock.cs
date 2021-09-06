using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class TomatoBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Tomato Block");
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
			item.createTile = TileType<Blocks.TomatoBlockTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Tomato>(), 8);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
}
