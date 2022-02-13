using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class MagicBean : ModItem
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
			Tooltip.SetDefault("Seeds have a 40% chance to grow beanstalks\n'This is NOT worth the cow, Jack.'\n'You don't just topdeck a magic beanstalk next turn.'\n'You lose tempo by doing literally nothing with those beans.'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsTurnToBeanstalks = true;

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cloud, 5);
			recipe.AddIngredient(ItemID.Spike, 15);
			recipe.AddIngredient(ItemID.DirtBlock, 25);
			recipe.AddIngredient(ItemID.Wood, 35);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}