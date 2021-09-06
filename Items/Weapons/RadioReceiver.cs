using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class RadioReceiver : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Call an airstrike at the mouse position");
		}

		public override void SetDefaults()
		{
			item.damage = 84;
			item.crit = 0;
			item.ranged = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 90;
			item.useAnimation = 90;
			item.useStyle = 1;
			item.knockBack = 1.3f;
			item.value = 220000;
			item.shoot = ProjectileID.AmethystBolt;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position = new Vector2(Main.MouseWorld.X, player.Center.Y - 400);
			speedX = 0;
			speedY = 8;
			type = ModContent.ProjectileType<AirstrikeMarkerButGood>();
            return true;
        }
    }
}