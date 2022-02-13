using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class BatteryBlasterProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrobeam");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 8;               //The width of projectile hitbox
            projectile.height = 8;              //The height of projectile hitbox
            projectile.alpha = 255;
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 40;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.extraUpdates = 70;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] != 1)
            {
                for (int i = 0; i < Main.maxNPCs; ++i)
                {
                    NPC npc = Main.npc[i];
                    if (npc.Distance(target.Center) <= 600 && npc.active && !npc.friendly && !npc.dontTakeDamage && !npc.immortal)
                    {
                        int proj = Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<BatteryBlasterProjSplashDamage>(), projectile.damage / 2, 0, projectile.owner);
                        BatteryBlasterProjSplashDamage bolt = Main.projectile[proj].modProjectile as BatteryBlasterProjSplashDamage;
                        Projectile boltProj = Main.projectile[proj];
                        boltProj.velocity = boltProj.DirectionTo(npc.Center) * 23;
                        bolt.npcToAvoidDamaging = target.whoAmI;

                    }
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            projectile.damage = (int)(projectile.damage * 0.9);
        }
        public override void AI()
        {
            for (int i = 0; i < 8; ++i)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] > 16f)
                {
                    Vector2 projectilePosition = projectile.Center;
                    projectilePosition -= projectile.velocity * (i * 0.25f);
                    projectile.rotation = projectile.velocity.ToRotation();
                    Dust dust;
                    dust = Main.dust[Dust.NewDust(projectilePosition, 1, 1, DustID.Electric, 0f, 0f, 0, default, 1.1f)];
                    dust.noGravity = true;
                    dust.fadeIn = 1.1f;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 1)
            {
                for (int i = 0; i < Main.maxNPCs; ++i)
                {
                    NPC npc = Main.npc[i];
                    if (npc.Distance(projectile.Center) <= 250 && npc.active && !npc.friendly && !npc.dontTakeDamage && !npc.immortal)
                    {
                        int proj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<BatteryBlasterProjSplashDamage>(), projectile.damage - projectile.damage / 10, 0, projectile.owner);
                        BatteryBlasterProjSplashDamage bolt = Main.projectile[proj].modProjectile as BatteryBlasterProjSplashDamage;
                        Projectile boltProj = Main.projectile[proj];
                        boltProj.velocity = boltProj.DirectionTo(npc.Center) * 23;
                        bolt.npcToAvoidDamaging = 230000;
                    }
                }
            }
        }
    }
}
