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
    public class UnshadedPancake : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heals 14 HP\nNever touch a whisk again.");
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
            item.healLife = 14;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(70));
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }
    }
}