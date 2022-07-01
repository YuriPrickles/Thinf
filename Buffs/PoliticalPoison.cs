  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class PoliticalPoison : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Political Poison");
			Description.SetDefault("You are politically dying\n74% reduced damage");
			Main.debuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.allDamage -= 0.74f;
			player.GetModPlayer<MyPlayer>().politicallyDying = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPCs>().Politics = true;
		}
    }
}