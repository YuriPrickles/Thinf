using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class PeatMoss : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Seeds have a 30% chance to explode into spores\n4% increased plant critical strike chance\n'With Captain Combustible, you are NEVER safe!'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsExplode = true;

			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerCrit += 4;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 5);
			recipe.AddIngredient(ItemID.Grenade, 15);
			recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
			recipe.AddIngredient(ItemID.Hellstone, 5);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}