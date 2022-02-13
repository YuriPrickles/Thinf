
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class CosmoSeed : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Seed");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Seed);
			aiType = ProjectileID.Seed;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int type = ProjectileID.PurpleLaser;
            for (int i = 0; i < 2; ++i)
            {
				int typerand = Main.rand.Next(4);
                switch (typerand)
                {
					case 0:
						type = ProjectileID.PurpleLaser;
						break;
					case 1:
						type = ProjectileID.HallowStar;
						break;
					case 2:
						type = ProjectileID.Meteor1;
						break;
					case 3:
						type = ProjectileID.StarWrath;
						break;
					default:
                        break;
                }
                Projectile proj = Projectile.NewProjectileDirect(target.position + new Vector2(Main.rand.Next(-500, 500), Main.rand.Next(-500, 500)), Vector2.Zero, type, projectile.damage / 4, projectile.knockBack, projectile.owner);
				proj.velocity = proj.DirectionTo(target.Center) * 9;
				proj.timeLeft = 600;
				proj.tileCollide = false;
				proj.melee = false;
				proj.ranged = false;
				proj.magic = false;
				proj.penetrate = 1;
			}
        }
    }
}