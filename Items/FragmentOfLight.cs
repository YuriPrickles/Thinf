using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class FragmentOfLight : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fragment of Light");
			Tooltip.SetDefault("A strong fragment made from the brightest lights.");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 99;
			item.value = 50000;
			item.rare = ItemRarityID.Green;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, new Vector3(255, 112, 234) / 255f);
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
    }
}
