using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class ShrimpLaser : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A rapid fire laser gun with splash damage");
		}

		public override void SetDefaults()
		{
			item.damage = 8;
			item.crit = 2;
			item.ranged= true;
			item.width = 32;
			item.height = 24;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			if (!Main.dedServ)
			{
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/StupidShrimp");
			}
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 22f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, -1);
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = ModContent.ProjectileType<ShrimpyShot>();
            return true;
        }
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			//Player player = Main.player[item.owner];
			//if (drawText)
			//{
			//	if (!player.GetModPlayer<MyPlayer>().ironMode)
			//	{
			//		spriteBatch.DrawString(Main.fontItemStack, $"Prawn Power in: {player.GetModPlayer<MyPlayer>().furnaceCharge} / 5000 damage", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Orange);
			//	}
			//	else
			//	{
			//		spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.DarkRed * 0.25f);
			//		spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Red * 0.5f);
			//		spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25 + Main.rand.Next(-10, 10), 50 + Main.rand.Next(-10, 10)), Color.Orange * 0.6f);
			//		spriteBatch.DrawString(Main.fontItemStack, $"FURNACE CHARGE ACTIVE! ({player.GetModPlayer<MyPlayer>().furnaceCharge})", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Yellow);
			//	}
			//}
		}
	}
}
