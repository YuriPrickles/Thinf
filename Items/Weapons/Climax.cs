using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Climax : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sends out a burst of bolts when hitting an enemy");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.crit = (int)0.15f;
			item.magic = true;
			item.width = 32;
			item.height = 38;
			item.useTime = 21;
			item.useAnimation = 21;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.knockBack = 2;
			item.value = 20000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.shootSpeed = 2f;
			item.scale = 2;
		}


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			int projectileSpawnAmount = 25;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				if (i % 5 == 0)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 5;
					damage = item.damage;  //projectile damage
					int type = ProjectileID.MartianTurretBolt;  //put your projectile
					Main.PlaySound(92, player.position);
					Projectile projectile = Main.projectile[Projectile.NewProjectile(player.position, projectileVelocity.RotatedByRandom(7), type, damage / 2, 2, player.whoAmI)];
					projectile.melee = true;
					projectile.hostile = false;
					projectile.friendly = true;
					projectile.penetrate = 2;
				}
			}
		}
    }
}