using Terraria.ModLoader;
using Terraria.ID;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Thinf.Blocks;
using Terraria;
using static Thinf.FarmerClass;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Thinf.Items.Weapons.FarmerWeapons
{
	public class DaybloomSling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Daybloom Slingshot");
			Tooltip.SetDefault("Shoots seeds with the power of day");
		}
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
			item.damage = 6;
			item.UseSound = SoundID.Item97;
			item.shoot = ProjectileID.Seed;
			item.noMelee = true;
			item.shootSpeed = 14f;
			item.useTime = 5;
			item.useAnimation = 15;
			item.reuseDelay = 15;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 1;
			item.consumable = false;
			item.useAmmo = ItemID.Seed;
		}
		public override float UseTimeMultiplier(Player player)
		{
			return ModPlayer(player).farmerSpeed;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, -2);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile projectile = Main.projectile[item.shoot];
			if (Main.dayTime)
			{
				projectile.scale = 4f;
			}
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
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
			if (Main.dayTime)
            {
				mult *= 1.5f;
            }
			add += ModPlayer(player).farmerDamageAdd;
			mult *= ModPlayer(player).farmerDamageMult;
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
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Daybloom, 10);
			recipe.AddIngredient(ItemID.DirtBlock, 50);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}