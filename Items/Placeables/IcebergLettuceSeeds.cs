using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class IcebergLettuceSeeds : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Plants an Iceberg Lettuce that freezes and damages enemies in its range\nUse on pots");
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
			item.createTile = TileType<Blocks.IcebergLettuceTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Shiverthorn, 15);
			recipe.AddIngredient(ItemID.Snowball, 50);
			recipe.AddIngredient(ItemID.GrassSeeds, 20);
			recipe.AddIngredient(ItemID.FrostCore);
			recipe.AddIngredient(ItemID.Bottle, 10);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}