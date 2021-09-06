using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class SoulOfFight : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Fight");
			Tooltip.SetDefault("The essence of badass fighters");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.maxStack = 999;
			item.value = 50000;
			item.rare = ItemRarityID.Green;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return false;
		}
    }
}
