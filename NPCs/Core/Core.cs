using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.Core
{
    [AutoloadBossHead]
    public class Core : ModNPC
    {
        public enum State : int
        {
            Vibing,
            Disturbed,
            Fighting,
            DeadLOL,
            DoingThatHalfHPCutsceneThing
        }
        // why am i making these public why am i making these public why am i making these public why am i making these public why am i making these public why am i making these public
        public bool hastaunted = true;
        public float rotat = 0;
        public int timesInsulted = 0;
        public int phaseCount = 2;
        public int revolverBlast = 0;
        public int idleFrameTimer = 0;
        public State state = State.Vibing;
        public bool doDeathAnim = false;
        public int deathAnimTimer = 0;
        public int deathFrameCounter = 6;
        public int timesShotRevolver = 0;
        public int seedSpreadTimer = 0;
        public int movementChangeTimer = 0;
        public int phaseTwoMoveDir = 1;
        public string lastHitProjName = "Stupid Projectile";
        public int timesChangedDir = 0;
        public bool doneHalftimeCutscene = false;
        public int cutsceneTimer = 0;
        public int phaseThreeTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 92;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 250000;
            npc.damage = 200;
            npc.defense = 118;
            npc.knockBackResist = 0f;
            npc.width = 80;
            npc.height = 80;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.townNPC = true;
            npc.immortal = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.life = 450000;
            npc.damage = 240;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = $"The Core of {Main.worldName}";
            potionType = ItemID.SuperHealingPotion;
        }
        public override bool CheckDead()
        {
            music = 0;
            npc.velocity = Vector2.Zero;
            idleFrameTimer = 0;
            state = State.DeadLOL;
            doDeathAnim = true;
            npc.damage = 0;
            npc.dontTakeDamage = true;
            revolverBlast = 0;
            npc.life = 1;
            npc.boss = false;
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.immortal = true;
                music = -1;
                npc.life = npc.lifeMax;
                npc.boss = false;
                npc.dontTakeDamage = true;
                state = State.Disturbed;
                phaseCount = 0;
                timesInsulted = 0;
                npc.velocity = Vector2.Zero;
                if (!hastaunted)
                {
                    if (!Main.dedServ)
                        Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/LoserHorn").WithVolume(0.8f));
                    hastaunted = true;
                }
                    //if (!hastaunted)
                    //{
                    //    hastaunted = true;
                    //    if (!Main.expertMode)
                    //    {
                    //        int tauntRand = Main.rand.Next(3);
                    //        switch (tauntRand)
                    //        {
                    //            case 0:
                    //                if (!Main.dedServ)
                    //                    Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt1").WithVolume(1.5f));
                    //                break;
                    //            case 1:
                    //                if (!Main.dedServ)
                    //                    Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt2").WithVolume(1.5f));
                    //                break;
                    //            case 2:
                    //                if (!Main.dedServ)
                    //                    Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt3").WithVolume(1.5f));
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Main.rand.Next() == 1)
                    //        {
                    //            int tauntRand = Main.rand.Next(3);
                    //            switch (tauntRand)
                    //            {
                    //                case 0:
                    //                    if (!Main.dedServ)
                    //                        Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt1").WithVolume(1.5f));
                    //                    break;
                    //                case 1:
                    //                    if (!Main.dedServ)
                    //                        Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt2").WithVolume(1.5f));
                    //                    break;
                    //                case 2:
                    //                    if (!Main.dedServ)
                    //                        Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTaunt3").WithVolume(1.5f));
                    //                    break;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (!Main.dedServ)
                    //                Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CoreTauntExpert").WithVolume(1.5f));
                    //        }
                    //    }
                    //}
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (doDeathAnim)
            {
                npc.velocity = Vector2.Zero;
                npc.frame.Y = GetFrame(deathFrameCounter);
                if (deathFrameCounter < 89)
                {
                    deathAnimTimer++;
                    if (deathAnimTimer >= 6)
                    {
                        deathFrameCounter++;
                        deathAnimTimer = 0;
                    }
                }
                else
                {
                    Thinf.Kaboom(npc.Center);
                    Main.NewText($"The Core of {Main.worldName} has been destroyed!", 175, 75, 255);

                    if (!Main.dedServ)
                    {
                        if (Main.rand.Next(100) == 0)
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/VineBoom").WithVolume(1.5f));
                        }
                        else
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/RegularBoom").WithVolume(1.5f));
                        }
                    }
                    Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 0);
                    for (int g = 0; g < 7; g++)
                    {

                    }
                    int loot = Main.rand.Next(5);
                    switch (loot)
                    {
                        case 0:
                            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.Ripcore>());
                            break;
                        case 1:
                            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.AppleAssaultBlaster>());
                            break;
                        case 2:
                            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Weapons.AppleStick>());
                            break;
                    }
                    coreDestroyed = true;
                    npc.life = 0;
                    npc.active = false;
                }
            }
            if (npc.life <= 125000 && !doneHalftimeCutscene && state != State.DeadLOL)
            {
                timesChangedDir = 0;
                npc.dontTakeDamage = true;
                state = State.DoingThatHalfHPCutsceneThing;
                phaseCount = -1;
                npc.velocity = Vector2.Zero;
                cutsceneTimer++;
                switch (cutsceneTimer)
                {
                    case 1:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "YOu really think that")].lifeTime = 239;
                        break;
                    case 240:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "that you'll be super cool")].lifeTime = 120;
                        break;
                    case 360:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "When the world is literally gonna die")].lifeTime = 180;
                        break;
                    case 540:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "Is that what you want??")].lifeTime = 120;
                        break;
                    case 660:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "well Then...")].lifeTime = 180;
                        break;
                    case 840:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "I'm going to kill you")].lifeTime = 180;
                        break;
                    case 1020:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.Salmon, "For real this time I was just testing you")].lifeTime = 360;
                        break;
                    case 1380:
                        doneHalftimeCutscene = true;
                        state = State.Fighting;
                        phaseCount = 1;
                        break;
                }
            }
            if (state != State.DeadLOL && state != State.DoingThatHalfHPCutsceneThing)
            {
                if (timesInsulted > 5)
                {
                    state = State.Fighting;
                }
                else if (downedHerbalgamation && downedPM && downedBlizzard && downedSoulCatcher)
                {
                    state = State.Disturbed;
                }
                else
                {
                    state = State.Vibing;
                }

                if (state == State.Vibing)
                {
                    npc.damage = 0;
                    npc.dontTakeDamage = true;
                    npc.frame.Y = GetFrame(0);
                    int dustSpawnAmount = 32;
                    for (int i = 0; i < dustSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                        Vector2 dustOffset = currentRotation.ToRotationVector2();
                        Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * (24 * 16), DustID.Enchanted_Gold, null, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                }
                if (state == State.Disturbed)
                {
                    int dustSpawnAmount = 32;
                    for (int i = 0; i < dustSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                        Vector2 dustOffset = currentRotation.ToRotationVector2();
                        Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * (6 * 16), DustID.Enchanted_Gold, null, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    npc.damage = 0;
                    npc.dontTakeDamage = false;
                    npc.frame.Y = GetFrame(1);
                }
                if (state == State.Fighting)
                {
                    hastaunted = false;
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Applecalypse");
                    idleFrameTimer++;
                    if (idleFrameTimer == 6)
                    {
                        npc.frame.Y = GetFrame(2);
                    }
                    if (idleFrameTimer == 12)
                    {
                        npc.frame.Y = GetFrame(3);
                    }
                    if (idleFrameTimer == 18)
                    {
                        npc.frame.Y = GetFrame(4);
                    }
                    if (idleFrameTimer == 24)
                    {
                        npc.frame.Y = GetFrame(5);
                        idleFrameTimer = 0;
                    }
                    npc.boss = true;
                    npc.damage = 200;
                    npc.immortal = false;
                    npc.dontTakeDamage = false;

                    if (phaseCount == 0)
                    {
                        if (timesShotRevolver >= 5)
                        {
                            timesShotRevolver = 0;
                            phaseCount = 1;
                            if (doneHalftimeCutscene)
                            {
                                Thinf.QuickSpawnNPC(npc, ModContent.NPCType<AppleCannon>());
                                Thinf.QuickSpawnNPC(npc, ModContent.NPCType<SeedSpitter>());
                            }
                            else
                            {
                                if (Main.rand.Next(2) == 1)
                                {
                                    Thinf.QuickSpawnNPC(npc, ModContent.NPCType<AppleCannon>());
                                }
                                else
                                {
                                    Thinf.QuickSpawnNPC(npc, ModContent.NPCType<SeedSpitter>());
                                }
                            }
                        }
                        revolverBlast++;
                        if (revolverBlast >= 120 && revolverBlast % 6 == 0)
                        {
                            Projectile projectile = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center)) * 5, ProjectileID.Bullet, 30, 2);
                            projectile.hostile = true;
                            projectile.friendly = false;
                            Main.PlaySound(SoundID.Item36, npc.Center);
                            if (revolverBlast >= 156)
                            {
                                timesShotRevolver++;
                                revolverBlast = 0;
                            }
                        }
                    }

                    if (phaseCount == 1)
                    {
                        if (timesChangedDir >= 10)
                        {
                            movementChangeTimer = 0;
                            timesChangedDir = 0;
                            seedSpreadTimer = 0;
                            phaseCount = 2;
                        }
                        Vector2 destination = new Vector2(350, -350) * phaseTwoMoveDir;
                        for (int i = 0; i < 10; ++i)
                        {
                            Dust dust;
                            Vector2 position = npc.position + new Vector2(0, 80);
                            dust = Dust.NewDustDirect(position, 80, 0, DustID.Flare, 0f, 0f, 0, new Color(255, 255, 255), 3.830233f);
                            dust.noGravity = true;
                            if (npc.Distance(player.Center + destination) >= 300)
                            {
                                dust.velocity = npc.velocity * -1;
                            }
                            else
                            {
                                dust.velocity = new Vector2(0, 7);
                            }
                        }

                        if (npc.Distance(player.Center + destination) >= 64)
                        {
                            npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, player.Center + destination, 0.75f)) * 9;
                        }
                        else
                        {
                            rotat++;
                            npc.velocity = npc.DirectionTo((player.Center + destination) + Vector2.One.RotatedBy(rotat) * 64f) * 2;
                        }

                        movementChangeTimer++;
                        if (movementChangeTimer >= 120)
                        {
                            phaseTwoMoveDir *= -1;
                            movementChangeTimer = 0;
                            timesChangedDir++;
                        }

                        seedSpreadTimer++;
                        if (doneHalftimeCutscene)
                        {
                            int projectileSpawnAmount = 16;
                            if (seedSpreadTimer == 80)
                            {
                                for (int i = 0; i < projectileSpawnAmount; ++i)
                                {
                                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                    proj.tileCollide = false;
                                    proj.timeLeft = Thinf.ToTicks(14);
                                }
                            }
                            if (seedSpreadTimer == 85)
                            {
                                for (int i = 0; i < projectileSpawnAmount; ++i)
                                {
                                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                    Vector2 projectileVelocity = currentRotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(22.5f));
                                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                    proj.tileCollide = false;
                                    proj.timeLeft = Thinf.ToTicks(14);
                                }
                            }
                            if (seedSpreadTimer == 90)
                            {
                                for (int i = 0; i < projectileSpawnAmount; ++i)
                                {
                                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                    proj.tileCollide = false;
                                    proj.timeLeft = Thinf.ToTicks(14);
                                }
                            }
                            if (seedSpreadTimer == 95)
                            {
                                for (int i = 0; i < projectileSpawnAmount; ++i)
                                {
                                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                    Vector2 projectileVelocity = currentRotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(22.5f));
                                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                    proj.tileCollide = false;
                                    proj.timeLeft = Thinf.ToTicks(14);
                                }
                                seedSpreadTimer = 0;
                            }
                        }
                        else if (seedSpreadTimer >= 80)
                        {
                            int projectileSpawnAmount = 16;
                            for (int i = 0; i < projectileSpawnAmount; ++i)
                            {
                                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity * 2.3f, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                proj.tileCollide = false;
                                proj.timeLeft = Thinf.ToTicks(14);
                            }
                            seedSpreadTimer = 0;
                        }
                    }

                    if (phaseCount == 2)
                    {
                        npc.velocity = Vector2.Zero;
                        int blasterSpawnRand = Main.rand.Next(4);
                        switch (blasterSpawnRand)
                        {
                            case 0:
                                NPC.NewNPC((int)player.Center.X, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X + 200, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X - 200, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X + 400, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X - 400, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X - 600, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X + 800, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)player.Center.X - 800, (int)(player.Center.Y - 450), ModContent.NPCType<GapplerBlaster>());
                                break;
                            case 1:
                                NPC.NewNPC((int)player.Center.X - 300, (int)(player.Center.Y - 300), ModContent.NPCType<GapplerBlaster>());
                                NPC npc1 = Main.npc[NPC.NewNPC((int)player.Center.X - 300, (int)(player.Center.Y + 300), ModContent.NPCType<GapplerBlaster>())];
                                npc1.rotation = MathHelper.ToRadians(-90);
                                NPC npc2 = Main.npc[NPC.NewNPC((int)player.Center.X + 300, (int)(player.Center.Y - 300), ModContent.NPCType<GapplerBlaster>())];
                                npc2.rotation = MathHelper.ToRadians(90);
                                NPC npc3 = Main.npc[NPC.NewNPC((int)player.Center.X + 300, (int)(player.Center.Y + 300), ModContent.NPCType<GapplerBlaster>())];
                                npc3.rotation = MathHelper.ToRadians(180);
                                break;
                            case 2:
                                NPC npc4 = Main.npc[NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y), ModContent.NPCType<GapplerBlaster>())];
                                npc4.rotation = MathHelper.ToRadians(90);
                                NPC npc5 = Main.npc[NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y + 200), ModContent.NPCType<GapplerBlaster>())];
                                npc5.rotation = MathHelper.ToRadians(90);
                                NPC npc6 = Main.npc[NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y + 400), ModContent.NPCType<GapplerBlaster>())];
                                npc6.rotation = MathHelper.ToRadians(90);
                                NPC npc7 = Main.npc[NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y - 200), ModContent.NPCType<GapplerBlaster>())];
                                npc7.rotation = MathHelper.ToRadians(90);
                                NPC npc8 = Main.npc[NPC.NewNPC((int)player.Center.X + 600, (int)(player.Center.Y - 400), ModContent.NPCType<GapplerBlaster>())];
                                npc8.rotation = MathHelper.ToRadians(90);
                                break;
                            case 3:
                                NPC npc9 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc9.rotation = npc9.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc10 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc10.rotation = npc10.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc11 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc11.rotation = npc11.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc12 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc12.rotation = npc12.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc13 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc13.rotation = npc13.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc14 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc14.rotation = npc14.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                NPC npc15 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-400, 400)), (int)(player.Center.Y + Main.rand.Next(-400, 400)), ModContent.NPCType<GapplerBlaster>())];
                                npc15.rotation = npc15.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                                break;
                            default:
                                break;
                        }
                        if (doneHalftimeCutscene)
                        {
                            phaseCount = 3;
                        }
                        else
                        {
                            phaseCount = 0;
                        }
                    }

                    if (phaseCount == 3)
                    {
                        phaseThreeTimer++;
                        if (phaseThreeTimer % 15 == 0)
                        {
                            Projectile.NewProjectile(new Vector2(player.Center.X + (player.direction * Main.screenWidth), player.Center.Y + Main.rand.Next(-750, 750)), new Vector2(4 * -player.direction, 0), ModContent.ProjectileType<AppleSeed>(), 40, 0);
                            Projectile.NewProjectile(new Vector2(player.Center.X + (player.direction * Main.screenWidth), player.Center.Y + Main.rand.Next(-750, 750)), new Vector2(3 * -player.direction, 0), ModContent.ProjectileType<AppleSeed>(), 40, 0);
                            Projectile.NewProjectile(new Vector2(player.Center.X + (player.direction * Main.screenWidth), player.Center.Y + Main.rand.Next(-750, 750)), new Vector2(4 * -player.direction, 0), ModContent.ProjectileType<AppleSeed>(), 40, 0);
                            Projectile.NewProjectile(new Vector2(player.Center.X + (player.direction * Main.screenWidth), player.Center.Y + Main.rand.Next(-750, 750)), new Vector2(3 * -player.direction, 0), ModContent.ProjectileType<AppleSeed>(), 40, 0);
                        }
                        if (phaseThreeTimer % 180 == 0)
                        {
                            NPC npc9 = Main.npc[NPC.NewNPC((int)(player.Center.X + Main.rand.Next(-200, 200)), (int)(player.Center.Y + Main.rand.Next(-200, 200)), ModContent.NPCType<GapplerBlaster>())];
                            npc9.rotation = npc9.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                        }
                        if (phaseThreeTimer == 900)
                        {
                            phaseCount = 0;
                        }
                    }
                }
            }
        }
        public override bool CanChat()
        {
            return (state != State.Fighting || state != State.DeadLOL || state != State.DoingThatHalfHPCutsceneThing);
        }
        public override string GetChat()
        {
            int textRand = Main.rand.Next(3);
            if (state == State.Vibing)
            {
                switch (textRand)
                {
                    case 0:
                        return "Just A suggestion: why don't you find soMETHING BETTER TO DO!! IS THIS ALL YOU DO? SOD OFF";
                    case 1:
                        return "THIS is NO PLACE for you! LEAVE!";
                    case 2:
                        return "LEAVE me alONE!! I dont CARE about yOUR TEA! I WILL ALWAYS BE A COFFEE PERSON!";
                }
            }
            if (state == State.Disturbed)
            {
                switch (textRand)
                {
                    case 0:
                        return $"Stop tryIng to hit me with that {lastHitProjName}!";
                    case 1:
                        return "Can you PLEASE PLEASE shut up I DONT need to invest in crypto";
                    case 2:
                        return "You REALLY don't want to destroy the core of the world...";
                }
            }
            return "Stop posting about Among Us! I'm tired of seeing it! My friends on TikTok send me memes, on Discord it's fucking memes! I was in a server, right? And all of the channels are just Among Us stuff. I showed my Champion underwear to my girlfriend and the logo, I flipped it and I said, 'Hey, babe, when the underwear is sus!' Haha, ding ding ding ding ding ding ding, ding-ding-ding! I fucking looked at a trashcan and I said, 'That's a bit sussy!' I looked at my penis, I think of an astronaut's helmet and I go, 'Penis? More like pen-sus!' Aaaaaaargh!";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (state == State.Disturbed)
            {
                button = "Insult";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Player player = Main.player[npc.target];
            if (firstButton)
            {
                timesInsulted++;
                if (timesInsulted > 5)
                {
                    state = State.Fighting;
                }
                if (timesInsulted >= 2)
                {
                    Main.npcChatText = "do you really have nothing better to do";
                }
                int textRand = Main.rand.Next(4);
                switch (textRand)
                {
                    case 0:
                        Main.npcChatText = "141.37.227.172";
                        break;
                    case 1:
                        Main.npcChatText = "You're gonna get yourself into big trouble!";
                        break;
                    case 2:
                        Main.npcChatText = "GIALBKEMGPQWENTCNJ#PJ#@%I)(";
                        break;
                    case 3:
                        Main.npcChatText = $"SHUT UP SHUT UP SHUT UP SHUT UP SHUT UP";
                        break;
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            lastHitProjName = projectile.Name;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (item.melee)
            {
                lastHitProjName = item.Name;
            }
        }

        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }

    public class Coremera : ModPlayer
    {
        public override void PostUpdate()
        {
        }
        public override void ModifyScreenPosition()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<Core>())
                {
                    Core core = Main.npc[i].modNPC as Core;
                    if (core.state == Core.State.DoingThatHalfHPCutsceneThing)
                    {
                        for (int j = 0; j < Main.maxPlayers; j++)
                        {
                            Player player = Main.player[j];
                            int time = 240;
                            if (player.active && !player.dead)
                            {
                                if (player.statLife <= 100)
                                {
                                    time = Thinf.ToTicks(30);
                                }
                                player.AddBuff(ModContent.BuffType<GhostMode>(), time);
                            }
                        }
                        Main.screenPosition = core.npc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                    }
                }
            }
        }
    }
}
