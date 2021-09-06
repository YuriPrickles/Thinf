using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace Thinf.Items.Weapons
{
	public class SoulMaker : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Creates Souls at your cursor that home into enemies\n'Mother, whither doth little souls cometh from?'");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 500;
			item.magic = true;
			item.mana = 30;
			item.width = 64;
			item.height = 64;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 120000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item91;
			item.autoReuse = true;
            item.shoot = ProjectileID.SpectreWrath;
			item.shootSpeed = 10f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			int projectileSpawnAmount = 7;
			for (int i = 0; i < projectileSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
				Vector2 projectileVelocity = currentRotation.ToRotationVector2();
				damage = item.damage;  //projectile damage
				type = ProjectileID.LostSoulFriendly;  //put your projectile
				Main.PlaySound(98, (int)player.position.X, (int)player.position.Y, 17);
				Projectile projectile = Main.projectile[Projectile.NewProjectile(Main.MouseWorld, projectileVelocity * 7, type, damage, 0, player.whoAmI)];
				projectile.magic = true;
				projectile.tileCollide = false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Linimisifrififlium>(), 15);
			recipe.AddIngredient(ItemID.Ectoplasm, 15);
			recipe.AddIngredient(ItemID.SpectreBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}