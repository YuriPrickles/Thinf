using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class RobotPumpkin : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Plants a Pumpkitron that shoots fast firing lasers\nUse on pots");
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
			item.createTile = TileType<Blocks.PumpkitronTile>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.CobaltBar, 15);
			recipe.AddIngredient(ItemID.MythrilBar, 15);
			recipe.AddIngredient(ItemID.AdamantiteBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.PalladiumBar, 15);
			recipe.AddIngredient(ItemID.OrichalcumBar, 15);
			recipe.AddIngredient(ItemID.TitaniumBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.PalladiumBar, 15);
			recipe.AddIngredient(ItemID.MythrilBar, 15);
			recipe.AddIngredient(ItemID.AdamantiteBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.PalladiumBar, 15);
			recipe.AddIngredient(ItemID.OrichalcumBar, 15);
			recipe.AddIngredient(ItemID.AdamantiteBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.CobaltBar, 15);
			recipe.AddIngredient(ItemID.OrichalcumBar, 15);
			recipe.AddIngredient(ItemID.AdamantiteBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.CobaltBar, 15);
			recipe.AddIngredient(ItemID.MythrilBar, 15);
			recipe.AddIngredient(ItemID.TitaniumBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();


			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.CobaltBar, 15);
			recipe.AddIngredient(ItemID.OrichalcumBar, 15);
			recipe.AddIngredient(ItemID.TitaniumBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Pumpkin, 150);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.PalladiumBar, 15);
			recipe.AddIngredient(ItemID.MythrilBar, 15);
			recipe.AddIngredient(ItemID.TitaniumBar, 15);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}