using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Manapush : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mana Push");
			Description.SetDefault("Increased max mana by 50%");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statManaMax2 += player.statManaMax2/2;
		}
	}
}