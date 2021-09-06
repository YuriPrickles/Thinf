using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class BatteryStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Zap Staff");
			Tooltip.SetDefault("Shoots rapid electric lasers");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.magic = true;
			item.mana = 20;
			item.width = 32;
			item.height = 32;
			item.useTime = 4;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item91;
			item.autoReuse = false;
            item.shoot = ProjectileID.SaucerLaser;
			item.shootSpeed = 32f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 speed = new Vector2(speedX, speedY);
			Projectile projectile = Projectile.NewProjectileDirect(position, speed, type, damage, knockBack, player.whoAmI);
			projectile.hostile = false;
			projectile.friendly = true;
            return false;
        }
    }
}