using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class NecromancyGuide : ModItem
	{
		int skullSummonTimer = 0;
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 38;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.LightPurple;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Professional's Advanced Expert Guide To Necromancy");
			Tooltip.SetDefault("Summons a floating skull every 16 seconds that rotates around the player\nThe skull damages enemies and inflicts shadowflames\nRight-click to kill every skull and heal the player for 10 HP for each skull\nYou can have a max amount of 5 skulls\n'Included in this issue: Top 10 types of Bone Ash! You WILL NOT BELIEVE number 6!'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSkull>()] < 5)
			{
				skullSummonTimer++;
				if (skullSummonTimer >= Thinf.ToTicks(16))
				{
					Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<FloatingSkull>(), 30, 0, player.whoAmI);
					skullSummonTimer = 0;
				}
			}
		}
	}
}