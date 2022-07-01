using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.PrimeMinister
{
    [AutoloadBossHead]
    public class PrimeMinisterCopter : ModNPC
    {
        int phaseCount = 0;
        int phaseZeroTimer = 0;
        int phaseZeroMovementTimer = 0;
        int phaseZeroDirectionToGo = 1;
        int phaseZeroCounter = 0;
        int phaseOneTimer = 0;
        int phaseOneCounter = 0;
        int phaseOneMovementTimer = 0;
        int phaseOneDirectionToGo = 1;
        int phaseOneSpreadDelay = 0;
        int phaseTwoSpinDelay = 0;
        int phaseTwoSpinTimer = 0;
        int beekataShootyTimer = 0;
        int beekataCounter = 0;
        bool readyToSpin = false;
        Vector2 spinPoint;
        float rotat = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying Bumblebee");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 75000;
            npc.damage = 60;
            npc.defense = 50;
            npc.knockBackResist = 0f;
            npc.width = 150;
            npc.height = 100;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            //music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Where_Your_Tax_Goes");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Where_Your_Tax_Goes");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Flying Bumblebee";
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<PrimeMinisterTank>());
            Main.NewText("<Prime Minister> GODDAMIT THIS WAS MY FAVORITE HELICOPTER", Color.Yellow);
            for (int g = 0; g < 10; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
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
            npc.spriteDirection = -npc.direction;
            if (phaseCount == 0)
            {
                phaseZeroMovementTimer++;
                if (phaseZeroMovementTimer >= 150)
                {
                    phaseZeroDirectionToGo *= -1;
                    phaseZeroMovementTimer = 0;
                }
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(250 * phaseZeroDirectionToGo, -150)) * 9;

                phaseZeroTimer++;
                if (phaseZeroTimer >= 30)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.RocketI, 60, 2);
                    projectile.hostile = true;
                    projectile.timeLeft = 80;
                    projectile.velocity = (projectile.DirectionTo(player.Center) * 5).RotatedByRandom(MathHelper.ToRadians(20));
                    phaseZeroTimer = 0;
                    phaseZeroCounter++;
                }
                if (phaseZeroCounter == 15)
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 15;
                    phaseZeroCounter = 0;
                    phaseZeroTimer = 0;
                    phaseZeroMovementTimer = 0;
                    phaseCount = 1;
                }
            }

            if (phaseCount == 1)
            {
                phaseOneMovementTimer++;
                if (phaseOneMovementTimer >= 150)
                {
                    phaseOneDirectionToGo *= -1;
                    phaseOneMovementTimer = 0;
                }
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(450 * phaseOneDirectionToGo, 0)) * 6;

                phaseOneTimer++;
                if (phaseOneTimer >= 150 && phaseOneTimer % 10 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.Stinger, 35, 0);
                    projectile.velocity = projectile.DirectionTo(player.Center) * 5;
                    if (phaseOneTimer >= 300)
                    {
                        Thinf.QuickSpawnNPC(npc, ModContent.NPCType<KamikazeBee>());
                        phaseOneTimer = 0;
                        phaseOneCounter++;
                    }
                }
                if (phaseOneCounter >= 5)
                {
                    phaseOneSpreadDelay++;
                    if (phaseOneSpreadDelay >= 30 && phaseOneSpreadDelay % 30 == 0)
                    {
                        int projectileSpawnAmount = 8;
                        for (int i = 0; i < projectileSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i + MathHelper.ToRadians(30 * (phaseOneSpreadDelay / 30));
                            Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                            int damage = 35;  //projectile damage
                            int type = ProjectileID.Stinger;
                            Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity * 5f, type, damage, 0)];
                        }
                        if (phaseOneSpreadDelay >= 120)
                        {
                            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<KamikazeBee>());
                            phaseOneSpreadDelay = 0;
                            phaseOneMovementTimer = 0;
                            phaseOneTimer = 0;
                            phaseOneCounter = 0;
                            phaseCount = 2;
                            rotat = 0;
                        }
                    }
                }
            }

            if (phaseCount == 2)
            {
                npc.rotation = npc.velocity.ToRotation();
                if (readyToSpin)
                {
                    beekataShootyTimer++;
                    if (beekataShootyTimer >= 5)
                    {
                        Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.One.RotatedByRandom(MathHelper.ToRadians(360)) * 4, ModContent.ProjectileType<BulletBee>(), 25, 2);
                        projectile.hostile = true;
                        beekataShootyTimer = 0;
                    }
                    npc.spriteDirection = -1;
                    rotat += 0.2f;
                    npc.velocity = npc.DirectionTo(spinPoint + Vector2.One.RotatedBy(rotat) * 64f) * 11;
                    phaseTwoSpinTimer++;
                }
                else
                {
                    phaseTwoSpinDelay++;
                }
                if (phaseTwoSpinDelay >= 60)
                {
                    spinPoint = player.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200));
                    readyToSpin = true;
                    phaseTwoSpinDelay = 0;
                }
                if (phaseTwoSpinTimer >= 150)
                {
                    beekataCounter++;
                    readyToSpin = false;
                    phaseTwoSpinTimer = 0;
                }
                if (beekataCounter >= 8)
                {
                    npc.rotation = MathHelper.ToRadians(0);
                    beekataCounter = 0;
                    beekataShootyTimer = 0;
                    phaseTwoSpinDelay = 0;
                    phaseTwoSpinTimer = 0;
                    phaseCount = 0;
                }
            }
        }
    }
}
