using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class GreaterReverseHealer : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("It heals in reverse\nHeals -75 HP\nEnemies will not die from this weapon\nMake sure you have a separate weapon to kill them");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 1;
			item.crit = 0;
			item.magic = true;
			item.mana = 18;
			item.width = 32;
			item.height = 32;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 100000;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileType<GreaterReverseHeart>() ;
			item.shootSpeed = 28f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.manaSick)
			{
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.GreaterHealingPotion, 150);recipe.AddIngredient(ItemID.ChlorophyteBar, 12);recipe.AddIngredient(ItemID.PurpleSolution, 500);recipe.AddIngredient(ItemID.LifeCrystal, 5);recipe.AddIngredient(ItemID.LifeFruit, 3);recipe.AddTile(TileID.LifeFruit); recipe.SetResult(this); recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.GreaterHealingPotion, 150);recipe.AddIngredient(ItemID.ChlorophyteBar, 12);recipe.AddIngredient(ItemID.RedSolution, 500);recipe.AddIngredient(ItemID.LifeCrystal, 5);recipe.AddIngredient(ItemID.LifeFruit, 3);recipe.AddTile(TileID.LifeFruit); recipe.SetResult(this); recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}