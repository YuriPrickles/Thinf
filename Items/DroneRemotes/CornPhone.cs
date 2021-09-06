using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.Placeables;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf.NPCs.PlayerDrones;
using Thinf.NPCs.SoulCatcher;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.DroneRemotes
{
	public class CornPhone : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Deploy a BCB-35");
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
			return (!player.GetModPlayer<DroneControls>().playerIsControllingDrone && !NPC.AnyNPCs(NPCType<BCB>()) && !player.HasBuff(BuffType<DroneRecharge>()));
		}
		public override bool UseItem(Player player)
		{
			player.GetModPlayer<DroneControls>().playerIsControllingDrone = true;
			NPC npc = Main.npc[NPC.NewNPC((int)(player.Center.X + 50 * player.direction), (int)player.Center.Y, NPCType<BCB>())];
			npc.target = player.whoAmI;
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Corn>(), 10);
			recipe.AddIngredient(ItemType<Leaf>(), 35);
			recipe.AddIngredient(ItemID.Wire, 50);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
