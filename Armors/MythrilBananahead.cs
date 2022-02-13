using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class MythrilBananahead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased plant speed\n20% increased plant critical strike chance");
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
            return body.type == ItemID.MythrilChainmail && legs.type == ItemID.MythrilGreaves;
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerSpeed *= 1.10f;
            modPlayer.farmerCrit += 20;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "12% increased plant damage";
            modPlayer.farmerDamageMult *= 1.12f;
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
            recipe.AddIngredient(ItemID.CobaltBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}