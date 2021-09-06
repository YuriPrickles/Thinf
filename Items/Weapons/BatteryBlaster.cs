using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class BatteryBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a beam that arcs to other enemies\n'Engineering students are really pissed off at this'");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.crit = 6;
			item.ranged = true;
			item.width = 58;
			item.height = 52;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ModContent.ProjectileType<GoTUProj>(); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 18f;

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 1; // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
				if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				{
					position += muzzleOffset;
				}
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(0)); // 30 degree spread.
				Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<BatteryBlasterProj>(), damage, knockBack, player.whoAmI)];
				projectile.timeLeft = 600;
			}

			return false;

		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);

		}
	}
}
