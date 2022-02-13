using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class PalladiumOrangehead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% plant damage\n12% increased plant speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.PalladiumBreastplate && legs.type == ItemID.PalladiumLeggings;
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerDamageMult *= 1.12f;
            modPlayer.farmerSpeed *= 1.12f;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "Greatly increases life regeneration after striking an enemy";
            player.palladiumRegen = true;
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
            recipe.AddIngredient(ItemID.PalladiumBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}