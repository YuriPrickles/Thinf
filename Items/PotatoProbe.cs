using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items.Placeables;

namespace Thinf.Items
{
	public class PotatoProbe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Potato Probe");
			Tooltip.SetDefault("Summons The Spud Lord \nOnly usable in the Underground Layer\nNot Consumable");
		}

		public override void SetDefaults()
		{
			item.width = 29;
			item.height = 30;
			item.maxStack = 1;
			item.value = 0;
			item.rare = ItemRarityID.Cyan;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{           
			return !NPC.AnyNPCs(mod.NPCType("SpudLord")) && ((player.ZoneDirtLayerHeight));  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.NewNPC((int)player.position.X, (int)(player.position.Y - 200), mod.NPCType("SpudLord"));   //boss spawn
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Potato>(), 50);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 20);
			recipe.AddIngredient(ItemID.Ectoplasm, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
