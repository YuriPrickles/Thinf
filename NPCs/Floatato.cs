using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class Floatato : ModNPC
    {
        int potatoShoot = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floatato");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = 44;
            npc.lifeMax = 260;
            npc.damage = 42;
            npc.defense = 18;
            npc.knockBackResist = 0f;
            npc.width = 64;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[24] = true;
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

            potatoShoot++;
            if (potatoShoot == 120)
            {
                Vector2 potatoVelocity = Vector2.Normalize(player.Center - npc.Center) * 12;
                Projectile.NewProjectile(npc.Center, potatoVelocity, ModContent.ProjectileType<BouncyTater>(), 34, 6);
                potatoShoot = 0;
            }
        }
    }
}
