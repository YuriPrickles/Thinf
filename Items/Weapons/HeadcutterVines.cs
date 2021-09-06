using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class HeadcutterVines : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Headcutter Vines");
			Tooltip.SetDefault("Shoot out deadly Man Eaters");
		}

		public override void SetDefaults()
		{
			item.damage = 101;
			item.crit = (int)0.15f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 12;
			item.useAnimation = 12;
			item.knockBack = 0;
			item.value = 20000;
			item.rare = ItemRarityID.LightPurple;
			item.noMelee = true; // Makes sure that the animation when using the item doesn't hurt NPCs.
			item.useStyle = ItemUseStyleID.HoldingOut; // Set the correct useStyle.
			item.scale = 1.5F;
			item.noUseGraphic = true; // Do not use the item graphic when using the item (we just want the ball to spawn).
			item.shoot = ModContent.ProjectileType<HeadcutterFlailHead>();
			item.shootSpeed = 15.1F;
			item.UseSound = SoundID.Item1;
			item.melee = true; // Deals melee damage.
			item.autoReuse = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.ChainGuillotines);recipe.AddIngredient(ItemID.FeralClaws);recipe.AddIngredient(ItemID.JungleSpores, 25);recipe.AddIngredient(ItemID.Vine, 5);recipe.AddIngredient(ItemID.ChlorophyteBar, 20);recipe.AddIngredient(ItemID.VialofVenom, 10);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.Venom, 600);
		}
	}
}