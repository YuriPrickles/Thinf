using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Ammo;
using static Terraria.ModLoader.ModContent;
using static Thinf.FarmerClass;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class CeramicPotHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+8% increased plant damage");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<CeramicVaseSuit>() && legs.type == ItemType<CeramicBrickBoots>();
        }
        public override void UpdateEquip(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            modPlayer.farmerDamageMult *= 1.08f;
        }
        public override void UpdateArmorSet(Player player)
        {
            FarmerClass modPlayer = ModPlayer(player);
            player.setBonus = "+2 defense for every 333 seeds you have in your inventory\nInfinity Seeds will give +36 defense";
            if (CountItemButBetterSuckMyNutsRelogic(ItemType<InfinitySeed>()) >= 1)
            {
                player.statDefense += 36 * player.CountItem(ItemType<InfinitySeed>());
            }
            if (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) >= 333)
            {
                player.statDefense += 2 * ((CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) - (CountItemButBetterSuckMyNutsRelogic(ItemID.Seed) % 333)) / 333);
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
            recipe.AddIngredient(ItemID.ClayPot, 15);
            recipe.AddIngredient(ItemID.RedBrick, 25);
            recipe.AddIngredient(ItemID.Hellstone, 1);
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