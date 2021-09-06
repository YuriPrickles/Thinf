using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace Thinf.Items.Weapons
{
	public class Nebulily : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots lots of petals");
		}

		public override void SetDefaults()
		{
			item.damage = 350;
			item.magic = true;
			item.mana = 8;
			item.width = 44;
			item.height = 32;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 120000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("NebulaPetal");
			item.shootSpeed = 10f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			int projectileSpawnAmount = 8;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2();
				damage = item.damage;  //projectile damage
				type = mod.ProjectileType("NebulaPetal");  //put your projectile
				Main.PlaySound(98, (int)player.position.X, (int)player.position.Y, 17);
				Projectile projectile = Main.projectile[Projectile.NewProjectile(Main.MouseWorld, projectileVelocity, type, damage, 0, player.whoAmI)];
				projectile.magic = true;
				projectile.tileCollide = false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.FlowerBoots);recipe.AddIngredient(ItemID.NebulaArcanum);recipe.AddIngredient(ItemID.LunarFlareBook);recipe.AddIngredient(mod.ItemType("CosmicHerbalPiece"), 10);recipe.AddIngredient(ItemID.ChlorophyteBar, 20);recipe.AddTile(TileID.LunarCraftingStation);recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}