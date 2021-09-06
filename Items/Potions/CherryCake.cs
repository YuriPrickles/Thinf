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
	public class CherryCake : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 250 HP\nGives Sugar Rush and Swiftness for 8 minutes\nWhen crafting this it has a chance to be not cooked properly\nSlightly increased Potion Sickness");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;
			item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 45;
			item.useTime = 45;
			item.maxStack = 30;
			item.consumable = true;
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 250;
		}
		public override void OnCraft(Recipe recipe)
		{
			WeightedRandom<(string, int, Color)> wRand = new WeightedRandom<(string, int, Color)>(Main.rand);
			wRand.Add(("You baked the cake perfectly!", item.type, Color.LightGoldenrodYellow),  0.2f);
			wRand.Add(("The cake was undercooked.", ModContent.ItemType<UndercookedCherryCake>(), Color.White), 0.3f);
			wRand.Add(("You burnt the cake.", ModContent.ItemType<BurntCherryCake>(), Color.DarkRed), 0.3f);
			(string chatText, int itemType, Color textColor) = wRand.Get();
			item.SetDefaults(itemType);
			item.type = itemType;
			Player player = Main.player[item.owner];
			player.QuickSpawnItem(ItemID.EmptyBucket, 7);
			CombatText.NewText(Main.LocalPlayer.getRect(), textColor, chatText, true);
		}
		public override bool UseItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(100));
			player.AddBuff(BuffID.SugarRush, Thinf.MinutesToTicks(8));
			player.AddBuff(BuffID.Swiftness, Thinf.MinutesToTicks(8));
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Cherry>(), 20);
			recipe.AddIngredient(ModContent.ItemType<Wheat>(), 30);
			recipe.AddIngredient(ModContent.ItemType<MilkBucket>(), 5);
			recipe.AddIngredient(ModContent.ItemType<EggYolk>(), 10);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class UndercookedCherryCake : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Heals 120 HP\nYou owe the universe a plate.");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 45;
			item.useTime = 45;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
			item.healLife = 120;
		}
		
		public override bool UseItem(Player player)
		{
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(150));
			player.AddBuff(BuffID.SugarRush, Thinf.MinutesToTicks(4));
			player.velocity.Y -= 10;
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
	public class BurntCherryCake : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It's still burning. How horrible are you at baking?");
		}
		public override void SetDefaults()
		{
			item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
			item.useStyle = 2;                 //this is how the item is holded when used
			item.useTurn = true;
			item.useAnimation = 45;
			item.useTime = 45;
			item.maxStack = 30;                 //this is where you set the max stack of item
			item.consumable = true;           //this make that the item is consumable when used
			item.width = 32;
			item.height = 32;
			item.value = 1000;
			item.rare = 1;
		}

		public override bool UseItem(Player player)
		{
			player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " ate bad food."), 60, 0);
			player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(200));
			if (Main.rand.Next(3) == 0)
			{
				player.AddBuff(BuffID.Burning, Thinf.ToTicks(10));
				player.AddBuff(BuffID.OnFire, Thinf.ToTicks(10));
			}
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(BuffID.PotionSickness);
		}
	}
}