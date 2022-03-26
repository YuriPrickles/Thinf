using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;
using Terraria.DataStructures;
using Thinf.Items.Placeables;

namespace Thinf.Items.Accessories
{
	public class NormalCorn : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 50;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Seeds have a 5% chance to cause cobs of corn to rain down at your cursor when you hit an enemy\nCobs deal 25x the damage of the seeds\nSo cool");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsCauseCornstrike = true;

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 1);
			recipe.AddIngredient(ModContent.ItemType<Corn>(), 40);
			recipe.AddIngredient(ItemID.Topaz, 3);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}