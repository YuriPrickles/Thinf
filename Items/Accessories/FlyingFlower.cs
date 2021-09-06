using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace Thinf.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class FlyingFlower : ModItem
	{


		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It's like rocket boots but better!\nWhy is it not animating right\nExpert");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = 10000;
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateEquip(Player player)
		{
			player.wingTimeMax = 60;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}
	}
}