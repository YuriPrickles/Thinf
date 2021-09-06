using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Placeables
{
	public class BloodclotSeeds : ModItem
	{
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 15;
			item.useTime = 10;
			item.maxStack = 99;
			item.consumable = true;
			item.placeStyle = 0;
			item.width = 16;
			item.height = 16;
			item.value = 16;
			item.createTile = TileType<Blocks.BloodclotTile>();
		}
	}
}