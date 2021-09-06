using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class LesserReverseHealer : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("It heals in reverse\nHeals -25 HP\nEnemies will not die from this weapon\nMake sure you have a separate weapon to kill them");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 1;
			item.crit = 0;
			item.magic = true;
			item.mana = 11;
			item.width = 32;
			item.height = 32;
			item.useTime = 9;
			item.useAnimation = 9;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 39000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("LesserReverseHeart") ;
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
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.LesserHealingPotion, 50);recipe.AddIngredient(ItemID.Bone, 25);recipe.AddIngredient(ItemID.VileMushroom, 3);recipe.AddTile(TileID.Heart); recipe.SetResult(this); recipe.SetResult(this); recipe.AddRecipe();
			recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.LesserHealingPotion, 50);recipe.AddIngredient(ItemID.Bone, 25);recipe.AddIngredient(ItemID.ViciousMushroom, 3);recipe.AddTile(TileID.Heart); recipe.SetResult(this); recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}