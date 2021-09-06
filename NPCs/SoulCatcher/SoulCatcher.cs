using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items;
using Thinf.NPCs.Nightmares;
using Thinf.Projectiles;
using static Thinf.ModNameWorld;
using static Thinf.MyPlayer;

namespace Thinf.NPCs.SoulCatcher
{
    [AutoloadBossHead]
    public class SoulCatcher : ModNPC
    {
        bool isDefeated = false;
        bool hasInsulted = false;
        int nightmareMobSummonTimerWhenEnraged = 0;
        int phaseFourHomingSoulBombTimer = 0;
        bool phaseFourHasSummonedNightmareMan = false;
        int phaseThreeTimer = 0;
        bool phaseOneHasMoved = false;
        int phaseOneMoveTimer = 0;
        int frameNumber = 0;
        bool introDone = false;
        int introCount = 0;
        int phaseCount = -1;
        int phaseZeroMoveTimer = 0;
        int phaseZeroMoveCount = 0;
        int outroCount = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 160000;   //boss life
            npc.damage = 42;  //boss damage
            npc.defense = 18;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 128;
            npc.value = Item.buyPrice(9, 46, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath14;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Anima_Mea");
            npc.boss = true;
            npc.netAlways = true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                frameNumber++;
                if (frameNumber >= 5)
                {
                    frameNumber = 0;
                }
                npc.frame.Y = frameNumber * (640 / 5);
            }
        }
        public override bool CheckDead()
        {
            if (isDefeated)
            {
                return true;
            }
            npc.life = 5000000;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            phaseCount = 69;
            npc.velocity = Vector2.Zero;
            isDefeated = true;
            return false;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void NPCLoot()
        {
            downedSoulCatcher = true;
            Item.NewItem(npc.getRect(), ItemID.SuperHealingPotion, 10 + Main.rand.Next(10));
            Item.NewItem(npc.getRect(), ModContent.ItemType<Linimisifrififlium>(), 30 + Main.rand.Next(31));
            Main.NewText("Soul Catcher was slain by Healing Overdose.", new Color(225, 25, 25));
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                introCount = 21039;
                if (!introDone && !hasInsulted)
                {
                    hasInsulted = true;
                    Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "You died before I could even attack, that was pretty easy...")].lifeTime = 150;
                }
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (isDefeated)
            {
                outroCount++;
                switch (outroCount)
                {
                    case 1:
                        Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "ALRIGHT, THAT'S IT!")].lifeTime = 120;
                        break;
                    case 120:
                        Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "IT'S TIME FOR YOU TO FEEL REAL SUFFERING")].lifeTime = 180;
                        break;
                    case 300:
                        Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "GAHHHAHAHAHAHAHAHAHAHAHAHAHHAHAHAHA")].lifeTime = 420;
                        break;
                }
                if (outroCount >= 300)
                {
                    npc.dontTakeDamage = false;
                    npc.defense = 99999999;
                    screenShake = true;
                    npc.life += 5000000;
                    npc.HealEffect(5000000);
                    if (npc.life >= 2140483647)
                    {
                        npc.NPCLoot();
                        Main.PlaySound(SoundID.PlayerKilled, npc.Center);
                        for (int g = 0; g < 7; g++)
                        {
                            int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                            Main.gore[goreIndex].scale = 1.5f;
                            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                            goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                            Main.gore[goreIndex].scale = 1.5f;
                            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                            goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                            Main.gore[goreIndex].scale = 1.5f;
                            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                            goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                            Main.gore[goreIndex].scale = 1.5f;
                            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        }
                        npc.life = 0;
                    }
                }
            }
            else
            {
                npc.spriteDirection = -npc.direction;
            }
            if (!introDone)
            {
                npc.dontTakeDamage = true;
                introCount++;
                switch (introCount)
                {
                    case 45:
                        Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "Dude I hate you so much")].lifeTime = 200;
                        break;
                    case 245:
                        Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "Stuffing those souls you released took like 2 months")].lifeTime = 330;
                        break;
                    case 575:
                        if (player.name == "LawnmowerKing" || player.name == "deplor" || player.name == "Sag" || player.name == "Deplor" || player.name == "callie" || player.name == "Callie" || player.name == "virginwarrior" || player.name == "Centiweeb" || player.name == "Centi" || player.name == "centiweeb" || player.name == "poopy doopy 2")
                        {
                            Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), "Your name may be cool but you are still going to die!")].lifeTime = 210;
                        }
                        else
                        {
                            Main.combatText[CombatText.NewText(npc.getRect(), new Color(242, 63, 63), $"Screw you {player.name}, your name sucks.")].lifeTime = 210;
                        }
                        break;
                }
                if (introCount == 785)
                {
                    npc.dontTakeDamage = false;
                    introCount = 0;
                    introDone = true;
                    phaseCount = 0;
                }
            }

            if (phaseCount == 0)
            {
                phaseZeroMoveTimer++;
                if (phaseZeroMoveTimer == 60)
                {
                    for (int i = 0; i < 25; ++i)
                    {
                        var velocity = new Vector2(Main.rand.Next(4, 8)).RotatedByRandom(MathHelper.ToRadians(360));
                        Projectile.NewProjectileDirect(npc.Center, velocity, ProjectileID.LostSoulHostile, 65, 5);
                    }
                    npc.velocity = npc.DirectionTo(player.Center) * 4;
                    phaseZeroMoveTimer = 0;
                    phaseZeroMoveCount++;
                }
                if (phaseZeroMoveCount == 3)
                {
                    phaseZeroMoveCount = 0;
                    phaseZeroMoveTimer = 0;
                    phaseCount = 1;
                    phaseOneHasMoved = false;
                }
            }
            if (!player.ZoneUnderworldHeight)
            {
                nightmareMobSummonTimerWhenEnraged++;
                if (nightmareMobSummonTimerWhenEnraged == 60)
                {
                    int type = 0;
                    int typeRand = Main.rand.Next(5);
                    switch (typeRand)
                    {
                        case 0:
                            type = ModContent.NPCType<NightmareFeederHead>();
                            break;
                        case 1:
                            type = ModContent.NPCType<NightmareHerpling>();
                            break;
                        case 2:
                            type = ModContent.NPCType<NightmareBat>();
                            break;
                        case 3:
                            type = ModContent.NPCType<NightmareHornet>();
                            break;
                        case 4:
                            type = ModContent.NPCType<NightmareTortoise>();
                            break;
                    }
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-250, 250), (int)npc.Center.Y, type);
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-250, 250), (int)npc.Center.Y, type);
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-250, 250), (int)npc.Center.Y, type);
                    nightmareMobSummonTimerWhenEnraged = 0;
                }
            }
            if (phaseCount == 1)
            {
                if (!phaseOneHasMoved)
                {
                    npc.velocity = npc.DirectionTo(player.Center + new Vector2(0, -100)) * 12;
                    phaseOneMoveTimer++;
                    if (phaseOneMoveTimer % 10 == 0)
                    {
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SoulBomb>(), 56, 1, Main.myPlayer)];
                        if (phaseOneMoveTimer % 20 == 0)
                        {
                            proj.ai[1] = 1;
                        }
                        else
                        {
                            proj.ai[1] = 2;
                        }
                        if (phaseOneMoveTimer == 60)
                        {
                            npc.velocity = Vector2.Zero;
                            phaseOneHasMoved = true;
                            phaseOneMoveTimer = 0;
                            phaseCount = 2;
                        }
                    }
                }
            }

            if (phaseCount == 2)
            {
                for (int i = 0; i < Main.rand.Next(7, 14); i++)
                {
                    NPC dummy = Main.npc[NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-250, 250), (int)npc.Center.Y, ModContent.NPCType<DummySpirit>())];
                    dummy.life = 2000;
                    dummy.lifeMax = 2000;
                    dummy.damage = 0;
                    dummy.defense = -10;
                }
                phaseCount = 3;
            }

            if (phaseCount == 3)
            {
                if (npc.Distance(player.Center) > 100)
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 5f;
                }
                phaseThreeTimer++;
                if (phaseThreeTimer >= 40 && phaseThreeTimer % 40 == 0)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SoulBomb>(), 45, 1)];
                    if (phaseThreeTimer % 80 == 0)
                    {
                        proj.ai[1] = 1;
                    }
                    else
                    {
                        proj.ai[1] = 2;
                    }
                    if (phaseThreeTimer == 160)
                    {
                        npc.velocity = Vector2.Zero;
                        phaseThreeTimer = 0;
                        phaseCount = 4;
                        phaseFourHasSummonedNightmareMan = false;
                    }
                }
            }

            if (phaseCount == 4)
            {
                phaseFourHomingSoulBombTimer++;
                if (phaseFourHomingSoulBombTimer == 150)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SoulBomb>(), 34, 1, 69, Main.myPlayer)];
                    if (Main.rand.Next(2) == 0)
                    {
                        proj.ai[1] = 2;
                    }
                    else
                    {
                        proj.ai[1] = 1;
                    }
                    phaseFourHomingSoulBombTimer = 0;
                }
                if (!phaseFourHasSummonedNightmareMan)
                {
                    int manSpawnAmount = 8;
                    for (int i = 0; i < manSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / manSpawnAmount) * i;
                        Vector2 owoffset = currentRotation.ToRotationVector2() * 2.5f;
                        NPC.NewNPC((int)(npc.Center + owoffset * 300f).X, (int)(npc.Center + owoffset * 300f).Y, ModContent.NPCType<NightmareMan>()); //code by eldrazi#2385
                        if (i == 7)
                        {
                            phaseFourHasSummonedNightmareMan = true;
                        }
                    }
                }
                if (NPC.AnyNPCs(ModContent.NPCType<NightmareMan>()))
                {
                    npc.velocity = Vector2.Zero;
                }
                else
                {
                    phaseFourHomingSoulBombTimer = 0;
                    phaseCount = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Nightmare>(), Thinf.ToTicks(30));
            npc.life += 5000;
            npc.HealEffect(5000);
        }
    }
}
