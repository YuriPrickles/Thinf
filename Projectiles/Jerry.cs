using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class Jerry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabySlime);
			projectile.width = 22;
			projectile.height = 30;
			aiType = ProjectileID.BabySlime;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.slime = false; // Relic from aiType
			return true;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<JerryOurLordAndSavior>());
			}
			if (player.HasBuff(ModContent.BuffType<JerryOurLordAndSavior>()))
			{
				projectile.timeLeft = 2;
			}
		}
	}
}