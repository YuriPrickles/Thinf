using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Meters;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class MoneyNecklace : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("Hit enemies to fill up the Money Meter\nWhen filled up, all enemies drop money when hit\n'Here comes the money'");
		}

		public override void UpdateEquip(Player player)
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<MoneyMeter>()))
            {
				NPC npc = Main.npc[NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<MoneyMeter>())];
				npc.target = player.whoAmI;
            }
			player.GetModPlayer<MyPlayer>().hasMoneyNecklace = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumCoin, 5);
			recipe.AddIngredient(ItemID.GoldCoin, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void UpdateInventory(Player player)
        {

        }
    }
}