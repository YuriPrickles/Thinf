using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class BatteryBlasterProjSplashDamage : ModProjectile
    {
        public int npcToAvoidDamaging = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrobeam Arc");
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 99999;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.extraUpdates = 70;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.whoAmI == npcToAvoidDamaging)
            {
                return false;
            }
            return null;
        }
        public override void AI()
        {
            for (int i = 0; i < 8; ++i)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 16f)
                {
                    Vector2 projectilePosition = projectile.Center;
                    projectilePosition -= projectile.velocity * (i * 0.25f);
                    projectile.rotation = projectile.velocity.ToRotation();
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(projectilePosition, 1, 1, DustID.Electric, 0f, 0f, 0, new Color(0, 255, 117), 0.4f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}
