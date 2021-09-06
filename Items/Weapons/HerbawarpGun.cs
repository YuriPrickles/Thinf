using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
    public class HerbawarpGun : ModItem
    {
        int timer;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots bullets from the mouse towards the player");
        }
        public override void SetDefaults()
        {
            item.damage = 80;
            item.crit = (int)0f;
            item.ranged = true;
            item.width = 42;
            item.height = 26;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = false;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item114;
            item.autoReuse = true;
            item.shootSpeed = 10f;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile projectile = Main.projectile[Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, type, damage, knockBack, player.whoAmI)];
                Vector2 perturbedSpeed = (projectile.DirectionTo(player.Center) * item.shootSpeed).RotatedByRandom(MathHelper.ToRadians(5));
                projectile.velocity = perturbedSpeed;
                projectile.timeLeft = Thinf.ToTicks(5);
            }
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 pos = Main.MouseWorld;
            for (int u = 0; u < 40; ++u)
            {
                dust = Main.dust[Terraria.Dust.NewDust(pos, 10, 10, 254, 0f, 0f, 0, new Color(255, 255, 255), 2.5f)];
                dust.noGravity = true;
                dust.fadeIn = 3f;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient(ItemID.PortalGun);
            recipe.AddIngredient(ModContent.ItemType<HerbalCore>(), 15);
            recipe.AddIngredient(ModContent.ItemType<CosmicHerbalPiece>(), 20);
            recipe.AddTile(TileID.MythrilAnvil); recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
