using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class ElectroBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
		}

		public override void AI()
		{
			if (Main.mouseRight || projectile.wet)
            {
				projectile.ai[0] = 2;
				projectile.Kill();
            }
			if (Main.rand.NextBool())
            {
				if (Main.rand.NextBool())
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Electric);
					dust.noGravity = true;
					dust.scale = 1.8f;
				}
			}
		}


        public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item94, Main.player[projectile.owner].Center);
			if (projectile.ai[0] == 2 && projectile.timeLeft < 119)
			{
				Projectile proj = Projectile.NewProjectileDirect(projectile.Center, Vector2.Zero, ModContent.ProjectileType<BatteryBlasterProj>(), projectile.damage, projectile.knockBack, projectile.owner);
				proj.timeLeft = 3;
				proj.ai[0] = 1;
			}
		}
    }
}
