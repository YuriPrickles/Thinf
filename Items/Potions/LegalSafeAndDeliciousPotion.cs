using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Potions
{
    public class LegalSafeAndDeliciousPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Legal, Safe, and Delicious Potion");
            Tooltip.SetDefault("Time to have the time of your life");
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
            item.width = 20;
            item.height = 24;
            item.value = 100;
            item.rare = 1;
            item.buffType = mod.BuffType("Energized");    //this is where you put your Buff name
            item.buffTime = 80000;    //this is the buff duration        20000 = 6 min
            return;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 5);
            recipe.AddIngredient(ItemID.StrangePlant1, 1);
            recipe.AddIngredient(ItemID.PixieDust, 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 5);
            recipe.AddIngredient(ItemID.StrangePlant1, 1);
            recipe.AddIngredient(ItemID.PixieDust, 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}