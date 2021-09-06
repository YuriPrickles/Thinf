using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Blizzard;

namespace Thinf.Projectiles
{
	public class ChillyCubeProj : ModProjectile
	{
		int hasCollided = 0;
		int epicBeamTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bouncy Tater");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 36;               //The width of projectile hitbox
			projectile.height = 36;              //The height of projectile hitbox

			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 120000;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(29, projectile.position, 104);
			hasCollided = 1;
			return false;
		}
		public override void AI()
		{
			if (hasCollided == 1)
			{
				epicBeamTimer++;
				if (epicBeamTimer % 12 == 0)
				{
					Main.PlaySound(SoundID.Item9, projectile.position);
				}
				projectile.velocity = Vector2.Zero;
				Dust dust;
				Vector2 position = projectile.Center;
				dust = Dust.NewDustPerfect(position, 111, new Vector2(0f, -10f), 0, new Color(255, 255, 255), 5f);
				dust.fadeIn = 3f;
				if (epicBeamTimer == Thinf.ToTicks(5))
				{
					Main.NewText("A strange figure comes out of the beacon!", 175, 75, 255);
					NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y - 200, ModContent.NPCType<Blizzard>());
					Main.PlaySound(SoundID.DD2_EtherianPortalOpen, projectile.position);
				}
				if (epicBeamTimer == 420)
				{
					projectile.Kill();
				}
			}
			if (hasCollided == 0)
			{
				projectile.rotation += 0.2f;
				projectile.velocity.Y += 0.12f;
			}
		}
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 120; ++i)
            {
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.Center - new Vector2(200, 0);
				dust = Main.dust[Terraria.Dust.NewDust(position, 400, 21, 111, 0f, -1.052632f, 0, new Color(255, 255, 255), 1.5f)];
				dust.fadeIn = 0.9473684f;
			}
		}
	}
}
