using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class Linimisifrififlium : ModItem
	{
		Vector3 lightColor;
		int timer = 8;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Made from many souls, Linimisifrififlium is a very powerful material, containing tons of energy\nPlease refer to it with they/them pronouns");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 14));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 99;
			item.value = 50000;
			item.rare = ItemRarityID.Expert;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
        public override void PostUpdate()
        {
			timer++;
			switch (timer)
            {
				case 8:
					lightColor = new Vector3(111, 222, 255);
					break;
				case 24:
					lightColor = new Vector3(51, 57, 255);
					break;
				case 40:
					lightColor = new Vector3(144, 49, 223);
					break;
				case 56:
					lightColor = new Vector3(255, 86, 235);
					break;
				case 72:
					lightColor = new Vector3(251, 46, 46);
					break;
				case 88:
					lightColor = new Vector3(255, 102, 34);
					break;
				case 104:
					lightColor = new Vector3(34, 255, 79);
					timer = 8;
					break;
			}
			Lighting.AddLight(item.Center, (lightColor / 255f));
        }
        public override bool CanUseItem(Player player)
		{
			return false;
		}
    }
}
