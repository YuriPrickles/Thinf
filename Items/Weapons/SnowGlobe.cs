using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class SnowGlobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots snowballs that make snowflakes\nRight-click to shoot a flurry of snowballs, at the cost of being frozen");
        }

        public override void SetDefaults()
        {
            item.damage = 124;
            item.magic = true;
            item.mana = 9;
            item.width = 32;
            item.height = 42;
            item.useTime = 9;
            item.useAnimation = 9;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item28;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<SnowGlobeBall>();
            item.shootSpeed = 15f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff(BuffID.Chilled))
            {
                player.AddBuff(BuffID.Frozen, 40);
                player.AddBuff(BuffID.Chilled, Thinf.ToTicks(10));
                int projectileSpawnAmount = 24;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                    if (player.buffImmune[BuffID.Chilled])
                    {
                        damage = 10;
                    }
                    Projectile.NewProjectile(player.Center, projectileVelocity * item.shootSpeed, type, damage, 0, player.whoAmI);
                }
                return false;
            }
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SnowGlobe);
            recipe.AddIngredient(ItemID.NorthPole);
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>(), 15);
            recipe.AddTile(TileID.MythrilAnvil); recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}