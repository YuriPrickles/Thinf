using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;

namespace Thinf.Items.Accessories
{
	public class Criticard : ModItem
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
			DisplayName.SetDefault("Criticard");
			Tooltip.SetDefault("+1% crit chance for every 5 gold you have\n'Y’know, on second thought, I’m not gonna become a communist because the bourgeoisie have really high crit damag\nand I- I don’t really wanna mess with that.'");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (CountItemButBetter(ItemID.GoldCoin) >= 5)
			{
				FarmerClass modPlayer = ModPlayer(player);
				player.magicCrit += 1 * ((CountItemButBetter(ItemID.GoldCoin) - (CountItemButBetter(ItemID.GoldCoin) % 5)) / 5);
				player.meleeCrit += 1 * ((CountItemButBetter(ItemID.GoldCoin) - (CountItemButBetter(ItemID.GoldCoin) % 5)) / 5);
				player.rangedCrit += 1 * ((CountItemButBetter(ItemID.GoldCoin) - (CountItemButBetter(ItemID.GoldCoin) % 5)) / 5);
				modPlayer.farmerCrit += 1 * ((CountItemButBetter(ItemID.GoldCoin) - (CountItemButBetter(ItemID.GoldCoin) % 5)) / 5);
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 20);
			recipe.AddIngredient(ItemID.SilverCoin, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 20);
			recipe.AddIngredient(ItemID.SilverCoin, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		private int CountItemButBetter(int type)
		{
			Player player = Main.player[item.owner];
			int num = 0;
			for (int i = 0; i != 58; i++)
			{
				if (player.inventory[i].stack > 0 && player.inventory[i].type == type)
				{
					num += player.inventory[i].stack;
				}
			}
			return num;
		}
	}
}