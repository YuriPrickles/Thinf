using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class ChlorophyllBoost : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Chlorophyll Boost");
			Description.SetDefault("300% increased plant damage and 10% increased plant speed");
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerDamageMult *= 3f;
			modPlayer.farmerSpeed *= 1.10f;
			highOnChlorophyll = true;
		}
	}
}