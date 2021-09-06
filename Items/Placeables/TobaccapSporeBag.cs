using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class TobaccapSporeBag : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Plants a Tobaccap that shoots a spread of smoke\nUse on pots");
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
			item.createTile = TileType<Blocks.TobaccapTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GlowingMushroom, 30);
			recipe.AddIngredient(ItemID.AshBlock, 75);
			recipe.AddIngredient(ItemID.Bottle, 2);
			recipe.AddTile(TileID.Campfire);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}