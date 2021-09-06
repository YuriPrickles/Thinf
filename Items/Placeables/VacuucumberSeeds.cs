using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class VacuucumberSeeds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Plants a Vacuucumber that sucks in nearby enemies\nUse on pots");
		}
		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 15;
			item.useTime = 10;
			item.maxStack = 99;
			item.consumable = true;
			item.width = 28;
			item.height = 28;
			item.value = 100000;
			item.createTile = TileType<Blocks.VacuucumberTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<FragmentOfFlight>(), 3);
			recipe.AddIngredient(ItemID.JungleGrassSeeds, 16);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
			recipe.AddIngredient(ItemID.BottledWater, 36);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}