  
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
			Description.SetDefault("You are politically dying\n50% reduced damage");
			Main.debuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.allDamage -= 0.5f;
			player.GetModPlayer<MyPlayer>().politicallyDying = true;
		}
    }
}