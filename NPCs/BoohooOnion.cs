using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class BoohooOnion : ModNPC
    {
        int tearTimer = 0;
        int expertTantrumTimer = 0;
        int jumpDelay = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crying Onion");
        }
        int frameNumber = 1;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 30;
            npc.damage = 18;
            npc.defense = 4;
            npc.knockBackResist = 1.1f;
            npc.width = 32;
            npc.height = 26;
            npc.value = Item.buyPrice(0, 0, 3, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<Onion>(), Main.rand.Next(3) + 1);
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
            if (npc.Distance(player.Center) <= 150)
            {
                tearTimer++;
                if (tearTimer == 8)
                {
                    Projectile.NewProjectileDirect(npc.Center + new Vector2(7, 0), new Vector2(0, 7), ProjectileID.RainNimbus, 12, 1, Main.myPlayer);
                    Projectile.NewProjectileDirect(npc.Center + new Vector2(-7, 0), new Vector2(0, 7), ProjectileID.RainNimbus, 12, 1, Main.myPlayer);
                    tearTimer = 0;
                }
                if (jumpDelay == 0)
                {
                    Jump(7, 10);
                }
                jumpDelay++;
                if (jumpDelay == 60)
                {
                    jumpDelay = 0;
                }
            }
            else
            {
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 2);
            }
            if (Main.expertMode)
            {
                expertTantrumTimer++;
                if (expertTantrumTimer >= 160 && expertTantrumTimer % 6 == 0)
                {
                    Projectile water = Projectile.NewProjectileDirect(npc.Center + new Vector2(5, 0), new Vector2(0, 5).RotatedByRandom(MathHelper.ToRadians(360)), ProjectileID.WaterStream, 14, 1, Main.myPlayer);
                    water.hostile = true;
                    water.friendly = false;
                }
                if (expertTantrumTimer == 280)
                {
                    expertTantrumTimer = -120;
                }
            }
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.Y -= verticalSpeed;
            npc.velocity.X = horizontalSpeed * npc.direction;
        }
    }
}