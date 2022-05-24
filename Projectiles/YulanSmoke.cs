using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class YulanSmoke : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			projectile.alpha = 255;
			projectile.width = 128;               //The width of projectile hitbox
			projectile.height = 128;              //The height of projectile hitbox
			projectile.magic = false;
			projectile.minion = false;
			projectile.melee = false;
			projectile.thrown = false;
			projectile.ranged = false;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 180;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
		}

		public override void AI()
		{
			for (int k = 0; k < 12; ++k)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, 128, 128, DustID.CrystalSerpent);
				dust.noGravity = true;
			}
		}
	}
}
