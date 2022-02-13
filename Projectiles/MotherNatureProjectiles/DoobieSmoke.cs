using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles.MotherNatureProjectiles
{
	public class DoobieSmoke : ModProjectile
	{
		int alphatimer = 0;
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 64;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1200;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;
			projectile.scale = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			if (Main.rand.Next(100) >= 75)
			{
				Dust.NewDust(projectile.position, 20, 20, DustID.Ash, 0 , 0, 0, default, 1.2f);
			}
			alphatimer++;
			if (alphatimer == 8)
			{
				projectile.alpha++;
				alphatimer = 0;
			}
			if (projectile.alpha >= 200)
            {
				projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Blackout, 600);
			target.AddBuff(BuffID.Obstructed, 100);
			target.AddBuff(BuffID.OnFire, 600);
			target.AddBuff(BuffID.Featherfall, 600);
		}

        public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
		}
	}
}
