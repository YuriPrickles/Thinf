using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class IceSlice : ModItem
	{
		int numSnow = 3;
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("A bootleg version of Blizzard's knife\nFreezes enemies\nKilling an enemy will summon snowflakes\nCrit deaths summmon more snowflakes\nHitting bosses will summon snowflakes at a lower damage");
		}

		public override void SetDefaults()
		{
			item.damage = 320;
			item.crit = 12;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = 20000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}
        public override void UseStyle(Player player)
        {
			player.itemRotation += MathHelper.ToRadians(0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (target.life < damage && !target.friendly)
			{
				if (crit)
                {
					numSnow = 7;
                }
				for (int i = 0; i < numSnow; i++)
				{
					Projectile projectile = Projectile.NewProjectileDirect(player.Center, new Vector2(5, 5).RotatedByRandom(MathHelper.ToRadians(360)), ModContent.ProjectileType<SnowGlobeBall>(), item.damage, item.knockBack, player.whoAmI);
					projectile.magic = false;
					projectile.melee = true;
					projectile.alpha = 255;
				}
				numSnow = 3;
			}
			if (target.boss)
            {
				if (crit)
				{
					numSnow = 7;
				}
				for (int i = 0; i < numSnow; i++)
				{
					Projectile projectile = Projectile.NewProjectileDirect(player.Center, new Vector2(5, 5).RotatedByRandom(MathHelper.ToRadians(360)), ModContent.ProjectileType<SnowGlobeBall>(), item.damage / 4, item.knockBack, player.whoAmI);
					projectile.magic = false;
					projectile.melee = true;
					projectile.alpha = 255;
				}
				numSnow = 3;
			}
			target.velocity = Vector2.Zero;
			target.AddBuff(BuffID.Frostburn, 6000);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 10);
			recipe.AddIngredient(ModContent.ItemType<FrozenEssence>(), 15);
			recipe.AddIngredient(ItemID.Frostbrand);
			recipe.AddIngredient(ItemID.IceBlade);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}