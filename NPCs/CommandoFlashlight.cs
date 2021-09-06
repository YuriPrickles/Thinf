using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class CommandoFlashlight : ModNPC
    {
        float rotat = 0;
        int phaseCount = 0;
        int phaseZeroTimer = 0;
        int phaseZeroCount = 0;
        int offset = 280;
        int phaseOneTimer = 0;
        int phaseOneCount = 0;
        int offsetTwo = 360;
        int offsetTwoHeight = 280;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2400;
            npc.damage = 27;
            npc.defense = 7;
            npc.knockBackResist = 0f;
            npc.width = 80;
            npc.height = 68;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<Bioluminator>());
            ModNameWorld.downedFlashlight = true;
        }
        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Vector3(255, 255, 0) / 255f);
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            npc.spriteDirection = -npc.direction;

            if (phaseCount == 0)
            {
                Vector2 destination = player.Center + new Vector2(offset, 0);
                phaseZeroTimer++;
                if (phaseZeroTimer >= 300)
                {
                    Projectile proj = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<Brightbolt>(), 18, 2);
                    proj.velocity = proj.DirectionTo(player.Center) * 6;
                    phaseZeroTimer = 0;
                    offset *= -1;
                    phaseZeroCount++;
                }
                if (npc.Distance(destination) >= 30)
                {
                    npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, destination, 0.45f)) * 7;
                }
                if (phaseZeroCount >= 5)
                {
                    phaseCount = 1;
                    phaseZeroTimer = 0;
                    phaseZeroCount = 0;
                }
            }

            if (phaseCount == 1)
            {
                if (phaseOneCount >= 10)
                {
                    phaseOneCount = 0;
                    phaseCount = 0;
                }
                Vector2 destination = player.Center + new Vector2(offsetTwo + offsetTwoHeight);
                phaseOneTimer++;
                if (phaseOneTimer < 0)
                {
                    if (phaseOneTimer % 45 == 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Projectile proj = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<Brightbolt>(), 18, 2);
                            proj.velocity = (proj.DirectionTo(player.Center) * 5).RotatedByRandom(MathHelper.ToRadians(10));
                        }
                    }
                    rotat += 0.02f;
                    npc.velocity = npc.DirectionTo(player.Center + Vector2.One.RotatedBy(rotat) * 360f) * 10;
                }
                if (phaseOneTimer % 20 == 0 && phaseOneTimer > 0)
                {
                    Projectile proj = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<Brightbolt>(), 18, 2);
                    proj.velocity = (proj.DirectionTo(player.Center) * 10).RotatedByRandom(MathHelper.ToRadians(10));
                    if (phaseOneTimer >= 60)
                    {
                        phaseOneCount++;
                        offsetTwoHeight *= -1;
                        phaseOneTimer = -120;
                    }
                }
                if (npc.Distance(destination) >= 30 && phaseOneTimer >= 0)
                {
                    npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, destination, 0.25f)) * 5;
                }
            }
        }
    }
}
