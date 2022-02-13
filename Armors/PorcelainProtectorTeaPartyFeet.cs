
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.FarmerClass;
using Thinf.Items;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class PorcelainProtectorTeaPartyFeet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("14% increased movement speed\n15% increased plant critical strike chance\n'Time to bring out the fine china'");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			FarmerClass modPlayer = ModPlayer(player);
			player.moveSpeed *= 1.14f;
			modPlayer.farmerCrit += 15;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CeramicBrickBoots>());
			recipe.AddIngredient(ItemType<LifeFragment>(), 15);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}