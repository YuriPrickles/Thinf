using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Ammo
{
	public class PackingPeanut : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Explodes into clumps of packing peanuts when hitting anything\n'With new advancements in Cardboard Tech, we have been able to compress anything into small, bullet-sized containers.'\n- Nicholas G. Boxx");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.ranged= true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.2f;
			item.value = Item.buyPrice(0, 0, 5, 0);
			item.rare = ItemRarityID.Green;
			item.shoot = ProjectileType<PackingPeanutProj>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 8f;                  //The speed of the projectile
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
