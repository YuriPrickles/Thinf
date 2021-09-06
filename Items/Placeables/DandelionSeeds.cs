using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class DandelionSeeds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Plants a Dandelion that shoots Seed Bombs\nUse on pots");
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
			item.createTile = TileType<Blocks.DandelionTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<LifeFragment>(), 1);
			recipe.AddIngredient(ItemID.Cannonball, 300);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 28);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}