using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class Transformer : ModItem
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
			Tooltip.SetDefault("When you are hurt, enemies take the same amount of damage\nImmunity to Electrified");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			hasTransformer = true;
			player.buffImmune[BuffID.Electrified] = true;
		}
	}
}