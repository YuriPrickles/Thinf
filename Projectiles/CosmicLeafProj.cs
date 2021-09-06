using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class CosmicLeafProj : ModProjectile
	{
		int starTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Leaf");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 36;               //The width of projectile hitbox
			projectile.height = 22;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 50;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();

			starTimer++;
			if (starTimer == 5)
            {
				Projectile proj1 = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(90)) * 0.5f, ProjectileID.HallowStar, projectile.damage, 0f, projectile.owner);
				proj1.tileCollide = false;
				proj1.timeLeft = 40;
				proj1.penetrate = 1;
				Projectile proj2 = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)) * 0.5f, ProjectileID.HallowStar, projectile.damage, 0f, projectile.owner);
				proj2.tileCollide = false;
				proj2.timeLeft = 40;
				proj2.penetrate = 1;
				starTimer = 0;
            }
		}


		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Grass, projectile.position);
		}
	}
}
