using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Cortal;
using Thinf.NPCs.SmellyBill;

namespace Thinf.Items
{
    public class SmellyBoot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smelly Boot");
            Tooltip.SetDefault("Lures a very powerful demon");
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
            return (!NPC.AnyNPCs(ModContent.NPCType<SmellyBill>()));  //you can't spawn this boss multiple times
        }
        public override bool UseItem(Player player)
        {
            NPC cortal = Main.npc[NPC.NewNPC((int)(player.Center.X + 150 * player.direction), (int)player.Center.Y, ModContent.NPCType<SmellyBill>())];
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GoatBleat"));
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OldShoe, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
