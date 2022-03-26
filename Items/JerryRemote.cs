using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs;
using Thinf.NPCs.JerryEX;
using Microsoft.Xna.Framework;
using System;

namespace Thinf.Items
{
    public class JerryRemote : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Jerry EX\nOnly usable in the Tomato Fields\n'You're gonna regret this!'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {           
            return (!NPC.AnyNPCs(ModContent.NPCType<JerryEXMain>()) && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneTomatoTown);  //you can't spawn this boss multiple times
        }
        public override bool UseItem(Player player)
        {
            if (!Main.dedServ)
                Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/JerrySpawn").WithVolume(1.5f));
            Main.NewText("Get out! Get out! You're nothing but an obstacle to me!", Color.Red);
            NPC jerry = Main.npc[NPC.NewNPC((int)player.Center.X, (int)(player.Center.Y - 500), ModContent.NPCType<JerryEXMain>())];
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.MacOSX)
            {
                Main.NewText("You're not even using Windows! Pathetic!", Color.Red);
            }
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<TomatoFish>(), 5);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
