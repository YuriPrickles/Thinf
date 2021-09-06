using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Projectiles;
using Thinf.NPCs.Blizzard;
using Thinf.Items.Placeables;

namespace Thinf.Items
{
	public class ChillyCube : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a cold mysterious beacon");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 36;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Red;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = 1;
			item.UseSound = SoundID.Item30;
			item.shoot = ModContent.ProjectileType<ChillyCubeProj>();
			item.shootSpeed = 10f;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{           
			return (!NPC.AnyNPCs(ModContent.NPCType<Blizzard>()) && player.ZoneSnow && player.ownedProjectileCounts[ModContent.ProjectileType<ChillyCubeProj>()] < 1);  //you can't spawn this boss multiple times
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 10);
			recipe.AddIngredient(ItemID.IceBlock, 125);
			recipe.AddIngredient(ModContent.ItemType<AsteroidBar>(), 3);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.needSnowBiome = true;
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
