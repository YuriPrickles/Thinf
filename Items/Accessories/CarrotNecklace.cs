using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;
using Thinf.Items.Placeables;

namespace Thinf.Items.Accessories
{
	public class CarrotNecklace : ModItem
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
			Tooltip.SetDefault("Seeds turn into returning Carrot Chips");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsAreCarrots = true;

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Carrot>(), 15);
			recipe.AddIngredient(ItemID.WoodenBoomerang);
			recipe.AddIngredient(ItemID.JungleGrassSeeds, 10);
			recipe.AddIngredient(ItemID.StoneBlock, 50);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}