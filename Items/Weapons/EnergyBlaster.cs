using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class EnergyBlaster : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots Energy balls that heal the player");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 240;
			item.magic = true;
			item.mana = 15;
			item.width = 32;
			item.height = 32;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 25;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.shoot = ProjectileType<EnergyBall>();
			item.shootSpeed = 2.3f;
			item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Linimisifrififlium>(), 15);
			recipe.AddIngredient(ItemID.WandofSparking);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void UpdateInventory(Player player)
		{
			item.color = Main.DiscoColor;
		}

        public override void PostUpdate()
        {
			item.color = Main.DiscoColor;
			Lighting.AddLight(item.Center, new Vector3(Main.DiscoColor.R, Main.DiscoColor.G, Main.DiscoColor.B) / 255);
        }
    }
}