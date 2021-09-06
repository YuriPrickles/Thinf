using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	// This file is showcasing inheritance to implement an accessory "type" that you can only have one of equipped
	// It also shows how you can interact with inherited methods
	// Additionally, it takes advantage of ValueTuple to make code more compact

	// First, we create an abstract class that all our exclusive accessories will be based on
	// This class won't be autoloaded by tModLoader, meaning it won't "exist" in the game, and we don't need to provide it a texture
	// Further down below will be the actual items (Green/Yellow Exclusive Accessory)
	public class TwigNecklace : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Blue;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Wood, 150);recipe.AddIngredient(ItemID.Vine, 2);recipe.AddIngredient(ItemID.PlatinumBar, 5);recipe.AddTile(TileID.Trees); recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Wood, 150);recipe.AddIngredient(ItemID.Vine, 2);recipe.AddIngredient(ItemID.GoldBar, 5);recipe.AddTile(TileID.Trees); recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Wood, 300);recipe.AddIngredient(ItemID.Acorn, 25);recipe.AddIngredient(ItemID.Vine, 5);recipe.AddIngredient(ItemID.VineRope, 150);recipe.AddIngredient(ItemID.GrassSeeds, 25);recipe.AddIngredient(ItemID.DirtBlock, 100);recipe.AddIngredient(ItemID.Bunny, 1);recipe.AddTile(TileID.Trees);recipe.SetResult(ItemID.LivingLoom); recipe.AddRecipe();
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("10% increased summon damage\n4% increased damage reduction");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.minionDamage += 0.10f;
			player.endurance += 0.03f;
		}
	}
}