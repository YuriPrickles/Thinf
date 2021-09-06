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
    public class ChlorophyllBottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Wait how did you get this????? Thats kinda sussy!!!!!");
        }
        public override void SetDefaults()
        {
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 1;                 //this is where you set the max stack of item
            item.consumable = true;           //this make that the item is consumable when used
            item.width = 16;
            item.height = 30;
            item.value = 1000;
            item.rare = 1;
        }
        public override bool OnPickup(Player player)
        {
            player.AddBuff(ModContent.BuffType<ChlorophyllBoost>(), Thinf.ToTicks(5));
            return false;
        }
    }
}