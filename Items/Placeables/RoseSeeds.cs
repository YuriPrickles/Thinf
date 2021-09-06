using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class RoseSeeds : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Plants a Rose Shield that shields nearby players with petals\nUse on pots");
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
			item.createTile = TileType<Blocks.RoseShieldTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Daybloom, 15);
			recipe.AddIngredient(ItemID.Moonglow, 15);
			recipe.AddIngredient(ItemID.RedPaint, 100);
			recipe.AddIngredient(ItemID.TurtleShell);
			recipe.AddIngredient(ItemID.Bottle, 2);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}