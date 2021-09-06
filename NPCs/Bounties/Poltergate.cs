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
        int phaseCount = 0;
        int splinterTimer = 0;
        int phaseZeroMovementTimer = 0;
        int phaseZeroDirectionToGo = -1;

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
                phaseZeroMovementTimer++;
                if (phaseZeroMovementTimer >= 150)
                {
                    phaseZeroDirectionToGo *= -1;
                    phaseZeroMovementTimer = 0;
                }
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(300 * phaseZeroDirectionToGo, -300 * phaseZeroDirectionToGo)) * 5;

                splinterTimer++;
                if (splinterTimer >= 180 && splinterTimer % 5 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<SplinterShot>(), 40, 0);
                    projectile.velocity = projectile.DirectionTo(player.Center + player.velocity * 35) * 10;
                    if (splinterTimer >= 280)
                    {
                        splinterTimer = 0;
                    }
                }
            }
        }
    }
}
