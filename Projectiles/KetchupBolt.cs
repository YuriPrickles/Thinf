using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class KetchupBolt : ModProjectile
	{
		int snowflakeDelay = 0;
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			projectile.width = 8;               //The width of projectile hitbox
			projectile.height = 8;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = Thinf.ToTicks(10);          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			projectile.alpha = 255;
			projectile.extraUpdates = 2;
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override void AI()
		{
            for (int i = 0; i < 4; i++)
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(projectile.position, 1, 1, DustID.Blood, 0, 0, 0, default, 0.6f)];
				dust.noGravity = true;
				dust.fadeIn = 0.7f;
			}
			projectile.ai[0]++;
			if (projectile.ai[0] >= 30)
			{
				if (projectile.Distance(Main.MouseWorld) >= 40)
				{
					projectile.velocity += Vector2.Lerp(projectile.velocity, projectile.DirectionTo(Main.MouseWorld) * 8f, .4f);
					if (projectile.velocity.Length() < 35f)
						projectile.velocity *= .5f;
				}
			}

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return true;
		}
	}
}
