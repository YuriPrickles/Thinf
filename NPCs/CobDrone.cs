using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{

    public class CobDrone : ModNPC
    {
        int buffTimer = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 25;
            npc.damage = 5;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 26;
            npc.value = Item.buyPrice(0, 0, 3, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<Corn>());
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 && !spawnInfo.player.ZoneDungeon && spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.02f;
            }
            return 0;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            Vector2 destination = player.Center + new Vector2(0, -100);
            int dustSpawnAmount = 32;
            for (int i = 0; i < dustSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                Vector2 dustOffset = currentRotation.ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 450, DustID.YellowTorch, null, 0, default, 1.4f);
                dust.noGravity = true;
            }
            buffTimer++;
            if (buffTimer == 240)
            {
                for (int i = 0; i < Main.maxNPCs; ++i)
                {
                    NPC npcToBuff = Main.npc[i];
                    if (npcToBuff.active && npcToBuff.Distance(npc.Center) <= 450 && !npcToBuff.friendly && !npcToBuff.boss && !npcToBuff.dontTakeDamage && !npcToBuff.immortal && npcToBuff.type != npc.type)
                    {
                        npcToBuff.life += 10;
                        npcToBuff.lifeMax += 10;
                        npcToBuff.HealEffect(10);
                        npcToBuff.defense += 1;
                        CombatText.NewText(npcToBuff.getRect(), Color.Yellow, "Stats buffed!", true);
                    }
                }
                buffTimer = 0;
            }

            npc.velocity = npc.DirectionTo(destination) * 1.5f;
        }
    }
}