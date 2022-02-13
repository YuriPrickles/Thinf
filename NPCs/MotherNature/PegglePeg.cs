using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs.MotherNature
{
    public class PegglePeg : ModNPC
    {
        int frameTimer = 0;
        int frameCount = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 90000000;
            npc.knockBackResist = 0f;
            npc.width = 16;
            npc.friendly = true;
            npc.height = 16;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.dontCountMe = true;
            npc.friendly = true;
            npc.immortal = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            frameCount = 1;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return target.type == ModContent.NPCType<Pillbug>();
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return false;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.frame.Y = GetFrame(frameCount);

            npc.position = new Vector2(Main.screenPosition.X + (npc.ai[0] * 64) + (32 * (npc.ai[1] % 2) + 128), Main.screenPosition.Y + (npc.ai[1] * 96) + 128);
        }

        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
