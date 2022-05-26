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
	public class HolySling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Slingshot");
			Tooltip.SetDefault("Something something david and goliath\nShoots more seeds in the hallow");
		}
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
			item.damage = 15;
			item.UseSound = SoundID.Item97;
			item.shoot = ProjectileID.Seed;
			item.noMelee = true;
			item.shootSpeed = 12f;
			item.useTime = 2;
			item.useAnimation = 10;
			item.reuseDelay = 18;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Yellow;
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
			if (player.ZoneHoly)
			{

			}
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
        public override bool UseItem(Player player)
		{
			if (player.ZoneHoly)
			{
				item.useTime = 2;
				item.useAnimation = 20;
				item.reuseDelay = 21;
			}
			else
            {
				item.useTime = 2;
				item.useAnimation = 10;
				item.reuseDelay = 18;
			}
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
			recipe.AddIngredient(ItemID.HallowedBar,25);
			recipe.AddIngredient(ItemID.SoulofFright, 20);
			recipe.AddIngredient(ItemID.Silk, 12);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}