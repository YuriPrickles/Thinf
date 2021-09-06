using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class CoreDestroyed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("The Core Has Been Destroyed!");
			Description.SetDefault("Honestly, what did you expect?");
			Main.debuff[Type] = true;
			Main.persistentBuff[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().coreDeadBuff = true;
		}
	}
}