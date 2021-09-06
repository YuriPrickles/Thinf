using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;

namespace Thinf.NPCs.Blizzard
{
    [AutoloadBossHead]
    public class Blizzard : ModNPC
    {
        bool isEnraged = false;
        int dashTimer = 0;
        int phaseCount = 1;
        int knifeTimer = 0;
        int knifeCounter = 0;
        int moveCheckPhaseThree = 0;
        int moveTimerPhaseThree = 0;
        int moveDirection;
        int knifeTimerTwo = 0;
        int knifeCounterTwo = 0;
        int knifeTimerThree = 0;
        int knifeCounterThree = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = 10;
            npc.lifeMax = 400000;
            npc.damage = 175;
            npc.defense = 30;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 68;
            npc.value = Item.buyPrice(2, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.Item48;
            npc.DeathSound = SoundID.Item27;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cold_Slice");
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            ModNameWorld.downedBlizzard = true;
            Item.NewItem(npc.getRect(), ModContent.ItemType<FrozenEssence>(), Main.rand.Next(20) + 35);
            Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(12) + 15);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (!target.ZoneSnow)
            {
                damage *= 3;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, Thinf.ToTicks(5));
            if (!target.ZoneSnow)
            {
                target.AddBuff(BuffID.Chilled, Thinf.MinutesToTicks(1));
                target.AddBuff(BuffID.Frostburn, Thinf.MinutesToTicks(1));
                target.AddBuff(BuffID.Frozen, Thinf.ToTicks(5));
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
            npc.spriteDirection = -npc.direction;

            if (!player.ZoneSnow)
            {
                isEnraged = true;
            }
            else
            {
                isEnraged = false;
            }
            if (phaseCount == 1)
            {
                dashTimer++;
                if (dashTimer >= Thinf.ToTicks(3) && dashTimer % 60 == 0)
                {
                    npc.aiStyle = -1;
                    npc.velocity = npc.DirectionTo(player.Center) * 15f;
                    if (moveTimerPhaseThree % 10 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, -npc.velocity * 0.4f, ModContent.ProjectileType<IceKnife>(), 56, 3, player.whoAmI);
                    }
                    Main.PlaySound(SoundID.Item28, npc.Center);
                    if (dashTimer == Thinf.ToTicks(6))
                    {
                        npc.aiStyle = 10;
                        dashTimer = 0;
                        if (!isEnraged)
                        {
                            phaseCount = 2;
                        }
                        if (isEnraged)
                        {
                            phaseCount = 4;
                        }
                    }
                }
            }

            if (phaseCount == 2)
            {
                knifeTimer++;
                if (knifeTimer == 50)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnife>(), 120, 3)];
                    proj.timeLeft = 1;
                    proj.velocity = proj.DirectionTo(player.Center) * 20f;
                    float numberProjectiles = 3;
                    float rotation = MathHelper.ToRadians(45);
                    proj.position += Vector2.Normalize(proj.velocity) * 45f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = proj.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1.5f;
                        Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), 90, 3, player.whoAmI);
                    }
                    Main.PlaySound(SoundID.Item28, npc.Center);
                    if (knifeCounter == 5)
                    {
                        phaseCount = 3;
                        knifeTimer = 0;
                        knifeCounter = 0;
                        if (npc.Center.X > player.Center.X)
                        {
                            moveDirection = -1;
                        }
                        else
                        {
                            moveDirection = 1;
                        }
                        npc.aiStyle = -1;
                    }
                    knifeTimer = 0;
                    knifeCounter++;
                }
            }

            if (phaseCount == 3)
            {
                if (moveCheckPhaseThree == 0)
                {
                    npc.velocity = npc.DirectionTo(player.Center + new Vector2(moveDirection * 150, -150)) * 15f;
                    moveCheckPhaseThree = 1;
                }

                if (moveCheckPhaseThree == 1)
                {
                    moveTimerPhaseThree++;
                    if (moveTimerPhaseThree >= Thinf.ToTicks(1))
                    {
                        npc.velocity = new Vector2(moveDirection * -6, 0);

                        if (moveTimerPhaseThree % 10 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.FrostShard, 60, 3, player.whoAmI);
                        }

                        if (moveTimerPhaseThree == Thinf.ToTicks(4))
                        {
                            moveTimerPhaseThree = 0;
                            if (npc.life > 300000)
                            {
                                phaseCount = 1;
                            }
                            else
                            {
                                phaseCount = 4;
                            }
                            moveCheckPhaseThree = 0;
                            npc.aiStyle = 10;
                        }
                    }
                }
            }

            if (npc.life <= 300000 || isEnraged)
            {
                npc.defense = 18;
                if (phaseCount == 4)
                {
                    npc.aiStyle = -1;
                    if (npc.Distance(player.Center) > 75)
                    {
                        npc.velocity = npc.DirectionTo(player.Center) * 2f;
                    }

                    knifeTimerTwo++;
                    if (knifeTimerTwo == 45)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnifeTwo>(), 120, 3)];
                        proj.timeLeft = 1;
                        proj.velocity = proj.DirectionTo(player.Center) * 20f;
                        float numberProjectiles = 5;
                        float rotation = MathHelper.ToRadians(22.5f);
                        proj.position += Vector2.Normalize(proj.velocity) * 45f;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = proj.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.6f;
                            Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnifeTwo>(), 90, 3, player.whoAmI);
                        }
                        knifeCounterTwo++;
                        knifeTimerTwo = 0;
                    }

                    if (knifeCounterTwo == 4)
                    {
                        int tileSpawnAmount = 2;
                        for (int i = 0; i < tileSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / tileSpawnAmount) * i;
                            Vector2 playerOffset = currentRotation.ToRotationVector2() * 4f;
                            if (NPC.CountNPCS(NPCID.IceElemental) < 3)
                            {
                                NPC npc = Main.npc[NPC.NewNPC((int)(player.Center + playerOffset * 50f).X, (int)(player.Center + playerOffset * 50f).Y, NPCID.IceElemental)];
                                npc.lifeMax = 1000;
                                npc.life = 1000;
                                npc.defense = 20;
                                npc.GivenName = "Lil' Chillers";
                                npc.damage = 0;
                            }
                        }
                        npc.aiStyle = 10;
                        knifeTimerTwo = 0;
                        knifeCounterTwo = 0;
                        if (npc.life <= 180000)
                        {
                            phaseCount = 5;
                        }
                        else
                        {
                            phaseCount = 1;
                        }
                    }
                }

                if (NPC.AnyNPCs(NPCID.IceElemental))
                {
                    npc.dontTakeDamage = true;
                    npc.GivenName = "Blizzard (Immune)";
                    npc.damage = 0;
                }

                else
                {
                    npc.dontTakeDamage = false;
                    npc.GivenName = "Blizzard";
                    npc.damage = 175;
                }

                if (npc.life <= 180000 || isEnraged)
                {
                    if (phaseCount == 5)
                    {
                        dashTimer++;
                        if (dashTimer >= Thinf.ToTicks(3) && dashTimer % 30 == 0)
                        {
                            npc.aiStyle = -1;
                            npc.velocity = npc.DirectionTo(player.Center) * 14f;
                            Main.PlaySound(SoundID.Item28, npc.Center);
                            if (dashTimer == Thinf.ToTicks(6))
                            {
                                npc.aiStyle = 10;
                                dashTimer = 0;
                                phaseCount = 6;
                            }
                        }
                    }

                    if (phaseCount == 6)
                    {
                        knifeTimerThree++;
                        if (knifeTimerThree == 4)
                        {
                            Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnife>(), 120, 3)];
                            proj.timeLeft = 1;
                            proj.velocity = proj.DirectionTo(player.Center) * 20f;
                            float numberProjectiles = 1;
                            if (Main.expertMode)
                            {
                                numberProjectiles = 2;
                            }
                            float rotation = MathHelper.ToRadians(45);
                            proj.position += Vector2.Normalize(proj.velocity) * 45f;
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = proj.velocity.RotatedByRandom(10);
                                Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), 90, 3, player.whoAmI);
                            }
                            knifeTimerThree = 0;
                            knifeCounterThree++;

                            if (knifeCounterThree == 20)
                            {
                                Projectile proj2 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnife>(), 120, 3)];
                                proj2.timeLeft = 1;
                                proj2.velocity = proj2.DirectionTo(player.Center) * 20f;
                                float numberProjectiles2 = 8;
                                if (Main.expertMode)
                                {
                                    numberProjectiles2 = 12;
                                }
                                float rotation2 = MathHelper.ToRadians(5f);
                                proj2.position += Vector2.Normalize(proj2.velocity) * 45f;
                                for (int i = 0; i < numberProjectiles2; i++)
                                {
                                    Vector2 perturbedSpeed2 = proj2.velocity.RotatedBy(MathHelper.Lerp(-rotation2, rotation2, i / (numberProjectiles2 - 1))) * 0.5f;
                                    Projectile.NewProjectile(npc.Center, perturbedSpeed2, ProjectileID.FrostWave, 90, 3, player.whoAmI);
                                }
                                knifeCounterThree = 0;
                                if (!isEnraged)
                                {
                                    phaseCount = 1;
                                }
                                else
                                {
                                    if (!Main.expertMode)
                                        phaseCount = 5;
                                    else
                                        phaseCount = 4;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
