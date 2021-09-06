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
    public class MilkBucket : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Good for the bones");
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
            item.buffType = BuffID.Ironskin;    //this is where you put your Buff name
            item.buffTime = Thinf.MinutesToTicks(6);    //this is the buff duration        20000 = 6 min
            return;
        }
    }
}