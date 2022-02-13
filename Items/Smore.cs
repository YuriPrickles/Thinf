using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class Smore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Political Smore");
			Tooltip.SetDefault("Favorite this item for +3 minion slots\nIt may look like a normal smore, but it actually has political opinions that you will not like");
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
			item.autoReuse = false;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override void UpdateInventory(Player player)
		{
			if (item.favorited)
			{
				player.maxMinions += 3;
			}
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override bool UseItem(Player player)
		{
			//Main.NewText("Memories of Prime Minister flash quickly in the smore...");
			////int rand = Main.rand.Next(3);
			//switch (rand)
			//{
			//	case 0:
			//		Main.NewText("This memory is him planning for the elections.");
			//		Main.NewText("'Alright boys, here's our game plan.'", Color.Yellow);
			//		Main.NewText("'We give away free drinks for everyone, but we put political poison in them.'", Color.Yellow);
			//		Main.NewText("'That way, they'll be hypnotized into supporting me and no one will know!'", Color.Yellow);
			//		break;
			//	case 1:
			//		Main.NewText("This memory is him voicing for an anime.");
			//		Main.NewText("It sounds too bad so I'm not going to play it for you.");
			//		break;
			//	case 2:
			//		Main.NewText("This memory is him freezing the larvae of Queen Bee's daughters");
			//		Main.NewText("'You little worms really think you're going to be the heir to the throne?'", Color.Yellow);
			//		Main.NewText("'There's no way I'm letting you all free.'", Color.Yellow);
			//		Main.NewText("'That would ruin my plans of WORLD DOMINATION!!!'", Color.Yellow);
			//		Main.NewText("'HAHAHAHAHHAHA *cough*'", Color.Yellow);
			//		break;
			//	default:
			//		break;
			//}
			return true;
		}
	}
}
