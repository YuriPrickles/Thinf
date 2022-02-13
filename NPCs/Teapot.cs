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
    public class Teapot : ModNPC
    {
        int frameTimer = 0;
        int frameCount = 0;
        int boilingShotTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 41000;
            npc.damage = 150;
            npc.defense = 170;
            npc.knockBackResist = 0f;
            npc.width = 66;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.Item27;
            npc.DeathSound = new LegacySoundStyle(SoundID.Shatter, 0);
            npc.netAlways = true;
        }

        public override void NPCLoot()
        {
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<Teacup>());
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<Teacup>());
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<Teacup>());
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

            GetNPCFrame(frameCount);

            frameTimer++;
            if (frameTimer == 6)
            {
                frameTimer = 0;
                frameCount++;
                if (frameCount >= 7)
                {
                    frameCount = 0;
                }
            }

            npc.velocity.X += 0.1f * npc.direction;
            if (npc.velocity.X > 5 || npc.velocity.X < -5)
            {
                npc.velocity.X *= 0.8f;
            }

            boilingShotTimer++;
            if (boilingShotTimer >= 240)
            {
                Projectile proj = Projectile.NewProjectileDirect(npc.Center + new Vector2(-12 * -npc.direction, -12), new Vector2(7 * npc.direction, 0), ProjectileID.WaterBolt, 120, 0);
                proj.hostile = true;
                proj.friendly = false;
                proj = Projectile.NewProjectileDirect(npc.Center + new Vector2(-12 * -npc.direction, -12), new Vector2(7 * npc.direction, 0).RotatedBy(MathHelper.ToRadians(45)), ProjectileID.WaterBolt, 120, 0);
                proj.hostile = true;
                proj.friendly = false;
                proj = Projectile.NewProjectileDirect(npc.Center + new Vector2(-12 * -npc.direction, -12), new Vector2(7 * npc.direction, 0).RotatedBy(MathHelper.ToRadians(-45)), ProjectileID.WaterBolt, 120, 0);
                proj.hostile = true;
                proj.friendly = false;
                boilingShotTimer = 0;
            }
        }
        private void GetNPCFrame(int framenum)
        {
            npc.frame.Y = npc.height * framenum;
            return;
        }
    }
}
