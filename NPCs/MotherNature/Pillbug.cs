using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs.MotherNature
{
    public class Pillbug : ModNPC
    {
        float pitch = 0;
        int frameTimer = 0;
        int frameCount = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50;
            npc.damage = 240;
            npc.defense = 900;
            npc.knockBackResist = 0f;
            npc.width = 16;
            npc.friendly = false;
            npc.height = 16;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.dontCountMe = true;
            npc.friendly = false;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = true;
            npc.netAlways = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            npc.velocity *= -1;
            npc.velocity = npc.velocity.RotatedByRandom(MathHelper.ToRadians(90));
            if (!Main.dedServ)
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PegHit"), 2, pitch);
            }
            pitch += 0.2f;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            npc.velocity.Y += 0.5f;

        }

        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
