using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class TrueCactusSword : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			DisplayName.SetDefault("True Cactus Sword");
			Tooltip.SetDefault("Swing your sword to summon a spiky barrier");
		}

		public override void SetDefaults()
		{
			item.damage = 19;
			item.crit = (int)0.11f;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 20000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GoodCactspike");
			item.shootSpeed = 10f;
		}

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}