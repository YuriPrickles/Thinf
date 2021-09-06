using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace Thinf.Items.Weapons
{
	public class PencilWand : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Draw stuff to kill enemies\nBe wary of the projectile limit");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.magic = true;
			item.mana = 2;
			item.width = 44;
			item.height = 32;
			item.useTime = 1;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 3;
			item.value = 120000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item20;
			item.shoot = 10;
			item.autoReuse = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 velocity = new Vector2 (0f,0f);
			damage = item.damage;  //projectile damage
			type = mod.ProjectileType("PencilTrail");  //put your projectile
			Main.PlaySound(98, (int)player.position.X, (int)player.position.Y, 17);
			Projectile projectile = Main.projectile[Projectile.NewProjectile(Main.MouseWorld, velocity, type, damage, 3, player.whoAmI)];
			projectile.magic = true;
			projectile.tileCollide = false;
			return false;
		}

		/*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe;recipe.AddIngredient(ItemID.FlowerBoots) ;
			recipe;recipe.AddIngredient(ItemID.NebulaArcanum);
			recipe;recipe.AddIngredient(ItemID.LunarFlareBook);
			recipe;recipe.AddIngredient(mod.ItemType("CosmicHerbalPiece"), 10);
			recipe;recipe.AddIngredient(ItemID.ChlorophyteBar, 20);
			recipe;recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.SetResult(this); recipe.AddRecipe();
		}*/
	}
}