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
	public class MechBreaker : ModItem
	{
		bool drawText = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Molten Madness");
			Tooltip.SetDefault("Fires molten iron in bursts of 5\nDeal enough damage to fill up the Furnace Meter\nWhen filled, you get a buff that shoots the weapon faster and in bursts of 7.");
		}

		public override void SetDefaults()
		{
			item.damage = 267;
			item.ranged = true;
			item.width = 58;
			item.height = 26;
			item.useTime = 4;
			item.useAnimation = 20;
			item.reuseDelay = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.value = Item.buyPrice(3, 78, 50, 0);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
			item.shoot = ModContent.ProjectileType<SlagShot>();
			item.noMelee = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<SlagShot>(), damage, knockBack, player.whoAmI);
            return false;
        }
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
			weapon.shoot = ModContent.ProjectileType<SlagShot>();
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
				if (!player.GetModPlayer<MyPlayer>().ironMode)
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Furnace Charge in: {player.GetModPlayer<MyPlayer>().furnaceCharge} / 5000 damage", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Orange);
				}
				else
				{
					spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.DarkRed * 0.25f);
					spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Red * 0.5f);
					spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Orange * 0.6f);
					spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Yellow);
				}
			}
		}
        public override void HoldItem(Player player)
		{
			if (!player.GetModPlayer<MyPlayer>().ironMode)
			{
				item.useTime = 4;
				item.useAnimation = 20;
				item.reuseDelay = 20;
				item.shootSpeed = 10f;
			}
			else
			{
				item.useTime = 3;
				item.useAnimation = 21;
				item.reuseDelay = 12;
				item.shootSpeed = 16f;
			}
			drawText = true;
		}
		public override void UpdateInventory(Player player)
		{
			drawText = false;
		}
		public override bool UseItem(Player player)
		{
			Main.PlaySound(item.UseSound);
			return true;
        }
	}
}