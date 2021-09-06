using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class AirstrikeMarker : ModProjectile
    {
        Vector2 rocketPos;
        bool hasSetRocketPos = false;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.height = 1;
            projectile.alpha = 255;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 240;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            if (!hasSetRocketPos)
            {
                rocketPos = projectile.Center;
                hasSetRocketPos = true;
            }
            if (projectile.timeLeft <= 1)
            {
                Projectile airstrike = Projectile.NewProjectileDirect(rocketPos, new Vector2(0, 16), ProjectileID.RocketIII, 220, 40, Main.myPlayer);
                airstrike.timeLeft = 1200;
                airstrike.tileCollide = true;
                airstrike.hostile = true;
                airstrike.friendly = false;
            }
            for (int i = 0; i < 10; ++i)
            {
                Vector2 projectilePosition = projectile.Center;
                //projectilePosition -= projectile.velocity * (i * 0.25f);
                projectile.rotation = projectile.velocity.ToRotation();
                Dust dust;
                dust = Dust.NewDustPerfect(projectilePosition, DustID.Smoke, null, 0, default, 0.4f);
                dust.noGravity = true;
                dust.fadeIn = 5f;
            }
        }

        public override void Kill(int timeLeft)
        {
        }
    }
}
