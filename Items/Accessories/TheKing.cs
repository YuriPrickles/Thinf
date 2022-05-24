using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class TheKing : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 12;
		}


		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Effects of Aloe Vera, Bloody Concoction, Holly Knight, and Undying Sage\n-100 max life\nAlso prevents pregnancies");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsHeal = true;
			player.GetModPlayer<MyPlayer>().seedsSpawnBloodyPlants = true;
			player.GetModPlayer<MyPlayer>().seedsIncreaseHollyBarrierDefense = true;
			player.GetModPlayer<MyPlayer>().seedsGiveYouInvincibility = true;

			if (player.statLifeMax2 < 102)
			{
				player.statLifeMax2 = 1;
			}
			else
			{
				player.statLifeMax2 -= 100;
			}
			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AloeVera>());
			recipe.AddIngredient(ModContent.ItemType<BloodyConcoction>());
			recipe.AddIngredient(ModContent.ItemType<HollyKnight>());
			recipe.AddIngredient(ModContent.ItemType<UndyingSage>());
			recipe.AddIngredient(ModContent.ItemType<LifeFragment>(), 18);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}