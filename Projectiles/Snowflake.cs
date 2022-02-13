using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class Snowflake : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.NorthPoleSnowflake;

		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
			DisplayName.SetDefault("Snowflake");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GoldCoin);

			projectile.friendly = false;         //Can the projectile deal damage to enemies?
			projectile.hostile = true;         //Can the projectile deal damage to the player?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 1200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			aiType = ProjectileID.NorthPoleSnowflake;           //Act exactly like default Bullet
		}

        public override bool PreAI()
        {
			projectile.velocity = Vector2.Zero;

			return true;
        }
        public override void AI()
		{
			Lighting.AddLight(projectile.Center, Color.LightBlue.ToVector3());
			projectile.velocity = Vector2.Zero;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.Frostburn, 240);
        }
        public override void Kill(int timeLeft)
		{

		}
	}
}
