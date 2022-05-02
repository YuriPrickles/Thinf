using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;
using static Thinf.FarmerClass;
using Thinf.Items.Placeables;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf.Buffs;

namespace Thinf.Items.Accessories
{
	public class HollyKnight : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 0, silver: 65);
			item.rare = ItemRarityID.Green;
		}

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Seeds increase your defense when hitting enemies\nDefense stacks up for every time you hit a seed at an enemy\nStacked defense slowly goes down every second\nCannot increase more than 70 defense");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().seedsIncreaseHollyBarrierDefense = true;
			player.AddBuff(ModContent.BuffType<HollyBarrier>(), 2);

			FarmerClass modPlayer = ModPlayer(player);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RedPaint, 20);
			recipe.AddIngredient(ModContent.ItemType<Cherry>(), 15);
			recipe.AddIngredient(ModContent.ItemType<RockLeaf>(), 15);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}