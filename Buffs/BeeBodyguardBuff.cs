using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Buffs
{
	public class BeeBodyguardBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Bee Bodyguard");
			Description.SetDefault("Your very own private army!");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.beeBodyguardMinion = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BeeBodyguard>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}