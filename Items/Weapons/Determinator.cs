using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Determinator : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Each hit summons a powerful spirit that deals debuffs when you are close\nHold right-click to make the Determinator return to you at full speed");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.crit = 0;
			item.melee = true;
			item.width = 42;
			item.height = 56;
			item.useStyle = 1;
			item.knockBack = 11;
			item.useTime = 14;
			item.useAnimation = 14;
			item.value = 20000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<DeterminatorProj>();
			item.shootSpeed = 19f;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.channel = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SoulOfFight>(), 16);
			recipe.AddIngredient(ItemID.EnchantedBoomerang);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<DeterminatorProj>()] == 0;
        }

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}