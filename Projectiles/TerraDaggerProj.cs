using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class TerraDaggerProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Dagger");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 40;               //The width of projectile hitbox
			projectile.height = 14;              //The height of projectile hitbox
			
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.magic = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			projectile.light = 0.24f;            //How much light emit around the projectile
			projectile.tileCollide = false;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override void AI()
		{
			projectile.ai[1]++;
			if (projectile.ai[1] < 121)
			{
				projectile.rotation = projectile.AngleTo(Main.MouseWorld);
			}
			if (projectile.ai[1] == 120)
			{
				for (int j = 0; j < 30; j++)
				{
					Dust dust;
					dust = Dust.NewDustPerfect(projectile.Center, 107, new Vector2(0, 0), 0, new Color(255, 255, 255), 1.2f);
					dust.noGravity = true;
				}
				if (projectile.ai[0] == 0)
				{
					projectile.velocity = projectile.DirectionTo(Main.MouseWorld) * 14f;
					projectile.rotation = projectile.velocity.ToRotation();
					projectile.ai[0] = 1;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}
