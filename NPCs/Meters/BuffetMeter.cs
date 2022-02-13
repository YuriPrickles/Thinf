using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Meters
{
    public class BuffetMeter : ModNPC
    {
        int frameCount = 0;
        int frameTimer = 0; // for frame 9 and 10

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 11;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.dontCountMe = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
        }

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];
            if (player.GetModPlayer<MyPlayer>().buffetMeter == 0)
            {
                frameCount = 0;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 0 && player.GetModPlayer<MyPlayer>().buffetMeter < 31)
            {
                frameCount = 40 * 1;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 30 && player.GetModPlayer<MyPlayer>().buffetMeter < 61)
            {
                frameCount = 40 * 2;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 60 && player.GetModPlayer<MyPlayer>().buffetMeter < 91)
            {
                frameCount = 40 * 3;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 90 && player.GetModPlayer<MyPlayer>().buffetMeter < 121)
            {
                frameCount = 40 * 4;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 120 && player.GetModPlayer<MyPlayer>().buffetMeter < 151)
            {
                frameCount = 40 * 5;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 150 && player.GetModPlayer<MyPlayer>().buffetMeter < 181)
            {
                frameCount = 40 * 6;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 180 && player.GetModPlayer<MyPlayer>().buffetMeter < 211)
            {
                frameCount = 40 * 7;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter > 210 && player.GetModPlayer<MyPlayer>().buffetMeter < 241)
            {
                frameCount = 40 * 8;
            }
            if (player.GetModPlayer<MyPlayer>().buffetMeter >= 240)
            {
                frameTimer++;
                if (frameTimer == 3)
                {
                    frameCount = 40 * 9;
                }
                if (frameTimer == 6)
                {
                    frameCount = 40 * 10;
                    frameTimer = 0;
                }
            }
            npc.frame.Y = frameCount;
        }
        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active || !Main.player[npc.target].GetModPlayer<MyPlayer>().hasArmorPlating)
            {
                npc.active = false;
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.Center = player.Center + new Vector2(0, -75);
        }
    }
}
