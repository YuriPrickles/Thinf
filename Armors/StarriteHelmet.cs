using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class StarriteHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% minion damage\n+2 max minions\n+2 max sentries");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<StarriteRobe>() && legs.type == ItemType<StarriteShoes>();
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.25f;
            player.maxMinions += 2;
            player.maxTurrets += 2;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+5 max HP for every minion you have";
            if (player.numMinions > 0)
            {
                player.statLifeMax2 += 5 * player.numMinions;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<StarriteBar>(), 15);
            recipe.AddIngredient(ItemID.StardustHelmet);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}