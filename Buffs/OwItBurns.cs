  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class OwItBurns : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("OW! IT BURNS!");
			Description.SetDefault("AAAAAAAAAAAAAAAAAAAAA FUCKING HELL IT BURNS SO MUCH");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPCs>().ItBurns = true;
		}
	}
}