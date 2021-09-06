using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Nightmare : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Nightmare");
			Description.SetDefault("Don't worry, it's just cosmetic.");
			Main.debuff[Type] = true;
			Main.persistentBuff[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			nightmare = true;
		}
	}
}