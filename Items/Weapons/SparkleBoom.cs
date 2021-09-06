using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class SparkleBoom : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Create lots of boom boom kabloomy explosions\n'Made possible with the help of Gno-Tech Inc.'");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 512;
			item.magic = true;
			item.mana = 10;
			item.width = 42;
			item.height = 42;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0.3f;
			item.value = 10000;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item73;
			item.autoReuse = true;
            item.shoot = ProjectileID.RainbowCrystalExplosion;
			item.shootSpeed = 0.1f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			speedX = 0;
			speedY = 0;
			position = Main.MouseWorld;
			Vector2 newPos = position;

			for (int i = 0; i < 5; ++i)
			{
				if (i != 2)
                {
					damage = 0;
                }
				else
                {
					damage = item.damage;
                }
				Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(0.1f, 1f));
				proj.minion = false;
				proj.timeLeft = 3;
				proj.magic = true;
				proj.penetrate = 1;
				proj.usesLocalNPCImmunity = true;
				proj.localNPCHitCooldown = 5;
			}
            return false;
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
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Linimisifrififlium>(), 15);
			recipe.AddIngredient(ItemType<DevilsMatchstick>());
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}