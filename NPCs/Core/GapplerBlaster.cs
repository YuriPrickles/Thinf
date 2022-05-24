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
    public class GapplerBlaster : ModNPC
    {
        int laserTimer = 0;
        int timeUntilNoShield = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 69420;
            npc.damage = 0;
            npc.defense = 20000;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 34;
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


            laserTimer++;
            if (laserTimer >= 300 && laserTimer % 20 == 0)
            {
                Projectile.NewProjectileDirect(npc.Center, new Vector2(0, 25).RotatedBy(npc.rotation), ModContent.ProjectileType<AppleLaser>(), 100, 5);
                if (laserTimer >= 480)
                {
                    Thinf.Kaboom(npc.Center);
                    npc.life = 0;
                    npc.active = false;
                }
            }
        }
    }
}
