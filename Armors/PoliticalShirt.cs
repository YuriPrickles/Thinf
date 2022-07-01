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
    [AutoloadEquip(EquipType.Body)]
    public class PoliticalShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+28% summon damage\n+5 max minions\n+3 max sentries\nGrants extra flight time");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 40;
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.35f;
            player.maxMinions += 5;
            player.maxTurrets += 2;
            player.wingTime += 73;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PoliticalPower>(), 25);
            recipe.AddIngredient(ItemID.BeeBreastplate);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}