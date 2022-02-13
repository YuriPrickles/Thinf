using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class RoyalTomatoMan : ModNPC
    {
        int throwTimer = 0;
        int expertTantrumTimer = 0;
        int jumpDelay = 0;
        public override void SetStaticDefaults()
        {

        }
        int frameNumber = 1;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 320;
            npc.damage = 53;
            npc.defense = 21;
            npc.knockBackResist = 0.8f;
            npc.width = 42;
            npc.height = 66;
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
            Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), Main.rand.Next(12) + 9);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 && Main.hardMode && spawnInfo.player.GetModPlayer<MyPlayer>().ZoneTomatoTown)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.65f;
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
            if (npc.Distance(player.Center) <= 240 && Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
            {
                npc.velocity.X = 0;
                throwTimer++;
                if (throwTimer >= 60)
                {
                    Projectile proj = Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 5, ModContent.ProjectileType<KetchupBottle>(), 20, 0);
                    proj.hostile = true;
                    proj.friendly = false;
                    throwTimer = 0 + Main.rand.Next(8, 15);
                    if (Main.expertMode)
                    {
                        throwTimer = 25;
                    }
                }
            }
            else
            {
                if (npc.velocity.X == 0)
                {
                    jumpDelay++;
                    if (jumpDelay >= 30)
                    {
                        Jump(3, 6);
                        jumpDelay = 0;
                    }
                }
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 4);
            }

        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.Y -= verticalSpeed;
            npc.velocity.X = horizontalSpeed * npc.direction;
        }
    }
}