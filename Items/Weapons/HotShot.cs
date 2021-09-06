using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class HotShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoot out flaming hot corn that bursts into flames upon contact");
		}

		public override void SetDefaults()
		{
			item.damage = 7;
			item.crit = 8;
			item.ranged= true;
			item.width = 48;
			item.height = 28;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 15f;
			item.channel = true;
			item.useAmmo = AmmoID.Bullet;

		}
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 20);
			recipe.AddIngredient(ItemID.Torch, 10);
			recipe.AddIngredient(ItemID.Furnace);
			recipe.AddIngredient(ModContent.ItemType<Corn>(), 5);
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = ModContent.ProjectileType<HotShotProj>();
            return true;
        }
    }
}
