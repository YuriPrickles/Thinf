using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Ammo
{
	public class InfinitySeed : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Screw you.\n(Crafted from 12 stacks of seeds)");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Seed);
			item.maxStack = 1;
			item.consumable = false;
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Seed, 11988);
			recipe.AddIngredient(ItemType<CosmicHerbalPiece>(), 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
