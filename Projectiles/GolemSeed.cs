using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class GolemSeed : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boing Seed");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Seed);
            projectile.penetrate = 4;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = true;          //Can the projectile collide with tiles?
            aiType = ProjectileID.Seed;
        }

        public override void AI()
        {
            projectile.rotation += 0.4f;
            projectile.velocity.Y += 0.6f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            if (Main.player[projectile.owner].ZoneJungle || Main.dayTime)
            {
                int projectileSpawnAmount = Main.rand.Next(2) + 3;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 6f;
                    int damage = projectile.damage / 5;
                    int type = ProjectileID.Seed;
                    Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity.RotatedByRandom(MathHelper.ToRadians(35)), type, damage, 1, projectile.owner)];
                    proj.tileCollide = false;
                    proj.timeLeft = 25;
                }
            }
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Tink, projectile.position);
        }
    }
}
