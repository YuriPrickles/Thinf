using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class Kaboom : ModProjectile
	{
		int frameTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 17;
			DisplayName.SetDefault("How did you die to this?");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 102;               //The width of projectile hitbox
			projectile.height = 140;              //The height of projectile hitbox

			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			frameTimer++;
			if (frameTimer == 12)
			{
				projectile.frame++;
				if (projectile.frame >= 17)
				{
					projectile.Kill();
				}
				frameTimer = 0;
			}
		}
	}
}
