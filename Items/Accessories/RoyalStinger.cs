using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class RoyalStinger : ModItem
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
			Tooltip.SetDefault("Crits paralyze enemies for 3 seconds\n+40 max HP when covered in honey");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			hasQueenStinger = true;
			if (player.HasBuff(BuffID.Honey))
            {
				player.statLifeMax2 += 40;
            }
		}
	}
}