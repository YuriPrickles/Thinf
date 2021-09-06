using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class SandBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Sand in the eye!'\nGood luck cleaning this\nConsumes more sand than normal");
		}

		public override void SetDefaults()
		{
			item.damage = 96;
			item.crit = (int)0f;
			item.ranged= true;
			item.width = 46;
			item.height = 24;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<SandRocket>();
			item.shootSpeed = 9f;
			item.useAmmo = AmmoID.Sand;

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Sandgun);recipe.AddIngredient(ItemID.RocketLauncher);recipe.AddIngredient(ItemID.AntlionMandible, 20);recipe.AddIngredient(ItemID.SunStone, 2);recipe.AddIngredient(ItemID.Topaz, 20);recipe.AddTile(TileID.AdamantiteForge);recipe.SetResult(this); recipe.AddRecipe();
		}


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < player.inventory.Length; i++)
			{
				Item item = player.inventory[i];
				if (item.type == ItemID.SandBlock)
                {
					if (item.stack > 0)
						item.stack = item.stack - 8; // can also do -= number
				}
			}
			return true;
		}
		
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.0f)];
			proj.penetrate = -1;
			proj.usesLocalNPCImmunity = true;
			proj.localNPCHitCooldown = -1; // 1 hit per npc max
			proj.localNPCHitCooldown = 20; // o
			return false;
		}*/
        /*
		 * Feel free to uncomment any of the examples below to see what they do
		 */

        // What if I wanted this gun to have a 38% chance not to consume ammo?
        /*public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .50f;
		}*/

        //What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
        // Uzi/Molten Fury style: Replace normal Bullets with Highvelocity
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.RocketI || type == ProjectileID.RocketII || type == ProjectileID.RocketIII || type == ProjectileID.RocketIV) // or ProjectileID.WoodenArrowFriendly
			{
				type = ModContent.ProjectileType<SandRocket>();
			}

			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/

        // What if I wanted it to shoot like a shotgun?
        // Shotgun style: Multiple Projectiles, Random spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				// If you want to randomize the speed to stagger the projectiles
				// float scale = 1f - (Main.rand.NextFloat() * .3f);
				// perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);

			}

			
			return false; // return false because we don't want tmodloader to shoot projectile

		}

		// What if I wanted an inaccurate gun? (Chain Gun)
		// Inaccurate Gun style: Single Projectile, Random spread 
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}*/

        // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);

		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?
		/*Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;*/

		// How can I get a "Clockwork Assault Rifle" effect?
		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
		/*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override bool ConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

		// How can I choose between several projectiles randomly?
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
	}
}
