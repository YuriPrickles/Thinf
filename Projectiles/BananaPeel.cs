using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class BananaPeel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Absolutely comedic banana peel that nobody would've saw coming, what a classic joke");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 10;              //The height of projectile hitbox
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 10;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
		}

        public override void AI()
        {
            projectile.velocity.Y += 0.25f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!target.boss)
			target.velocity.Y -= 12f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.NPCDeath1, projectile.position);
		}
	}
}
