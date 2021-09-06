using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class SpinachSeeds : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Plants a Spinach Captain that boosts damage of players and turrets around it\nUse on pots\nBoosts plants regardless of growth stage, but only boosts players when fully grown");
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
			item.createTile = TileType<Blocks.SpinachTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Waterleaf, 15);
			recipe.AddIngredient(ItemID.GrassSeeds, 25);
			recipe.AddIngredient(ItemID.GreenPaint, 100);
			recipe.AddIngredient(ItemID.SoulofMight, 30);
			recipe.AddIngredient(ItemID.Bottle, 10);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}