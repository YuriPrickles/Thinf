using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class NebulaPetalTwo : ModProjectile
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
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
		}
	}
}
