
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class SnowGlobeBallTwo : ModProjectile
	{
		int snowflake = 0;
		public override string Texture => "Terraria/Projectile_" + ProjectileID.SnowBallFriendly;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snowball");
		}

		public override void AI()
		{
			projectile.penetrate = -1;
			projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(1.5f));
			snowflake++;

			if (snowflake == 5)
			{
				Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ModContent.ProjectileType<Snowflake>(), 0, 0, projectile.owner);
				proj.melee = false;
				proj.magic = true;
				snowflake = 0;
			}
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
			aiType = 0;
			projectile.aiStyle = -1;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 240;
		}
	}
}