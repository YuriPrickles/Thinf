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
        PhaseOne, // leaves, dust, and crops
        PhaseTwo, // fish, doobie wyvern, and pillbug
        PhaseThree
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

        SafeCrop safeCrop = SafeCrop.Tomato;
        KarlaPhase phaseCount = KarlaPhase.PhaseOne;
        KarlaAttacks attackCount = KarlaAttacks.Idle;
        int attackTimer = 0;
        int dir = 1;
        int dir2 = 1;
        int frameTimer = 0;
        int frameCount = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mother Nature");
            Main.npcFrameCount[npc.type] = 28;
        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lifeMax = 500000;
            npc.damage = 280;
            npc.defense = 310;
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 128;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath4;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TempTheme");
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

            npc.frame.Y = GetFrame(frameCount);

            // Animation Code
            switch (attackCount)
            {
                case KarlaAttacks.Idle:
                    frameTimer++;
                    if (frameTimer >= 10)
                    {
                        frameCount++;
                        if (frameCount >= 4)
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
                        if (frameCount < 27)
                        {
                            frameCount++;
                        }
                        frameTimer = 0;
                    }
                    break;
                case KarlaAttacks.Crops:
                    frameTimer++;
                    if (frameTimer >= 10)
                    {
                        if (frameCount < 17)
                        {
                            frameCount++;
                        }
                        else
                        {
                            switch (safeCrop)
                            {
                                case SafeCrop.Tomato:
                                    frameCount = 18;
                                    break;
                                case SafeCrop.Pumpkin:
                                    frameCount = 19;
                                    break;
                                case SafeCrop.Lemon:
                                    frameCount = 20;
                                    break;
                                case SafeCrop.Cabbage:
                                    frameCount = 21;
                                    break;
                            }
                        }
                        frameTimer = 0;
                    }
                    break;
                case KarlaAttacks.Leaves:
                    frameTimer++;
                    if (frameTimer >= 10)
                    {
                        if (frameCount < 10)
                        {
                            frameCount++;
                        }
                        frameTimer = 0;
                    }
                    break;
            }

            if (npc.alpha > 0)
            {
                npc.damage = 0;
            }
            else
            {
                npc.damage = 280;
            }
            if (npc.life > npc.lifeMax * 0.66f)
            {
                phaseCount = KarlaPhase.PhaseOne;
            }
            if (npc.life <= npc.lifeMax * 0.66f)
            {
                phaseCount = KarlaPhase.PhaseTwo;
            }
            if (npc.life <= npc.lifeMax * 0.33f)
            {
                phaseCount = KarlaPhase.PhaseThree;
            }
            if (npc.alpha > 0)
            {
                npc.alpha -= 5;
            }
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        frameCount = 11;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 2:
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        attackCount = KarlaAttacks.DoobieWyvern;
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        frameCount = 11;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 2:
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        attackCount = KarlaAttacks.DoobieWyvern;
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        frameCount = 11;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 2:
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        attackCount = KarlaAttacks.DoobieWyvern;
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        frameCount = 11;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 2:
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        attackCount = KarlaAttacks.DoobieWyvern;
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        frameCount = 22;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 1:
                                        attackCount = KarlaAttacks.Fish;
                                        attackCount = KarlaAttacks.PillbugPeggle;
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
                case KarlaAttacks.Fish:
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
                                        attackCount = KarlaAttacks.DoobieWyvern;
                                        frameCount = 11;
                                        npc.velocity = Vector2.Zero;
                                        break;
                                    case 2:
                                        attackCount = KarlaAttacks.PillbugPeggle;
                                        attackCount = KarlaAttacks.DoobieWyvern;
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

        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
