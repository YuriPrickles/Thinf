  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class DroneRecharge : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Drone Recharge");
			Description.SetDefault("Cannot use any Drones");
			Main.debuff[Type] = true;
			Main.persistentBuff[Type] = true;
		}
	}
}