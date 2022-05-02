using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class FishHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It feels so comfortable inside\n+6% minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<FishPlate>() && legs.type == ItemType<FishLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.06f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "4% increased movement and running speed for every minion you have";
            if (player.numMinions > 0)
            {
                player.accRunSpeed += player.numMinions * 1.04f;
                player.moveSpeed += player.numMinions * 1.04f;
            }
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
            ModRecipe recipe = new ModRecipe(mod); recipe.AddIngredient(ItemID.Bass, 4); recipe.AddIngredient(ItemID.Goldfish, 1); recipe.AddTile(TileID.Anvils); recipe.SetResult(this); recipe.AddRecipe();
        }
    }
}