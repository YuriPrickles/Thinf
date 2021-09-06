using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class HotShotProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spicy Hot Shot");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 16;              //The height of projectile hitbox
			projectile.alpha = 0;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.extraUpdates = 0;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();

			if (projectile.wet)
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int projectileSpawnAmount = 2;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 6f;
				int damage = projectile.damage / 2;
				int type = ProjectileID.MolotovFire2;
				Projectile proj = Main.projectile[Projectile.NewProjectile(projectile.Center, projectileVelocity, type, damage, 1, projectile.owner)];
				proj.thrown = false;
				proj.ranged = true;
				proj.tileCollide = true;
				proj.timeLeft = 240;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int k = 0; k < 2; k++)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 3, 3, 127, 0f, 0f, 0, new Color(255, 255, 255), 0.5f)];
				dust.noGravity = true;
				dust.fadeIn = 0.4f;

			}
			return true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 300);
        }
    }
}
