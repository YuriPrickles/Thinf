using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class SandRocket : ModProjectile
	{

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Sand Rocket");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 32;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			int projectileSpawnAmount = 8;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2();

				// Spawn projectile with the velocity, profit.
				float Speed = 21f;  //projectile speed
				Vector2 vector8 = new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2));
				int damage = 45;  //projectile damage
				int type = ProjectileID.SandBallGun;  //put your projectile
				Main.PlaySound(98, (int)projectile.position.X, (int)projectile.position.Y, 17);
				Projectile.NewProjectile(projectile.Center, projectileVelocity, type, damage, 0, projectile.owner); //code by eldrazi#2385
			}
		}
	}
}
