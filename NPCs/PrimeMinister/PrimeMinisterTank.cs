using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.PrimeMinister
{
    [AutoloadBossHead]
    public class PrimeMinisterTank : ModNPC
    {
        int phaseCount = 0;
        int phaseZeroBombTimer = 0;
        int phaseZeroCounter = 0;
        int phaseOneShootTimer = 0;
        int phaseOneCounter = 0;
        int phaseTwoFlareDelay = 0;
        int phaseTwoAirStrikeDelay = 0;
        bool flareShotCheck = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beehive Blaster");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 120000;
            npc.damage = 75;
            npc.defense = 120;
            npc.knockBackResist = 0f;
            npc.width = 220;
            npc.height = 120;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Where_Your_Tax_Goes");
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The Beehive Blaster";
            Thinf.QuickSpawnNPC(npc, ModContent.NPCType<PrimeMinisterTheManHimself>());
            Main.NewText("<Prime Minister> GAH! I JUST CLEANED THIS TANK!", Color.Yellow);
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
            npc.spriteDirection = npc.direction;

            if (phaseCount == 0)
            {
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 2);

                phaseZeroBombTimer++;
                if (phaseZeroBombTimer >= 100)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.BeeHive, 70, 10);
                    projectile.velocity = projectile.DirectionTo(player.Center + new Vector2(0, -75)) * 12;
                    projectile.scale = 3f;
                    projectile.Hitbox.Inflate(projectile.width * 2, projectile.height * 2);
                    phaseZeroBombTimer = 0;
                    phaseZeroCounter++;
                }

                if (phaseZeroCounter >= 8)
                {
                    npc.noTileCollide = true;
                    npc.velocity.Y -= 15;
                    phaseZeroCounter = 0;
                    phaseCount = 1;
                }
            }
            if (phaseCount == 1)
            {
                npc.noTileCollide = false;
                npc.velocity.X = (npc.DirectionTo(player.Center).X * 4);
                if (npc.Distance(player.Center) <= 240 && npc.collideY)
                {
                    Jump(10, 12);
                }
                phaseOneShootTimer++;
                if (phaseOneShootTimer >= 50 && phaseOneShootTimer % 3 == 0)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.BulletDeadeye, 50, 2);
                    projectile.velocity = (projectile.DirectionTo(player.Center + player.velocity * 50) * 3.4f).RotatedByRandom(MathHelper.ToRadians(2));
                    if (phaseOneShootTimer >= 125)
                    {
                        phaseOneShootTimer = 0;
                        phaseOneCounter++;
                    }
                }
                if (phaseOneCounter >= 10)
                {
                    npc.velocity.Y = 0;
                    phaseOneCounter = 0;
                    phaseCount = 2;
                }
            }
            if (phaseCount == 2)
            {
                npc.velocity.X = 0;
                npc.defense = 165;
                if (!flareShotCheck)
                {
                    phaseTwoFlareDelay++;
                    if (phaseTwoFlareDelay >= 240)
                    {
                        Projectile projectile = Projectile.NewProjectileDirect(npc.Center, new Vector2(0, -15), ProjectileID.Flare, 0, 2);
                        projectile.tileCollide = false;
                        projectile.friendly = false;
                        projectile.timeLeft = 120;
                        flareShotCheck = true;
                    }
                }
                else
                {
                    phaseTwoAirStrikeDelay++;
                    if (phaseTwoAirStrikeDelay >= 240 && phaseTwoAirStrikeDelay % 30 == 0)
                    {
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(player.Center.X, npc.Center.Y - 500), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                    }
                    if (phaseTwoAirStrikeDelay >= 480)
                    {
                        npc.defense = 120;
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        Projectile.NewProjectileDirect(new Vector2(npc.Center.X + Main.rand.Next(-1000, 1000), npc.Center.Y - 1000), new Vector2(0, 30), ModContent.ProjectileType<AirstrikeMarker>(), 0, 2);
                        phaseTwoFlareDelay = 0;
                        phaseTwoAirStrikeDelay = 0;
                        phaseCount = 0;
                    }
                }
            }
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.X = horizontalSpeed * npc.direction;
            npc.velocity.Y -= verticalSpeed;
        }
    }
}
