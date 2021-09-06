using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class FloatingSkull : ModProjectile
	{
		float rotationTimer = MathHelper.ToRadians(0);
		Vector2 destination;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		
		public override void SetDefaults()
		{
			projectile.width = 28;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 69420000;
			projectile.timeLeft = 99999999;
			projectile.alpha = 0;
			projectile.light = 0;
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
			projectile.extraUpdates = 0;
			aiType = ProjectileID.Bullet;
		}
        public override void AI()
        {
			if (Main.mouseRight)
            {
				projectile.Kill();
            }
			rotationTimer += 0.1f;
			if (rotationTimer == MathHelper.ToRadians(360))
            {
				rotationTimer = MathHelper.ToRadians(0);
            }
			Player player = Main.player[projectile.owner];
			destination = player.Center + Vector2.One.RotatedBy(rotationTimer) * 250;
			projectile.velocity = projectile.DirectionTo(destination) * 5;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.ShadowFlame, 60);
        }
        public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			player.statLife += 10;
			player.HealEffect(10);
		}
	}
}