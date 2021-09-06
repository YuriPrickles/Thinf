using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	public class BloodCrawlerGear : ModItem
	{
		int bloodShotTimer = 0;
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Gives the ability to climb walls\nShoots bursts of crimson energy in random directions behind the player while sliding/grabbing onto walls");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.spikedBoots += 2;
			if (player.sliding)
			{
				bloodShotTimer++;
				if (bloodShotTimer == 10)
				{
					Projectile projectile = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileID.RubyBolt, 22, 0, player.whoAmI)];
					projectile.magic = false;
					projectile.velocity = projectile.DirectionTo(Main.MouseWorld.RotatedByRandom(MathHelper.ToRadians(360))) * 9f;
					projectile.velocity *= player.direction;
					projectile.tileCollide = false;
					bloodShotTimer = 0;
				}
			}
		}
	}
}