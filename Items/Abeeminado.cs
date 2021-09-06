using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items
{
	public class Abeeminado : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abeeminado");
			Tooltip.SetDefault("Summons the Beenado \nThis is only the start");
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
			return !NPC.AnyNPCs(mod.NPCType("Beenado"));  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Beenado"));   //boss spawn
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.RainCloud, 20);recipe.AddIngredient(ItemID.BottledHoney, 5);recipe.AddIngredient(ItemID.BeeWax, 3);recipe.AddIngredient(ItemID.SoulofFlight, 2);recipe.AddTile(TileID.DemonAltar);recipe.SetResult(this); recipe.AddRecipe();

		}
	}
}
