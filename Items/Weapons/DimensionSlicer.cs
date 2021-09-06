using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class DimensionSlicer : ModItem
    {
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Summons a portal upon killing an enemy\n'Slicing through multiple realities'");
        }

        public override void SetDefaults()
        {
            item.damage = 24;
            item.crit = (int)0.7f;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 5505;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Cortascale>(), 20);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (damage > target.life)
            {
                Projectile.NewProjectile(target.position, Vector2.Zero, ModContent.ProjectileType<PortalButGood>(), item.damage, 0, player.whoAmI);
            }
        }
    }
}