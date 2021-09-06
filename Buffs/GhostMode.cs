using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class GhostMode : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ghost Mode");
			Description.SetDefault("Take no damage");
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}