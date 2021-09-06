using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.FarmerClass;

namespace Thinf.Items.Placeables
{
	public class Herbs : GlobalItem
	{
		public virtual void SafeSetDefaults(Item item)
		{
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf|| item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed)
			{
				item.damage = 9;
				item.UseSound = SoundID.Item1;
				item.useTime = 14;
				item.useAnimation = 14;
				item.autoReuse = true;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.width = 32;
				item.height = 32;
				item.rare = ItemRarityID.Green;
				item.consumable = true;
				item.maxStack = 999;
				item.useTurn = true;
				item.scale = 2;
			}
		}
		public sealed override void SetDefaults(Item item)
		{
			if (item.type == ItemID.Seed)
			{
				item.noMelee = true;
				item.autoReuse = true;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.consumable = true;
				item.useTime = 5;
				item.useAnimation = 5;
				item.damage = 4;
				item.shoot = ProjectileID.Seed;
				item.shootSpeed = 12f;
				item.UseSound = SoundID.Item1;
			}
			
			// all vanilla damage types must be false for custom damage types to work
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed)
			{
				SafeSetDefaults(item);
				item.melee = false;
				item.ranged = false;
				item.magic = false;
				item.thrown = false;
				item.summon = false;
			}
		}
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed)
            {
				item.stack -= 1;
            }
			if (item.type == ItemID.Shiverthorn && player.ZoneSnow)
			{
				target.AddBuff(BuffID.Frostburn, 300);
			}

			if (item.type == ItemID.Fireblossom && (player.ZoneUnderworldHeight))
			{
				target.AddBuff(BuffID.OnFire, 300);
			}
		}
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
		{
			if (item.type == ItemID.Daybloom && Main.dayTime)
            {
				mult *= 1.3f;
            }

			if (item.type == ItemID.Moonglow && !Main.dayTime)
			{
				mult *= 1.3f;
			}

			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed || item.type == ItemID.Seed)
			{
				add += ModPlayer(player).farmerDamageAdd;
				mult *= ModPlayer(player).farmerDamageMult;
			}
		}

        public override float UseTimeMultiplier(Item item, Player player)
        {
			if (item.type == ItemID.Daybloom && Main.dayTime)
			{
				return 1.4f + ModPlayer(player).farmerSpeed;
			}
			if (item.type == ItemID.Moonglow && !Main.dayTime)
			{
				return 1.4f + ModPlayer(player).farmerSpeed;
			}
			return ModPlayer(player).farmerSpeed;
        }
        public override void GetWeaponKnockback(Item item, Player player, ref float knockback)
		{
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed || item.type == ItemID.Seed)
			{
				// Adds knockback bonuses
				knockback += ModPlayer(player).farmerKnockback;
			}
		}

		public override void GetWeaponCrit(Item item, Player player, ref int crit)
		{
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed || item.type == ItemID.Seed)
			{
				// Adds crit bonuses
				crit += ModPlayer(player).farmerCrit;
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.type == ItemID.Daybloom || item.type == ItemID.Blinkroot || item.type == ItemID.Shiverthorn || item.type == ItemID.Moonglow || item.type == ItemID.Waterleaf || item.type == ItemID.Fireblossom || item.type == ItemID.Deathweed || item.type == ItemID.Seed)
			{
				// Get the vanilla damage tooltip
				TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
				if (tt != null)
				{
					// We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
					// So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
					string[] splitText = tt.text.Split(' ');
					string damageValue = splitText.First();
					string damageWord = splitText.Last();
					// Change the tooltip text
					tt.text = damageValue + " plant " + damageWord;
				}
			}
		}
	}
}