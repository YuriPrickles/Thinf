using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Overflow : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("Overflow");
			Tooltip.SetDefault("You're not supposed to have this\nTrust me");
		}

		public override void SetDefaults()
		{
			item.damage = 214748364;
			item.crit = (int)214748364f;
			item.melee = true;
			item.width = 128;
			item.height = 128;
			item.useTime = 1;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 11;
			item.value = 2147483647;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ProjectileID.NebulaBlaze2;
			item.shootSpeed = 256f;
		}



        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}