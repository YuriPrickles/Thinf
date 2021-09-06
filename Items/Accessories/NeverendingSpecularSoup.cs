using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	// This file is showcasing inheritance to implement an accessory "type" that you can only have one of equipped
	// It also shows how you can interact with inherited methods
	// Additionally, it takes advantage of ValueTuple to make code more compact

	// First, we create an abstract class that all our exclusive accessories will be based on
	// This class won't be autoloaded by tModLoader, meaning it won't "exist" in the game, and we don't need to provide it a texture
	// Further down below will be the actual items (Green/Yellow Exclusive Accessory)
	public class NeverendingSpecularSoup : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 22;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Expert;
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("+25% summon damage when submerged in water\n+10% summon damage if on land");
		}

		public override void UpdateEquip(Player player)
		{
			if (player.wet) 
			{
				player.minionDamage += player.minionDamage * 0.25f;
			}
            else
            {
				player.minionDamage += player.minionDamage * 0.10f;
			}
		}
	}
}