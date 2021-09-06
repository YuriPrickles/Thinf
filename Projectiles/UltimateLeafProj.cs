using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace Thinf.Projectiles
{
	public class UltimateLeafProj : ModProjectile
	{
		int laserTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chlorophyte Leaf");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 48;               //The width of projectile hitbox
			projectile.height = 22;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			laserTimer++;
			if (laserTimer == 30)
            {
				int projectileSpawnAmount = 8;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();
					int damage = projectile.damage / 2;
					int type = ModContent.ProjectileType<LeafProj>();
					Projectile leaf = Projectile.NewProjectileDirect(projectile.Center, projectileVelocity * 6, type, damage, 0, projectile.owner);
					leaf.tileCollide = false;
					leaf.penetrate = 5;
					leaf.timeLeft = 60;
				}
				laserTimer = 0;
			}
		}
		public override void Kill(int timeLeft)
		{
			int projectileSpawnAmount = 8;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2();
				int damage = projectile.damage / 2;
				int type = ModContent.ProjectileType<LeafProj>();
				Main.PlaySound(98, (int)projectile.position.X, (int)projectile.position.Y, 17);
				Projectile leaf = Projectile.NewProjectileDirect(projectile.Center, projectileVelocity * 9, type, damage, 0, projectile.owner);
				leaf.tileCollide = false;
				leaf.timeLeft = 120;
			}
		}
	}
}
