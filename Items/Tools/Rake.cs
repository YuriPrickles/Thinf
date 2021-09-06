using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Tools
{
	public class Rake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rake");
			Tooltip.SetDefault("Till dirt to make crops grow faster");
		}

		public override void SetDefaults()
		{
			item.damage = 25;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 5;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override bool UseItem(Player player)
        {
			Tile tile = Framing.GetTileSafely((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
			if (tile.type == TileID.Grass || tile.type == TileID.Dirt)
            {
				tile.type = (ushort)TileType<TilledDirt>();
            }

			return true;
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 60);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}