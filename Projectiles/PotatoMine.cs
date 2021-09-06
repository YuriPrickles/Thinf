using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly

{
	public class PotatoMine : ModProjectile
	{
		int boomcheck = 0;
		int hitboxchange = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PotatoMine");
			Main.projFrames[projectile.type] = 4;  //this is where you add how many frames u'r projectile has to make the animation
		}

		public override void SetDefaults()
		{
			projectile.width = 34; //Set the hitbox width
			projectile.height = 38;   //Set the hitbox heinght
			projectile.hostile = false;    //tells the game if is hostile or not.
			projectile.friendly = true;   //Tells the game whether it is friendly to players/friendly npcs or not
			projectile.ignoreWater = true;    //Tells the game whether or not projectile will be affected by water
			projectile.timeLeft = 7200;  // this is the projectile life time( 60 = 1 second so 900 is 15 seconds )     
			projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed  -1 is infinity
			projectile.tileCollide = true; //Tells the game whether or not it can collide with tiles/ terrain
			projectile.sentry = true; //tells the game that this is a sentry
			projectile.alpha = 0;
		}
		public override void Kill(int timeLeft)
		{
			hitboxchange = 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (boomcheck == 0)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}

				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}

				for (int g = 0; g < 2; g++)
				{
					int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}
				Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 1.5f);
				Projectile.NewProjectile((int)projectile.Center.X + 100, (int)projectile.Center.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 1.5f);
				Projectile.NewProjectile((int)projectile.Center.X - 100, (int)projectile.Center.Y, 0, 0, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 1.5f);
				for (int i = 0; i < 20; i++)
				{
					int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Dirt, 0, 0, 0, default, 3.50f);
					Main.dust[dust].noGravity = false;
					Main.dust[dust].velocity *= 2.5f;
				}
				boomcheck = 1;
			}
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.Center);
			projectile.timeLeft = 3;
		}


		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		public override void AI()
		{
			projectile.velocity.Y += 0.30f;
			if (projectile.timeLeft <= 3) 
			{
				projectile.tileCollide = false;
				projectile.alpha = 255;
				projectile.position = projectile.Center;
				projectile.width = 450;
				projectile.height = 450;
				projectile.Center = projectile.position;
			} 
		}
	}
}