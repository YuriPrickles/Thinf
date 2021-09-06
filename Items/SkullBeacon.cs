using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items
{
	public class SkullBeacon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skull Beacon");
			Tooltip.SetDefault("Starts the Dungeon Army");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.scale = 1;
			item.maxStack = 20;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item1;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Yellow;
		}

		public override bool UseItem(Player player)
		{
			if (!ModNameWorld.DungeonArmyUp)
			{
				Main.PlaySound(SoundID.DD2_WyvernScream, player.position);
				Main.NewText("The skull shines and roars loudly...", 175, 75, 255, false);
				DungeonArmy.StartCustomInvasion();
				return true;
			}
			else
			{
				return false;
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 50);
			recipe.AddIngredient(ItemID.Ectoplasm, 5);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfLight>(), 5);
			recipe.AddIngredient(ModContent.ItemType<FragmentOfNight>(), 5);
			recipe.AddIngredient(ItemID.LunarBar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}