using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class FishPlate : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			DisplayName.SetDefault("Fish Breastplate");
			Tooltip.SetDefault("You're really willing to wear this for minion slots?"
				+ "\n+4% summon damage");
		}

		public override void SetDefaults() {
			item.width = 40;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player) 
		{
			player.minionDamage += 0.04f;
			player.maxMinions += 1;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Bass, 10);recipe.AddIngredient(ItemID.Goldfish, 2);recipe.AddTile(TileID.Anvils); recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}