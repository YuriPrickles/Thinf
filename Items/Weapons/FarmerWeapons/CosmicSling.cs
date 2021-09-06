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
	public class CosmicSling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Slingshot");
			Tooltip.SetDefault("Shoots seeds that shoot lasers at enemies it hits");
		}
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
			item.damage = 120;
			item.UseSound = SoundID.Item97;
			item.shoot = ProjectileType<CosmoSeed>();
			item.noMelee = true;
			item.shootSpeed = 12f;
			item.useTime = 4;
			item.useAnimation = 12;
			item.reuseDelay = 9;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 1;
			item.consumable = false;
			item.useAmmo = ItemID.Seed;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = ProjectileType<CosmoSeed>();

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
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, -2);
        }
        // As a modder, you could also opt to make these overrides also sealed. Up to the modder
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
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
		public override float UseTimeMultiplier(Player player)
		{
			return ModPlayer(player).farmerSpeed;
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
			recipe.AddIngredient(ItemType<CosmicHerbalPiece>(), 14);
			recipe.AddIngredient(ItemID.LunarBar, 15);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}