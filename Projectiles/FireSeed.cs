
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class FireSeed : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hell Seed");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Seed);
			aiType = ProjectileID.Seed;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 240);
        }
    }
}