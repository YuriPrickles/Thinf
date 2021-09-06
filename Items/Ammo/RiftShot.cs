using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Ammo
{
	public class RiftShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Teleports behind another enemy after hitting one");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.ranged= true;
			item.width = 8;
			item.height = 16;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.shoot = ProjectileType<RiftShotProj>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 6f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MusketBall, 75);
			recipe.AddIngredient(ItemType<Cortascale>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 75);
			recipe.AddRecipe();
		}
	}
}
