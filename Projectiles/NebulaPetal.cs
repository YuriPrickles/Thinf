using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class NebulaPetal : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.FlowerPowPetal;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Petal");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.FlowerPowPetal);
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
			projectile.magic = true;
			projectile.timeLeft = 300;
			projectile.aiStyle = 41;
		}

		
		public override void AI()
		{

			projectile.rotation = projectile.velocity.ToRotation();

			if (projectile.timeLeft == 150)
			{
				int projectileSpawnAmount = 4;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();
					int damage = projectile.damage;  //projectile damage
					int type = mod.ProjectileType("NebulaPetalTwo");  //put your projectile
					Main.PlaySound(98, (int)projectile.position.X, (int)projectile.position.Y, 17);
					Projectile.NewProjectile(projectile.position, projectileVelocity * 7, type, damage, 0, projectile.owner); //code by eldrazi#2385
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
		}
	}
}
