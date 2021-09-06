using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.Items.Potions
{
    public class Bread : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bread");
            Tooltip.SetDefault("Heals 45 HP\nReduced Potion Sickness");
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
            item.width = 32;
            item.height = 32;
            item.value = 1000;
            item.rare = 1;
            item.healLife = 75;
            item.buffType = BuffID.PotionSickness;    //this is where you put your Buff name
            item.buffTime = 2400;    //this is the buff duration        20000 = 6 min
            return;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 3);
            recipe.AddIngredient(ModContent.ItemType<Wheat>(), 15);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}