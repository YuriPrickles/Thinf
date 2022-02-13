using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.Herbalgamation;

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
			if (ModNameWorld.downedHerbalgamation)
			{
				if (ModNameWorld.timeLoop)
				{
					NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Herbalgamation>());
					mod.Logger.InfoFormat("'we're in a time loop arent we'");
					mod.Logger.InfoFormat("'who gave this cocksucker the time looper'");
					mod.Logger.InfoFormat("'idk'");
				}
				else
				{
					Main.NewText("Unable to summon Herbalgamation due to error: Boss already canonically dead");
					Main.NewText("(See client.log for details.)");
					mod.Logger.InfoFormat("Anomaly found at line 34 of HerbalgamatedClump.cs:");
					mod.Logger.InfoFormat("'we're dead alreaedy stoppppp'");
					Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/VineBoom").WithVolume(1.5f));
				}
			}
			else
			{
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Herbalgamation>());   //boss spawn
			}
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
