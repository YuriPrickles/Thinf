using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
    public class KingSlimesCrown : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 1, silver: 24);
            item.rare = ItemRarityID.Green;
        }

        public override void UpdateInventory(Player player)
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King Slime's Crown");
            Tooltip.SetDefault("Increased jump height\nReleases slime every few seconds");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 4;
            MyPlayer.hasKingSlimeCrown = true;
        }
    }
}