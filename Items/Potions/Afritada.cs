using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Thinf.Items.Placeables;
using static Thinf.NPCs.GlobalNPCs;

namespace Thinf.Items.Potions
{
	public class Afritada : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 150 HP\nGives Well Fed, Regeneration, and Rapid Healing for 7 Minutes\nWhen crafting this it has a chance to be not cooked properly");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 150;
		}
		public override void OnCraft(Recipe recipe)
		{
			WeightedRandom<(string, int, Color)> wRand = new WeightedRandom<(string, int, Color)>(Main.rand);
			wRand.Add(("You cooked the afritada perfectly!", item.type, Color.LightGoldenrodYellow),  0.4f);
			wRand.Add(("The afritada was undercooked.", ModContent.ItemType<UndercookedAfritada>(), Color.White), 0.3f);
			wRand.Add(("The afritada was overcooked.", ModContent.ItemType<OvercookedAfritada>(), Color.Orange), 0.2f);
			wRand.Add(("You burnt the afritada.", ModContent.ItemType<BurntAfritada>(), Color.Black), 0.1f);
			(string chatText, int itemType, Color textColor) = wRand.Get();
			item.SetDefaults(itemType);
			item.type = itemType;
			CombatText.NewText(Main.LocalPlayer.getRect(), textColor, chatText, true);
		}
		public override bool UseItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(60));
			player.AddBuff(BuffID.Regeneration, Thinf.MinutesToTicks(8));
			player.AddBuff(BuffID.RapidHealing, Thinf.MinutesToTicks(8));
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Potato>(), 20);
			recipe.AddIngredient(ModContent.ItemType<Carrot>(), 20);
			recipe.AddIngredient(ModContent.ItemType<Tomato>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Lycofin>(), 3);
			recipe.AddIngredient(ModContent.ItemType<Steak>(), 12);
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class UndercookedAfritada : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 40 HP\nDid you even turn on the fire?");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 60;
		}
		
		public override bool UseItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(120));
			if (Main.rand.Next(3) == 0)
			{
				player.AddBuff(BuffID.Weak, Thinf.MinutesToTicks(3));
			}
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
	public class OvercookedAfritada : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 75 HP\nToo burnt.");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 60;
		}

		public override bool UseItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(80));
			player.AddBuff(BuffID.BrokenArmor, Thinf.ToTicks(80));
			if (Main.rand.Next(3) == 0)
			{
				player.AddBuff(BuffID.Burning, Thinf.ToTicks(5));
			}
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
	public class BurntAfritada : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 20 HP\nI didn't tell you to make charcoal.");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 20;
		}

		public override bool UseItem(Player player)
		{
			player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " ate bad food."), 60, 0);
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(80));
			if (Main.rand.Next(3) == 0)
			{
				player.AddBuff(BuffID.Burning, Thinf.ToTicks(10));
			}
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
}