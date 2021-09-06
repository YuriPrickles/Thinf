using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using static Thinf.MyPlayer;
namespace Thinf.Items
{
	public class RodOfCortal : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to craft a Rod of Discord\nConditions for obtaining:\n1/5 drop from Cortal\nMust be in Cavern layer\nMust be during a Pirate Invasion\nMust be in the Hallow\nMust not be in water");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Cyan;
			item.consumable = false;
		}
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfLight>(), 50);
			recipe.AddIngredient(ItemID.CrystalShard, 30);
			recipe.AddIngredient(ItemID.TeleportationPotion, 10);
			recipe.AddIngredient(ItemID.DeepPinkPaint, 10);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(ItemID.RodofDiscord);
			recipe.AddRecipe();
		}
        public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
