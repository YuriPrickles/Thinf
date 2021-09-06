using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class RawChicken : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("May cause infections when eaten raw");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 50000;
			item.rare = ItemRarityID.Orange;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.consumable = true;
			item.healLife = 30;
			item.UseSound = SoundID.Item2;
		}
		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.MinutesToTicks(1));

			if (Main.rand.Next(2) == 0)
            {
				player.AddBuff(BuffID.Poisoned, Thinf.ToTicks(45));
				player.AddBuff(BuffID.Weak, Thinf.ToTicks(45));
			}
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
}
