using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;
using System.Linq;
using Thinf.NPCs;

namespace Thinf.Projectiles
{
	public class GlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        readonly int[] seedTypes = {
            ProjectileID.Seed,
            ModContent.ProjectileType<GhostSeed>(),
            ModContent.ProjectileType<FireSeed>(),
            ModContent.ProjectileType<FrostSeed>(),
            ModContent.ProjectileType<CosmoSeed>(),
            ModContent.ProjectileType<DeathSeed>(),
            ModContent.ProjectileType<NoGravSeed>()
        };
        public override void SetDefaults(Projectile projectile)
        {
        }
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().seedsShine && seedTypes.Contains(projectile.type))
            {
                Lighting.AddLight(projectile.Center, Color.Yellow.ToVector3());
            }
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().seedsSpawnVilePlants && Main.rand.NextFloat() <= .1f && seedTypes.Contains(projectile.type))
            {
                NPC.NewNPC((int)projectile.Center.X, (int)(projectile.Center.Y - 64), ModContent.NPCType<VilePottedPlant>());
            }
            if (player.GetModPlayer<MyPlayer>().seedsSpawnBloodyPlants && Main.rand.NextFloat() <= .1f && seedTypes.Contains(projectile.type))
            {
                NPC.NewNPC((int)projectile.Center.X, (int)(projectile.Center.Y - 64), ModContent.NPCType<BloodyPottedPlant>());
            }
            return true;
        }
        public override void Kill(Projectile projectile, int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().seedsExplode && Main.rand.NextFloat() <= .3f && seedTypes.Contains(projectile.type))
            {
                Projectile.NewProjectileDirect(projectile.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.SporeCloud, projectile.damage / 2, 0, projectile.owner);
                Projectile.NewProjectileDirect(projectile.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.SporeCloud, projectile.damage / 2, 0, projectile.owner);
                Projectile.NewProjectileDirect(projectile.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.SporeCloud, projectile.damage / 2, 0, projectile.owner);
                Projectile.NewProjectileDirect(projectile.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.SporeCloud, projectile.damage / 2, 0, projectile.owner);
                Projectile.NewProjectileDirect(projectile.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.SporeCloud, projectile.damage / 2, 0, projectile.owner);
            }
            if (player.GetModPlayer<MyPlayer>().seedsTurnToBeanstalks && Main.rand.NextFloat() <= .4f && seedTypes.Contains(projectile.type))
            {
                Projectile.NewProjectileDirect(projectile.Center + new Vector2(0, -24), new Vector2(0, 0), ModContent.ProjectileType<Beanstalk>(), projectile.damage, 0, projectile.owner);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().seedsShine && Main.rand.NextFloat() <= .25f && seedTypes.Contains(projectile.type))
            {
                target.AddBuff(BuffID.OnFire, 240);
            }
            if (player.GetModPlayer<MyPlayer>().seedsRainTearsWhenHitting && Main.rand.NextFloat() <= .25f && seedTypes.Contains(projectile.type))
            {
                for (int i = 0; i < 15; i++)
                {
                    Projectile.NewProjectileDirect(projectile.Center + new Vector2(Main.rand.Next(-1500, 1500), Main.rand.Next(-1250, -750)), new Vector2(0, Main.rand.Next(8, 16)), ProjectileID.RainFriendly, projectile.damage, 0, projectile.owner).tileCollide = false;
                }
            }
            if (player.GetModPlayer<MyPlayer>().seedsIncreaseHollyBarrierDefense && seedTypes.Contains(projectile.type) && player.GetModPlayer<MyPlayer>().hollyDefenseStack < 71)
            {
                player.GetModPlayer<MyPlayer>().hollyDefenseStack++;
            }
            if (player.GetModPlayer<MyPlayer>().seedsCauseCornstrike && seedTypes.Contains(projectile.type) && Main.rand.NextFloat() <= .05f)
            {
                for (int i = 0; i < 1 + Main.rand.Next(2); i++)
                {
                    Projectile proj = Projectile.NewProjectileDirect(projectile.Center + new Vector2(Main.rand.Next(-1500, 1500), Main.rand.Next(-1750, -1000)), Vector2.Zero, ModContent.ProjectileType<ShuckShot>(), projectile.damage * 25, 0, projectile.owner);
                    proj.velocity = Vector2.Normalize(Main.MouseWorld - proj.Center) * 7;
                    proj.tileCollide = false;
                }
            }
            if (player.GetModPlayer<MyPlayer>().seedsHeal && Main.rand.NextFloat() <= .25f && seedTypes.Contains(projectile.type))
            {
                int healAmount = Main.rand.Next(4) + 2;
                player.statLife += healAmount;
                player.HealEffect(healAmount);
            }
        }
    }
}
