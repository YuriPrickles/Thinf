using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class CollarboneCollar : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("You gain +10 defense and +8% damage when below 50% HP\n'When skin rots off, you'll have to get past the bones'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.statLife < player.statLifeMax2 * 0.5f)
			{
				player.statDefense += 15;
				player.allDamage *= 1.08f;
			}
		}
	}
}