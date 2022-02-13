
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class CeramicBrickBoots : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("7% increased movement speed");
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
			player.moveSpeed *= 1.07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ClayPot, 10);
			recipe.AddIngredient(ItemID.RedBrick, 15);
			recipe.AddIngredient(ItemID.Hellstone, 1);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}