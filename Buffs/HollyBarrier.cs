using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class HollyBarrier : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Holly Barrier");
			Description.SetDefault("Increased defense based on how much seeds you hit\nDecreases over time when not hitting enemies");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += player.GetModPlayer<MyPlayer>().hollyDefenseStack;
		}
	}
}