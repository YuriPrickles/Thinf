
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class DeathAcorn : ModProjectile
	{
		public override string Texture => "Terraria/Item_" + ItemID.Acorn;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acorn of Immense Misery");
		}

		public override void SetDefaults()
		{
			projectile.Size = new Vector2(20);
			projectile.penetrate = 1;
			projectile.timeLeft = 800;
			projectile.friendly = false;
			projectile.hostile = true;
		}

        public override void AI()
        {
			projectile.rotation += 0.1f;
			if (projectile.ai[0] == 1)
            {

            }
        }
    }
}