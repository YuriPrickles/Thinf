using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class SoulBomb : ModProjectile
    {
        int type;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            projectile.width = 32;               //The width of projectile hitbox
            projectile.height = 32;              //The height of projectile hitbox
            projectile.alpha = 255;
            projectile.friendly = false;         //Can the projectile deal damage to enemies?
            projectile.hostile = true;         //Can the projectile deal damage to the player?
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = false;          //Can the projectile collide with tiles?
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.ai[0] == 69 && projectile.Distance(player.Center) >= 50)
            {
                if (projectile.ai[1] == 1)
                {
                    type = ModContent.ProjectileType<LightPiece>();
                }
                if (projectile.ai[1] == 2)
                {
                    type = ModContent.ProjectileType<NightPiece>();
                }
                if (projectile.ai[1] == 1)
                {
                    Dust dust = Dust.NewDustDirect(projectile.Center, 8, 8, DustID.SpectreStaff, 0, 0, 0, Color.Pink, 5);
                    dust.noGravity = true;
                    dust.fadeIn = 3;
                }
                if (projectile.ai[1] == 2)
                {
                    Dust dust = Dust.NewDustDirect(projectile.Center, 8, 8, DustID.SpectreStaff, 0, 0, 0, Color.Purple, 5);
                    dust.noGravity = true;
                    dust.fadeIn = 3;
                }
                projectile.velocity = projectile.DirectionTo(player.Center) * 4;
            }

            if (projectile.ai[1] == 1)
            {
                type = ModContent.ProjectileType<LightPiece>();
            }
            if (projectile.ai[1] == 2)
            {
                type = ModContent.ProjectileType<NightPiece>();
            }
            if (projectile.ai[1] == 1)
            {
                Dust dust = Dust.NewDustDirect(projectile.Center, 8, 8, DustID.SpectreStaff, 0, 0, 0, Color.Pink, 5);
                dust.noGravity = true;
                dust.fadeIn = 3;
            }
            if (projectile.ai[1] == 2)
            {
                Dust dust = Dust.NewDustDirect(projectile.Center, 8, 8, DustID.SpectreStaff, 0, 0, 0, Color.Purple, 5);
                dust.noGravity = true;
                dust.fadeIn = 3;
            }
            if (projectile.timeLeft == 20)
            {
                int projectileSpawnAmount = 4;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                    int damage = 43;  //projectile damage
                    Projectile.NewProjectile(projectile.Center, projectileVelocity * 5, type, damage, 1); //code by eldrazi#2385
                }
                Main.PlaySound(SoundID.Item15, projectile.position);
            }
        }

        public override void Kill(int timeLeft)
        {
        }
    }
}
