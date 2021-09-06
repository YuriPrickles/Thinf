  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class PotatoMineRecharge : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Detonato Cooldown");
			Description.SetDefault("Cannot summon another Detonato Sentry");
			Main.debuff[Type] = true;
		}
	}
}