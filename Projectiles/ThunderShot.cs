using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Projectiles
{
	public class ThunderShot : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Thunder Shot");
        }
        public override void SetDefaults()
		{
			projectile.width = 16;
            projectile.height = 32;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = 9999999;
			projectile.timeLeft = 600;
			projectile.aiStyle = 29;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.light = 1.1f;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.AddBuff(BuffID.Electrified, 30, true);
		}
		/*public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				projectile.velocity *= 0.75f;
				Main.PlaySound(SoundID.Item10, projectile.position);
			}
			return false;
		}*/

		/*public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustType<Sparkle>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
			}
			Main.PlaySound(SoundID.Item25, projectile.position);
		}*/
	}
}