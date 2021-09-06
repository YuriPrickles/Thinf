using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class SoulKeychain : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots magical bolts\nThe light bolt deals massive damage and teleports enemies randomly\nThe night bolt removes a small amout of defense while increasing damage and speed\nThe flight bolt send enemies up in the air");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.LaserRifle);
			item.damage = 50;
			item.magic = true;
			item.mana = 15;
			item.width = 32;
			item.height = 32;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.noMelee = false; //so the item's animation doesn't do damage
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.LightPurple;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType ("LightBolt");
			item.shootSpeed = 9f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (type == mod.ProjectileType("LightBolt"))
            {
				damage *= 3;
            }
			int boltrand;

			boltrand = Main.rand.Next(3);

			if (boltrand == 0)
			{
				item.shoot = mod.ProjectileType("LightBolt");
			}

			if (boltrand == 1)
			{
				item.shoot = mod.ProjectileType("NightBolt");
			}

			if (boltrand == 2)
			{
				item.shoot = mod.ProjectileType("FlightBolt");
			}
			return true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.RainbowRod);recipe.AddIngredient(mod.ItemType("FragmentOfLight"), 10);recipe.AddIngredient(mod.ItemType("FragmentOfNight"), 10);recipe.AddIngredient(mod.ItemType("FragmentOfFlight"), 10);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}