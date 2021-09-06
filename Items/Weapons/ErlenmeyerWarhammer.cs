using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class ErlenmeyerWarhammer : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Releases fumes after hitting an enemy\n'Bet you didn't have THIS in your school laboratory?'");
		}

		public override void SetDefaults()
		{
			item.damage = 120;
			item.crit = 4;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 20000;
			//item.shoot = ProjectileID.ToxicCloud2;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.scale = 2;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
            for (int i = 0; i < 3; ++i)
			{
				Projectile projectile = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, ProjectileID.ToxicCloud2, item.damage / 2, 0.1f);
				projectile.velocity = (projectile.DirectionTo(target.Center) * 8).RotatedByRandom(MathHelper.ToRadians(360));
			}
		}
    }
}