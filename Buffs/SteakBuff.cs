using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class SteakBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Steak!");
			Description.SetDefault("Increased defense by 9");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 9;
		}
	}
}