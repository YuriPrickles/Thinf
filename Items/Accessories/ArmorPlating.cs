using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Meters;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class ArmorPlating : ModItem
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
			Tooltip.SetDefault("Hit enemies to fill up the Buffet Meter\nWhen filled up, plates of food will appear, giving you buffs when touched\nBuffet Frenzy will last for 1 minute and 30 seconds");
		}

		public override void UpdateEquip(Player player)
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<BuffetMeter>()))
			{
				NPC npc = Main.npc[NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<BuffetMeter>())];
				npc.target = player.whoAmI;
			}
			player.GetModPlayer<MyPlayer>().hasArmorPlating = true;
		}
		public override void AddRecipes()
		{

		}
		public override void UpdateInventory(Player player)
		{

		}
	}
}