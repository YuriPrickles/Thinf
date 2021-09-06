using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class IceKnifeTwo : ModProjectile
	{
		int snowflakeDelay = 0;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 32;              //The height of projectile hitbox
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = Thinf.ToTicks(5);          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override void AI()
		{
			Player player = Main.LocalPlayer;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

			projectile.ai[0]++;
			if (projectile.ai[0] == 60)
            {
				projectile.velocity = projectile.DirectionTo(player.Center) * 4;
            }
			snowflakeDelay++;
			if (snowflakeDelay == 20)
			{
				int damage = 56;  //projectile damage
				int type = ProjectileID.NorthPoleSnowflake;
				Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.Zero, type, damage, 1)]; //code by eldrazi#2385
				proj.hostile = true;
				proj.friendly = false;
				proj.tileCollide = false;
				snowflakeDelay = 0;
			}
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
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 180);
			target.AddBuff(BuffID.Bleeding, 300);
		}
	}
}
