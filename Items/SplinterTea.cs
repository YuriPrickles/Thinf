using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using SubworldLibrary;
using Thinf.NPCs.Bounties;

namespace Thinf.Items
{
	public class SplinterTea : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Bounty Lure\n'I recommend not drinking this if you want to live'");
		}

		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 48;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{
			TheGatrix gatrix = new TheGatrix();
			if (!NPC.AnyNPCs(ModContent.NPCType<Poltergate>()) && SLWorld.subworld && MyPlayer.insideGatrix)
            {
				return true;
            }
			return false;  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Poltergate>());
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		//public override void AddRecipes()
		//{
		//	ModRecipe recipe = new ModRecipe(mod);
		//	recipe.AddIngredient(ItemID.Wood, 70);
		//	recipe.AddIngredient(ItemID.Mug);
		//	recipe.AddIngredient(ItemID.BottledWater, 3);
		//	recipe.AddIngredient(ItemID.WoodenFence, 50);
		//	recipe.AddTile(TileID.CookingPots);
		//	recipe.SetResult(this);
		//	recipe.AddRecipe();
		//}
	}
}
