using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class CarrotSword : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Carrotyx Claymore");
			Tooltip.SetDefault("Summons Carrots from the ground upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			item.damage = 75;
			item.crit = 0;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 1.3f;
			item.value = 220000;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ModContent.ItemType<Placeables.CarrotyxBar>(), 15); recipe.AddIngredient(ModContent.ItemType<Placeables.Carrot>(), 50); recipe.AddIngredient(ItemID.DirtBlock, 50);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			int numberProjectiles = 10; // shoots 6 projectiles
			for (int index = 0; index < numberProjectiles; ++index)
			{
				Vector2 vector2_1 = new Vector2((float)((double)target.position.X + ((double)target.width * 0.5) + (double)(Main.rand.Next(401) * (double)-target.direction)), (float)((double)target.position.Y + (double)target.height * 0.5 + 1000.0));   //this defines the projectile width, direction and position
				vector2_1.X = (float)(((double)vector2_1.X + (double)target.Center.X) / 2.0) + (float)Main.rand.Next(-600, 401);
				vector2_1.Y -= (float)(100 * index);
				float num12 = (float)target.position.X + Main.screenPosition.X - vector2_1.X;
				float num13 = (float)target.position.Y + Main.screenPosition.Y - vector2_1.Y;
				if ((double)num13 < 0.0) num13 *= -1f;
				if ((double)num13 < 20.0) num13 = 20f;
				float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
				float num15 = item.shootSpeed / num14;
				float num16 = num12 * num15;
				float num17 = num13 * num15;
				float SpeedX = num16 + (float)Main.rand.Next(-10, 11) * 0.02f; //change the Main.rand.Next here to, for example, (-10, 11) to reduce the spread. Change this to 0 to remove it altogether
				float SpeedY = num17 + (float)Main.rand.Next(-10, 11) * 0.02f;
				Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX / 2, -12, mod.ProjectileType("Carrot"), damage, knockBack, player.whoAmI, 0.0f, (float)Main.rand.Next(5));
			}
		}
    }
}