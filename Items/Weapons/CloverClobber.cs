using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class CloverClobber : ModItem
    {
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Spawns 3 damaging leaves when you hit an enemy");
        }

        public override void SetDefaults()
        {
            item.damage = 11;
            item.crit = 5;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.reuseDelay = 30;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = 5505;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.scale = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Leaf>(), 30);
            recipe.AddIngredient(ItemID.BladeofGrass);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            int rotatRand = Main.rand.Next(361);
            int projectileSpawnAmount = 3;
            for (int i = 0; i < projectileSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                Vector2 projectileVelocity = currentRotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(rotatRand));
                int type = ProjectileID.CrystalLeafShot;
                Projectile proj = Projectile.NewProjectileDirect(target.Center + projectileVelocity * 150f, Vector2.Zero, type, damage, 1, player.whoAmI);
                proj.melee = true;
                proj.magic = false;
                proj.tileCollide = false;
                proj.velocity = (proj.DirectionTo(target.Center) * 5);

            }
        }
    }
}