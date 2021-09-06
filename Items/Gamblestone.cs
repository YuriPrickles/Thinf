using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class Gamblestone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gamblestone");
			Tooltip.SetDefault("You look like my next mistake\n<right> for disappointment or victory");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 9999999;
			item.value = 1;
			item.rare = ItemRarityID.Red;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			int gemchance;
			gemchance = Main.rand.Next(7);

			if (gemchance == 0)
				Item.NewItem(player.getRect(), ItemID.Sapphire, Main.rand.Next(8));
			if (gemchance == 1)
				Item.NewItem(player.getRect(), ItemID.Ruby, Main.rand.Next(8));
			if (gemchance == 2)
				Item.NewItem(player.getRect(), ItemID.Topaz, Main.rand.Next(8));
			if (gemchance == 3)
				Item.NewItem(player.getRect(), ItemID.Emerald, Main.rand.Next(8));
			if (gemchance == 4)
				Item.NewItem(player.getRect(), ItemID.Amber, Main.rand.Next(8));
			if (gemchance == 5)
				Item.NewItem(player.getRect(), ItemID.Amethyst, Main.rand.Next(8));
			if (gemchance == 6)
				Item.NewItem(player.getRect(), ItemID.Diamond, Main.rand.Next(8));

			int matchance;
			matchance = Main.rand.Next(36);

			if (matchance == 0)
				Item.NewItem(player.getRect(), ItemID.DirtBlock, Main.rand.Next(300) + 300);
			if (matchance == 1)
				Item.NewItem(player.getRect(), ItemID.DirtBlock, Main.rand.Next(300) + 300);
			if (matchance == 2)
				Item.NewItem(player.getRect(), ItemID.DirtBlock, Main.rand.Next(300) + 300);
			if (matchance == 3)
				Item.NewItem(player.getRect(), ItemID.DirtBlock, Main.rand.Next(300) + 300);
			if (matchance == 4)
				Item.NewItem(player.getRect(), ItemID.StoneBlock, Main.rand.Next(200) + 300);
			if (matchance == 5)
				Item.NewItem(player.getRect(), ItemID.StoneBlock, Main.rand.Next(200) + 300);
			if (matchance == 6)
				Item.NewItem(player.getRect(), ItemID.IronOre, Main.rand.Next(25) + 35);
			if (matchance == 7)
				Item.NewItem(player.getRect(), ItemID.IronOre, Main.rand.Next(25) + 35);
			if (matchance == 8)
				Item.NewItem(player.getRect(), ItemID.IronOre, Main.rand.Next(25) + 35);
			if (matchance == 9)
				Item.NewItem(player.getRect(), ItemID.IronOre, Main.rand.Next(25) + 35);
			if (matchance == 10)
				Item.NewItem(player.getRect(), ItemID.LeadOre, Main.rand.Next(25) + 35);
			if (matchance == 11)
				Item.NewItem(player.getRect(), ItemID.LeadOre, Main.rand.Next(25) + 35);
			if (matchance == 12)
				Item.NewItem(player.getRect(), ItemID.LeadOre, Main.rand.Next(25) + 35);
			if (matchance == 13)
				Item.NewItem(player.getRect(), ItemID.LeadOre, Main.rand.Next(25) + 35);
			if (matchance == 14)
				Item.NewItem(player.getRect(), ItemID.SilverOre, Main.rand.Next(15) + 25);
			if (matchance == 15)
				Item.NewItem(player.getRect(), ItemID.SilverOre, Main.rand.Next(15) + 25);
			if (matchance == 16)
				Item.NewItem(player.getRect(), ItemID.TungstenOre, Main.rand.Next(15) + 25);
			if (matchance == 17)
				Item.NewItem(player.getRect(), ItemID.TungstenOre, Main.rand.Next(15) + 25);
			if (matchance == 18)
				Item.NewItem(player.getRect(), ItemID.GoldOre, Main.rand.Next(10) + 25);
			if (matchance == 19)
				Item.NewItem(player.getRect(), ItemID.PlatinumOre, Main.rand.Next(10) + 25);
			if (matchance == 20)
				Item.NewItem(player.getRect(), ItemID.CobaltOre, Main.rand.Next(50) + 35);
			if (matchance == 21)
				Item.NewItem(player.getRect(), ItemID.CobaltOre, Main.rand.Next(50) + 35);
			if (matchance == 22)
				Item.NewItem(player.getRect(), ItemID.PalladiumOre, Main.rand.Next(50) + 35);
			if (matchance == 23)
				Item.NewItem(player.getRect(), ItemID.PalladiumOre, Main.rand.Next(50) + 35);
			if (matchance == 24)
				Item.NewItem(player.getRect(), ItemID.MythrilOre, Main.rand.Next(30) + 25);
			if (matchance == 25)
				Item.NewItem(player.getRect(), ItemID.MythrilOre, Main.rand.Next(30) + 25);
			if (matchance == 26)
				Item.NewItem(player.getRect(), ItemID.OrichalcumOre, Main.rand.Next(30) + 25);
			if (matchance == 27)
				Item.NewItem(player.getRect(), ItemID.OrichalcumOre, Main.rand.Next(30) + 25);
			if (matchance == 28)
				Item.NewItem(player.getRect(), ItemID.AdamantiteOre, Main.rand.Next(20) + 25);
			if (matchance == 29)
				Item.NewItem(player.getRect(), ItemID.TitaniumOre, Main.rand.Next(20) + 25);
			if (matchance == 30)
				Item.NewItem(player.getRect(), ItemID.ChlorophyteOre, Main.rand.Next(30) + 25);
			if (matchance == 31)
				Item.NewItem(player.getRect(), ItemID.SpectreBar, Main.rand.Next(10) + 10);
			if (matchance == 32)
				Item.NewItem(player.getRect(), ItemID.ShroomiteBar, Main.rand.Next(10) + 10);
			if (matchance == 33)
				Item.NewItem(player.getRect(), ItemID.SpectreBar, Main.rand.Next(10) + 10);
			if (matchance == 34)
				Item.NewItem(player.getRect(), ItemID.ShroomiteBar, Main.rand.Next(10) + 10);
			if (matchance == 35)
				Item.NewItem(player.getRect(), ItemID.LunarOre, Main.rand.Next(25) + 5);
			return;
		}
	}
}
