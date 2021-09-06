using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
            DeadLOL
        }

        public int timesInsulted = 0;
        public int phaseCount = 2;
        int revolverBlast = 0;
        int idleFrameTimer = 0;
        public State state = State.Vibing;
        bool doDeathAnim = false;
        int deathAnimTimer = 0;
        int deathFrameCounter = 6;
        int timesShotRevolver = 0;
        int seedSpreadTimer = 0;
        int movementChangeTimer = 0;
        int phaseTwoMoveDir = 1;
        int timesChangedDir = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 92;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 100000;
            npc.damage = 200;
            npc.defense = 60;
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

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = $"The Core of {Main.worldName}";
            potionType = ItemID.SuperHealingPotion;
        }
        public override bool CheckDead()
        {
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
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (doDeathAnim)
            {
                npc.frame.Y = GetFrame(deathFrameCounter);
                if (deathFrameCounter < 91)
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
                    Main.NewText($"The Core of {Main.worldName} has been destroyed!", 175, 75, 255);

                    Main.PlaySound(SoundID.Item14, npc.Center);
                    Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 0);
                    for (int g = 0; g < 7; g++)
                    {
                        int goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                    coreDestroyed = true;
                    npc.life = 0;
                    npc.active = false;
                }
            }
            if (state != State.DeadLOL)
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
                            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<AppleCannon>());
                            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<SeedSpitter>());
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
                            Vector2 position = npc.Center;
                            dust = Dust.NewDustDirect(position, 80, 0, 127, 0f, 0f, 0, new Color(255, 255, 255), 3.830233f);
                            dust.noGravity = true;
                            if (npc.Distance(destination) >= 50)
                            {
                                dust.velocity = npc.velocity * -1;
                            }
                            else
                            {
                                dust.velocity = new Vector2(0, 7);
                            }
                        }

                        npc.velocity = npc.DirectionTo(player.Center + destination) * 9;
                        movementChangeTimer++;
                        if (movementChangeTimer >= 240)
                        {
                            phaseTwoMoveDir *= -1;
                            movementChangeTimer = 0;
                            timesChangedDir++;
                        }

                        seedSpreadTimer++;
                        if (seedSpreadTimer >= 80)
                        {
                            int projectileSpawnAmount = 16;
                            for (int i = 0; i < projectileSpawnAmount; ++i)
                            {
                                float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                                Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                                Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, ModContent.ProjectileType<AppleSeed>(), 40, 1)];
                                proj.tileCollide = false;
                                proj.timeLeft = Thinf.ToTicks(14);
                            }
                            seedSpreadTimer = 0;
                        }
                    }

                    if (phaseCount == 2)
                    {
                        npc.velocity = Vector2.Zero;
                        int blasterSpawnRand = Main.rand.Next(3);
                        switch (blasterSpawnRand)
                        {
                            case 0:
                                NPC.NewNPC((int)npc.Center.X, (int)(npc.Center.Y - 250), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)npc.Center.X + 200, (int)(npc.Center.Y - 250), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)npc.Center.X - 200, (int)(npc.Center.Y - 250), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)npc.Center.X + 400, (int)(npc.Center.Y - 250), ModContent.NPCType<GapplerBlaster>());
                                NPC.NewNPC((int)npc.Center.X - 400, (int)(npc.Center.Y - 250), ModContent.NPCType<GapplerBlaster>());
                                break;
                            case 1:
                                NPC.NewNPC((int)npc.Center.X - 300, (int)(npc.Center.Y - 300), ModContent.NPCType<GapplerBlaster>());
                                NPC npc1 = Main.npc[NPC.NewNPC((int)npc.Center.X - 300, (int)(npc.Center.Y + 300), ModContent.NPCType<GapplerBlaster>())];
                                npc1.rotation = MathHelper.ToRadians(-90);
                                NPC npc2 = Main.npc[NPC.NewNPC((int)npc.Center.X + 300, (int)(npc.Center.Y - 300), ModContent.NPCType<GapplerBlaster>())];
                                npc2.rotation = MathHelper.ToRadians(90);
                                NPC npc3 = Main.npc[NPC.NewNPC((int)npc.Center.X + 300, (int)(npc.Center.Y + 300), ModContent.NPCType<GapplerBlaster>())];
                                npc3.rotation = MathHelper.ToRadians(180);
                                break;
                            case 2:
                                NPC npc4 = Main.npc[NPC.NewNPC((int)npc.Center.X + 500, (int)(npc.Center.Y), ModContent.NPCType<GapplerBlaster>())];
                                npc4.rotation = MathHelper.ToRadians(90);
                                NPC npc5 = Main.npc[NPC.NewNPC((int)npc.Center.X + 500, (int)(npc.Center.Y + 200), ModContent.NPCType<GapplerBlaster>())];
                                npc5.rotation = MathHelper.ToRadians(90);
                                NPC npc6 = Main.npc[NPC.NewNPC((int)npc.Center.X + 500, (int)(npc.Center.Y + 400), ModContent.NPCType<GapplerBlaster>())];
                                npc6.rotation = MathHelper.ToRadians(180);
                                NPC npc7 = Main.npc[NPC.NewNPC((int)npc.Center.X + 500, (int)(npc.Center.Y - 200), ModContent.NPCType<GapplerBlaster>())];
                                npc7.rotation = MathHelper.ToRadians(180);
                                NPC npc8 = Main.npc[NPC.NewNPC((int)npc.Center.X + 500, (int)(npc.Center.Y - 400), ModContent.NPCType<GapplerBlaster>())];
                                npc8.rotation = MathHelper.ToRadians(180);
                                break;
                            default:
                                break;
                        }
                        phaseCount = 0;
                    }
                }
            }
        }
        public override bool CanChat()
        {
            return (state != State.Fighting);
        }
        public override string GetChat()
        {
            int textRand = Main.rand.Next(3);
            if (state == State.Vibing)
            {
                switch (textRand)
                {
                    case 0:
                        return "What do you want? Get out of here!";
                    case 1:
                        return "This is no place for you!";
                    case 2:
                        return "Please leave me alone...";
                }
            }
            if (state == State.Disturbed)
            {
                switch (textRand)
                {
                    case 0:
                        return "Stop trying to hit me, you s-sussy baka!";
                    case 1:
                        return "Leave if you know what's best for you, dear.";
                    case 2:
                        return "You REALLY don't want to destroy the literal core of the world...";
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
            if (firstButton)
            {
                timesInsulted++;
                if (timesInsulted > 5)
                {
                    state = State.Fighting;
                }
                if (timesInsulted >= 3)
                {
                    Main.npcChatText = "ALRIGHT, THAT'S IT I WILL KILL YOU IF YOU INSULT ME ONE MORE TIME";
                }
                int textRand = Main.rand.Next(3);
                switch (textRand)
                {
                    case 0:
                        Main.npcChatText = "im n-not ugly... *sniffles*";
                        break;
                    case 1:
                        Main.npcChatText = "stop c-calling me stupid :(";
                        break;
                    case 2:
                        Main.npcChatText = "heuuhhhhaheuhhhehhhrrrrhuee";
                        break;
                }
            }
        }

        private int GetFrame(int framenum)
        {
            return 80 * framenum;
        }
    }
}
