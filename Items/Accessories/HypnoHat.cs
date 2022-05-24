using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using static Terraria.Player;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class HypnoHat : ModItem
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
			Tooltip.SetDefault("30% increased speed\nApplies the Hallucination shader to the wearer\n'I would like to clarify that this is not lean'");
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(ModContent.BuffType<Hallucinating>(), 2);
			player.moveSpeed *= 1.3f;
			player.accRunSpeed *= 1.3f;
		}
	}
}