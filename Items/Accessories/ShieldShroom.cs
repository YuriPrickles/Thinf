using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
	public class ShieldShroom : ModItem
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
			DisplayName.SetDefault("Shield-Shroom");
			Tooltip.SetDefault("+25 defense when standing still\nHowever, your damage is reduced by 50%\n'May require a cup of coffee during the day'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.velocity == Vector2.Zero)
			{
				int dustSpawnAmount = 30;
				for (int i = 0; i < dustSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
					Vector2 dustOffset = currentRotation.ToRotationVector2();
					Dust dust = Dust.NewDustPerfect(player.Center + dustOffset * 30, DustID.Moss_Purple);
					dust.noGravity = true;
				}
				player.statDefense += 25;
				player.allDamage *= 0.5f;
            }
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 20);
			recipe.AddIngredient(ItemID.GlowingMushroom, 40);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 20);
			recipe.AddIngredient(ItemID.GlowingMushroom, 40);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}