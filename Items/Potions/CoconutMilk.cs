using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf;

namespace Thinf.Items.Potions
{
    public class CoconutMilk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased defense by 3 and 4% increased damage reduction");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
            item.useStyle = ItemUseStyleID.EatingUsing;                 //this is how the item is holded when used
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;                 //this is where you set the max stack of item
            item.consumable = true;           //this make that the item is consumable when used
            item.width = 20;
            item.height = 30;
            item.value = 100;
            item.rare = ItemRarityID.Blue;
            item.buffType = mod.BuffType("CoconutMilkBuff");    //this is where you put your Buff name
            item.buffTime = Thinf.ToTicks(240);    //this is the buff duration        20000 = 6 min
            return;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 3);
            recipe.AddIngredient(ModContent.ItemType<Coconut>(), 5);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}