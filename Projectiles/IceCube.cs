
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class IceCube : ModProjectile
	{
		public float rotat = 0;
		public override void SetStaticDefaults()
		{

		}

		public override void AI()
		{
			Projectile target = Main.projectile[(int)projectile.ai[1]];
			projectile.ai[0] += 0.05f;
			projectile.velocity = projectile.DirectionTo(target.Center + Vector2.One.RotatedBy(projectile.ai[0]) * 128f) * 12;
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 40;
		}

		public override void Kill(int timeLeft)
		{
			int projectileSpawnAmount = 4;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 7f;
				int damage = projectile.damage / 3;
				int type = ProjectileID.FrostBlastHostile;
				Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity, type, damage, 1, projectile.owner)];
			}
		}
	}
}