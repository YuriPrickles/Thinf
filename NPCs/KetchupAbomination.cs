using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class KetchupAbomination : ModNPC
    {
        bool secondPhase = false;
        int idleFrameTimer = 0;
        int tearTimer = 0;
        int expertTantrumTimer = 0;
        int jumpDelay = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 5;

        }
        int frameNumber = 1;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 750;
            npc.damage = 50;
            npc.defense = 28;
            npc.knockBackResist = 0f;
            npc.width = 56;
            npc.height = 40;
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
            Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), Main.rand.Next(7) + 5);
            Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(4) + 2);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 && Main.hardMode && spawnInfo.player.GetModPlayer<MyPlayer>().ZoneTomatoTown)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.91f;
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

            npc.spriteDirection = -npc.direction;
            idleFrameTimer++;
            if (idleFrameTimer == 6)
            {
                npc.frame.Y = GetFrame(0);
            }
            if (idleFrameTimer == 12)
            {
                npc.frame.Y = GetFrame(1);
            }
            if (idleFrameTimer == 18)
            {
                npc.frame.Y = GetFrame(2);
            }
            if (idleFrameTimer == 24)
            {
                npc.frame.Y = GetFrame(3);
            }
            if (idleFrameTimer == 32)
            {
                npc.frame.Y = GetFrame(4);
                idleFrameTimer = 0;
            }

            if (npc.velocity.X == 0)
            {
                jumpDelay++;
                if (jumpDelay >= 50)
                {
                    Jump(3, 9);
                    jumpDelay = 0;
                }
            }
            if (secondPhase)
            {
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 8);
            }
            else
            {
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 4);
            }
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                secondPhase = true;
            }
            if (secondPhase)
            {
                npc.damage = 72;
                npc.defense = 40;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (secondPhase)
            {
                npc.life += 50;
                npc.HealEffect(50);
                CombatText.NewText(npc.getRect(), Color.OrangeRed, "Nom");
            }
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.Y -= verticalSpeed;
            npc.velocity.X = horizontalSpeed * npc.direction;
        }
        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}