using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class CupcakeLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Macintosh Beam");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 1;               //The width of projectile hitbox
			projectile.height = 1;              //The height of projectile hitbox
			projectile.alpha = 255;
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 900;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
			projectile.extraUpdates = 70;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			{
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, Main.DiscoColor.ToVector3() / 255);
			for (int i = 0; i < 5; ++i)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] > 16f)
				{
					Vector2 projectilePosition = projectile.Center;
					projectilePosition -= projectile.velocity * (i * 0.25f);
					projectile.rotation = projectile.velocity.ToRotation();
					if (Main.rand.Next(12) == 0)
					{
						Dust dust;
						dust = Main.dust[Dust.NewDust(projectilePosition, 1, 1, DustID.PortalBolt, 0f, 0f, 0, new Color(255, 0, 222), Main.rand.NextFloat(0.4f, 0.9f))];
						dust.noGravity = true;
						dust.fadeIn = 1.1f;
					}
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{

		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.immuneTime = 0;
			projectile.Kill();
			for (int i = 0; i < 7; i++)
			{
				Dust dust;
				dust = Main.dust[Dust.NewDust(target.position, 1, 1, DustID.PortalBoltTrail, 0f, 0f, 0, new Color(255, 0, 222), 1.3f)];
				dust.noGravity = true;
				dust.fadeIn = 1.1f;
			}
		}

		public override void Kill(int timeLeft)
		{
		}
	}
}
