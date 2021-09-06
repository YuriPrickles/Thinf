using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{

	public class SnowpoffStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snowpoff Staff");
			Tooltip.SetDefault("Summons a Snowpoff to fight for you\nBehold! Another Snowpoff.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.knockBack = 2f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.buffType = mod.BuffType("Snowpoff");
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			item.shoot = mod.ProjectileType("Snowpoff");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Snowball, 400);recipe.AddIngredient(ItemID.BorealWood, 300);recipe.AddIngredient(ItemID.GoldCoin, 20);recipe.AddTile(TileID.WorkBenches);recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}