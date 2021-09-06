using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class SapBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            projectile.width = 24;               //The width of projectile hitbox
            projectile.height = 24;              //The height of projectile hitbox
            projectile.magic = false;
            projectile.minion = false;
            projectile.melee = false;
            projectile.thrown = false;
            projectile.ranged = false;
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.penetrate = 2;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = true;          //Can the projectile collide with tiles?
        }

        public override void AI()
        {
            projectile.rotation += 0.1f;
            projectile.velocity.Y += 0.03f;
            if (projectile.timeLeft <= 3)
            {
                projectile.position = projectile.Center;
                projectile.width = 36;
                projectile.width = 36;
                projectile.Center = projectile.position;
            }
            Dust dust;
            Vector2 position = projectile.position;
            dust = Dust.NewDustDirect(position, 24, 24, 259, 0f, 0f, 0, new Color(255, 255, 255), 1.842105f);
            dust.noGravity = true;
            dust.fadeIn = 2.25f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.timeLeft = 3;
            target.velocity = Vector2.Zero;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 3;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath1, projectile.Center);
        }
    }
}
