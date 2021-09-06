using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class DeterminatorProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Determinator");
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 56;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.aiStyle = 3;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			if (Main.mouseRight && projectile.timeLeft < 990)
            {
				projectile.velocity = projectile.DirectionTo(Vector2.Lerp(projectile.Center, Main.player[projectile.owner].Center, 0.35f)) * 34;
            }
			if (Main.rand.NextBool())
            {
				if (Main.rand.NextBool())
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Clentaminator_Red);
					dust.noGravity = true;
					dust.scale = 1.8f;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile proj = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity, ModContent.ProjectileType<FightingSpirit>(), projectile.damage, projectile.knockBack, projectile.owner);
			proj.melee = true;
			proj.magic = false;
		}
	}
}
