using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PotatiumiteChestplate : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("10% increased ranged and minion damage\n10% increased ranged crit chance");
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
			player.rangedDamage += 0.1f;
			player.minionDamage += 0.1f;
			player.rangedCrit += 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Potato>(), 50);
			recipe.AddIngredient(ItemType<PotatiumiteBar>(), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}