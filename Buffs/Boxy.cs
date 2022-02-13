using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Boxy : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Boxy");
			Description.SetDefault("25% damage reduction and increased life regen when standing still");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.velocity == Vector2.Zero)
			{
				player.lifeRegen += 30;
				player.endurance += 0.25f;
			}
		}
	}
}