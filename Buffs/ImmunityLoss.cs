using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class ImmunityLoss : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Never gonna");
			Description.SetDefault("give you up");
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}