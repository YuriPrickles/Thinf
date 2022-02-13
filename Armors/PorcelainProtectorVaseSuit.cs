using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PorcelainProtectorVaseSuit : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("17% increased plant speed\n'Time to bring out the fine china'");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 23;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 24;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MyPlayer>().hasAnyVaseSuit = true;
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerSpeed *= 1.17f;
		}

        public override void UpdateVanity(Player player, EquipType type)
		{
			player.GetModPlayer<MyPlayer>().hasAnyVaseSuit = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CeramicVaseSuit>());
			recipe.AddIngredient(ItemType<LifeFragment>(), 20);
			recipe.AddIngredient(ItemID.LunarBar, 16);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}