using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class Pearstrike : ModNPC
    {
        int strikeTimer = 0;
        int jumpTimer = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 75;
            npc.damage = 2;
            npc.defense = 14;
            npc.knockBackResist = 1.4f;
            npc.width = 36;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 0, 3, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(15) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<RadioReceiver>());
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss3 && !spawnInfo.player.ZoneDungeon && spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
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

            jumpTimer++;
            if (jumpTimer >= 100)
            {
                if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                {
                    Jump(4, 3);
                }
                jumpTimer = 0;
            }

            strikeTimer++;
            if (strikeTimer >= 60 && strikeTimer % 15 == 0)
            {
                Projectile projectile = Projectile.NewProjectileDirect(new Vector2(player.Center.X, npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarkerForPear>(), 0, 2);
                if (strikeTimer == 90)
                {
                    strikeTimer = -180;
                }
            }
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.X = horizontalSpeed * npc.direction;
            npc.velocity.Y -= verticalSpeed;
        }
    }
}