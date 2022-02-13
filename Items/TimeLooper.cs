using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items.Placeables;
using Thinf.NPCs.Core;

namespace Thinf.Items
{
    public class TimeLooper : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows the Post-Moon Lord bosses to be resummoned normally again\n\nSo essentially you grab time, put in the oven at 900 degrees celsius\nThen you take it out, and throw it in a pit\n Then you grab it and smash it\nThere you go time is now disrupted");
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 30;
            item.maxStack = 1;
            item.value = 0;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            return ModNameWorld.downedBlizzard && ModNameWorld.downedPM && ModNameWorld.downedSoulCatcher && ModNameWorld.downedHerbalgamation;
        }
        public override bool UseItem(Player player)
        {
            ModNameWorld.timeLoop = true;
            Main.NewText("Time has been put in an oven, thrown in a pit, and smashed into bits");
            Main.NewText("Herbalgamation, Blizzard, Soul Catcher, and Prime Minister can be summoned again");
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldWatch);
            recipe.AddIngredient(ModContent.ItemType<Linimisifrififlium>());
            recipe.AddIngredient(ModContent.ItemType<HerbalCore>());
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>());
            recipe.AddIngredient(ModContent.ItemType<PoliticalPower>());
            recipe.AddTile(ModContent.TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe(); 
            
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumWatch);
            recipe.AddIngredient(ModContent.ItemType<Linimisifrififlium>());
            recipe.AddIngredient(ModContent.ItemType<HerbalCore>());
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>());
            recipe.AddIngredient(ModContent.ItemType<PoliticalPower>());
            recipe.AddTile(ModContent.TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
