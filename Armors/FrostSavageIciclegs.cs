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
    public class FrostSavageIciclegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% melee damage\nDefense increases based on how low your HP is\nDefense increase rate: 6 defense");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            player.statDefense += (int)MathHelper.Lerp(31, 25, ((player.statLife - (int)(player.statLifeMax2 * .8f)) / (player.statLifeMax2 - (int)(player.statLifeMax2 * .8f))));
            player.meleeDamage += 0.30f;
        }
        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<FrozenEssence>(), 20);
            recipe.AddIngredient(ItemID.FrostLeggings);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}