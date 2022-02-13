using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class SpaghettiBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Spaghetti!");
			Description.SetDefault("Increased damage by 20%");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.allDamage += 0.2f;
		}
	}
}