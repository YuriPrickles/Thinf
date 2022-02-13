using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class StaffOfTheRedPainter : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots bolts of ketchup\nKetchup 6:17-18 'The Red Painter will grab his staff, and create a new world.'\n'Ketchup swirls from the crystal, covering any surface the Red Painter wants to.'");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 65;
			item.magic = true;
			item.mana = 9;
			item.width = 34;
			item.height = 34;
			item.useTime = 5;
			item.useAnimation = 15;
			item.reuseDelay = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item67;
			item.autoReuse = true;
            item.shoot = ProjectileType<KetchupBolt>();
			item.shootSpeed = 12f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 speed = new Vector2(speedX, speedY);
			Projectile.NewProjectileDirect(position, speed.RotatedByRandom(MathHelper.ToRadians(120)), type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}