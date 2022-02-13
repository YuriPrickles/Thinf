using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles.MotherNatureProjectiles
{
    public enum WaveType
    {
        Normal,
        Mini,
        Spam
    }
    public class DustParticleGeometryDashWave : ModProjectile
    {
        int spawnDir = 0;
        WaveType waveType = WaveType.Normal;
        bool hasSetWaveType = false;
        int geometryDashWaveMovementTimer = 23;
        int frameTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2400;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            if (!hasSetWaveType)
            {
                spawnDir = projectile.direction;
                waveType = (WaveType)Main.rand.Next(3);
                if (waveType == WaveType.Normal) projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(45 * -spawnDir));
                if (waveType == WaveType.Mini) projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(67.5f * -spawnDir));
                if (waveType == WaveType.Spam)
                {
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-45 * -spawnDir));
                    geometryDashWaveMovementTimer = 17;
                }
                //Main.NewText(waveType.ToString());
                hasSetWaveType = true;
            }
            geometryDashWaveMovementTimer++;

            if (waveType == WaveType.Spam)
            {
                switch (geometryDashWaveMovementTimer)
                {
                    case 9:
                        projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(90) * spawnDir);
                        break;
                    case 18:
                        projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-90) * spawnDir);
                        geometryDashWaveMovementTimer = 0;
                        break;
                }
            }
            switch (geometryDashWaveMovementTimer)
            {
                case 45:
                    switch (waveType)
                    {
                        case WaveType.Normal:
                            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(90) * spawnDir);
                            break;
                        case WaveType.Mini:
                            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(135) * spawnDir);
                            break;
                    }
                    break;
                case 90:
                    switch (waveType)
                    {
                        case WaveType.Normal:
                            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(90) * -spawnDir);
                            break;
                        case WaveType.Mini:
                            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-135) * spawnDir);
                            break;
                    }
                    geometryDashWaveMovementTimer = 0;
                    break;
            }
            if (Main.rand.Next(100) >= 75)
            {
                Dust.NewDust(projectile.position, 20, 20, DustID.Smoke);
            }
            frameTimer++;
            if (frameTimer == 5)
            {
                projectile.frame++;
                if (projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
                frameTimer = 0;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Blackout, 180);
        }
        public override void Kill(int timeLeft)
        {

        }
    }
}
