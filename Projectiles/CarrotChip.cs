using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class CarrotChip : ModProjectile
	{
		int frameTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			frameTimer++;
			if (frameTimer == 5)
			{
				projectile.frame++;
				if (projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
				frameTimer = 0;
			}
			if (projectile.timeLeft <= 245 && projectile.timeLeft > 240)
            {
				projectile.velocity *= 0.5f;
            }
			if (projectile.timeLeft == 240)
            {
				projectile.velocity *= -10;
            }
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
		}
	}
}
