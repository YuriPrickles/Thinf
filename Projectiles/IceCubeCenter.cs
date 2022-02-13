using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class IceCubeCenter : ModProjectile
	{
		bool spawnedCubes = false;
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 16;              //The height of projectile hitbox
			projectile.alpha = 255;
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
		}
		public override void AI()
		{
			if (!spawnedCubes)
			{
				int projectileSpawnAmount = 4;
				if (Main.expertMode || projectile.ai[0] == 2)
				{
					projectileSpawnAmount = 6;
				}
				if (projectile.ai[0] == 4)
				{
					projectileSpawnAmount = 8;
				}
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 7f;
					int damage = 90;
					int type = ModContent.ProjectileType<IceCube>();
					Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.Zero, type, damage, 1, projectile.owner)];
					proj.ai[0] = currentRotation;
					proj.ai[1] = projectile.whoAmI;
					proj.timeLeft = 180;
					if (Main.expertMode || projectile.ai[0] == 2)
					{
						proj.timeLeft = 200;
					}
					if (projectile.ai[0] == 4)
					{
						proj.timeLeft = 220;
					}
				}
				spawnedCubes = true;
			}
		}

		public override void Kill(int timeLeft)
		{

		}
	}
}
