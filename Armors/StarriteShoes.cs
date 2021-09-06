
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using Thinf.Blocks;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class StarriteShoes : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("+20% minion damage\n+2 max minions");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 30;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 1;
		}

		public override void UpdateEquip(Player player) {
			player.minionDamage += 0.20f;
			player.maxMinions += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<StarriteBar>(), 20);
			recipe.AddIngredient(ItemID.StardustLeggings);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}