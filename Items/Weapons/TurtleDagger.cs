using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class TurtleDagger : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Turtle Dagger");
			Tooltip.SetDefault("You spin me 'round, baby\nRight 'round like a Tortoise, baby");
		}

		public override void SetDefaults()
		{
			item.damage = 98;
			item.crit = (int)0.4f;
			item.melee = true;
			item.width = 32;
			item.height = 48;
			item.useStyle = 1;
			item.knockBack = 11;
			item.useTime = 14;
			item.useAnimation = 14;
			item.value = 20000;
			item.rare = ItemRarityID.Lime;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<TurtStab>();
			item.shootSpeed = 10f;
			item.channel = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.FlyingKnife);recipe.AddIngredient(ItemID.FeralClaws);recipe.AddIngredient(ItemID.JungleSpores, 25);recipe.AddIngredient(ItemID.TurtleShell, 3);recipe.AddIngredient(ItemID.ChlorophyteBar, 20);recipe.AddIngredient(ItemID.VialofVenom, 10);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}