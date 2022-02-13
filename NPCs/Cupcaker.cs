using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class Cupcaker : ModNPC
    {
        int jumptimer = 0;
        int beamTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 23000;
            npc.damage = 78;
            npc.defense = 65;
            npc.knockBackResist = 0f;
            npc.width = 36;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
        }

        public override void AI()
        {
            npc.spriteDirection = npc.direction;
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            beamTimer++;
            if (beamTimer >= 120)
            {
                int dustSpawnAmount = 32;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 64, DustID.PortalBolt, new Vector2(0, -Main.rand.Next(4)), 0, new Color(255, 0, 222), Main.rand.NextFloat(0.4f, 0.9f));
                    dust.noGravity = true;
                }
                if (beamTimer >= 180 && beamTimer % 1 == 0)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                    {
                        Projectile.NewProjectile(npc.Center + new Vector2(0, -15), Vector2.Normalize(player.Center - npc.Center) * 2, ModContent.ProjectileType<CupcakeLaser>(), 5, 0);
                    }
                    if (beamTimer >= 600)
                    {
                        beamTimer = 0;
                    }
                }
            }

            if (npc.velocity.Y == 0)
            {
                GetNPCFrame(1);
                npc.velocity.X = 0;
            }
            else
            {
                GetNPCFrame(0);
            }
            jumptimer++;
            if (jumptimer >= 120 && npc.velocity.Y == 0)
            {
                npc.velocity.X += 2 * npc.direction;
                npc.velocity.Y -= 7;
                jumptimer = 0;
            }
        }
        private void GetNPCFrame(int framenum)
        {
            npc.frame.Y = npc.height * framenum;
            return;
        }
    }
}
