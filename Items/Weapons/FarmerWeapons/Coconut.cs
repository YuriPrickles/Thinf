using Terraria.ModLoader;
using Terraria.ID;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Thinf.Blocks;
using Terraria;
using static Thinf.FarmerClass;
using System.Collections.Generic;
using System.Linq;

namespace Thinf.Items.Weapons.FarmerWeapons
{
	public class Coconut : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A strong throwable weapon\nAlso tastes good!");
		}
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
			item.damage = 34;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<CoconutProj>();
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shootSpeed = 9f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.consumable = true;
		}

		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			// all vanilla damage types must be false for custom damage types to work
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}

		// As a modder, you could also opt to make these overrides also sealed. Up to the modder
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			add += ModPlayer(player).farmerDamageAdd;
			mult *= ModPlayer(player).farmerDamageMult;
		}
		public override float UseTimeMultiplier(Player player)
		{
			return ModPlayer(player).farmerSpeed;
		}
		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			// Adds knockback bonuses
			knockback += ModPlayer(player).farmerKnockback;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			// Adds crit bonuses
			crit += ModPlayer(player).farmerCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
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