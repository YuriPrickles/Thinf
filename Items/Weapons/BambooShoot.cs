using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
	public class BambooShoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a very fast bullet");
		}

		public override void SetDefaults()
		{
			item.damage = 54;
			item.crit = (int)0f;
			item.ranged= true;
			item.width = 64;
			item.height = 28;
			item.useTime = 58;
			item.useAnimation = 58;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 32f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 120);
			recipe.AddIngredient(ItemID.FlintlockPistol);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
