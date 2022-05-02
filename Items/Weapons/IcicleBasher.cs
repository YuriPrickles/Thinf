using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class IcicleBasher : ModItem
    {
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Rains ice on struck enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.crit = 7;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = 5505;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.scale = 3;
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>(), 15);
            recipe.AddIngredient(ItemID.IceBlade);
            recipe.AddTile(ModContent.TileType<Blocks.StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            for (int i = 0; i < 5; ++i)
            {
                Projectile projectile = Projectile.NewProjectileDirect(player.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-700, -450)), Vector2.Zero, ProjectileID.IceSickle, item.damage, item.knockBack, player.whoAmI);
                projectile.velocity = projectile.DirectionTo(target.Center) * 40;
                projectile.penetrate = 1;
                projectile.tileCollide = false;
                if (crit)
                {
                    projectile.scale = 2;
                }
            }
        }
    }
}