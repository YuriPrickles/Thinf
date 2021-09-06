using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class SpudLauncher : ModItem
	{
		int spudcount = 0;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots Rockets AND Potatoes");
		}

		public override void SetDefaults()
		{
			item.damage = 75;
			item.crit = (int)0f;
			item.ranged= true;
			item.width = 48;
			item.height = 32;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = false;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Rocket;
			if (item.useAmmo == ItemID.RocketI)
			{
				item.shoot = ProjectileID.RocketI;
			}
			if (item.useAmmo == ItemID.RocketII)
			{
				item.shoot = ProjectileID.RocketII;
			}
			if (item.useAmmo == ItemID.RocketIII)
			{
				item.shoot = ProjectileID.RocketIII;
			}
			if (item.useAmmo == ItemID.RocketIV)
			{
				item.shoot = ProjectileID.RocketIV;
			}


		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (item.useAmmo == ItemID.RocketI)
            {
				item.shoot = ProjectileID.RocketI;
            }
			if (item.useAmmo == ItemID.RocketII)
			{
				item.shoot = ProjectileID.RocketII;
			}
			if (item.useAmmo == ItemID.RocketIII)
			{
				item.shoot = ProjectileID.RocketIII;
			}
			if (item.useAmmo == ItemID.RocketIV)
			{
				item.shoot = ProjectileID.RocketIV;
			}
			Projectile rocketproj = Main.projectile[item.shoot];
			rocketproj.hostile = false;
			rocketproj.velocity *= 4;

			

			spudcount++;
			if (spudcount >= 3)
			{
				int numberProjectiles = 5;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
					Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<BouncyTater>(), damage / 2, knockBack, player.whoAmI)];
					projectile.hostile = false;
					projectile.friendly = true;
				}
				spudcount = 0;
			}
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod); recipe.AddIngredient(ModContent.ItemType<Placeables.PotatiumiteBar>(), 15); recipe.AddIngredient(ModContent.ItemType<Placeables.Potato>(), 50); recipe.AddIngredient(ItemID.DirtBlock, 50);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}
