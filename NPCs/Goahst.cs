using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class Goahst : ModNPC
    {
        float rotat = Main.rand.Next(360);
        int grapeTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }
        int frameNumber = 1;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 300;
            npc.damage = 90;
            npc.defense = 15;
            npc.knockBackResist = 1.4f;
            npc.width = 50;
            npc.height = 72;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.netAlways = true;
            animationType = NPCID.Harpy;
        }
        public override void NPCLoot()
        {

        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            rotat += 0.02f;
            npc.velocity = npc.DirectionTo(player.Center + Vector2.One.RotatedBy(rotat) * 512f) * 8;
            grapeTimer++;
            if (grapeTimer >= 120 && grapeTimer % 4 == 0)
            {
                if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.Fireball, 1, 1, Main.myPlayer);
                    projectile.velocity = projectile.DirectionTo(player.Center).RotatedByRandom(MathHelper.ToRadians(20)) * 5;
                    projectile.hostile = true;
                    projectile.friendly = false;
                    projectile.tileCollide = true;
                    projectile.timeLeft = 120;
                }
                if (grapeTimer == 128)
                {
                    grapeTimer = 0;
                }
            }
        }
    }
}