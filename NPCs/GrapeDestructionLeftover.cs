using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class GrapeDestructionLeftover : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50;
            npc.damage = 10;
            npc.defense = 5;
            npc.knockBackResist = 1.5f;
            npc.width = 22;
            npc.height = 26;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            animationType = NPCID.Harpy;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<Grapes>());
            Item.NewItem(npc.getRect(), ItemID.Heart);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
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

            npc.rotation += 0.3f * npc.direction;
            if (npc.velocity.Y == 0)
            {
                Jump(5, 4);
            }
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.Y -= verticalSpeed;
            npc.velocity.X = horizontalSpeed * npc.direction * Main.rand.NextFloat(0.2f, 1.5f);
        }
    }
}