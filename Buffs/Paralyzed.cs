  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Thinf.Buffs
{
	public class Paralyzed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Convenient Movement Stopper");
			Description.SetDefault("Either there's a cutscene going on or you're an enemy who got paralyzed");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.velocity = Vector2.Zero;
			player.statDefense += 500000;
        }

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalNPCs>().paralyzed = true;
		}
	}
}