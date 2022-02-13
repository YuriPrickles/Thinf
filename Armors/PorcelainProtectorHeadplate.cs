using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Ammo;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class PorcelainProtectorHeadplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+17% increased plant damage\n'Time to bring out the fine china'");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 18;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PorcelainProtectorVaseSuit>() && legs.type == ItemType<PorcelainProtectorTeaPartyFeet>();
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerDamageMult *= 1.17f;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "+5 HP and 5% plant damage for every 333 seeds you have";
            if (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) >= 333)
            {
                player.statLifeMax2 += 5 * ((CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) - (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) % 333)) / 333);
                float damageInc = (0.05f * (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) - (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) % 333)) / 333);
                modPlayer.farmerDamageMult *= (1 + damageInc);
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CeramicPotHelmet>());
            recipe.AddIngredient(ItemType<LifeFragment>(), 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        private int CountItemButBetterSuckMyNutsRelogic(int type)
        {
            Player player = Main.player[item.owner];
            int num = 0;
            for (int i = 0; i != 58; i++)
            {
                if (player.inventory[i].stack > 0 && player.inventory[i].type == type)
                {
                    num += player.inventory[i].stack;
                }
            }
            return num;
        }
    }
}