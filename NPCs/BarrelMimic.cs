using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
    public class BarrelMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barrel Mimic");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = 39;  //5 is the flying AI
            npc.lifeMax = 2500;   //boss life
            npc.damage = 124;  //boss damage
            npc.defense = 28;    //boss defense
            npc.knockBackResist = 0.1f;
            npc.width = 30;
            npc.height = 48;
            npc.value = Item.buyPrice(0, 1, 20, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            animationType = NPCID.GiantTortoise;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(3) == 0)
                Item.NewItem(npc.getRect(), mod.ItemType("OldKey"));
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneChestWasteland ? 20f : 0f;
        }

        public override void AI()
        {
            npc.spriteDirection = -npc.direction;
        }
    }
}
