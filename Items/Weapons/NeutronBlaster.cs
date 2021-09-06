using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Items.Weapons
{
	public class NeutronBlaster : ModItem
	{
		int overheat;
		int shoot = 1;
        int cooldown = 0;
        int SOCK = 0;

        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A powerful cannon that overheats if used too much\nOverheat Time: 30 shots\nCooldown: 12 seconds\nIf used during Overheat you will take damage\nManual Cooldown rate: 10%");
		}

		public override void SetDefaults()
		{
			item.damage = 90;
			item.crit = (int)2f;
			item.ranged= true;
			item.width = 64;
			item.height = 16;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item75;
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 12f;
			item.useAmmo = AmmoID.Bullet;
		}


		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!player.HasBuff(ModContent.BuffType<Overheat>()))
				overheat++;

			if (overheat >= 30)
			{
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.OrangeRed, "Overheat!", true);
				player.AddBuff(ModContent.BuffType<Overheat>(), 720);
				overheat = 0;
			}

			if (player.HasBuff(ModContent.BuffType<Overheat>()))
			{
				player.AddBuff(BuffID.Burning, 10);
				return false;
			}

			if (overheat == 15)
			{
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Yellow, "Weapon is 50% Overheated!", true);
			}

			if (overheat == 22)
			{
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Orange, "Weapon is 75% Overheated!", true);
			}

			cooldown = 0;

			return true;  // code by pollen__#0004
		}

		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(10) == 0)
			{
				if (player.itemAnimation == 0 && overheat < 60 && overheat >= 0)
				{
					overheat--;

					if (cooldown == 0)
					{
						CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Teal, "Cooling Down...", true);
						cooldown = 1;
						SOCK = 1;
					}
				}
			}

			if (overheat == 0 && !player.HasBuff(ModContent.BuffType<Overheat>()) && SOCK == 1)
			{
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.LightBlue, "Weapon is 0% Overheated!", true);
				SOCK = 0;
			}
			return;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);

		}
	}
}