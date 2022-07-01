using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class StarriteRobe : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("+15% summon damage\n+3 max minions\n+2 max sentries");
		}

		public override void SetDefaults() {
			item.width = 40;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 30;
		}

		public override void UpdateEquip(Player player) 
		{
			player.minionDamage += 0.25f;
			player.maxMinions += 3;
			player.maxTurrets += 2;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<StarriteBar>(), 25);
			recipe.AddIngredient(ItemID.StardustBreastplate);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}