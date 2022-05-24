using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.HypnoKeeper;

namespace Thinf.Items
{
	public class MysteriousRadio : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons Hypno Keeper\nUsable only in Hardmode");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 28;
			item.maxStack = 1;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{           
			return !NPC.AnyNPCs(ModContent.NPCType<HypnoKeeper>());  //you can't spawn this boss multiple times
		}
		public override bool UseItem(Player player)
		{
			NPC.NewNPC((int)player.Center.X, (int)(player.Center.Y - 150), ModContent.NPCType<HypnoKeeper>());
			Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0);
			return true;
		}
	}
}
