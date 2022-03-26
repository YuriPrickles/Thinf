using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles.MotherNatureProjectiles
{
	public class ColorLooper : ModProjectile
	{
		int frameTimer = 0;
		public override void SetStaticDefaults()
		{

			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 64;               //The width of projectile hitbox
			projectile.height = 40;              //The height of projectile hitbox
			
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
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
			}
			return false;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			frameTimer++;
			if (frameTimer == 5)
			{
				projectile.frame++;
				if (projectile.frame >= 2)
				{
					projectile.frame = 0;
				}
				frameTimer = 0;
			}
			//for (int k = 0; k < 27; ++k)
			//{
			//	Dust dust = Main.dust[Dust.NewDust(projectile.position, 16, 16, DustID.RainbowRod)];
			//	dust.noGravity = true;
			//}
			projectile.ai[1]++;
			if (projectile.ai[1] >= 10)
			{
				projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(1));
			}
		}

		public override void Kill(int timeLeft)
		{
		}
	}
}
