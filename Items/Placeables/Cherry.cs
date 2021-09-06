using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class Cherry : ModItem
	{
        public override void SetStaticDefaults()
        {
			//Tooltip.SetDefault("Plants a Carrot crop\nUse on tilled dirt");
        }
        public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 15;
			item.useTime = 10;
			item.maxStack = 99;
			item.placeStyle = 0;
			item.width = 16;
			item.height = 16;
			item.value = 80;
			//item.createTile = TileType<Blocks.CarrotTile>();
		}
	}
}