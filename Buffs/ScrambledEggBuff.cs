using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class ScrambledEggBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Scrambled Egg!");
			Description.SetDefault("All enemies are confused!");
		}

		public override void Update(Player player, ref int buffIndex)
		{
            for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC npc = Main.npc[i];
				if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(player.Center) <= 1000)
				{
					npc.AddBuff(BuffID.Confused, 2);
				}
			}
		}
	}
}