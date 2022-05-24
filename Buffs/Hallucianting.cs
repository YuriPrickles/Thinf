using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Hallucinating : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hallucinating");
			Description.SetDefault("You didn't take your normal pills");

			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}