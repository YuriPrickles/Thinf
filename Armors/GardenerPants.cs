
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class GardenerPants : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("5% increased plant speed");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerSpeed *= 1.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Potato>(), 35);
			recipe.AddIngredient(ItemType<PotatiumiteBar>(), 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}