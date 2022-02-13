using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
    public class VilePottedPlant : ModNPC
    {
        int frameCount = 0;
        int idleFrameTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 70;   //boss life
            npc.damage = 0;  //boss damage
            npc.defense = 25;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 54;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.friendly = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.type == ProjectileID.TinyEater || projectile.hostile)
            {
                return false;
            }
            return true;
        }
        public override void AI()
        {
            npc.velocity.Y += 1;
            npc.frame.Y = GetFrame(frameCount);
            idleFrameTimer++;
            if (idleFrameTimer == 6)
            {
                frameCount++;
                if (frameCount >= 8)
                {
                    frameCount = 0;
                }
                idleFrameTimer = 0;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Projectile.NewProjectileDirect(npc.Center, new Vector2(0, -2).RotatedByRandom(MathHelper.ToRadians(90)), ProjectileID.TinyEater, (int)damage, 0, Player.FindClosest(npc.Center, 10000, 10000)).melee = false;
        }
        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
