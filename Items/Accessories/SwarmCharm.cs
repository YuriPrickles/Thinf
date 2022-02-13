using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	public class SwarmCharm : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.BeeWax, 40);recipe.AddIngredient(ItemID.Stinger, 10);recipe.AddTile(TileID.Anvils);recipe.SetResult(this); recipe.AddRecipe();
		}

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("Decreased max life by 150 \n +5 max minions");
		}

		public override void UpdateEquip(Player player)
		{
			if (player.statLifeMax2 >= 151) 
			{
				player.statLifeMax2 -= 150; 
			}
            else
            {
				Main.NewText("Are your sure about this?");
				player.statLifeMax2 = 1;
            }
			player.maxMinions += 5;
		}
	}
}