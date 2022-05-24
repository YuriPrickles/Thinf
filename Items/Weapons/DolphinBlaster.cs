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
	public class DolphinBlaster : ModItem
	{
		bool drawText = false;
		int reloadTime = 100;
		int ammo = 6;
		bool reloading = false;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A powerful shotgun that shoots fish guts\nDamage gets lowered the farther the guts travel\nDeals extra muzzle damage at point blank range");
		}

		public override void SetDefaults()
		{
			item.damage = 50;
			item.crit = 0;
			item.ranged= true;
			item.width = 74;
			item.height = 42;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			if (!Main.dedServ)
			{
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/DolphinBlaster").WithVolume(0.66f);
			}
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<FishGuts>(); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 15f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}

        public override bool CanUseItem(Player player)
        {
			if (reloading)
			{
				return false;
			}
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = ModContent.ProjectileType<FishGuts>();
			if (reloading)
            {
				return false;
            }
			if (ammo > 0 && !reloading)
            {
				ammo--;
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC target = Main.npc[i];
				if (target.active && target.Distance(player.Center) <= 80)
                {
					if (player.direction == -1 && target.Center.X <= player.Center.X)
					{
						target.StrikeNPC(item.damage / 4, 1, 0);
					}
					if (player.direction == 1 && target.Center.X >= player.Center.X)
					{
						target.StrikeNPC(item.damage / 4, 1, 0);
					}
				}
            }
            return true;
        }
        public override void HoldItem(Player player)
		{
			drawText = true;
		}
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[item.owner];
			if (drawText)
			{
				if (ammo <= 0)
                {
					reloading = true;
                }
				if (reloading)
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Reloading ({reloadTime})", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Blue);
					reloadTime--;
					if (reloadTime == 99)
					{
						if (!Main.dedServ)
						{
							Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/DolphinBlaster_Reload"), player.Center);
						}
					}
					if (reloadTime <= 0)
                    {
						ammo = 6;
						reloadTime = 100;
						reloading = false;
                    }
				}
				else
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Fish Guts: {ammo}", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.Blue);
				}
			}
		}
		public override void UpdateInventory(Player player)
		{
			drawText = false;
		}
	}
}
