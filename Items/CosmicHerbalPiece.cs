using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;

namespace Thinf.Items
{
	public class CosmicHerbalPiece : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Herbal Piece");
			Tooltip.SetDefault("A powerful herb from outer space");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 28;
			item.maxStack = 99;
			item.value = 5000;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentVortex);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ItemID.Deathweed);
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentNebula);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ItemID.Deathweed);
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentSolar);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ItemID.Deathweed);
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentStardust);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ItemID.Deathweed);
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentVortex);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ModContent.ItemType<Bloodclot>());
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentNebula);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ModContent.ItemType<Bloodclot>());
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentSolar);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ModContent.ItemType<Bloodclot>());
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarOre, 3);
			recipe.AddIngredient(ItemID.FragmentStardust);
			recipe.AddIngredient(ItemID.Daybloom);
			recipe.AddIngredient(ItemID.Waterleaf);
			recipe.AddIngredient(ModContent.ItemType<Bloodclot>());
			recipe.AddIngredient(ItemID.Blinkroot);
			recipe.AddIngredient(ItemID.Fireblossom);
			recipe.AddIngredient(ItemID.Shiverthorn);
			recipe.AddIngredient(ItemID.Moonglow);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
