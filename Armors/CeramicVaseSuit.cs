using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class CeramicVaseSuit : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("8% increased plant speed");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 23;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MyPlayer>().hasAnyVaseSuit = true;
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerSpeed *= 1.08f;
		}

        public override void UpdateVanity(Player player, EquipType type)
		{
			player.GetModPlayer<MyPlayer>().hasAnyVaseSuit = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ClayPot, 25);
			recipe.AddIngredient(ItemID.RedBrick, 40);
			recipe.AddIngredient(ItemID.Hellstone, 1);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}