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
    public class Teacup : ModNPC
    {
        int jumptimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 11000;
            npc.damage = 98;
            npc.defense = 75;
            npc.knockBackResist = 0f;
            npc.width = 48;
            npc.height = 24;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.Item27;
            npc.DeathSound = new LegacySoundStyle(SoundID.Shatter, 0);
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

            if (npc.velocity.Y == 0)
            {
                npc.velocity.X = 0;
            }
            jumptimer++;
            if (jumptimer >= 120 && npc.velocity.Y == 0)
            {
                npc.velocity.X += (3 + Main.rand.Next(5)) * npc.direction;
                npc.velocity.Y -= 5;
                jumptimer = 0;
            }
        }
    }
}
