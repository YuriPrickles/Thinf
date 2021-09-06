using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class LavaPiss : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magma Stream");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 8;               //The width of projectile hitbox
			projectile.height = 8;              //The height of projectile hitbox
			projectile.alpha = 255;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 40;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 0;
			aiType = ProjectileID.WaterStream;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
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
			projectile.rotation = projectile.velocity.ToRotation();
            projectile.velocity.Y += 0.15f;

			if (projectile.wet)
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 3, 3, 127, 0f, 0f, 0, new Color(255, 255, 255), 3.026316f)];
				dust.noGravity = true;
				dust.fadeIn = 1.7f;

			}
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 240);
        }
    }
}
