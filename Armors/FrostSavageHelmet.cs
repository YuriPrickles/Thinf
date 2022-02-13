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
    public class FrostSavageHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+20% melee speed and damage\nDefense increases based on how low your HP is\nDefense increase rate: 5 defense");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<FrostSavageChestplate>() && legs.type == ItemType<FrostSavageIciclegs>();
        }
        public override void UpdateEquip(Player player)
        {
            MyPlayer.hasFrostSavageHelmetMeleeSpeedBuff = true;
            player.statDefense += (int)MathHelper.Lerp(35, 30, ((player.statLife - (int)(player.statLifeMax2 * .8f)) / (player.statLifeMax2 - (int)(player.statLifeMax2 * .8f))));
            player.meleeDamage += 0.20f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Melee attacks inflict frostburn and havea a chance to paralyze enemies for 5 seconds";
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<FrozenEssence>(), 15);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddTile(TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}