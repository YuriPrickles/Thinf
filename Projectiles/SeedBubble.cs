
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class SeedBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void AI()
        {
            projectile.velocity *= 0.88f;
            projectile.velocity.Y -= 0.12f;
        }
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 70;
        }

        public override void Kill(int timeLeft)
        {
            int projectileSpawnAmount = 8;
            for (int i = 0; i < projectileSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 5f;
                int damage = projectile.damage;
                int type = ModContent.ProjectileType<NoGravSeed>();
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity.RotatedByRandom(MathHelper.ToRadians(360)), type, damage / 3, 1, projectile.owner)];
                proj.tileCollide = true;
            }
        }
    }
}