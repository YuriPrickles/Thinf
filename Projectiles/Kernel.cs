using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class Kernel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kernel");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 8;               //The width of projectile hitbox
			projectile.height = 8;              //The height of projectile hitbox
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

		}
    }
}
