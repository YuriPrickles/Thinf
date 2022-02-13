using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
    [AutoloadEquip(EquipType.Head)]
    public class CarrotyxHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased melee crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 23;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<CarrotyxChestpiece>() && legs.type == ItemType<CarrotyxDrillLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.2f;
        }
        public override void UpdateArmorSet(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
            {
                Main.PlaySound(SoundID.Item23);
                player.velocity.Y += 25;
                player.GetModPlayer<MyPlayer>().drillDashTimer = 15;
            }
            player.GetModPlayer<MyPlayer>().hasHoodorHeadgearCarrotyx = "Headgear";
            player.setBonus = "Double-tap down to dash downwards\nAny enemies hit in the dash are damaged for 1/7th of your max HP";
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
            recipe.AddIngredient(ItemType<Carrot>(), 45);
            recipe.AddIngredient(ItemType<CarrotyxBar>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}