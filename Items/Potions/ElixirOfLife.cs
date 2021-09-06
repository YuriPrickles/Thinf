using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Potions
{
    public class ElixirOfLife : ModItem
    {
        int invincetime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elixir of Life");
            Tooltip.SetDefault("Removes all debuffs, grants temporary immunity\nHeals 75 HP, has longer potion sickness.");
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
            item.height = 30;
            item.value = 100;
            item.healLife = 75;
            item.rare = ItemRarityID.LightRed;
            item.buffType = BuffID.PotionSickness;    //this is where you put your Buff name
            item.buffTime = 7200;    //this is the buff duration        20000 = 6 min
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }
        public override void OnConsumeItem(Player player)
        {
            player.ClearBuff(BuffID.CursedInferno);
            player.ClearBuff(BuffID.Ichor);
            player.ClearBuff(BuffID.Poisoned);
            player.ClearBuff(BuffID.OnFire);
            player.ClearBuff(BuffID.Electrified);
            player.ClearBuff(BuffID.BrokenArmor);
            player.ClearBuff(BuffID.Darkness);
            player.ClearBuff(BuffID.Blackout);
            player.ClearBuff(BuffID.Slow);
            player.ClearBuff(BuffID.Venom);
            player.ClearBuff(BuffID.VortexDebuff);
            player.ClearBuff(BuffID.Bleeding);
            player.ClearBuff(BuffID.Weak);
            player.ClearBuff(BuffID.Silenced);
            player.ClearBuff(BuffID.Frostburn);
            player.ClearBuff(BuffID.Chilled);
            player.ClearBuff(BuffID.Webbed);
            player.ClearBuff(BuffID.Confused);
            player.ClearBuff(BuffID.OgreSpit);
            player.ClearBuff(BuffID.WitheredArmor);
            player.ClearBuff(BuffID.WitheredWeapon);
            player.ClearBuff(BuffID.Stoned);
            player.ClearBuff(BuffID.Rabies);

            drankelixir = true;
        }
    }
}