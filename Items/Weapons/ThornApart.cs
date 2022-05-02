using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class ThornApart : ModItem
	{
		int chargeTimer = 0;
		bool drawText = false;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A spinning weapon that spins\nHit enemies to charge up Thorn Orbs\nRight-Click for a devastating orb based on your charge!");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 500;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = Item.buyPrice(3, 78, 50, 0);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.channel = true;
			item.noMelee = true;
			item.shootSpeed = 25f;
			item.shoot = ModContent.ProjectileType<Projectiles.ThornApartProj>();
			item.noUseGraphic = true;
			item.noMelee = true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			Player player = Main.player[item.owner];
			if (drawText)
			{
				spriteBatch.DrawString(Main.fontItemStack, $"Next Orb in: {player.GetModPlayer<MyPlayer>().thornApartCharge} / 100 hits", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.MistyRose);
				spriteBatch.DrawString(Main.fontItemStack, $"Orbs: {player.GetModPlayer<MyPlayer>().thornApartShots} / 5", player.Center - Main.screenPosition + new Vector2(-25, 70), Color.MistyRose);
			}

		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2 && player.GetModPlayer<MyPlayer>().thornApartShots > 0)
			{
				for (int i = 0; i < player.GetModPlayer<MyPlayer>().thornApartShots; i++)
				{
					Vector2 speed = new Vector2(4, 4);
					Projectile.NewProjectileDirect(player.Center, speed.RotatedByRandom(MathHelper.ToRadians(120)), ModContent.ProjectileType<ThornOrb>(), item.damage * 10, item.knockBack, player.whoAmI);
				}
				player.GetModPlayer<MyPlayer>().thornApartShots = 0;

			}
			return true;
        }
        public override void HoldItem(Player player)
        {
			drawText = true;
		}
        public override void UpdateInventory(Player player)
        {
			drawText = false;
        }
        public override bool UseItem(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}