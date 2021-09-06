using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items
{
	public class CoreResetTest : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Resets Core's downed bool");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Expert;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override bool UseItem(Player player)
		{
			ModNameWorld.coreDestroyed = false;
			Main.NewText("Core Restored!");
			return true;
		}
	}
}
