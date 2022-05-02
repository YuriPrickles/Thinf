using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class SlagShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slag Shot");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

        public override void SetDefaults()
		{
			projectile.width = 24;               //The width of projectile hitbox
			projectile.height = 12;              //The height of projectile hitbox
			
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.light = 0.1f;
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[projectile.owner];
			if (!player.GetModPlayer<MyPlayer>().ironMode && player.GetModPlayer<MyPlayer>().furnaceCharge <= 5000)
			{
				player.GetModPlayer<MyPlayer>().furnaceCharge += damage / 15;
			}
			if (!player.GetModPlayer<MyPlayer>().ironMode)
            {
				target.AddBuff(BuffID.OnFire, Thinf.MinutesToTicks(1));
            }
			else
			{
				target.AddBuff(ModContent.BuffType<OwItBurns>(), Thinf.ToTicks(3));
			}
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item73, projectile.position).Volume = 0.1f;
            for (int i = 0; i < 8; i++)
            {
				Dust.NewDust(projectile.Center, 30, 30, DustID.LavaBubbles);
            }
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Player player = Main.player[projectile.owner];
			//Redraw the projectile with the color not influenced by light
			if (player.GetModPlayer<MyPlayer>().ironMode)
			{
				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				for (int k = 0; k < projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
					Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
					spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, new Color(color.ToVector3() * Color.Orange.ToVector3() * 0.9f), projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
	}
}
