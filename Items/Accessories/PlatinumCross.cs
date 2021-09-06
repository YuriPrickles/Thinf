using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class PlatinumCross : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 34;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("Immune to Cursed Flames and Ichor if above half-health\nLife regeneration increased");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.statLife >= player.statLifeMax / 2)
			{
				player.buffImmune[BuffID.CursedInferno] = true;
				player.buffImmune[BuffID.Ichor] = true;
			}
            else
            {
				player.buffImmune[BuffID.CursedInferno] = false;
				player.buffImmune[BuffID.Ichor] = false;
			}
			player.lifeRegen += 4;
		}
	}
}