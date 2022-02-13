using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class GoatarangProj : ModProjectile
	{
		int fireTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goatarang");
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.aiStyle = 3;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			fireTimer++;
			if (fireTimer >= 16)
			{
				int projectileSpawnAmount = 6;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();
					Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity, ProjectileID.ImpFireball, projectile.damage, 1, projectile.owner)];
					proj.tileCollide = false;
					proj.minion = false;
					proj.melee = true;
					proj.penetrate = -1;
					proj.timeLeft = Thinf.ToTicks(3);
				}
				fireTimer = 0;
			}
			if (Main.rand.NextBool())
            {
				if (Main.rand.NextBool())
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire);
					dust.noGravity = true;
					dust.scale = 1.8f;
					dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire);
					dust.noGravity = true;
					dust.scale = 1.8f;
					dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire);
					dust.noGravity = true;
					dust.scale = 1.8f;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		}
	}
}
