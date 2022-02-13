using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class AppleStick : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots Apple seeds");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 320;
			item.magic = true;
			item.mana = 11;
			item.width = 48;
			item.height = 48;
			item.useTime = 2;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = false; //so the item's animation doesn't do damage
			item.knockBack = 25;
			item.value = 10000;
			item.shoot = ProjectileType<AppleSeedFriendly>();
			item.shootSpeed = 9f;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item117;
			item.autoReuse = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-75, 75), Main.MouseWorld.Y + Main.screenHeight), new Vector2(0, -item.shootSpeed), type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-75, 75), Main.MouseWorld.Y + Main.screenHeight), new Vector2(0, -item.shootSpeed), type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-75, 75), Main.MouseWorld.Y + Main.screenHeight), new Vector2(0, -item.shootSpeed), type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-75, 75), Main.MouseWorld.Y + Main.screenHeight), new Vector2(0, -item.shootSpeed), type, damage, knockBack, player.whoAmI);
			return false;
        }
    }
}