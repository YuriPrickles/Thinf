using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class VileBugSpray : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Seeds have a 10% chance to spawn Vile Potted Plants that spawn tiny eaters when hit\n5% increased plant speed");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsSpawnVilePlants = true;

			FarmerClass modPlayer = ModPlayer(player);
			modPlayer.farmerSpeed *= 1.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 15);
			recipe.AddIngredient(ItemID.BottledWater, 15);
			recipe.AddIngredient(ItemID.CorruptSeeds, 10);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}