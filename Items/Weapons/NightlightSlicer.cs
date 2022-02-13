using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class NightlightSlicer : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Nightlight Slicer");
			Tooltip.SetDefault("Release little bugs after slicing enemies");
		}

		public override void SetDefaults()
		{
			item.damage = 64;
			item.crit = 6;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1;
			item.value = 30000;
			item.rare = ItemRarityID.LightPurple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			
			return true;
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			int bugchance;
			int type = mod.ProjectileType("LightBug"); ;

			bugchance = Main.rand.Next(2);

			if (bugchance == 0)
			{
				type = mod.ProjectileType("LightBug");
				item.shootSpeed = 10f;
			}

			if (bugchance == 1)
			{
				type = mod.ProjectileType("NightBug");
				item.shootSpeed = 10f;
			}
			Projectile.NewProjectile(target.position.X, target.position.Y, 3, 3, type, item.damage, 2f, player.whoAmI);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.Excalibur);recipe.AddIngredient(ItemID.NightsEdge);recipe.AddIngredient(mod.ItemType("FragmentOfLight"), 15);recipe.AddIngredient(mod.ItemType("FragmentOfNight"), 15);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}
    }
}