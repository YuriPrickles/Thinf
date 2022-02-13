using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
    public class FlyingSaucer : ModNPC
    {
        int frameTimer = 0;
        int frameCount = 0;
        int bounceDelay = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.damage = 120;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 16;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.velocity.X = 4;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (npc.velocity.X != 4 || npc.velocity.X != -4)
            {
                npc.velocity.X = 4;
            }

            GetNPCFrame(frameCount);

            frameTimer++;
            if (frameTimer == 6)
            {
                frameTimer = 0;
                frameCount++;
                if (frameCount >= 7)
                {
                    frameCount = 0;
                }
            }

            if (npc.velocity != Vector2.Zero)
            {
                npc.noTileCollide = false;
            }
            else
            {
                npc.noTileCollide = true;
                npc.rotation = npc.velocity.ToRotation();
            }
            bounceDelay++;
            if ((npc.collideX || npc.collideY) && bounceDelay >= 60)
            {
                //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GARY_GET_THE_FUCK_OUT_OF_THAT_IMP_PUNT_OH_NO_HE_CANT_HEAR_US_HE_HAS_AIRPODS_ON_GARY_NOOOOOO"));
                npc.velocity.X *= -1f;
                bounceDelay = 0;
            }
        }
        private void GetNPCFrame(int framenum)
        {
            npc.frame.Y = npc.height * framenum;
            return;
        }
    }
}
