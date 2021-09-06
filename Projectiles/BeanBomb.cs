
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class BeanBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void AI()
        {
            if (projectile.timeLeft == 25)
            {
                projectile.velocity = Vector2.Zero;
            }
            projectile.rotation += 0.3f;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 40;
        }

        public override void Kill(int timeLeft)
        {
            int projectileSpawnAmount = 8;
            for (int i = 0; i < projectileSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 6f;
                int damage = projectile.damage;
                int type = ProjectileID.MolotovFire2;
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity, type, damage, 1, projectile.owner)];
                proj.thrown = false;
                proj.ranged = true;
                proj.tileCollide = true;
                proj.scale *= 2;
                proj.Size *= 2;
                proj.timeLeft = 25;
            }
        }
    }
}