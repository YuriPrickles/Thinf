using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Buffs
{
	public class CortalJunior : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Baby Cortal");
			Description.SetDefault("It's all fun and games 'till he busts out the Phantasmal Spheres and Cursed Flames");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("CortalJunior")] > 0)
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