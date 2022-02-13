using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Core
{
    public class ThePicture : ModNPC
    {
        int timeUntilRage = 0;
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Screnshot");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10000;
            npc.damage = 0;
            npc.defense = 100;
            npc.knockBackResist = 0f;
            npc.width = 192;
            npc.height = 101;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit8;
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

            timeUntilRage++;
            if (timeUntilRage >= 360)
            {
                if (timeUntilRage == 361)
                {
                    Main.NewText("I'm a beautiful screnshot!");
                }
                npc.dontTakeDamage = false;
                npc.damage = 200;
                npc.velocity.Y = npc.DirectionTo(player.Center).Y * 10;
                npc.velocity.X = npc.DirectionTo(player.Center).X * 3;
            }
        }
    }
}
