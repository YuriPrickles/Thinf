using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class AcornSpirit : ModNPC
    {
        float rotat = 0;
        int phaseCount = 0;
        int phaseZeroTimer = 0;
        int phaseOneTimer = 0;
        int phaseTwoTimer = 0;
        public override void SetStaticDefaults()
        {
            //your mother
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 800;
            npc.damage = 12;
            npc.defense = 4;
            npc.knockBackResist = 0f;
            npc.width = 48;
            npc.height = 48;
            npc.value = Item.buyPrice(0, 12, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit41;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.netAlways = true;
            npc.boss = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Mushroom;

            ModNameWorld.downedAcornSpirit = true;
            Item.NewItem(npc.getRect(), ItemID.Acorn, Main.rand.Next(21, 45));
            Item.NewItem(npc.getRect(), ItemID.Wood, Main.rand.Next(125, 250));
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Aglet);
            }

            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.WoodenBoomerang, 1, false, PrefixID.Legendary);
            }

            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Spear, 1, false, PrefixID.Legendary);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Aglet, 1, false, PrefixID.Warding);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.ThrowingKnife, Main.rand.Next(250, 500));
            }

            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Blowpipe, 1, false, PrefixID.Unreal);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.CordageGuide, 1, false, PrefixID.Warding);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Radar, 1, false, PrefixID.Warding);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.ClimbingClaws, 1, false, PrefixID.Warding);
            }

            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.WandofSparking, 1, false, PrefixID.Mythical);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Umbrella);
            }

            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.Glowstick, Main.rand.Next(50, 85));
            }

            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.LivingLoom);
            }

            if (Main.rand.Next(25) == 0)
            {
                Item.NewItem(npc.getRect(), ItemID.WoodHelmet, 1, false, PrefixID.Guarding);
                Item.NewItem(npc.getRect(), ItemID.WoodBreastplate, 1, false, PrefixID.Guarding);
                Item.NewItem(npc.getRect(), ItemID.WoodGreaves, 1, false, PrefixID.Guarding);
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (phaseCount == 0)
            {

                phaseZeroTimer++;
                Thinf.NPCGotoPlayer(npc, player, 3);
                if (phaseZeroTimer % 50 == 0)
                {
                    for (int i = 0; i < Main.rand.Next(2, 3); ++i)
                    {
                        Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<DeathAcorn>(), 6, 3);
                        projectile.velocity = (projectile.DirectionTo(player.Center) * 5f).RotatedByRandom(MathHelper.ToRadians(19));
                    }
                }
                if (phaseZeroTimer >= 420)
                {
                    phaseZeroTimer = 0;
                    phaseCount = 1;
                }
            }

            if (phaseCount == 1)
            {
                rotat += 0.02f;
                npc.velocity = npc.DirectionTo(player.Center + Vector2.One.RotatedBy(rotat) * 280f) * 9;

                phaseOneTimer++;
                if (phaseOneTimer % 9 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<DeathAcorn>(), 9, 3);
                    projectile.velocity = projectile.DirectionTo(player.Center) * 3.5f;
                }
                if (phaseOneTimer >= 50)
                {
                    phaseOneTimer = 0;
                    phaseCount = 2;
                }
            }

            if (phaseCount == 2)
            {
                phaseTwoTimer++;
                if (phaseTwoTimer % 50 == 0)
                {
                    Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<DeathAcorn>(), 14, 3);
                }
                Thinf.NPCGotoPlayer(npc, player, 1.2f);
                if (phaseTwoTimer == 720)
                {
                    phaseTwoTimer = 0;
                    phaseCount = 0;
                }
            }
        }
    }
}
