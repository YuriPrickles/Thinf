  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Overheat : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("OVERHEAT!");
			Description.SetDefault("Cannot use Overheat Weapons!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
	}
}