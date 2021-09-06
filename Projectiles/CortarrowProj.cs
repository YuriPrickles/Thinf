using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class CortarrowProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cortarrow");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 32;               //The width of projectile hitbox
            projectile.height = 18;              //The height of projectile hitbox

            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            projectile.light = 0.4f;            //How much light emit around the projectile
            projectile.tileCollide = true;          //Can the projectile collide with tiles?
            aiType = ProjectileID.WoodenArrowFriendly;           //Act exactly like default Bullet
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.boss)
            {
                if (damage >= target.life)
                {
                    Projectile.NewProjectile(target.position, Vector2.Zero, ModContent.ProjectileType<PortalButGood>(), projectile.damage, 0, projectile.owner);
                }
            }
            else if (Main.rand.Next(8) == 0)
            {
                Projectile.NewProjectile(target.position, Vector2.Zero, ModContent.ProjectileType<PortalButGood>(), projectile.damage, 0, projectile.owner);
            }
        }
    }
}
