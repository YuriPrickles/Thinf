using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Ammo
{
	public class Cortarrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a portal upon killing an enemy");
		}

		public override void SetDefaults()
		{
			item.damage = 6;
			item.ranged= true;
			item.width = 8;
			item.height = 16;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 2f;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.shoot = ProjectileType<CortarrowProj>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 8f;                  //The speed of the projectile
			item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 50);
			recipe.AddIngredient(ItemType<Cortascale>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}
