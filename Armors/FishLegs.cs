
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class FishLegs : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Flip flop flip"
				+ "\n5% increased movement speed\n+3% minion damage");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 30;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 1;
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed *= 1.05f;
			player.minionDamage += 0.03f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Bass, 6);recipe.AddIngredient(ItemID.Goldfish, 1);recipe.AddTile(TileID.Anvils); recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}