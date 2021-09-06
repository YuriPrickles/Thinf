using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs;

namespace Thinf.Items
{
	public class EyeballOnAPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons Blobbo\nKilling Blobbo will corrupt the nearby area");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 26;
			item.maxStack = 999;
			item.value = 100;
			item.rare = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{           
			return !NPC.AnyNPCs(ModContent.NPCType<Blobbo>());
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Blobbo>());   //boss spawn
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
			recipe.AddIngredient(ItemID.RottenChunk, 5);
			recipe.AddIngredient(ItemID.EbonsandBlock, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
