using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Thinf.NPCs.GlobalNPCs;

namespace Thinf.Items.Potions
{
    public class PerfectPancake : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heals 125 HP\nReduced Potion Sickness\nDemons will not hurt you when you have a Perfect Pancake in your inventory\nWhen crafting this it has a chance to not be perfect");
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
            item.healLife = 125;
        }
        public override void OnCraft(Recipe recipe)
        {
            if (Main.rand.Next(3) == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            CombatText.NewText(Main.LocalPlayer.getRect(), Color.Black, "You cooked the pancakes very badly.", true);
                            item.SetDefaults(ModContent.ItemType<MixeledPancake>());
                            item.type = ModContent.ItemType<MixeledPancake>();
                        }
                        else
                        {
                            CombatText.NewText(Main.LocalPlayer.getRect(), Color.DarkRed, "You cooked the pancakes badly.", true);
                            item.SetDefaults(ModContent.ItemType<UnshadedPancake>());
                            item.type = ModContent.ItemType<UnshadedPancake>();
                        }
                    }
                    else
                    {
                        CombatText.NewText(Main.LocalPlayer.getRect(), Color.Red, "You cooked the pancakes decently!", true);
                        item.SetDefaults(ModContent.ItemType<PillowShadedPancake>());
                        item.type = ModContent.ItemType<PillowShadedPancake>();
                    }
                }
                else
                {
                    CombatText.NewText(Main.LocalPlayer.getRect(), Color.Orange, "You cooked the pancakes greatly!", true);
                    item.SetDefaults(ModContent.ItemType<GradientPancake>());
                    item.type = ModContent.ItemType<GradientPancake>();
                }
            }
            else
            {
                CombatText.NewText(Main.LocalPlayer.getRect(), Color.LightGoldenrodYellow, "You cooked the pancakes perfectly!", true);
            }
            Player player = Main.player[item.owner];
            player.QuickSpawnItem(ItemID.EmptyBucket, 3);
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(35));
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddIngredient(ModContent.ItemType<Wheat>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Butter>(), 15);
            recipe.AddIngredient(ModContent.ItemType<EggYolk>(), 5);
            recipe.AddIngredient(ModContent.ItemType<MilkBucket>(), 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}