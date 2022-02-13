using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class PlixieglassSword : ModItem
	{
		public int chargeLevel = 0;
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Hit enemies to charge up your sword\nRight-click to shoot out a charged wave of pixie dust\nDamage and Speed scale with the charge level");
		}

		public override void SetDefaults()
		{
			item.damage = 75;
			item.crit = 0;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 20000;
			//item.shoot = ProjectileID.ToxicCloud2;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item9;
			item.autoReuse = true;
			item.scale = 1;
			item.useTurn = true;
			item.shootSpeed = 5f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PixieDust, 25);
			recipe.AddIngredient(ItemID.CrystalShard, 10);
			recipe.AddIngredient(ItemID.YellowStainedGlass, 20);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<MyPlayer>().plexiCharge = chargeLevel;
			Dust.NewDustDirect(player.position + new Vector2(0, 35), 3 * chargeLevel, 3 * chargeLevel, DustID.Pixie, 0, -1 * chargeLevel / 2, 0, default, 0.7f);
			if (chargeLevel == 15)
			{
				Dust.NewDustDirect(player.position, 24, 64, DustID.TreasureSparkle, 0, -2);
			}
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (chargeLevel != 15 && player.altFunctionUse != 2)
			{
				chargeLevel++;
			}
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				int damage = item.damage;
				float speedX = item.shootSpeed;
				float speedY = item.shootSpeed;
				damage *= (int)(1 + (0.3f * chargeLevel));
				speedX *= (int)(1 + (0.65f * chargeLevel));
				speedY *= (int)(1 + (0.65f * chargeLevel));
				Projectile.NewProjectileDirect(player.Center, player.DirectionTo(Main.MouseWorld) * speedX, ModContent.ProjectileType<Plexislash>(), damage, item.knockBack, player.whoAmI);

				chargeLevel = 0;
			}
			return true;
        }
    }
}