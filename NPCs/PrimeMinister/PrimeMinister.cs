using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.NPCs.PrimeMinister
{
    [AutoloadBossHead]
    public class PrimeMinister : ModNPC
    {
        int phaseOneFinishDelay = 0;
        int frameNumber = 0;
        int phaseCount = -1;
        int phaseNegOneTimer = 0;
        int phaseZeroDashCounter = 0;
        bool phaseZeroNearPlayerCheck = false;
        int phaseZeroPewPewGunTimer = 0;
        int phaseZeroPewPewCountdown = 0;
        int phaseZeroNumberOfTimesAlreadyDone = 0;
        int phaseOneCannonTimer = 0;
        int phaseOneCannonCount = 0;
        int phaseTwoShotgunTimer = 0;
        int phaseTwoShotgunCount = 0;
        int phaseTwoLaserTimer = 0;
        int phaseTwoLaserCount = 0;
        float phaseTwoLaserSpinCounter = 0;
        bool phaseTwoIsDoingShotgunAttack = false;
        int phaseThreeDashTimer = 0;
        int phaseThreeFastDashTimer = 0;
        int phaseThreeDashCount = 0;
        int phaseFourMachineGunTimer = 0;
        int phaseFourMachineGunCount = 0;

        /*mmm yes i am a very good leader yes yes i dont watch anime and i treat all people fairly
         and i i i uh um I DO NOT ABUSE MY POWER DONT SAY THAT BECAUSE IM A VERY GOOD LEADER
        -Prime minister, after killing the larvae and hypnotizing the bees*/
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 175000;
            npc.damage = 90;
            npc.defense = 94;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.width = 120;
            npc.height = 130;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Where_Your_Tax_Goes");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/WeLiveInASociety");
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                frameNumber++;
                if (frameNumber >= 6)
                {
                    frameNumber = 0;
                }
                npc.frame.Y = frameNumber * (780 / 6);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Prime Minister's Robot Bee";
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<PrimeMinisterCopter>());
            Main.NewText("<Prime Minister> HEY! That robot bee took me 2 months to buy using public funds!", Color.Yellow);
            for (int g = 0; g < 10; g++)
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
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.spriteDirection = npc.direction;
            if (phaseCount == -1)
            {
                npc.velocity = npc.DirectionTo(player.Center) * 5;
                phaseNegOneTimer++;
                if (phaseNegOneTimer >= 240 || npc.Distance(player.Center) <= 240)
                {
                    phaseCount = 0;
                }
            }
            if (phaseCount == 0)
            {
                phaseZeroDashCounter++;
                if (phaseZeroDashCounter >= 120)
                {
                    if (phaseZeroNearPlayerCheck)
                    {
                        if (phaseZeroPewPewCountdown >= 25)
                        {
                            phaseZeroNumberOfTimesAlreadyDone++;
                            phaseZeroNearPlayerCheck = false;
                            phaseZeroPewPewCountdown = 0;
                            phaseZeroDashCounter = 0;
                            phaseZeroPewPewGunTimer = 0;
                            if (phaseZeroNumberOfTimesAlreadyDone >= 3)
                            {
                                if (npc.life <= 125000)
                                {
                                    phaseCount = 3;
                                }
                                else
                                {
                                    phaseCount = 1;
                                }
                                phaseZeroNumberOfTimesAlreadyDone = 0;
                            }
                        }
                        npc.velocity = Vector2.Zero;
                        phaseZeroPewPewGunTimer++;
                        if (phaseZeroPewPewGunTimer % 7 == 0)
                        {
                            Main.PlaySound(SoundID.Item36, npc.Center);
                            Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.Bullet, 35, 0);
                            projectile.velocity = (projectile.DirectionTo(player.Center) * 3).RotatedByRandom(MathHelper.ToRadians(10));
                            projectile.hostile = true;
                            projectile.friendly = false;
                            phaseZeroPewPewCountdown++;
                            phaseZeroPewPewGunTimer = 0;
                        }
                    }
                    else
                    {
                        if (npc.Distance(player.Center) <= 360)
                        {
                            phaseZeroNearPlayerCheck = true;
                        }
                        else
                        {
                            player.AddBuff(ModContent.BuffType<PoliticalPoison>(), 2);
                        }
                        int dustSpawnAmount = 128;
                        for (int i = 0; i < dustSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                            Vector2 dustOffset = currentRotation.ToRotationVector2();
                            Dust dust = Main.dust[Dust.NewDust(npc.Center + dustOffset * 360, 12, 12, DustID.Venom, 0, 0, 0, default, 1.4f)];
                            dust.velocity = npc.DirectionFrom(dust.position) * -24;
                            dust.noGravity = true;
                        }
                        npc.velocity.X = npc.DirectionTo(player.Center).X * 9;
                        npc.velocity.Y = 0;
                    }
                }
            }
            if (phaseCount == 1)
            {
                if (phaseOneCannonCount == 10)
                {
                    int dustSpawnAmount = 128;
                    for (int i = 0; i < dustSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                        Vector2 dustOffset = currentRotation.ToRotationVector2();
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + dustOffset * 360, 12, 12, DustID.Venom, 0, 0, 0, default, 1.4f)];
                        dust.velocity = npc.DirectionFrom(dust.position) * -24;
                        dust.noGravity = true;
                    }
                }
                if (phaseOneCannonCount >= 12)
                {
                    phaseOneCannonTimer = 0;
                    phaseOneFinishDelay++;
                    if (phaseOneFinishDelay >= 300)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            Projectile projectile = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-4, 4) * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        }
                        phaseOneCannonCount = 0;
                        phaseCount = 2;
                    }
                    else
                    {
                        int dustSpawnAmount = 128;
                        for (int i = 0; i < dustSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                            Vector2 dustOffset = currentRotation.ToRotationVector2();
                            Dust dust = Main.dust[Dust.NewDust(npc.Center + dustOffset * 360, 12, 12, DustID.TheDestoryer, 0, 0, 0, default, 1.4f)];
                            dust.velocity = npc.DirectionFrom(dust.position) * -24;
                            dust.noGravity = true;
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            Dust dust1 = Main.dust[Dust.NewDust(npc.Center, 12, 12, DustID.TheDestoryer, 0, 0, 0, default, 1.4f)];
                            dust1.velocity = new Vector2(0, 30);
                            dust1.noGravity = true;
                            Dust dust2 = Main.dust[Dust.NewDust(npc.Center, 12, 12, DustID.TheDestoryer, 0, 0, 0, default, 1.4f)];
                            dust2.velocity = new Vector2(0, -30);
                            dust2.noGravity = true;
                            Dust dust3 = Main.dust[Dust.NewDust(npc.Center, 12, 12, DustID.TheDestoryer, 0, 0, 0, default, 1.4f)];
                            dust3.velocity = new Vector2(-30, 0);
                            dust3.noGravity = true;
                            Dust dust4 = Main.dust[Dust.NewDust(npc.Center, 12, 12, DustID.TheDestoryer, 0, 0, 0, default, 1.4f)];
                            dust4.velocity = new Vector2(30, 0);
                            dust4.noGravity = true;
                        }
                    }
                }
                phaseOneCannonTimer++;
                if (phaseOneCannonTimer >= 150 && phaseOneCannonTimer % 12 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, new Vector2(7 * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                    projectile.hostile = true;
                    projectile.friendly = false;
                    phaseOneCannonCount++;
                }
            }

            if (phaseCount == 2)
            {
                if (phaseTwoIsDoingShotgunAttack)
                {
                    npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, player.Center, 0.35f)) * 4;
                    phaseTwoShotgunTimer++;
                    if (npc.life > 125000)
                    {
                        if (phaseTwoShotgunTimer >= 90)
                        {
                            int numberProjectiles = 4 + Main.rand.Next(3); // 4 or 5 shots
                            for (int i = 0; i < numberProjectiles; ++i)
                            {
                                Main.PlaySound(SoundID.Item36, npc.Center);
                                Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.BulletHighVelocity, 45, 5, player.whoAmI);
                                projectile.hostile = true;
                                projectile.friendly = false;
                                projectile.velocity = (projectile.DirectionTo(player.Center) * 3).RotatedByRandom(MathHelper.ToRadians(12));
                            }
                            phaseTwoShotgunCount++;
                            if (phaseTwoShotgunCount >= 5)
                            {
                                phaseCount = 0;
                                phaseTwoShotgunCount = 0;
                                phaseTwoShotgunTimer = 0;
                                phaseTwoIsDoingShotgunAttack = false;
                            }
                            phaseTwoShotgunTimer = 0;
                        }
                    }
                    else
                    {
                        if (phaseTwoShotgunTimer >= 40)
                        {
                            int numberProjectiles = 6 + Main.rand.Next(4); // 4 or 5 shots
                            for (int i = 0; i < numberProjectiles; ++i)
                            {
                                Main.PlaySound(SoundID.Item36, npc.Center);
                                Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.BulletHighVelocity, 45, 5, player.whoAmI);
                                projectile.hostile = true;
                                projectile.friendly = false;
                                projectile.velocity = (projectile.DirectionTo(player.Center) * 2).RotatedByRandom(MathHelper.ToRadians(15));
                            }
                            phaseTwoShotgunCount++;
                            if (phaseTwoShotgunCount >= 12)
                            {
                                phaseCount = 0;
                                phaseTwoShotgunCount = 0;
                                phaseTwoShotgunTimer = 0;
                                phaseTwoIsDoingShotgunAttack = false;
                            }
                            phaseTwoShotgunTimer = 0;
                        }
                    }
                }
                else
                {
                    if (npc.life <= 75000)
                    {
                        if (npc.Distance(player.Center) <= 360)
                        {
                            phaseZeroNearPlayerCheck = true;
                        }
                        else
                        {
                            player.AddBuff(ModContent.BuffType<PoliticalPoison>(), 2);
                        }
                        int dustSpawnAmount = 128;
                        for (int i = 0; i < dustSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                            Vector2 dustOffset = currentRotation.ToRotationVector2();
                            Dust dust = Main.dust[Dust.NewDust(npc.Center + dustOffset * 360, 12, 12, DustID.Venom, 0, 0, 0, default, 1.4f)];
                            dust.velocity = npc.DirectionFrom(dust.position) * -24;
                            dust.noGravity = true;
                        }
                    }
                    phaseTwoLaserSpinCounter += 1.4f;
                    npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, player.Center, 0.35f)) * 4;
                    phaseTwoLaserTimer++;
                    if (phaseTwoLaserTimer >= 4)
                    {
                        int projectileSpawnAmount = 4;
                        for (int i = 0; i < projectileSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i + MathHelper.ToRadians(90) + MathHelper.ToRadians(phaseTwoLaserSpinCounter);
                            Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                            int damage = 110;
                            int type = ProjectileID.RayGunnerLaser;
                            Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity * 10, type, damage, 1)];
                            projectile.tileCollide = false;
                            projectile.hostile = true;
                            projectile.friendly = false;
                        }
                        phaseTwoLaserCount++;
                        if (phaseTwoLaserCount >= 120)
                        {
                            phaseTwoLaserSpinCounter = 0;
                            phaseTwoLaserTimer = 0;
                            phaseTwoIsDoingShotgunAttack = true;
                            phaseTwoLaserCount = 0;
                        }
                        phaseTwoLaserTimer = 0;
                    }
                }
            }

            if (phaseCount == 3)
            {
                phaseThreeDashTimer++;
                if (phaseThreeDashTimer >= 50)
                {
                    if (phaseThreeDashTimer >= 120)
                    {
                        phaseThreeFastDashTimer++;
                        if (phaseThreeFastDashTimer >= 30)
                        {
                            npc.velocity = npc.DirectionTo(player.Center) * 15;
                            phaseThreeFastDashTimer = 0;
                            phaseThreeDashTimer = 0;
                            phaseThreeDashCount++;
                        }
                    }
                    else
                    {
                        npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, player.Center + new Vector2(100, 0), 0.35f)) * 6;
                        if (phaseThreeDashTimer % 10 == 0)
                        {
                            Projectile projectile = Projectile.NewProjectileDirect(npc.Center, new Vector2(7 * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                            projectile.hostile = true;
                            projectile.friendly = false;
                        }
                    }
                }
                if (phaseThreeDashCount == 5)
                {
                    npc.velocity = Vector2.Zero;
                    phaseCount = 4;
                }
            }

            if (phaseCount == 4)
            {
                npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, player.Center, 0.35f)) * 4;
                int dustSpawnAmount = 360;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Main.dust[Dust.NewDust(npc.Center + dustOffset * 720, 12, 12, DustID.Venom, 0, 0, 0, default, 1.4f)];
                    dust.velocity = npc.DirectionFrom(dust.position) * -24;
                    dust.noGravity = true;
                }
                if (npc.Distance(player.Center) <= 720)
                {

                }
                else
                {
                    player.AddBuff(ModContent.BuffType<PoliticalPoison>(), 2);
                }
                phaseFourMachineGunTimer++;
                if (phaseFourMachineGunTimer >= 80 && phaseFourMachineGunTimer % 4 == 0)
                {
                    Main.PlaySound(SoundID.Item36, npc.Center);
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.Bullet, 35, 0);
                    projectile.velocity = (projectile.DirectionTo(player.Center) * 7).RotatedByRandom(MathHelper.ToRadians(2));
                    projectile.hostile = true;
                    projectile.friendly = false;
                    if (phaseFourMachineGunTimer >= 120)
                    {
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(7 * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(7 * -npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(5 * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(5 * -npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(3 * npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        Projectile.NewProjectileDirect(npc.Center, new Vector2(3 * -npc.direction, -5), ModContent.ProjectileType<BeeRocket>(), 65, 0);
                        phaseFourMachineGunCount++;
                        phaseFourMachineGunTimer = 0;
                    }
                }
                if (phaseFourMachineGunCount >= 8)
                {
                    if (npc.life <= 75000)
                    {
                        int npcSpawnAmount = 16;
                        for (int i = 0; i < npcSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
                            Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
                            Vector2 spawnPos = npc.Center + spawnOffset * 120;
                            NPC politicianBee = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<PoliticianBee>())];
                            politicianBee.ai[3] = currentRotation;
                        }
                    }
                    phaseCount = 1;
                }
            }
        }
    }
}
