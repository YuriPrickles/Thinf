using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class GuidePortrait : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guide the Guide the");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 60;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.rotation += 0.04f;

			//Player player = Main.player[Player.FindClosest(projectile.Center, 10000, 10000)];

			//if (projectile.velocity.Length() < 7)
			//{
			//	projectile.velocity += projectile.DirectionTo(player.Center) * 0.3f;
			//}
			//else
   //         {
			//	projectile.velocity /= 3;
   //         }
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			int rand = Main.rand.Next(4);
            switch (rand)
            {
				case 0:
					Main.NewText("It's all in the mind");
					break;
				case 1:
					Main.NewText("Cure it");
					break;
				case 2:
					Main.NewText("Someone can help you");
					break;
				case 3:
					Main.NewText("Nothing is real");
					break;
			}
			Main.PlaySound(SoundID.Chat);
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
