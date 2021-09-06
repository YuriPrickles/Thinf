using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class CrystalGoliath : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Crystal Goliath");
			Tooltip.SetDefault("It's big\nIt's really big");
		}

		public override void SetDefaults()
		{
			item.damage = 56;
			item.crit = (int)0.11f;
			item.melee = true;
			item.width = 96;
			item.height = 96;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 1;
			item.knockBack = 11;
			item.value = 20000;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.BreakerBlade);recipe.AddIngredient(ItemID.CrystalShard, 15);recipe.AddIngredient(ItemID.SoulofLight, 7);recipe.AddIngredient(ItemID.PearlwoodSword, 5);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}