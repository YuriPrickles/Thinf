using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Items.Potions
{
	public class BoxyPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% damage reduction and increased life regen when standing still\n'In another world, this gives +12% Resistance to the party member who drinks it.'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(2, 30));
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 42;
			item.maxStack = 99;
			item.buffType = ModContent.BuffType<Boxy>();    //this is where you put your Buff name
			item.buffTime = Thinf.MinutesToTicks(7);    //this is the buff duration        20000 = 6 min
			item.value = Item.buyPrice(0, 1, 25, 0);
			item.rare = ItemRarityID.Orange;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.autoReuse = false;
			item.consumable = true;
		}
		public override void UpdateInventory(Player player)
		{

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
