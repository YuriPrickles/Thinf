using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
    public class OvergrownSmore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Favorite this item for 20% increased damage\nAn entire botanical ecosystem is in this smore");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 32;
            item.maxStack = 99;
            item.value = 5000;
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 90;
            item.useTime = 90;
            item.useStyle = 4;
            item.autoReuse = false;
            item.consumable = false;
        }
        public override void UpdateInventory(Player player)
        {
            if (item.favorited)
            {
                player.allDamage += 0.2f;
            }
        }
        public override bool CanUseItem(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            //Main.NewText("Memories of ??? flash quickly in the ...");
            //int rand = Main.rand.Next(3);
            //switch (rand)
            //{
            //    case 0:
            //        Main.NewText("This memory is very unclear...");
            //        Main.NewText("It is a bunch of herbs in a box, with the perspectives changing from one herb to another.", Color.Lime);
            //        break;
            //    case 1:
            //        Main.NewText("This memory is filled with screams.");
            //        Main.NewText("You can hear people trying to shoot ???, but inevitably fail.", Color.Lime);
            //        break;
            //    case 2:
            //        Main.NewText("This memory is filled with hope.");
            //        Main.NewText("You hear laughs in the box of herbs.", Color.Lime);
            //        Main.NewText("The laughs are combining...", Color.Lime);
            //        Main.NewText("They are now one.", Color.Lime);
            //        break;
            //    default:
            //        break;
            //}
            return true;
        }
    }
}
