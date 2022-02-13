using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class CarrotyxChestpiece : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("20% increased meleedamage\n10% decreased mana usage");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 23;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 18;
		}

		public override void UpdateEquip(Player player) 
		{
			player.meleeDamage += 0.2f;
			player.manaCost *= 0.9f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Carrot>(), 50);
			recipe.AddIngredient(ItemType<CarrotyxBar>(), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}