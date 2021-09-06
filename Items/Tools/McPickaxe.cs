using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Tools
{
	public class McPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("McPickaxe");
			Tooltip.SetDefault("Can mine Potatiumite and Carrotyx\nWho knew the Spud Lord was a great chef?");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 6;
			item.useAnimation = 20;
			item.pick = 201;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
	}
}