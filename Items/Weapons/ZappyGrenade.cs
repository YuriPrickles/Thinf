using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;

namespace Thinf.Items.Weapons
{
	internal class ZappyGrenade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zappy Grenade");
			Tooltip.SetDefault("Inflicts Shockeds\nHas a short fuse");
			ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 75;
			item.ranged= true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shootSpeed = 12f;
			item.shoot = mod.ProjectileType("ZappyGrenade");
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.consumable = true;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 40;
			item.useTime = 40;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 0, 20, 0);
			item.rare = ItemRarityID.Blue;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ModContent.ItemType<Battery>());recipe.AddIngredient(ItemID.Grenade, 5);recipe.AddTile(TileID.WorkBenches); recipe.SetResult(this, 5); recipe.AddRecipe();
		}
	}
}