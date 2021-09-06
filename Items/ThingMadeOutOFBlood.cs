using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs;

namespace Thinf.Items
{
	public class ThingMadeOutOfBlood : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons Bloodo\nKilling Bloodo will crimsonify the nearby area");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 32;
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
			return !NPC.AnyNPCs(ModContent.NPCType<Bloodo>());
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Bloodo>());   //boss spawn
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimstoneBlock, 50);
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddIngredient(ItemID.CrimsandBlock, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
