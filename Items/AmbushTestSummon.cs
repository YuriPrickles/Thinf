using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class AmbushTestSummon : ModItem
	{
        public override string Texture => "Terraria/Item_" + ItemID.LightDisc;
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Test Item, spawns Commando Flashlight's ambush");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 1;
			item.value = 5000;
			item.rare = ItemRarityID.Expert;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
        public override bool UseItem(Player player)
        {
			player.GetModPlayer<MyPlayer>().Ambush();
            return true;
        }
        public override bool CanUseItem(Player player)
		{
			return true;
		}
	}
}
