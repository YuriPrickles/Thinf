using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class AdamantiteStrawberryhead : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% plant damage\n15% increased plant critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 12;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.AdamantiteBreastplate && legs.type == ItemID.AdamantiteLeggings;
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerDamageMult *= 1.20f;
            modPlayer.farmerCrit += 15;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "20% increased plant speed";
            modPlayer.farmerSpeed *= 1.20f;
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
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}