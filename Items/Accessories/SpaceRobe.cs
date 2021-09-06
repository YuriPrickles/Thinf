using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class SpaceRobe : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("Releases stars when you are hit\n7% increased damage\n+10 defense in space");
		}

		public override void UpdateEquip(Player player)
		{
			spacerobeworn = true;
			player.allDamage *= 1.07f;
			if (player.ZoneSkyHeight)
            {
				player.statDefense += 10;
            }
		}

        public override void UpdateInventory(Player player)
        {

        }
    }
}