using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{

	public class DetonatoStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a Detonato sentry that explodes\nCooldown: 1 min 30 sec\nUse it wisely!");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 700;
			item.knockBack = 0f;
			item.mana = 50;
			item.width = 32;
			item.height = 32;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;
			item.noMelee = true;
			item.summon = true;
			item.sentry = true;
			item.shoot = mod.ProjectileType("PotatoMine");
			item.autoReuse = false;
		}

        public override bool CanUseItem(Player player)
        {
			return !player.HasBuff(ModContent.BuffType<PotatoMineRecharge>());
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 SPos = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);   //this make so the projectile will spawn at the mouse cursor position
			position = SPos;

			player.AddBuff(ModContent.BuffType<PotatoMineRecharge>(), 5400);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ModContent.ItemType<Placeables.PotatiumiteBar>(), 15); recipe.AddIngredient(ModContent.ItemType<Placeables.Potato>(), 50); recipe.AddIngredient(ItemID.DirtBlock, 50);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}