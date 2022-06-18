using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles.MotherNatureProjectiles;

namespace Thinf.NPCs.MotherNature
{
    public enum KarlaPhase
    {
        CutsceneOne,
        CutsceneTwo,
        PhaseOne, // leaves, dust, and crops
        PhaseTwo, // fish, doobie wyvern, and pillbug
        PhaseThree //unused :p
    }
    public enum KarlaAttacks
    {
        Idle,
        DustAttack,
        Crops,
        Leaves,

        DoobieWyvern,
        Fish,
        PillbugPeggle
    }
    public enum SafeCrop
    {
        Tomato,
        Pumpkin,
        Lemon,
        Cabbage
    }
    public class MotherNature : ModNPC
    {
        bool diedDuringCutscene = false;
        int cutsceneTimer = 0;

        int deathCutsceneTimer = 0;

        SafeCrop safeCrop = SafeCrop.Tomato;
        KarlaPhase phaseCount = KarlaPhase.CutsceneOne;
        KarlaAttacks attackCount = KarlaAttacks.Idle;
        int attackTimer = 0;
        int dir = 1;
        int dir2 = 1;
        int frameTimer = 0;
        int frameCount = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mother Nature");
            Main.npcFrameCount[npc.type] = 26;
        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lifeMax = 500000;
            npc.damage = 280;
            npc.defense = 175;
            npc.knockBackResist = 0f;
            npc.width = 62;
            npc.height = 76;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SomeReallyWackyStuff");
            npc.netAlways = true;
        }

        public override bool CheckActive()
        {
            return false;
        }
        public override bool CheckDead()
        {
            npc.velocity = Vector2.Zero;
            npc.damage = 0;
            phaseCount = KarlaPhase.CutsceneTwo;
            npc.dontTakeDamage = true;
            npc.life = 5200;
            return false;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                cutsceneTimer = 21039;
                if (phaseCount == KarlaPhase.CutsceneOne && npc.life == 500000)
                {
                    diedDuringCutscene = true;
                    npc.life = 90;
                    Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "You dumb twat")].lifeTime = 150;
                }
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;


            npc.spriteDirection = npc.direction;

            npc.frame.Y = GetFrame(frameCount);

            // Animation Code
            if (phaseCount == KarlaPhase.CutsceneTwo)
            {
                deathCutsceneTimer++;
                if (deathCutsceneTimer >= 2120)
                {
                    npc.alpha++;
                }
                if (ModNameWorld.downedMom)
                {
                    if (deathCutsceneTimer >= 2120)
                    {
                        npc.alpha++;
                    }
                    switch (deathCutsceneTimer)
                    {
                        case 1:
                            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SomeReallyWackyStuff");
                            break;
                        case 20:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "GAH! This is taking me too long!")].lifeTime = 300;
                            break;
                        case 320:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "It's not really fair when you can't heal yourself!")].lifeTime = 300;
                            break;
                        case 620:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, $"You may... have brought me down to 0 HP...")].lifeTime = 300;
                            break;
                        case 920:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "But I'm not like every enemy you've encountered.")].lifeTime = 300;
                            break;
                        case 1220:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "I don't just die immediately after losing my HP.")].lifeTime = 300;
                            break;
                        case 1520:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "I just wanted to let you know that...")].lifeTime = 300;
                            break;
                        case 1820:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "You're gonna have to try slightly harder.")].lifeTime = 300;
                            break;
                        case 2120:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "See you later when I respawn!")].lifeTime = 200;
                            break;
                        case 2380:
                            music = 0;
                            ModNameWorld.downedMom = true;
                            Main.NewText("Karla was slain...", Color.DarkRed);
                            npc.active = false;
                            break;
                    }
                }
                else
                {
                    if (deathCutsceneTimer >= 20)
                    {
                        npc.alpha++;
                    }
                    switch (deathCutsceneTimer)
                    {
                        case 1:
                            music = 0;
                            break;
                        case 20:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "Ok then")].lifeTime = 100;
                            break;
                        case 120:
                            Main.NewText("Karla was slain...", Color.DarkRed);
                            if (!Main.dedServ)
                                Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/TacoBell").WithVolume(1.1f));
                            npc.active = false;
                            break;
                    }
                }
            }
            if (phaseCount == KarlaPhase.CutsceneOne)
            {
                frameTimer++;
                if (frameTimer >= 10)
                {
                    frameCount++;
                    if (frameCount >= 2)
                    {
                        frameCount = 0;
                    }
                    frameTimer = 0;
                }

                npc.dontTakeDamage = true;
                cutsceneTimer++;
                if (!ModNameWorld.willSkipTalking)
                {
                    switch (cutsceneTimer)
                    {
                        case 20:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "I know you're there, Raisin")].lifeTime = 300;
                            break;
                        case 320:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "Hiding behind that pathetic excuse of a player")].lifeTime = 300;
                            break;
                        case 620:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, $"And you {Main.player[npc.target].name}, what do you get out of this?")].lifeTime = 300;
                            break;
                        case 920:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "You're going to be bored once there's nothing else to beat.")].lifeTime = 300;
                            break;
                        case 1220:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "'Wow guys, we beat the final boss, thanks for watching this let's play!!!'")].lifeTime = 300;
                            break;
                        case 1520:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "And after that you're going to desert this timeline and open Premiere Pro or something")].lifeTime = 300;
                            break;
                        case 1820:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "Why can't you just live your life without screwing up other people?")].lifeTime = 300;
                            break;
                        case 2120:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "Let me connect to the WiFi for a sec...")].lifeTime = 200;
                            break;
                        case 2340:
                            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Otherworldly_Mom");
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "Alright done")].lifeTime = 300;
                            break;
                        case 2620:
                            npc.dontTakeDamage = false;
                            ModNameWorld.willSkipTalking = true;
                            phaseCount = KarlaPhase.PhaseOne;
                            break;
                    }
                }
                else
                {
                    switch (cutsceneTimer)
                    {
                        case 20:
                            Main.combatText[CombatText.NewText(npc.getRect(), Color.LimeGreen, "You know you're not actually killing me right???")].lifeTime = 200;
                            break;
                        case 220:
                            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Otherworldly_Mom");
                            break;
                        case 260:
                            npc.dontTakeDamage = false;
                            phaseCount = KarlaPhase.PhaseOne;
                            break;
                    }
                }
            }
            else
            {
                switch (attackCount)
                {
                    case KarlaAttacks.Idle:
                        frameTimer++;
                        if (frameTimer >= 10)
                        {
                            frameCount++;
                            if (frameCount >= 2)
                            {
                                frameCount = 0;
                            }
                            frameTimer = 0;
                        }
                        break;
                    case KarlaAttacks.DustAttack:
                        frameTimer++;
                        if (frameTimer >= 10)
                        {
                            if (frameCount < 21)
                            {
                                frameCount++;
                            }
                            else
                            {
                                frameCount = 20;
                            }
                            frameTimer = 0;
                        }
                        break;
                    case KarlaAttacks.Crops:
                        frameTimer++;
                        if (frameTimer >= 10)
                        {
                            switch (safeCrop)
                            {
                                case SafeCrop.Tomato:
                                    if (frameCount != 7)
                                    {
                                        frameCount++;
                                    }
                                    else
                                    {
                                        frameCount = 4;
                                    }
                                    break;
                                case SafeCrop.Pumpkin:
                                    if (frameCount != 11)
                                    {
                                        frameCount++;
                                    }
                                    else
                                    {
                                        frameCount = 8;
                                    }
                                    break;
                                case SafeCrop.Lemon:
                                    if (frameCount != 15)
                                    {
                                        frameCount++;
                                    }
                                    else
                                    {
                                        frameCount = 12;
                                    }
                                    break;
                                case SafeCrop.Cabbage:
                                    if (frameCount != 19)
                                    {
                                        frameCount++;
                                    }
                                    else
                                    {
                                        frameCount = 16;
                                    }
                                    break;
                            }
                            frameTimer = 0;
                        }
                        break;
                    case KarlaAttacks.Leaves:
                        frameTimer++;
                        if (frameTimer >= 10)
                        {
                            if (frameCount < 3)
                            {
                                frameCount++;
                            }
                            else
                            {
                                frameCount = 2;
                            }
                            frameTimer = 0;
                        }
                        break;

                    case KarlaAttacks.DoobieWyvern:
                        frameTimer++;
                        if (frameTimer >= 10)
                        {
                            if (frameCount < 23)
                            {
                                frameCount++;
                            }
                            else
                            {
                                frameCount = 22;
                            }
                            frameTimer = 0;
                        }
                        break;
                    case KarlaAttacks.Fish:
                        frameCount = 24;
                        break;
                    case KarlaAttacks.PillbugPeggle:
                        frameCount = 25;
                        break;
                }
            }
            if (npc.alpha > 0)
            {
                npc.damage = 0;
            }
            else if (phaseCount != KarlaPhase.CutsceneTwo && phaseCount != KarlaPhase.CutsceneTwo)
            {
                npc.damage = 280;
            }
            if (npc.life > npc.lifeMax * 0.5f && phaseCount != KarlaPhase.CutsceneOne && phaseCount != KarlaPhase.CutsceneTwo)
            {
                phaseCount = KarlaPhase.PhaseOne;
            }
            if (npc.life <= npc.lifeMax * 0.5f && phaseCount != KarlaPhase.CutsceneOne && phaseCount != KarlaPhase.CutsceneTwo)
            {
                phaseCount = KarlaPhase.PhaseTwo;
            }
            if (npc.alpha > 0 && phaseCount != KarlaPhase.CutsceneTwo)
            {
                npc.alpha -= 5;
            }
            if (phaseCount != KarlaPhase.CutsceneOne && phaseCount != KarlaPhase.CutsceneTwo)
            {
                switch (attackCount)
                {
                    case KarlaAttacks.Idle:
                        if (npc.Distance(player.Center) >= 1200)
                        {
                            npc.velocity = Vector2.Zero;
                            npc.alpha += 25;
                            if (npc.alpha >= 255)
                            {
                                npc.Center = player.Center + new Vector2(0, -150);
                            }
                        }
                        else
                        {
                            Vector2 targetPos = player.Center - new Vector2(0, 250);
                            npc.velocity.X += targetPos.X > npc.Center.X ? 0.2f : -0.2f;
                            npc.velocity.Y += (float)(Math.Cos((Math.Abs(npc.Center.Y - targetPos.Y) * MathHelper.Pi) / (3f * 180))) / 24;
                            if (npc.velocity.Y > 24f)
                            {
                                npc.velocity.Y *= .5f;
                            }
                        }
                        //Basically, when at peak, start going down.
                        //When at end, start going up
                        //This will probably not be exact, and you'll likely have to fiddle with either the 32 in Cos, or something to tighten up how far it can go from the player

                        attackTimer++;
                        if (attackTimer % 60 == 0)
                        {
                            //npc.velocity.X += (Vector2.Normalize(player.Center - npc.Center) * 9).X;
                            //npc.velocity.Y = (Vector2.Normalize(player.Center - npc.Center) * 4.5f).Y;
                        }
                        if (attackTimer >= 600)
                        {
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DustAttack;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case KarlaAttacks.DustAttack:

                        npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(520 * dir, 250 * -dir2)) * 8f, .1f);
                        //Above just slowly sets the velocity to go towards the target position.
                        if (npc.velocity.Length() < 20f)
                            npc.velocity *= .5f;

                        attackTimer++;
                        if (attackTimer % 15 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 3f), ModContent.ProjectileType<DustParticleGeometryDashWave>(), 98, 0);
                        }
                        if (attackTimer % 200 == 0)
                        {
                            dir *= -1;
                            dir2 *= Main.rand.Next(-1, 1);
                        }
                        if (attackTimer >= 600)
                        {
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;


                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case KarlaAttacks.Crops:
                        attackTimer++;
                        if (attackTimer % 20 == 0)
                        {
                            int damage = 65;
                            for (int i = 0; i < 4; i++)
                            {
                                int type = Main.rand.Next(4);
                                if (safeCrop == (SafeCrop)type)
                                {
                                    damage = 0;
                                }
                                switch (type)
                                {
                                    case 0: type = ModContent.ProjectileType<TomatoOfDeath>(); break;
                                    case 1: type = ModContent.ProjectileType<PumpkinOfDeath>(); break;
                                    case 2: type = ModContent.ProjectileType<LemonOfDeath>(); break;
                                    case 3: type = ModContent.ProjectileType<CabbageOfDeath>(); break;
                                }
                                Projectile.NewProjectile(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * (3f + Main.rand.NextFloat(0.1f, 0.5f))).RotatedByRandom(MathHelper.ToRadians(75)), type, damage, 0);
                            }
                        }
                        if (attackTimer >= 600)
                        {
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DustAttack;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }

                        }
                        break;
                    case KarlaAttacks.Leaves:
                        attackTimer++;

                        npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(0, -250)) * 8f, .1f);
                        //Above just slowly sets the velocity to go towards the target position.
                        if (npc.velocity.Length() < 20f)
                            npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop

                        if (attackTimer % 8 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.NextFloat(-2.2f, 2.2f), -5), ModContent.ProjectileType<BurningLeaf>(), 134, 0);
                        }
                        if (attackTimer >= 600)
                        {
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DustAttack;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case KarlaAttacks.DoobieWyvern:
                        npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(350 * -player.direction, -250)) * 10f, .1f);
                        //Above just slowly sets the velocity to go towards the target position.
                        if (npc.velocity.Length() < 20f)
                            npc.velocity *= .5f;
                        attackTimer++;

                        if (attackTimer == 1)
                        {
                            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<DoobieWyvern>());
                        }

                        if (attackTimer % 60 == 0)
                        {
                            NPC strawbirdy = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Strawbirdy>())];
                            strawbirdy.velocity.X = 4 * npc.direction;
                        }


                        if (attackTimer >= 600)
                        {
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                if (Main.npc[i].type == ModContent.NPCType<DoobieWyvern>())
                                {
                                    Main.npc[i].active = false;
                                }
                            }
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;


                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    case KarlaAttacks.Fish:

                        if (npc.Distance(player.Center) >= 1200)
                        {
                            npc.velocity = Vector2.Zero;
                            npc.alpha += 25;
                            if (npc.alpha >= 255)
                            {
                                npc.Center = player.Center + new Vector2(0, -150);
                            }
                        }
                        else
                        {
                            Vector2 targetPos = player.Center - new Vector2(0, 250);
                            npc.velocity.X += targetPos.X > npc.Center.X ? 0.2f : -0.2f;
                            npc.velocity.Y += (float)(Math.Cos((Math.Abs(npc.Center.Y - targetPos.Y) * MathHelper.Pi) / (3f * 180))) / 24;
                            if (npc.velocity.Y > 24f)
                            {
                                npc.velocity.Y *= .5f;
                            }
                        }
                        attackTimer++;

                        if (attackTimer % 120 == 0)
                        {
                            int type = ModContent.ProjectileType<ColorLooper>();
                            int projectileSpawnAmount = 8;
                            for (int i = 0; i < projectileSpawnAmount; ++i)
                            {
                                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                                Projectile projectile = Main.projectile[Projectile.NewProjectile(player.Center + projectileVelocity * 500f, Vector2.Zero, type, 80, 1, player.whoAmI)];
                                projectile.noDropItem = true;
                                projectile.velocity = projectile.DirectionTo(player.Center) * 4;
                            }
                        }
                        if (attackTimer >= 1200)
                        {
                            for (int i = 0; i < Main.maxProjectiles; i++)
                            {
                                if (Main.projectile[i].type == ModContent.ProjectileType<ColorLooper>())
                                {
                                    Main.projectile[i].active = false;
                                }
                            }
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;


                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.PillbugPeggle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case KarlaAttacks.PillbugPeggle:
                        npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(0, 350)) * 10f, .1f);
                        //Above just slowly sets the velocity to go towards the target position.
                        if (npc.velocity.Length() < 20f)
                            npc.velocity *= .5f;
                        attackTimer++;

                        if (attackTimer == 1)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 25; j++)
                                {
                                    NPC peg = Main.npc[NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<PegglePeg>())];
                                    peg.ai[0] = j;
                                    peg.ai[1] = i;
                                }
                            }
                        }

                        if (attackTimer % 30 == 0)
                        {
                            NPC pillbug = Main.npc[NPC.NewNPC((int)player.Center.X + Main.rand.Next(-150, 150), (int)player.Center.Y - 600, ModContent.NPCType<Pillbug>())];
                            pillbug.velocity = new Vector2(0, 5).RotatedByRandom(MathHelper.ToRadians(90));
                        }

                        if (attackTimer >= 1200)
                        {
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                if (Main.npc[i].type == ModContent.NPCType<PegglePeg>())
                                {
                                    Main.npc[i].active = false;
                                }
                            }
                            attackTimer = 0;
                            int switchRand = Main.rand.Next(3);
                            switch (phaseCount)
                            {
                                case KarlaPhase.PhaseOne:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 0;
                                            npc.velocity = Vector2.Zero;


                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Crops;
                                            safeCrop = (SafeCrop)Main.rand.Next(4);
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Leaves;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseTwo:
                                    switch (switchRand)
                                    {
                                        case 0:
                                            attackCount = KarlaAttacks.DoobieWyvern;
                                            frameCount = 22;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 1:
                                            attackCount = KarlaAttacks.Fish;
                                            frameCount = 11;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                        case 2:
                                            attackCount = KarlaAttacks.Idle;
                                            frameCount = 5;
                                            npc.velocity = Vector2.Zero;
                                            break;
                                    }
                                    break;
                                case KarlaPhase.PhaseThree:
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                }
            }
        }

        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
