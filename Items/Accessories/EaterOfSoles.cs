using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class EaterOfSoles : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Any NPCs below you onscreen have their debuff immunities disabled\n'The corruption is great with puns, convert to corruptionism now by calling 666-666-667'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            for (int i = 0; i < Main.maxNPCs; ++i)
            {
				NPC npc = Main.npc[i];
				if (npc.active && npc.type != NPCID.TargetDummy && !npc.friendly && !npc.dontTakeDamage && npc.Center.Y > player.Center.Y && !npc.boss)
				{
					npc.AddBuff(ModContent.BuffType<ImmunityLoss>(), 2);
				}
			}
		}
	}
}