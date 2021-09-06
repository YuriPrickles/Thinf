  
using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class ZucchiniCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Zucchini Cooldown");
			Description.SetDefault("Cannot use The Great Zucchini");
			Main.debuff[Type] = true;
			Main.persistentBuff[Type] = true;
		}
	}
}