
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class BlizzardShard : ModProjectile
	{

		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 14;
			projectile.penetrate = -1;
			projectile.alpha = 0;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.timeLeft = Thinf.ToTicks(10);
			projectile.penetrate = 1;
			projectile.tileCollide = true;
		}
		
		public override void AI()
		{
			Player player = Main.player[Player.FindClosest(projectile.position, 900000, 900000)];
			if (projectile.timeLeft >= Thinf.ToTicks(6) && projectile.timeLeft <= Thinf.ToTicks(8))
			{
				if (projectile.Distance(player.Center) <= 16 * 16)
				{
					projectile.velocity = projectile.DirectionTo(player.Center) * 2f;
				}
				else
				{
					projectile.velocity = projectile.DirectionTo(player.Center) * 8;
				}
				projectile.rotation = projectile.AngleTo(player.Center);
			}
			else
            {
				projectile.rotation = projectile.velocity.ToRotation();
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item28, projectile.Center);
			projectile.Kill();
			return true;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Frostburn, 300);
        }
        public override void Kill(int timeLeft)
		{

		}
	}
}