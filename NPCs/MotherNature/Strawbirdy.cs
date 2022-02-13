using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.MotherNature
{
    public class Strawbirdy : ModNPC
    {
        int frameTimer = 0;
        int frameCount = 0;
        int potatoShoot = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1540;
            npc.damage = 40;
            npc.defense = 90;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 26;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.Item48;
            npc.DeathSound = SoundID.Item27;
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

            npc.spriteDirection = npc.direction;

            npc.frame.Y = GetFrame(frameCount);
            frameTimer++;
            if (frameTimer >= 6)
            {
                frameTimer = 0;
                frameCount++;
                if (frameCount >= 2)
                {
                    frameCount = 0;
                }
            }
            potatoShoot++;
            if (potatoShoot == 150)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, 3), ProjectileID.IcewaterSpit, 56, 6);
                potatoShoot = 0;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.immuneTime = 0;
        }
        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
