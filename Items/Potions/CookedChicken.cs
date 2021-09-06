using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Potions;

namespace Thinf.Items.Potions
{
	public class CookedChicken : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Finger lickin good!");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 50000;
			item.rare = ItemRarityID.Orange;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.consumable = true;
			item.healLife = 30;
			item.UseSound = SoundID.Item2;
		}
		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(30));
			player.AddBuff(BuffID.WellFed, Thinf.MinutesToTicks(4));
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RawChicken>(), 1);
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
