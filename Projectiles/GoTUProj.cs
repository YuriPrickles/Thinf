using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class GoTUProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Beam");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 8;               //The width of projectile hitbox
			projectile.height = 8;              //The height of projectile hitbox
			projectile.alpha = 255;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 40;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
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
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			projectile.damage = (int)(projectile.damage * 0.9);
		}
		public override void AI()
		{
			for (int i = 0; i < 8; ++i)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] > 16f)
				{
					Vector2 projectilePosition = projectile.Center;
					projectilePosition -= projectile.velocity * (i * 0.25f);
					projectile.rotation = projectile.velocity.ToRotation();
					Dust dust;
					dust = Main.dust[Dust.NewDust(projectilePosition, 1, 1, 75, 0f, 0f, 0, new Color(0, 255, 117), 1.1f)];
					dust.noGravity = true;
					dust.fadeIn = 1.1f;
				}
			}
		}

		public override void Kill(int timeLeft)
		{

		}
    }
}
