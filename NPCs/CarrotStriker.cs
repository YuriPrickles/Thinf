using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
    public class CarrotStriker : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrot Striker");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = 71;  //5 is the flying AI
            npc.lifeMax = 25;   //boss life
            npc.damage = 42;  //boss damage
            npc.defense = 20000;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 64;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit38;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;

        }

        public override void AI()
        {
        }
    }
}
