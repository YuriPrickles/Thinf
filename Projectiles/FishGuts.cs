using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class FishGuts : ModProjectile
	{
		//public override string Texture => "Terraria/Gore_" + 90;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishy Guts");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			for (int i = 0; i < 4; ++i)
			{
				Vector2 projectilePosition = projectile.Center;
				//projectilePosition -= projectile.velocity * (i * 0.25f);
				projectile.rotation = projectile.velocity.ToRotation();
				Dust dust;
				dust = Dust.NewDustPerfect(projectilePosition, DustID.Blood, null, 0, default, Main.rand.NextFloat(0.5f, 1.6f));
				dust.noGravity = false;
				dust.fadeIn = 1f;
			}
			if (projectile.timeLeft == 590)
            {
				projectile.damage /= 2;
			}
			if (projectile.timeLeft == 570)
			{
				projectile.damage /= 2;
			}
			if (projectile.timeLeft == 550)
			{
				projectile.damage /= 2;
			}
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.wet)
			{
				projectile.velocity.Y -= 0.2f;
			}
			else
			{
				projectile.velocity.Y += 0.05f;
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, projectile.position);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
