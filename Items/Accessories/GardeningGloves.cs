using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class GardeningGloves : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% increased speed for plant class weapons");
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
			modPlayer.farmerSpeed *= 1.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddIngredient(ItemID.Silk, 25);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(ItemID.Leather);
			recipe.AddRecipe();
		}
	}
}