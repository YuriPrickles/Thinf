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
    public class SeedSpitter : ModNPC
    {
        int seedSpitTimer = 0;
        int timeUntilNoShield = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apple Seed Spitter");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 120;
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

        public override void NPCLoot()
        {
            Thinf.Kaboom(npc.Center);
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.velocity.Y = npc.DirectionTo(player.Center).Y * 10;
            npc.velocity.X = npc.DirectionTo(player.Center + new Vector2(400, 0)).X * 5;

            seedSpitTimer++;
            if (seedSpitTimer >= 180 && seedSpitTimer % 12 == 0)
            {
                Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 5).RotatedByRandom(MathHelper.ToRadians(15)), ModContent.ProjectileType<AppleSeed>(), 60, 5);
                if (seedSpitTimer >= 240)
                {
                    Projectile proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 5).RotatedByRandom(MathHelper.ToRadians(15)), ModContent.ProjectileType<AppleSeed>(), 60, 5);
                    proj.scale = 3;
                    proj.Size *= 3;
                    seedSpitTimer = 0;
                }
            }
            timeUntilNoShield++;
            if (timeUntilNoShield >= 360)
            {
                npc.dontTakeDamage = false;
            }
            else
            {
                int dustSpawnAmount = 16;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * (4 * 16), DustID.Enchanted_Gold, null, 0, default, 1.5f);
                    dust.noGravity = true;
                }
            }
        }
    }
}
