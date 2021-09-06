
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class GhostSeed : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Seed);
			aiType = ProjectileID.Seed;
			projectile.alpha = 125;
			projectile.tileCollide = false;
			projectile.timeLeft = 24;
			projectile.penetrate = 13;
		}
	}
}