using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs;

namespace Thinf.Items
{
    public class KeyOfSouls : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Key of Souls");
            Tooltip.SetDefault("Summons The Keys of Light. Night, and Flight\nOnly usable in the Chest Wasteland");
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
            item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {           
            return (!NPC.AnyNPCs(mod.NPCType("LightKey")) && !NPC.AnyNPCs(mod.NPCType("NightKey")) && !NPC.AnyNPCs(mod.NPCType("FlightKey")) && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneChestWasteland);  //you can't spawn this boss multiple times
        }
        public override bool UseItem(Player player)
        {
            Main.NewText("Your storage system sucked so much the gods want to kill you!", 175, 75, 255);
            NPC.NewNPC((int)player.Center.X, (int)(player.Center.Y - 300), ModContent.NPCType<FlightKey>());
            NPC.NewNPC((int)player.Center.X - 150, (int)(player.Center.Y + 300), ModContent.NPCType<LightKey>());
            NPC.NewNPC((int)player.Center.X + 150, (int)(player.Center.Y + 300), ModContent.NPCType<NightKey>());
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<OldKey>(), 3);
            recipe.AddIngredient(ItemID.SoulofFlight, 35);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
