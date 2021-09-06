using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Player;

namespace Thinf.Items.Accessories
{
	// This file is showcasing inheritance to implement an accessory "type" that you can only have one of equipped
	// It also shows how you can interact with inherited methods
	// Additionally, it takes advantage of ValueTuple to make code more compact

	// First, we create an abstract class that all our exclusive accessories will be based on
	// This class won't be autoloaded by tModLoader, meaning it won't "exist" in the game, and we don't need to provide it a texture
	// Further down below will be the actual items (Green/Yellow Exclusive Accessory)
	public class SpiritSocks : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Green;
		}

        public override void UpdateInventory(Player player)
        {
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Tabi, 1);
			recipe.AddIngredient(ItemID.Ectoplasm, 25);
			recipe.AddIngredient(ItemID.SoulofFright, 10);
			recipe.AddIngredient(ItemID.FrostsparkBoots);
			recipe.AddIngredient(ItemID.LavaWaders);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault($"Press F to dash towards the cursor (Can be changed in Settings)\nTeleporting costs 200 mana\nAllows the wearer to run super duper fast\n15% increased movement speed\nProvides the ability to walk on water, honey & lava\nGrants immunity to fire blocks and 10 seconds of immunity to lava");
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Main.myPlayer == player.whoAmI && player.statMana >= 200 && player.statManaMax2 >= 200 && !player.manaSick && Thinf.SpiritSocks.JustPressed)
            {
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.LightSkyBlue, "wooosh");
				player.velocity = player.DirectionTo(Main.MouseWorld) * 25f;
				player.statMana -= 200;
				player.eocDash = 5;
            }
			player.accRunSpeed = 9f;
			player.moveSpeed += 0.15f;
			player.waterWalk = true;
			player.lavaMax += 600;
			player.fireWalk = true;
			player.iceSkate = true;
		}
	}
}