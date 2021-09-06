using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Buffs
{
	public class Starlazer : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Starlazer");
			Description.SetDefault("The Starlazers will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.starsMinion = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Stars>()] > 0)
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