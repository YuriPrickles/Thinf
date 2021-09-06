using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class TreeTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots leaves depending on your biome"
				+ "\nNot the Trees!");
		}

		public override void SetDefaults()
		{
			item.damage = 24;
			item.magic = true;
			item.mana = 21;
			item.width = 48;
			item.height = 48;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.LightPurple;
			item.UseSound = SoundID.Item9;
			item.shootSpeed = 10f;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MagicLeaf");
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.SpellTome);recipe.AddIngredient(ItemID.SoulofNight, 15);recipe.AddIngredient(ItemID.SoulofLight, 15);recipe.AddIngredient(ItemID.Acorn, 50);recipe.AddIngredient(ItemID.Wood, 200);recipe.AddIngredient(ItemID.Ebonwood, 200);recipe.AddIngredient(ItemID.Pearlwood, 200);recipe.AddIngredient(ItemID.BorealWood, 200);recipe.AddIngredient(ItemID.PalmWood, 200);recipe.AddIngredient(ItemID.RichMahogany, 200);recipe.AddIngredient(ItemID.ChlorophyteBar, 4);recipe.AddTile(TileID.Bookcases);recipe.SetResult(this); recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert && !player.ZoneGlowshroom && !player.ZoneMeteor && !player.ZoneBeach && !player.ZoneDesert && player.ZoneOverworldHeight)
			{
				item.shoot = ProjectileType<Projectiles.MagicLeaf>();
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneDesert || player.ZoneBeach)
			{
				item.shoot = ProjectileType<Projectiles.HotLeaf>();
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneSkyHeight)
			{
				item.shoot = ProjectileType<Projectiles.MagicLeaf>();
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneJungle)
			{
				item.shoot = ProjectileType<Projectiles.JungLeaf>();
				int numberProjectiles = 15 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneSnow)
			{
				item.shoot = ProjectileType<Projectiles.SnowLeaf>();
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneCorrupt)
			{
				item.shoot = ProjectileType<Projectiles.EbonLeaf>();
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneCrimson)
			{
				item.shoot = ProjectileType<Projectiles.ShadeLeaf>();
				int numberProjectiles = 3 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}

			if (player.ZoneHoly)
			{
				item.shoot = ProjectileType<Projectiles.PixieLeaf>();
				int numberProjectiles = 2 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
																													// If you want to randomize the speed to stagger the projectiles
																													// float scale = 1f - (Main.rand.NextFloat() * .3f);
																													// perturbedSpeed = perturbedSpeed * scale; 
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}
			return false;
		}
	}
}
