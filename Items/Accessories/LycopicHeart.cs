using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;
using Thinf.Items.Placeables;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Thinf.Items.Accessories
{
	public class LycopicHeart : ModItem
	{
		bool drawText = false;
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Hitting enemies increases your damage reduction up to a max of 25%, and resets when you get hit.\nWhen hit with high enough damage reduction, it releases a shockwave that damages nearby enemies.\nAfter not hitting anything for a while, the damage reduction will start decaying.\n'Lycopene, son. It hardens the heart in response to high cholesterol.'\nPetre, M.A.S. (2018, October 3). Lycopene: Health Benefits and Top Food Sources. Healthline.https://www.healthline.com/nutrition/lycopene");
		}

        public override void UpdateEquip(Player player)
		{
			drawText = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().lycopicHeart = true;

			player.endurance += player.GetModPlayer<MyPlayer>().lycopicDefense;
		}

        public override void UpdateInventory(Player player)
		{
			drawText = false;
		}
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (drawText)
			{
			}
		}
    }
}