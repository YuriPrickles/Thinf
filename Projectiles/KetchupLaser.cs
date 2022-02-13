using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class KetchupLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 16;              //The height of projectile hitbox
			projectile.alpha = 255;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 900;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 70;
		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{

		}
		public override void AI()
		{
			for (int i = 0; i < 5; ++i)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] > 16f)
				{
					Vector2 projectilePosition = projectile.Center;
					projectilePosition -= projectile.velocity * (i * 0.25f);
					Dust dust;
					dust = Main.dust[Dust.NewDust(projectilePosition, 1, 1, DustID.Blood, 0f, 0f, 0, default, 0.6f)];
					dust.noGravity = true;
					dust.fadeIn = 0.7f;
				}
			}
		}

		public override void Kill(int timeLeft)
		{

		}
    }
}
