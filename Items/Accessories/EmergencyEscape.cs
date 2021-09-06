using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Accessories
{
	public class EmergencyEscape : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("When you are about to die, you teleport back to spawn and all enemies despawn.");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			MyPlayer.hasEmergencyEscape = true;
		}
	}
}