using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Player;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class GlassesOfCthulhu : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Green;
		}

        public override void UpdateInventory(Player player)
        {
        }

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants an infinite Night Owl buff\nEffects do not stack with other night vision items\n7% increased ranged damage during the night");
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.NightOwl, 2);
			if (!Main.dayTime)
            {
				player.rangedDamage *= 1.07f;
            }
		}
	}
}