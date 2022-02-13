using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles.MotherNatureProjectiles
{
	public class BurningLeaf : ModProjectile
	{
		int frameTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 24;               //The width of projectile hitbox
			projectile.height = 12;              //The height of projectile hitbox
			
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 2400;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			if (projectile.velocity.Y < 3)
			{
				projectile.velocity.Y += 0.01f;
			}

			if (projectile.velocity.Y > 0)
            {
				if (projectile.timeLeft % 80 == 0)
                {
					projectile.velocity.X *= -0.7f;
					projectile.velocity.Y -= 0.5f;
                }
            }
			if (projectile.timeLeft <= 2340)
			{
				if (Main.rand.Next(100) >= 55)
				{
					Dust.NewDust(projectile.position, 20, 20, DustID.Fire);
				}
				frameTimer++;
				if (frameTimer == 5)
				{
					projectile.frame++;
					if (projectile.frame >= 2)
					{
						projectile.frame = 1;
					}
					frameTimer = 0;
				}
			}
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (projectile.timeLeft <= 2340)
			{
				target.AddBuff(BuffID.Burning, 180);
			}
		}
        public override void Kill(int timeLeft)
		{

		}
	}
}
