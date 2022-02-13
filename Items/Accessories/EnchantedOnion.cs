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
	public class EnchantedOnion : ModItem
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
			Tooltip.SetDefault("Seeds have a 15% chance to rain tears from above when hitting enemies\n'Cry about it'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 18));
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsRainTearsWhenHitting = true;

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 50);
			recipe.AddIngredient(ItemID.RainCloud, 20);
			recipe.AddIngredient(ModContent.ItemType<Onion>(), 10);
			recipe.AddIngredient(ItemID.Amethyst, 3);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}