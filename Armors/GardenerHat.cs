using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class GardenerHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+5% increased plant damage");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<GardenerShirt>() && legs.type == ItemType<GardenerPants>();
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerDamageMult *= 1.05f;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "9% increased plant speed";
            modPlayer.farmerSpeed *= 1.09f;
            /* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.Leather, 5);
            recipe.AddIngredient(ItemID.GreenDye, 1);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}