
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class AppleRocket : ModProjectile
	{

		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.RocketI);
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.timeLeft = Thinf.ToTicks(10);
			projectile.penetrate = 1;
		}
		
        public override void AI()
        {
			Player player = Thinf.FindNearestPlayer(20000, projectile.Center);
			projectile.velocity = projectile.DirectionTo(player.Center) * 3;
			projectile.rotation += 0.3f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item14, projectile.Center);
            projectile.Kill();
            return true;
        }
        public override void Kill(int timeLeft)
        {
            int projectileSpawnAmount = 8;
            for (int i = 0; i < projectileSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                proj.tileCollide = false;
                proj.timeLeft = Thinf.ToTicks(10);
            }
        }
    }
}