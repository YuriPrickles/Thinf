using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class NightmareSmore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Favorite this item for +120 max mana\nIs this even a smore");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 12));
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 28;
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
				player.statManaMax2 += 120;
			}
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
        public override bool UseItem(Player player)
        {
			//Main.NewText("Memories of Soul Catcher flash quickly in the mouths of the smore...");
			//int rand = Main.rand.Next(3);
   //         switch (rand)
   //         {
			//	case 0:
			//		Main.NewText("This memory is him placing lava inside Hellstone Ore.");
			//		Main.NewText("'Hehe! They'll never see this coming!'", new Color(242, 63, 63));
			//		Main.NewText("'But what if you needed Hellstone yourself?'", Color.CornflowerBlue);
			//		Main.NewText("'oh right'", new Color(242, 63, 63));
			//		break;
			//	case 1:
			//		Main.NewText("This memory is him trying to put the souls in a single file line.");
			//		Main.NewText("'ALRIGHT EVERYONE, SINGLE FILE LINE! I'VE BEEN SCREAMING FOR 8 HOURS!'", new Color(242, 63, 63));
			//		break;
			//	case 2:
			//		Main.NewText("This memory is him typing frantically on a computer.");
			//		Main.NewText("'SC24: For the last time, I DIDN'T SPLICE MY SPEEDRUN!'", new Color(242, 63, 63));
			//		Main.NewText("'Reply to SC24: ur in denial mate it's super obvious'", Color.SlateGray);
			//		Main.NewText("'SC24: SHUT UP'", new Color(242, 63, 63));
			//		break;
   //             default:
   //                 break;
   //         }
			return true;
        }
    }
}
