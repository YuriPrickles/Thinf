using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items
{
	public class HerbalgamatedClump : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("example boss summon");
			Tooltip.SetDefault("summons the\nthe the the the the the the the the the the the the the the\nnot cosnumalbe");
		}

		public override void SetDefaults()
		{
			item.width = 52;
			item.height = 46;
			item.maxStack = 1;
			item.value = 100;
			item.rare = ItemRarityID.Red;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{           
			return !NPC.AnyNPCs(mod.NPCType("Herbalgamation"));  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Herbalgamation"));   //boss spawn
			Main.PlaySound(SoundID.Tink, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CosmicHerbalPiece>(), 10);
			recipe.AddIngredient(ItemID.Waterleaf, 3);
			recipe.AddIngredient(ItemID.Daybloom, 3);
			recipe.AddIngredient(ItemID.Blinkroot, 3);
			recipe.AddIngredient(ItemID.Fireblossom, 3);
			recipe.AddIngredient(ItemID.Shiverthorn, 3);
			recipe.AddIngredient(ItemID.Deathweed, 3);
			recipe.AddIngredient(ItemID.Moonglow, 3);
			recipe.AddIngredient(ItemID.MudBlock, 30);
			recipe.AddIngredient(ItemID.DirtBlock, 30);
			recipe.AddIngredient(ItemID.GrassSeeds, 300);
			recipe.AddIngredient(ItemID.Vine, 5);
			recipe.AddIngredient(ItemID.JungleSpores, 10);
			recipe.AddIngredient(ItemID.Wood, 175);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CosmicHerbalPiece>(), 10);
			recipe.AddIngredient(ItemID.Waterleaf, 3);
			recipe.AddIngredient(ItemID.Daybloom, 3);
			recipe.AddIngredient(ItemID.Blinkroot, 3);
			recipe.AddIngredient(ItemID.Fireblossom, 3);
			recipe.AddIngredient(ItemID.Shiverthorn, 3);
			recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 3);
			recipe.AddIngredient(ItemID.Moonglow, 3);
			recipe.AddIngredient(ItemID.MudBlock, 30);
			recipe.AddIngredient(ItemID.DirtBlock, 30);
			recipe.AddIngredient(ItemID.GrassSeeds, 300);
			recipe.AddIngredient(ItemID.Vine, 5);
			recipe.AddIngredient(ItemID.JungleSpores, 10);
			recipe.AddIngredient(ItemID.Wood, 175);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
