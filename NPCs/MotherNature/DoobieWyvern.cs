using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using Thinf.Projectiles.MotherNatureProjectiles;

namespace Thinf.NPCs.MotherNature
{
    public class DoobieWyvern : ModNPC
    {
        int smokeTimer = 0;
        int frameTimer = 0;
        int frameCount = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doobie Wyvern");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -420;
            npc.lifeMax = 420;
            npc.damage = 420;
            npc.defense = 420;
            npc.knockBackResist = 420f;
            npc.width = 480;
            npc.height = 480;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.alpha = 255;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;
            npc.scale = 1;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.frame.Y = GetFrame(frameCount);

            npc.alpha--;
            frameTimer++;
            if (frameTimer >= 12)
            {
                frameTimer = 0;
                frameCount++;
                if (frameCount >= 3)
                {
                    frameCount = 0;
                }
            }

            npc.Center = Main.screenPosition + new Vector2(npc.width/2, Main.screenHeight -npc.height/2);

            smokeTimer++;
            if (smokeTimer >= 120)
            {
                if (smokeTimer % 10 == 0)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Normalize(player.Center - npc.Center) * 3, ModContent.ProjectileType<DoobieSmoke>(), 56, 0);
                }
                if (smokeTimer >= 200)
                {
                    smokeTimer = 0;
                }
            }
        }
        public override void DrawBehind(int index)
        {
            Main.instance.DrawCacheNPCsOverPlayers.Add(index);
        }
        private int GetFrame(int framenum)
        {
            return (int)(npc.height * framenum);
        }
    }
}
