using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Items.Potions
{
    public class TurtleMasterPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Turtle Master Potion");
            Tooltip.SetDefault("Increased endurance, decreased movement speed\nTotally original idea");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.width = 28;
            item.height = 26;
            item.value = 100;
            item.rare = ItemRarityID.Blue;
            item.buffType = ModContent.BuffType<TurtleMaster>();
            item.buffTime = 3600;
            return;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.TurtleShell, 1);
            recipe.AddIngredient(ItemID.Moonglow, 3);
            recipe.AddIngredient(ItemID.IronBar, 6);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}