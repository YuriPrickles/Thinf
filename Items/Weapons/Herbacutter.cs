using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Herbacutter : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Sprays leaves at a short range");
		}

		public override void SetDefaults()
		{
			item.damage = 210;
			item.crit = 2;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 1;
			item.knockBack = 19;
			item.value = 20000;
			item.shoot = ProjectileID.Leaf;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 17f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CosmicHerbalPiece>(), 12);
			recipe.AddIngredient(ItemID.LeafBlower);
			recipe.AddIngredient(ItemID.WoodenSword);
			recipe.AddIngredient(ItemID.BorealWoodSword);
			recipe.AddIngredient(ItemID.PalmWoodSword);
			recipe.AddIngredient(ItemID.RichMahoganySword);
			recipe.AddTile(ModContent.TileType<Blocks.StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 6 + Main.rand.Next(4);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed *= scale;
				Projectile projectile = Main.projectile[Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockBack, player.whoAmI)];
				projectile.melee = true;
				projectile.magic = false;
				projectile.timeLeft = 30;
			}
			return false;
		}
	}
}