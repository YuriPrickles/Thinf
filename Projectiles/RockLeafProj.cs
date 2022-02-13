using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class RockLeafProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rock Leaf");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 18;              //The height of projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!target.boss)
			{
				target.velocity = Vector2.Zero;
			}
			else if (Main.rand.Next(10) == 0)
			{
				target.velocity = Vector2.Zero;
			}
        }

        public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 9; i++)
            {
				Dust dust = Dust.NewDustDirect(projectile.position, 4, 4, DustID.Stone);
            }
			Main.PlaySound(SoundID.Tink, projectile.position);
		}
	}
}
