using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Thinf.ModNameWorld;

namespace Thinf.Items
{
	public class ThingThatCausesDingSounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Left click to activate ding mode\nDing mode will use the vanquish sounds from garden warfare 2 and display stuff in chat\nCant be bothered to make mod configs\nGARY GET OUT OF THAT IMP PUNT OH NO HE CANT HEAR US HE HAS AIRPODS ON GARY NOOOOOO");
		}

		public override void SetDefaults()
		{
			item.width = 72;
			item.height = 72;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Red;
			item.useAnimation = 10;
			item.useTime = 10;
			item.UseSound = SoundID.Item2;
			item.useStyle = 2;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override bool UseItem(Player player)
		{
            if (MyPlayer.dingMode)
            {
                MyPlayer.dingMode = false;
                Main.NewText("Ding mode off");
            }
            else
            {
                MyPlayer.dingMode = true;
                Main.NewText("Ding mode on");
            }
            return true;
		}
	}
}
