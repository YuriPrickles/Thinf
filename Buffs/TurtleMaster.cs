using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class TurtleMaster : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Turtle Master");
			Description.SetDefault("Increased damage reduction by 30% \nDecreased movement speed");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.moveSpeed -= 1.0f;
			player.endurance += 0.3f;
		}
	}
}