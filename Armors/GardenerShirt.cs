using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class GardenerShirt : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("5% increased plant damage and crit chance");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 23;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerDamageMult *= 1.05f;
			modPlayer.farmerCrit += 5;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 20);
			recipe.AddIngredient(ItemID.Leather, 10);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}