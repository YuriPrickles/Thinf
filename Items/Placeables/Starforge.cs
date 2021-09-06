
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Placeables
{
	public class Starforge : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The Star Prince's glorius oven.");
		}

		public override void SetDefaults()
		{
			item.width = 86;
			item.height = 60;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.rare = ItemRarityID.Purple;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 5000000;
			item.createTile = mod.TileType("StarforgeTile");
		}
	}
}