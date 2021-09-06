using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Energized : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Energized");
			Description.SetDefault("Why would you drink this");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.maxRunSpeed += 20;
			player.moveSpeed += 20;
			player.statDefense *= 0;
			player.statLifeMax2 = 69;
			player.runAcceleration += 9;
		}
	}
}