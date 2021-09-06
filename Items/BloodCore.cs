using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items.Placeables;
using Microsoft.Xna.Framework;

namespace Thinf.Items
{
	public class BloodCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Crimsonifies an area around the player");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 0;
			item.rare = ItemRarityID.LightRed;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool UseItem(Player player)
		{
			int dustSpawnAmount = 64;
			for (int i = 0; i < dustSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
				Vector2 dustOffset = currentRotation.ToRotationVector2() * 2.5f;
				Projectile spray = Projectile.NewProjectileDirect(player.Center, dustOffset * 2, ProjectileID.CrimsonSpray, 0, 0, player.whoAmI);
			}
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
	}
}
