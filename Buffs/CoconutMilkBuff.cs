using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class CoconutMilkBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Coconut Milk");
			Description.SetDefault("Increased defense and endurance, milk is good for your bones!");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 3;
			player.endurance += 0.04f;
		}
	}
}