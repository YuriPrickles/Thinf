using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class CoolBeats : ModItem
    {
        int numSnow = 3;
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Killing enemies or hitting bosses summons a cool shockwave\n'Roadie Z might like this...'");
        }

        public override void SetDefaults()
        {
            item.damage = 240;
            item.crit = 12;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 2;
            item.value = 20000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 10f;
            item.scale = 2;
        }
        public override void UseStyle(Player player)
        {

        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.life < damage && !target.friendly && !target.immortal)
            {
                Projectile projectile = Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0).RotatedByRandom(MathHelper.ToRadians(360)), ModContent.ProjectileType<Shockwave>(), item.damage, item.knockBack * 2, player.whoAmI);
                projectile.melee = true;
            }
            if (target.boss && Main.rand.Next(4) == 0)
            {
                Projectile projectile = Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0).RotatedByRandom(MathHelper.ToRadians(360)), ModContent.ProjectileType<Shockwave>(), item.damage, item.knockBack * 2, player.whoAmI);
                projectile.melee = true;
            }
            target.AddBuff(BuffID.Frostburn, 600);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 10);
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>(), 15);
            recipe.AddIngredient(ItemID.BorealWoodHammer);
            recipe.AddIngredient(ItemID.Wire, 120);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        /*public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.position.X, target.position.Y, 0, 0, mod.ProjectileType("GoodCactspike"), item.damage, 10f, 0);
		}*/
    }
}