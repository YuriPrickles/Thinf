
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class PotatiumiteLeggings : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("20% increased movement speed\n5% increased damage");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.2f;
			player.allDamage += 0.05f;
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