using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Legs)]
    public class PoliticalPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+18% minion damage\n+3 max minions\nIncreased running speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 31;
        }
        public override void UpdateEquip(Player player)
        {
            player.accRunSpeed += 3f;
            player.minionDamage += 0.24f;
            player.maxMinions += 4;
        }
        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PoliticalPower>(), 20);
            recipe.AddIngredient(ItemID.BeeGreaves);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}