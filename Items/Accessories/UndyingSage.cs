using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;
using Thinf.Items.Placeables;
using Thinf.Items.Weapons.FarmerWeapons;

namespace Thinf.Items.Accessories
{
	public class UndyingSage : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Seeds have a 5% chance to give you 3 seconds of Ghost Mode invincibility\n-250 max life");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsGiveYouInvincibility = true;
			if (player.statLifeMax2 < 252)
            {
				player.statLifeMax2 = 1;
            }
			else
			{
				player.statLifeMax2 -= 250;
			}
			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Leaf>(), 15);
			recipe.AddIngredient(ItemID.LifeCrystal);
			recipe.AddIngredient(ItemID.Prismite);
			recipe.AddIngredient(ItemID.SpectreBar, 30);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}