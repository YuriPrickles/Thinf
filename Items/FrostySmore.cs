using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Blizzard;

namespace Thinf.Items
{
	public class FrostySmore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Favorite this item for 25 defense\nWe can't thaw this. Don't even try.");
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
				player.statDefense += 25;
            }
        }
        public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<BlizzardRefrosted>()) && !NPC.AnyNPCs(ModContent.NPCType<Blizzard>());
		}
		public override bool UseItem(Player player)
		{
			NPC.NewNPC((int)player.Center.X, (int)(player.Center.Y - 175), ModContent.NPCType<BlizzardRefrosted>());
			//Main.NewText("Memories of Blizzard flash quickly in the ice...");
			//int rand = Main.rand.Next(3);
			//switch (rand)
			//{
			//	case 0:
			//		Main.NewText("This memory is her 20th birthday.");
			//		Main.NewText("She looked very happy, surrounded by friends and family.", Color.CornflowerBlue);
			//		break;
			//	case 1:
			//		Main.NewText("This memory is her making snowmen with ice cream.");
			//		Main.NewText("The snowman came to life and she gave it a gun.", Color.CornflowerBlue);
			//		Main.NewText("What");
			//		break;
			//	case 2:
			//		Main.NewText("This memory is her final battle with you.", Color.CornflowerBlue);
			//		Main.NewText("I think we know how this one went.", Color.CornflowerBlue);
			//		break;
			//	default:
			//		break;
			//}
			return true;
		}
	}
}
