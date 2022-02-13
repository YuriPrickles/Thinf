using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class AloeVera : ModItem
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
			Tooltip.SetDefault("Seeds have a 25% chance to heal you for 2-5 HP\nOnly works when hitting enemies");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsHeal = true;

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 25);
			recipe.AddIngredient(ItemID.ClayPot, 3);
			recipe.AddIngredient(ItemID.DirtBlock, 25);
			recipe.AddIngredient(ItemID.HealingPotion, 10);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}