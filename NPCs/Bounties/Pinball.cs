using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Bounties
{
    public class Pinball : ModNPC
    {
        int phaseCount = 0;
        int spinTimer = 0;
        int spinCount = 0;
        int phaseTwoEscapeTimer = 0;

        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lifeMax = 70000;
            npc.damage = 240;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 64;
            npc.height = 64;
            npc.value = Item.buyPrice(5, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            if (npc.velocity != Vector2.Zero)
            {
                npc.noTileCollide = false;
                npc.rotation++;
            }
            else
            {
                npc.noTileCollide = true;
                npc.rotation = npc.velocity.ToRotation();
            }
            if (npc.collideX || npc.collideY)
            {
                npc.noTileCollide = true;
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GARY_GET_THE_FUCK_OUT_OF_THAT_IMP_PUNT_OH_NO_HE_CANT_HEAR_US_HE_HAS_AIRPODS_ON_GARY_NOOOOOO"));
                npc.velocity *= -1f;
            }
            if (phaseCount == 0)
            {
                spinTimer++;
                if (spinTimer >= 300)
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 30;
                    spinTimer = 0;
                }
            }
        }
    }
}
