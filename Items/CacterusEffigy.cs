using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items
{
	public class CacterusEffigy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacterus 'Effigy'");
			Tooltip.SetDefault("Summons Cacterus \nOnly usable in the Corrupted/Crimson Desert");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{           
			return !NPC.AnyNPCs(mod.NPCType("Cacterus")) && ((player.ZoneCorrupt || player.ZoneCrimson) && (player.ZoneDesert));  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Cacterus"));   //boss spawn
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Cactus, 50);recipe.AddIngredient(ItemID.Waterleaf, 3);recipe.AddIngredient(ItemID.EbonsandBlock, 10);recipe.AddTile(TileID.DemonAltar);recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Cactus, 50);recipe.AddIngredient(ItemID.Waterleaf, 3);recipe.AddIngredient(ItemID.CrimsandBlock, 10);recipe.AddTile(TileID.DemonAltar);recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant1);recipe.SetResult(ItemID.StrangePlant2);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant1);recipe.SetResult(ItemID.StrangePlant3);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant1);recipe.SetResult(ItemID.StrangePlant4);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant2);recipe.SetResult(ItemID.StrangePlant3);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant2);recipe.SetResult(ItemID.StrangePlant4);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant2);recipe.SetResult(ItemID.StrangePlant1);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant3);recipe.SetResult(ItemID.StrangePlant2);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant3);recipe.SetResult(ItemID.StrangePlant1);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant3);recipe.SetResult(ItemID.StrangePlant4);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant4);recipe.SetResult(ItemID.StrangePlant2);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant4);recipe.SetResult(ItemID.StrangePlant3);
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StrangePlant4);recipe.SetResult(ItemID.StrangePlant1);
		}
	}
}
