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
    public class GhostPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ghost Potion");
            Tooltip.SetDefault("Take no damage from anything\nCannot be used when you have Potion Sickness");
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
            item.width = 28;
            item.height = 26;
            item.value = 100;
            item.rare = ItemRarityID.Cyan;
            item.buffType = ModContent.BuffType<GhostMode>();
            item.buffTime = Thinf.ToTicks(10);
            return;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffID.PotionSickness))
            {
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, 6000);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.Blinkroot, 1);
            recipe.AddIngredient(ModContent.ItemType<GhostSprout>(), 1);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}