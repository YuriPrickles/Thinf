using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Blocks;

namespace Thinf.Items
{
	public class MeteorMania : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Drops a meteor\nNothing happens if there's still a meteor biome in the world");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 32;
			item.maxStack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool UseItem(Player player)
		{
			WorldGen.dropMeteor();
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Meteorite, 20);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
