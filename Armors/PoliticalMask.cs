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
    [AutoloadEquip(EquipType.Head)]
    public class PoliticalMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+24% minion damage\n+4 max minions\n+3 max sentries\nGrants an infinite Honey buff");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 37;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PoliticalShirt>() && legs.type == ItemType<PoliticalPants>();
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.27f;
            player.maxMinions += 4;
            player.maxTurrets += 3;
            player.AddBuff(BuffID.Honey, 2);
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<MyPlayer>().poisonAura = true;
            player.setBonus = "Grants an aura of Political Poison, with the range increasing for every minion you have.";
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<PoliticalPower>(), 15);
            recipe.AddIngredient(ItemID.BeeHeadgear);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}