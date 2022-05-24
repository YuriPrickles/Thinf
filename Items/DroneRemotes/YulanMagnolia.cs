using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.Placeables;
using Thinf.NPCs;
using Thinf.NPCs.PlayerDrones;
using Thinf.NPCs.SoulCatcher;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.DroneRemotes
{
	public class YulanMagnolia : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Deploys the Yulan Magnolia\nLeft click for bullet thats leaves a smoke screen behind\nPress UP to make it jump");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.maxStack = 1;
			item.value = 100;
			item.rare = 1;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return (!player.GetModPlayer<DroneControls>().playerIsControllingDrone && !NPC.AnyNPCs(NPCType<YulanMagnoliaBird>()) && !player.HasBuff(BuffType<DroneRecharge>()));
		}
		public override bool UseItem(Player player)
		{
			player.GetModPlayer<PositionResetter>().resetPos = player.Center;
			player.GetModPlayer<DroneControls>().playerIsControllingDrone = true;
			NPC npc = Main.npc[NPC.NewNPC((int)(player.Center.X + 50 * player.direction), (int)player.Center.Y, NPCType<YulanMagnoliaBird>())];
			npc.target = player.whoAmI;
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GrassSeeds, 30);
			recipe.AddIngredient(ItemID.HellstoneBar, 25);
			recipe.AddIngredient(ItemID.FlowerofFire);
			recipe.AddRecipeGroup(RecipeGroupID.Birds, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
