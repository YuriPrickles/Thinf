using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
	public class SpiritThruster : ModItem
	{
		int chargeTimer = 0;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Hold Right click to charge a dash towards the mouse\nIf charged to the max, you get extra damage\nIncreased defense while holding this");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 220;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = Item.buyPrice(0, 78, 50, 0);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.channel = true;
			item.noMelee = false;
			item.shootSpeed = 10f;
			item.shoot = ModContent.ProjectileType<Projectiles.SpiritThrusterProj>();
			item.noUseGraphic = true;
			item.noMelee = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophytePartisan);
			recipe.AddIngredient(ItemID.Gungnir);
			recipe.AddIngredient(ModContent.ItemType<SoulOfFight>(), 15);
			recipe.AddTile(TileID.MythrilAnvil); recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			if ((Main.mouseRightRelease && chargeTimer >= 50) || chargeTimer >= 300)
			{
				player.statDefense += 45;
				if (chargeTimer >= 300)
				{
					player.itemAnimation = 60;
					player.AddBuff(BuffID.ParryDamageBuff, 60);
				}
				player.velocity = player.DirectionTo(Main.MouseWorld) * (chargeTimer / 10);
				chargeTimer = 0;
			}
			if (player.altFunctionUse == 2)
			{
				item.useAnimation = 15;
				item.useTime = 2;
				item.channel = true;
				item.shoot = ProjectileID.WoodenArrowFriendly;
				item.noUseGraphic = false;
				item.noMelee = false;
				chargeTimer++;
				item.scale = 4;
			}
			if (player.altFunctionUse != 2 && Main.mouseRightRelease)
			{
				item.useTime = 15;
				item.useAnimation = 15;
				item.channel = true;
				item.scale = 1;
				item.shoot = ModContent.ProjectileType<Projectiles.SpiritThrusterProj>();
				item.noUseGraphic = true;
				item.noMelee = true;
			}

			player.statDefense += 45;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2 || item.shoot == ProjectileID.WoodenArrowFriendly)
			{
				damage = 0;
				type = 0;
			}
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}