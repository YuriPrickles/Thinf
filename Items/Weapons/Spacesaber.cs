using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Spacesaber : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Left-click to deflect projectiles\nThat is, if you time it correctly");
		}

		public override void SetDefaults()
		{
			item.damage = 90;
			item.crit = (int)0.11f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 20000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.useTurn = true;
			item.scale = 1.75f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.BluePhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.RedPhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.YellowPhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.GreenPhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.WhitePhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 15);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.PurplePhasesaber);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool UseItem(Player player)
        {
			if (player.altFunctionUse != 2)
			{
				item.autoReuse = false;
				item.useAnimation = 7;
				item.damage = 10;
				item.useTime = 7;
            }
			if (player.altFunctionUse == 2)
			{
				item.autoReuse = true;
				item.useAnimation = 12;
				item.useTime = 12;
				item.damage = 90;
			}
			for (int k = 0; k < Main.maxProjectiles; ++k)
            {
				var fatHitbox = player.Hitbox;
				fatHitbox.Inflate(80, 80);
				var projectile = Main.projectile[k];
				if (projectile.active && player.altFunctionUse != 2 && projectile.hostile && projectile.Hitbox.Intersects(fatHitbox))
				{
					projectile.velocity *= -1;
					projectile.hostile = false;
					projectile.friendly = true;
				}
            }
			return true;
        }
    }
}