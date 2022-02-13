using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace Thinf.Projectiles
{
	public class CrimeraLeafProj : ModProjectile
	{
		Player player = Main.LocalPlayer;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimera Larvae");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 18;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			
		}
        public override void AI()
		{
			if (projectile.ai[0] == 120)
            {
				projectile.penetrate = -1;
            }
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.penetrate < 3)
			{
				projectile.velocity = projectile.DirectionTo(player.Center) * 24;
				projectile.damage = 0;
				projectile.tileCollide = false;
				if (projectile.Distance(player.Center) <= 20f)
                {
					player.HealEffect(2);
					player.statLife += 2;
					Main.PlaySound(SoundID.Item2, projectile.position);
					projectile.Kill();
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Grass, projectile.position);
			for (int k = 0; k < 40; ++k)
            {
				Dust.NewDust(projectile.Center, 20, 20, 5);
            }
		}
	}
}
