using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class TerraDagger : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Conjures daggers from around the player, launching towards the cursor");
		}

		public override void SetDefaults()
		{
			item.damage = 105;
			item.crit = (int)0.3f;
			item.mana = 6;
			item.magic = true;
			item.width = 14;
			item.height = 40;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 100000;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.rare = ItemRarityID.LightPurple;
			item.UseSound = SoundID.Item60;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<TerraDaggerProj>();
			item.shootSpeed = 0f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position = new Vector2(Main.rand.Next((int)player.position.X - 100,((int)player.position.X + 100)), Main.rand.Next((int)player.position.Y - 100, (int)player.position.Y + 100));
			Dust dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			for (int k = 0; k < 30; k++)
			{
				dust = Main.dust[Dust.NewDust(position, 10, 10, 107, 0f, 0f, 0, new Color(255, 255, 255), 1.7f)];
				dust.noGravity = true;
				dust.fadeIn = 2.3f;
			}
			return true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ModContent.ItemType<BrokenHeroKnife>());recipe.AddIngredient(ItemID.MagicDagger); recipe.AddIngredient(ItemID.SkyFracture); recipe.AddIngredient(ItemID.SpiritFlame);recipe.AddTile(TileID.MythrilAnvil);recipe.SetResult(this); recipe.AddRecipe();
		}

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}