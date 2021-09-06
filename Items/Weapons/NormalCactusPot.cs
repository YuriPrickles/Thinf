using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{

	public class NormalCactusPot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Normal Cactus Pot");
			Tooltip.SetDefault("Summons a Flying Cactus to fight for you \n It's totally fine, no anomalies in this pot.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.knockBack = 1f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.buffType = ModContent.BuffType<FlyingCactus>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			item.shoot = ModContent.ProjectileType<FlyingCactusMinion>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}
	}

	}