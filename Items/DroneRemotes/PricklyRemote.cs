using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.Placeables;
using Thinf.NPCs;
using Thinf.NPCs.PlayerDrones;
using Thinf.NPCs.SoulCatcher;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.DroneRemotes
{
	public class PricklyRemote : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Deploys the Cactiny\nLeft click for a semi auto spike shot\nRight click for a spike burst");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.maxStack = 1;
			item.value = 100;
			item.rare = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return (!player.GetModPlayer<DroneControls>().playerIsControllingDrone && !NPC.AnyNPCs(NPCType<Cactiny>()) && !player.HasBuff(BuffType<DroneRecharge>()));
		}
		public override bool UseItem(Player player)
		{
			player.GetModPlayer<PositionResetter>().resetPos = player.Center;
			player.GetModPlayer<DroneControls>().playerIsControllingDrone = true;
			NPC npc = Main.npc[NPC.NewNPC((int)(player.Center.X + 50 * player.direction), (int)player.Center.Y, NPCType<Cactiny>())];
			npc.target = player.whoAmI;
			return true;
		}
	}
}
