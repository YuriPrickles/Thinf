using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Bounties
{
    public class Poltergate : ModNPC
    {
        int splinterCount = 0;
        int phaseCount = 0;
        int splinterTimer = 0;
        int phaseZeroMovementTimer = 0;
        int phaseZeroDirectionToGo = -1;
        int phaseOneTimer = 0;
        int cutsceneTimer = 0;
        bool doneRadicowWarning = false;
        int healTimer = 0;
        int boostTimer = 0;

        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lifeMax = 80000;
            npc.damage = 150;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 98;
            npc.height = 90;
            npc.value = Item.buyPrice(5, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            if (npc.life <= 5000 && npc.ai[0] == 0)
            {
                npc.velocity = Vector2.Zero;
                npc.defense = 70;
                npc.dontTakeDamage = true;
                npc.ai[0] = 1;
                Main.NewText("<Poltergate> YOU'LL NEVER GET ME ALIVE, METAL COW!");
                phaseCount = 2;
            }
            if (npc.localAI[0] == 0f)
            {
                int damage = npc.damage / 4;
                for (int k = 0; k < 5; k++)
                {
                    int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<FenceProjectile>(), damage, 0, Main.myPlayer);
                    if (proj == 1000)
                    {
                        npc.active = false;
                        return;
                    }
                    FenceProjectile arm = Main.projectile[proj].modProjectile as FenceProjectile;
                    arm.octopus = npc.whoAmI;
                    arm.width = 16f;
                    arm.length = FenceProjectile.minLength;
                    arm.minAngle = (k - 0.5f) * (float)Math.PI / 3f;
                    arm.maxAngle = (k + 0.5f) * (float)Math.PI / 3f;
                    Main.projectile[proj].rotation = (arm.minAngle + arm.maxAngle) / 2f;
                    Main.projectile[proj].netUpdate = true;
                }
                npc.localAI[0] = 1f;
            }
            if (phaseCount == 0)
            {
                if (splinterCount >= 5)
                {
                    Main.NewText("<Poltergate> GAH! GET AWAY FROM MEEEEE!!");
                    if (!doneRadicowWarning)
                    {
                        Main.NewText("<Radicow> Don't let him get too far! He's gonna try to heal himself!", Color.LightGreen);
                        doneRadicowWarning = true;
                    }
                    splinterCount = 0;
                    phaseCount = 1;
                }
                phaseZeroMovementTimer++;
                if (phaseZeroMovementTimer >= 150)
                {
                    phaseZeroDirectionToGo *= -1;
                    phaseZeroMovementTimer = 0;
                }
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(300 * phaseZeroDirectionToGo, -300 * phaseZeroDirectionToGo)) * 4;

                splinterTimer++;
                if (splinterTimer >= 180 && splinterTimer % 10 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<SplinterShot>(), 20, 0);
                    projectile.velocity = projectile.DirectionTo(player.Center + player.velocity * 35) * 6;
                    if (splinterTimer >= 280)
                    {
                        splinterTimer = 0;
                        splinterCount++;
                    }
                }
            }

            if (phaseCount == 1)
            {
                phaseOneTimer++;
                if (phaseOneTimer >= 600)
                {
                    npc.localAI[0] = 0;
                    phaseCount = 0;
                    phaseOneTimer = 0;
                }
                for (int i = 0; i < Main.maxProjectiles; ++i)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == ModContent.ProjectileType<FenceProjectile>())
                    {
                        proj.active = false;
                    }
                }

                if (npc.Distance(player.Center) <= 500)
                {
                    npc.velocity = npc.DirectionFrom(player.Center) * 6;
                }
                else
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 4;
                    healTimer++;
                    if (healTimer >= 5)
                    {
                        npc.life += 10;
                        npc.HealEffect(10);
                    }
                }
            }

            if (phaseCount == 2)
            {
                cutsceneTimer++;
                if (cutsceneTimer == 120)
                {
                    Main.NewText("<Radicow> ARGH! You're not getting away this time, you criminal!", Color.LightGreen);
                }
                if (cutsceneTimer >= 300)
                {
                    phaseCount = 3;
                }
            }

            if (phaseCount == 3)
            {
                for (int i = 0; i < Main.maxProjectiles; ++i)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == ModContent.ProjectileType<FenceProjectile>())
                    {
                        proj.active = false;
                    }
                }
                npc.ai[0] = 3;
                if (npc.Distance(player.Center) <= 1000)
                {
                    boostTimer++;
                    if (boostTimer >= 600)
                    {
                        npc.velocity = npc.DirectionFrom(player.Center) * 12;
                        if (boostTimer >= 660)
                        {
                            boostTimer = 0;
                        }
                    }
                    npc.velocity = npc.DirectionFrom(player.Center) * 9;
                }
                else
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 5;
                }
                npc.dontTakeDamage = false;
            }
        }
    }
}
