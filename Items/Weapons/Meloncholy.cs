using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Meloncholy : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Meloncholy");
			Tooltip.SetDefault("Left-click to rain melons from the sky\nRight-click to summon weaker melons that surround you, which then launch towards enemies.\n A healthy way to ruin their mojos");
		}

		public override void SetDefaults()
		{
			item.damage = 170;
			item.crit = (int)0.11f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 11;
			item.value = 20000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = mod.ProjectileType("Melon");
			item.shootSpeed = 10f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.StarWrath);recipe.AddIngredient(ItemID.Starfury);recipe.AddIngredient(mod.ItemType("CosmicHerbalPiece"), 10);recipe.AddIngredient(ItemID.ChlorophyteBar, 20);recipe.AddTile(TileID.LunarCraftingStation);recipe.SetResult(this); recipe.AddRecipe();
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (player.altFunctionUse != 2)
			{
				int numberProjectiles = 3; // shoots 6 projectiles
				for (int index = 0; index < numberProjectiles; ++index)
				{
					Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
					vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-170, 171);
					vector2_1.Y -= (float)(100 * index);
					float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
					float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
					if ((double)num13 < 0.0) num13 *= -1f;
					if ((double)num13 < 20.0) num13 = 20f;
					float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
					float num15 = item.shootSpeed / num14;
					float num16 = num12 * num15;
					float num17 = num13 * num15;
					float SpeedX = num16 + (float)Main.rand.Next(-10, 11) * 0.02f; //change the Main.rand.Next here to, for example, (-10, 11) to reduce the spread. Change this to 0 to remove it altogether
					float SpeedY = num17 + (float)Main.rand.Next(-10, 11) * 0.02f;
					Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX/2, SpeedY/2, mod.ProjectileType("Melon"), damage, knockBack, player.whoAmI, 0.0f, (float)Main.rand.Next(5));
				}
			}

			if (player.altFunctionUse == 2)
            {
				Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("MelonTwo"), item.damage/2, 10f, player.whoAmI);
			}
			return false;
		}

		

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe;recipe.AddIngredient(ItemID.BreakerBlade);
			recipe;recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe;recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe;recipe.AddIngredient(ItemID.PearlwoodSword, 5);
			recipe;recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.SetResult(this); recipe.AddRecipe();
		}*/

		/*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
	}
}