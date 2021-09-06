using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class SappyAcorn : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Plants a Sap-ling that shoots sap balls that briefly stop enemies\nUse on pots");
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
			item.createTile = TileType<Blocks.SaplingTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 50);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.HoneyBlock, 30);
			recipe.AddIngredient(ItemID.Acorn, 5);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}