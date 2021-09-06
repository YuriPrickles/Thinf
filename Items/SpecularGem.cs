using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.Cortal;

namespace Thinf.Items
{
    public class SpecularGem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Specular Gemstone");
            Tooltip.SetDefault("Summons Cortal\nEnrages when you are not wet");
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
            return (!NPC.AnyNPCs(mod.NPCType("Cortal")) && player.wet);  //you can't spawn this boss multiple times
        }
        public override bool UseItem(Player player)
        {
            NPC cortal = Main.npc[NPC.NewNPC((int)(player.Center.X - 100 * player.direction), (int)player.Center.Y, ModContent.NPCType<Cortal>())];
            cortal.velocity = cortal.DirectionTo(player.Center) * 8;
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.SpecularFish, 5);recipe.AddIngredient(ItemID.Sapphire, 1);recipe.AddIngredient(ItemID.SharkFin, 10);recipe.AddIngredient(ItemID.RecallPotion, 3);recipe.AddTile(TileID.DemonAltar);recipe.SetResult(this); recipe.AddRecipe();
        }
    }
}
