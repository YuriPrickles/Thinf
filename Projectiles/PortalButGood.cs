using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class PortalButGood : ModProjectile
    {
        int type;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portal");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 14;               //The width of projectile hitbox
            projectile.height = 24;              //The height of projectile hitbox
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.melee = true;           //Is the projectile shoot by a ranged weapon?
            projectile.penetrate = -1;
            projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            projectile.light = 0.35f;            //How much light emit around the projectile
            projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = false;          //Can the projectile collide with tiles?
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            int projRand = Main.rand.Next(4);
            switch (projRand)
            {
                case 0:
                    type = ProjectileID.FireArrow;
                    break;
                case 1:
                    type = ProjectileID.MeteorShot;
                    break;
                case 2:
                    type = ProjectileID.Mushroom;
                    break;
                case 3:
                    type = ProjectileID.VilethornBase;
                    break;
            }
            if (projectile.timeLeft % 10 == 0)
            {
                Projectile spewedProj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.One * 8, type, 9, 1)];
                spewedProj.melee = true;
                spewedProj.magic = false;
                spewedProj.velocity = spewedProj.velocity.RotatedByRandom(MathHelper.ToRadians(360));
                spewedProj.ranged = false;
                spewedProj.owner = projectile.owner;
                spewedProj.noDropItem = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Projectile spewedProj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.One * 8, type, 9, 1)];
                spewedProj.melee = true;
                spewedProj.magic = false;
                spewedProj.velocity = spewedProj.velocity.RotatedByRandom(MathHelper.ToRadians(360));
                spewedProj.ranged = false;
                spewedProj.owner = projectile.owner;
                spewedProj.noDropItem = true;
            }
        }
    }
}
