using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class DemonGauntlet : ModItem
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
				Tooltip.SetDefault("15% increased melee speed\nHas a 33% chance of inflicting Disintegrated to enemies\n'El pollo ardiente...'");
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeSpeed += 0.15f;
			hasdemonglove = true;
		}
	}
}