using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Meters
{
    public class MoneyMeter : ModNPC
    {
        int frameCount = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 11;
        }
        public override void SetDefaults()
        {
            npc.boss = false;
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 72;
            npc.height = 42;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.dontCountMe = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            npc.dontTakeDamage = true;
        }

        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];
            if (player.GetModPlayer<MyPlayer>().moneyMeter == 0)
            {
                frameCount = 0;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 0 && player.GetModPlayer<MyPlayer>().moneyMeter < 21)
            {
                frameCount = 42 * 1;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 20 && player.GetModPlayer<MyPlayer>().moneyMeter < 41)
            {
                frameCount = 42 * 2;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 40 && player.GetModPlayer<MyPlayer>().moneyMeter < 61)
            {
                frameCount = 42 * 3;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 60 && player.GetModPlayer<MyPlayer>().moneyMeter < 81)
            {
                frameCount = 42 * 4;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 80 && player.GetModPlayer<MyPlayer>().moneyMeter < 101)
            {
                frameCount = 42 * 5;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 100 && player.GetModPlayer<MyPlayer>().moneyMeter < 121)
            {
                frameCount = 42 * 6;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 120 && player.GetModPlayer<MyPlayer>().moneyMeter < 141)
            {
                frameCount = 42 * 7;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 140 && player.GetModPlayer<MyPlayer>().moneyMeter < 161)
            {
                frameCount = 42 * 8;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 160 && player.GetModPlayer<MyPlayer>().moneyMeter < 181)
            {
                frameCount = 42 * 9;
            }
            if (player.GetModPlayer<MyPlayer>().moneyMeter > 180 && player.GetModPlayer<MyPlayer>().moneyMeter < 199)
            {
                frameCount = 42 * 10;
            }
            npc.frame.Y = frameCount;
        }
        public override void AI()
        {
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active || !Main.player[npc.target].GetModPlayer<MyPlayer>().hasMoneyNecklace)
            {
                npc.active = false;
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.Center = player.Center + new Vector2(0, -75);
        }
    }
}
