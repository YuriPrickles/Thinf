
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class Tom : ModProjectile
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
            int projectileSpawnAmount = 3;
            for (int i = 0; i < projectileSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 6f;
                int damage = projectile.damage;
                int type = ProjectileID.RubyBolt;
                Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, new Vector2(7, 0).RotatedByRandom(MathHelper.ToRadians(360)), type, damage, 1, projectile.owner)];
                proj.magic = false;
                proj.tileCollide = false;
                proj.hostile = true;
                proj.friendly = false;
            }
        }
    }
}