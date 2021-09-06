using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class WateringCan : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("8% increased plant damage" +
							   "\n3% increased plant critical strike chance");
		}

		public override void SetDefaults()
		{
			item.Size = new Vector2(32);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerDamageMult *= 1.08f; // add 20% to the multiplicative bonus
			modPlayer.farmerCrit += 3; // add 15% crit
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater, 3);
			recipe.AddIngredient(ItemID.Rope, 3);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 40);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}