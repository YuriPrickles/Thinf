using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;

namespace Thinf.Items.Weapons
{
	public class BadMoonRising : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("When you deal critical damage, that damage is shared to every enemy inside the circle\n'Oh no...'");

			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.crit = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 30;
			item.height = 26;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 2.5f;
			item.damage = 140;
			item.rare = ItemRarityID.Red;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(silver: 1);
			item.shoot = ModContent.ProjectileType<Projectiles.BadMoonRisingProj>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 12);
			recipe.AddIngredient(ItemID.Kraken);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}