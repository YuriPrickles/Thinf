using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class ChaosBallers : ModProjectile
    {
        public override string Texture => "Terraria/NPC_" + NPCID.ChaosBall;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 8;
            projectile.height = 8;
            projectile.alpha = 128;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 0.4f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; ++i)
            {
                Dust dust;
                dust = Dust.NewDustPerfect(projectile.Center, DustID.Shadowflame, null, 0, default, 1.2f);
                dust.noGravity = true;
                dust.fadeIn = 1.4f;
            }
        }

        public override void Kill(int timeLeft)
        {
        }
    }
}
