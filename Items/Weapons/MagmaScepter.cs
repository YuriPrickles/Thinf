using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class MagmaScepter : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots a stream of magma");
		}

		public override void SetDefaults()
		{
			item.damage = 27;
			item.magic = true;
			item.mana = 4;
			item.width = 44;
			item.height = 30;
			item.useTime = 4;
			item.useAnimation = 16;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 7;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType ("LavaPiss") ;
			item.shootSpeed = 15f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(ItemID.LavaBucket);recipe.AddIngredient(ItemID.AquaScepter);recipe.AddIngredient(ItemID.HellstoneBar, 15);recipe.AddTile(TileID.Anvils);recipe.SetResult(this); recipe.AddRecipe();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}
	}
}