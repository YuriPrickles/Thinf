
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class PackingPeanutClump : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void AI()
        {
            if (projectile.timeLeft <= 45)
            {
                projectile.velocity *= 0.7f;
            }
            projectile.rotation += 0.5f;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 70;
        }

    }
}