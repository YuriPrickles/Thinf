using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
    public class MoneyTroughMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Money Trough");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 700;   //boss life
            npc.damage = 45;  //boss damage
            npc.defense = 12;    //boss defense
            npc.knockBackResist = 0.3f;
            npc.width = 30;
            npc.height = 28;
            npc.value = Item.buyPrice(0, 0, 40, 90);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(17) == 0)
                Item.NewItem(npc.getRect(), mod.ItemType("OldKey"));
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneChestWasteland ? 20f : 0f;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            //npc.spriteDirection = -npc.direction;
            Vector2 distance = player.Center - npc.Center;
            if (distance.Length() <= 150 || npc.life < 700)
            {
                npc.aiStyle = 14;
            }
        }
    }
}
