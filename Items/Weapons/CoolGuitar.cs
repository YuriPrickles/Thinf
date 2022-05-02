using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Thinf.Items.Weapons
{
	public class CoolGuitar : ModItem
	{
		bool drawText = false;

		public override void SetStaticDefaults()
		{
			Item.staff[item.type] = true;
			Tooltip.SetDefault("Blast your enemies with the power of music!\nDeal enough damage to activate Super Ultra Guitar Mode, giving more beams, less mana use, faster turning, and less immunity frames!\nMake sure to re-use the Guitar once Super Ultra Guitar Mode is active to get the effects!!");
		}

		public override void SetDefaults()
		{
			// Start by using CloneDefaults to clone all the basic item properties from the vanilla Last Prism.
			// For example, this copies sprite size, use style, sell price, and the item being a magic weapon.
			item.CloneDefaults(ItemID.LastPrism);
			item.mana = 15;
			item.damage = 140;
			item.shoot = ModContent.ProjectileType<CoolGuitarHeldProj>();
			item.shootSpeed = 30f;
			item.useStyle = ItemHoldStyleID.HarpHoldingOut;
		}

		// Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<CoolGuitarHeldProj>()] <= 0;
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[item.owner];
			if (drawText)
			{
				if (!player.GetModPlayer<MyPlayer>().partyTime)
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Super Ultra Guitar Mode in: {player.GetModPlayer<MyPlayer>().partyCharge} / 7500 damage", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.LightBlue);
				}
				else
				{
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Purple * 0.25f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Indigo * 0.3f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Blue * 0.5f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-5, 5), 50 + Main.rand.Next(-10, 10)), Color.Green * 0.55f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-5, 5), 50 + Main.rand.Next(-10, 10)), Color.Yellow * 0.6f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-5, 5), 50 + Main.rand.Next(-10, 10)), Color.Orange * 0.7f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-5, 5), 50 + Main.rand.Next(-10, 10)), Color.Red * 0.8f);
					spriteBatch.DrawString(Main.fontItemStack, $"SUPER ULTRA GUITAR MODE ACTIVE! ({player.GetModPlayer<MyPlayer>().partyCharge})", player.Center - Main.screenPosition + new Vector2(-25, 50), Main.DiscoColor);
				}
			}
		}
		public override void HoldItem(Player player)
		{
			drawText = true;
		}
		public override void UpdateInventory(Player player)
		{
			drawText = false;
		}
	}
}