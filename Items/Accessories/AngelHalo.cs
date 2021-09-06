using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Player;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class AngelHalo : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Green;
		}

        public override void UpdateInventory(Player player)
        {
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 16);
			recipe.AddIngredient(ItemID.SunplateBlock, 32);
			recipe.AddIngredient(ItemID.Feather, 25);
			recipe.AddIngredient(ItemID.SoulofFlight, 40);
			recipe.AddTile(TileID.SkyMill);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Immune to fall damage\nGives the player the featherfall effect");
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.noFallDmg = true;
			player.slowFall = true;
		}
	}
}