using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class GunOfTheUniverse : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Nice try, Meteor Armor doesn't affect this");
		}

		public override void SetDefaults()
		{
			item.damage = 275;
			item.crit = 6;
			item.magic = true;
			item.mana = 8;
			item.width = 48;
			item.height = 22;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<GoTUProj>(); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 18f;

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpaceGun);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 20);
			recipe.AddIngredient(ItemID.MeteoriteBar, 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
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
				Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI)];
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
