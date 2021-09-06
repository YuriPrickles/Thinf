using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class FrogStaff : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Lick the enemy");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.crit = 2;
			item.magic = true;
			item.mana = 9;
			item.width = 38;
			item.height = 38;
			item.useTime = 12;
			item.useAnimation = 12;
			item.knockBack = 0;
			item.value = 20000;
			item.rare = ItemRarityID.Green;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noUseGraphic = false;
			item.shoot = ModContent.ProjectileType<Tongue>();
			item.shootSpeed = 18f;
			item.UseSound = SoundID.Item1;
			item.melee = true; // Deals melee damage.
			item.autoReuse = true;
		}

        public override bool CanUseItem(Player player)
        {
			if (player.ownedProjectileCounts[item.shoot] != 0)
            {
				return false;
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Frog);
			recipe.AddIngredient(ItemID.RichMahogany, 50);
			recipe.AddIngredient(ItemID.JungleSpores, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 60);
		}
	}
}