using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class GrapeDestruction : ModNPC
    {
        int grapeTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        int frameNumber = 1;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 65;
            npc.damage = 10;
            npc.defense = 10;
            npc.knockBackResist = 1.4f;
            npc.width = 48;
            npc.height = 64;
            npc.value = Item.buyPrice(0, 0, 3, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            animationType = NPCID.Harpy;
        }
        public override void NPCLoot()
        {
            for (int i = 0; i < 3; ++i)
            {
                Thinf.QuickSpawnNPC(npc, ModContent.NPCType<GrapeDestructionLeftover>());
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 && !spawnInfo.player.ZoneDungeon && spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.04f;
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
            Thinf.NPCGotoPlayer(npc, player, 2f);
            grapeTimer++;
            if (grapeTimer >= 36 && grapeTimer % 12 == 0)
            {
                if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.EmeraldBolt, 1, 1, Main.myPlayer);
                    projectile.velocity = projectile.DirectionTo(player.Center).RotatedByRandom(MathHelper.ToRadians(20)) * 5;
                    projectile.hostile = true;
                    projectile.friendly = false;
                    projectile.tileCollide = true;
                    projectile.timeLeft = 120;
                }
                if (grapeTimer == 72)
                {
                    grapeTimer = 0;
                }
            }
        }
    }
}