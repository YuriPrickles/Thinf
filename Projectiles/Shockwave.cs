
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class Shockwave : ModProjectile
    {
        int distance = 32;
        bool setFirstHitBox = false;
        public override void SetStaticDefaults()
        {

        }

        public override void AI()
        {
            distance += 16;
            int dustSpawnAmount = 75;
            for (int i = 0; i < dustSpawnAmount; ++i)
            {
                float currentRotation = MathHelper.TwoPi / dustSpawnAmount * i;
                Vector2 dustOffset = currentRotation.ToRotationVector2();
                Dust dust = Dust.NewDustDirect(projectile.Center + dustOffset * distance, 8, 8, DustID.Cloud, 0, 0, 0, default, 1.5f);
                dust.noGravity = true;
                dust.fadeIn = 0.1f;
            }
            Rectangle newHitbox = projectile.Hitbox;
            newHitbox.Inflate(16, 16);
            projectile.Hitbox = newHitbox;
        }
        public override void SetDefaults()
        {
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
        }
        public override void Kill(int timeLeft)
        {
        }
    }
}