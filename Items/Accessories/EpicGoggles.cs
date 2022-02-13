using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class EpicGoggles : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
			item.defense = 2;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Any enemy near your cursor will be roasted\nImmunity to Cursed Inferno\n'All you have to do is imagine that you inflict Cursed Inferno'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().epicGoggles = true;
			player.buffImmune[BuffID.CursedInferno] = true;
		}
	}
}