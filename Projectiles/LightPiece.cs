using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class LightPiece : ModProjectile
	{
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.width = 28;               //The width of projectile hitbox
			projectile.height = 28;              //The height of projectile hitbox
			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 20;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
		}
		public override void AI()
		{
			projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(0.6f));
			Dust dust = Dust.NewDustDirect(projectile.Center, 28, 28, DustID.SpectreStaff, 0, 0, 0, Color.Pink, 2.2f);
			dust.noGravity = true;
			dust.fadeIn = 3;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item15, projectile.position);
		}
	}
}
