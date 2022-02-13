using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items.Weapons;
using System;

namespace Thinf.Projectiles
{
	public class Plexislash : ModProjectile
	{
		float[] lV = new float[8];
		int index = 2;
		bool resetInit;
		bool texMade;
		Texture2D tex;
		Color[] lC = new Color[5] { Color.White, Color.White, Color.Orange, new Color(255, 214, 0), Color.Gold };
		bool setPenetrate = false;
		int charge = 0;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 80;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.melee = true;
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, Color.Yellow.ToVector3());
			projectile.rotation = projectile.velocity.ToRotation();

			Player player = Main.player[projectile.owner];

			Dust.NewDustDirect(projectile.position, 64, 64, DustID.TreasureSparkle, 0, 0);
			if (!setPenetrate)
            {
				Item item = player.HeldItem;
				if (item.type == ModContent.ItemType<PlixieglassSword>())
                {
					projectile.penetrate = 1 * player.GetModPlayer<MyPlayer>().plexiCharge;
					charge = player.GetModPlayer<MyPlayer>().plexiCharge;
				}
				setPenetrate = true;
			}
			lV[0] += (float)(Math.PI * 2) / 240;
			lV[1] = (float)(0.5 * Math.Sin(charge * 3 * lV[0] - 1.571f) + 0.5f);
			if (lV[0] >= Math.PI)
			{
				if (!resetInit)
				{
					resetInit = true;
					if (index++ > 4)
						index = 2;
				}
				if (lV[0] >= 2 * Math.PI)
				{
					if (index++ > 4)
						index = 2;
					lV[0] = 0;
					lC[0] = lC[index];
					lC[1] = lC[(index + 1) > 4 ? 2 : index + 1];
					resetInit = false;
				}
			}
			lV[2] = MathHelper.Lerp(lC[0].R, lC[1].R, lV[1]);
			lV[3] = MathHelper.Lerp(lC[0].G, lC[1].G, lV[1]);
			lV[4] = MathHelper.Lerp(lC[0].B, lC[1].B, lV[1]);
			lV[5] = MathHelper.Lerp(lC[1].R, lC[(index + 2) > 4 ? 3 : index + 2].R, lV[1]);
			lV[6] = MathHelper.Lerp(lC[1].G, lC[(index + 2) > 4 ? 3 : index + 2].G, lV[1]);
			lV[7] = MathHelper.Lerp(lC[1].B, lC[(index + 2) > 4 ? 3 : index + 2].B, lV[1]);
		}

		int LerpRound(float f1, float f2, float scale) => Round(MathHelper.Lerp(f1, f2, scale));
		int Round(float f) => (int)Math.Round(f);
		Color LerpColor(Color c1, Color c2, float lerp) => new Color(LerpRound(c1.R, c2.R, lerp), LerpRound(c1.G, c2.G, lerp), LerpRound(c1.B, c2.B, lerp), LerpRound(c1.A, c2.A, lerp));
		Color Get(bool type = false) => new Color(Round(lV[type ? 5 : 2]), Round(lV[type ? 6 : 3]), Round(lV[type ? 7 : 4]));
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 15; i++)
			{
				Dust.NewDustDirect(Main.player[projectile.owner].position, 30, 30, DustID.Pixie, 0, 0);
			}
			Main.PlaySound(SoundID.Item9, projectile.position);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (!texMade)
			{
				tex = new Texture2D(Main.graphics.GraphicsDevice, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height);
				Color[] array = new Color[Main.projectileTexture[projectile.type].Width * Main.projectileTexture[projectile.type].Height];
				Main.projectileTexture[projectile.type].GetData(array);
				for (int a = 0; a < array.Length; a++)
				{
					if (array[a] != Color.Transparent)
						array[a] = Color.White;
				}
				tex.SetData(array);
				texMade = true;
			}
			Texture2D pT = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				float scale = ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = LerpColor(projectile.GetAlpha(Get()), projectile.GetAlpha(Get(true)), scale) * ((float)projectile.timeLeft / 1000);
				spriteBatch.Draw(tex, drawPos, null, color, projectile.rotation, drawOrigin, (projectile.scale * scale) * lV[1], SpriteEffects.None, 0f);
				//spriteBatch.Draw(pT, drawPos, null, color * 0.5f, projectile.rotation, drawOrigin, projectile.scale * scale, SpriteEffects.None, 0f);

			}
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, null, Get(true) * ((float)projectile.timeLeft / 1000), projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(pT, projectile.Center - Main.screenPosition, null, Get(true) * ((float)projectile.timeLeft / 1000) * 0.5f, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
