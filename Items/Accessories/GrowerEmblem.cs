using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class GrowerEmblem : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("15% increased plant damage");
		}

		public override void SetDefaults()
		{
			item.Size = new Vector2 (32);
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerDamageMult *= 1.15f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(item.type);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem);
			recipe.AddRecipe();
		}
	}
}