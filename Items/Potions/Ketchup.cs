using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.Placeables;

namespace Thinf.Items.Potions
{
	public class Ketchup : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Drink while having the Well Fed buff for an extra boost!");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 16;
			item.height = 40;
			item.value = 60000;
			item.rare = ItemRarityID.Blue;
		}
        public override bool UseItem(Player player)
        {
			if (player.HasBuff(BuffID.WellFed))
			{
				player.AddBuff(ModContent.BuffType<Yum>(), 24000);
				player.ClearBuff(BuffID.WellFed);
			}
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ModContent.ItemType<Tomato>(), 15);
			recipe.AddIngredient(ItemID.Daybloom, 1);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this); 
			recipe.AddRecipe();
		}
	}
}