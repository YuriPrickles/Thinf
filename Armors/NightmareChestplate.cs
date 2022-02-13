using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class NightmareChestplate : ModItem
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("23% increased ranged damage\n31% increased ranged fire rate");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 23;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 36;
		}

		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			color = drawPlayer.GetImmuneAlphaPure(Color.Red, shadow);
		}
		public override void UpdateEquip(Player player) 
		{
			player.GetModPlayer<MyPlayer>().hasNightmareChestplate = true;
			player.rangedDamage += 0.23f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<NightmareFuel>(), 20);
			recipe.AddIngredient(ItemID.SoulofFright, 30);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}