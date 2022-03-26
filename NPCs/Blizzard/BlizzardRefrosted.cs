using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;

namespace Thinf.NPCs.Blizzard
{
    public class BlizzardRefrosted : ModNPC
    {
        int frostburstDelay = 0;
        int frameCount = 0;
        int idleFrameTimer = 0;
        int phaseCount = -1;
        int knifeTimer = 0;
        int knifeCount = 0;
        int shardTimer = 0;
        int shardCount = 0;
        int iceTimer = 0;
        int iceCount = 0;
        int cutsceneTimer = 0;
        bool didCutscene = false;
        int spawnCutsceneTimer = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 24;
            DisplayName.SetDefault("Blizzard");
        }
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.lifeMax = 240000;
            npc.damage = 175;
            npc.defense = 48;
            npc.knockBackResist = 0f;
            npc.width = 60;
            npc.height = 84;
            npc.value = Item.buyPrice(2, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.Item48;
            npc.DeathSound = SoundID.Item27;
            npc.netAlways = true;
            npc.alpha = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cold_Slice");
        }
        //public override bool CheckDead()
        //{
        //    npc.velocity = Vector2.Zero;
        //    npc.ai[1] = 123;
        //    npc.life = 1;
        //    npc.dontTakeDamage = true;
        //    phaseCount = -1240;
        //    npc.damage = 0;
        //    npc.boss = false;
        //    music = 0;
        //    return didCutscene;
        //}
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            ModNameWorld.downedBlizzard = true;
            Item.NewItem(npc.getRect(), ModContent.ItemType<FrozenEssence>(), Main.rand.Next(40) + 54);
            Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(25) + 30);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, Thinf.ToTicks(5));
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                int projectileSpawnAmount = 24;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                    Projectile.NewProjectile(npc.Center, projectileVelocity * 9, ModContent.ProjectileType<SnowGlobeBallTwo>(), 0, 0);
                }
                npc.active = false;
            }
            Player player = Main.player[npc.target];

            npc.netUpdate = true;
            npc.spriteDirection = -npc.direction;

            frostburstDelay = 0;
            if (frostburstDelay >= 10)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, 15), ModContent.ProjectileType<Frostburst>(), 1, 2);
                frostburstDelay = 0;
            }
            if (phaseCount == -1)
            {
                spawnCutsceneTimer++;
                switch (spawnCutsceneTimer)
                {
                    case 120:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "I'm going to kill you")].lifeTime = 240;
                        break;
                    case 360:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "Just you wait...")].lifeTime = 240;
                        break;
                    case 600:
                        Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "I've learned the most annoying attack patterns while I was dead")].lifeTime = 240;
                        break;
                    case 840:
                        phaseCount = 0;
                        npc.dontTakeDamage = false;
                        break;
                    default:
                        break;
                }
            }

            npc.frame.Y = GetFrame(frameCount);
            idleFrameTimer++;
            if (idleFrameTimer == 4)
            {
                frameCount++;
                if (frameCount >= 24)
                {
                    frameCount = 0;
                }
                idleFrameTimer = 0;
            }
            if (phaseCount == 0)
            {
                if (npc.Distance(player.Center) >= 480)
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 6;
                }
                else
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 3;
                }
                if (knifeCount >= 12)
                {
                    knifeTimer = 0;
                    knifeCount = 0;
                    phaseCount = 1;
                }
                knifeTimer++;
                if (knifeTimer == 30)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnife>(), 75, 3)];
                    proj.timeLeft = 1;
                    proj.velocity = proj.DirectionTo(player.Center) * 9f;
                    float numberProjectiles;
                    if (Main.expertMode)
                    {
                        numberProjectiles = 2 + Main.rand.Next(4);
                    }
                    else
                    {
                        numberProjectiles = 2 + Main.rand.Next(3);
                    }
                    float rotation = MathHelper.ToRadians(45);
                    proj.position += Vector2.Normalize(proj.velocity) * 45f;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = proj.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1.5f;
                        Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), 82, 3, player.whoAmI);
                    }
                    Main.PlaySound(SoundID.Item28, npc.Center);
                    knifeTimer = 0;
                    knifeCount++;
                }
            }

            if (phaseCount == 1)
            {
                npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(0, -220)) * 7f, .1f);
                //Above just slowly sets the velocity to go towards the target position.
                if (npc.velocity.Length() < 20f)
                    npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop

                if (shardCount >= 9)
                {
                    shardCount = 0;
                    shardTimer = 0;
                    phaseCount = 2;
                }
                shardTimer++;
                if (shardTimer >= 40 && shardTimer != 80)
                {
                    int dustSpawnAmount = 32;
                    for (int i = 0; i < dustSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                        Vector2 dustOffset = currentRotation.ToRotationVector2();
                        Dust dust1 = Dust.NewDustPerfect(npc.Center + dustOffset * 64, DustID.IceRod, null, 0, default, 0.4f);
                        dust1.noGravity = true;
                    }
                    Dust dust = Dust.NewDustPerfect(npc.Center, DustID.IceGolem, new Vector2(0, 30), 0, default, 1.4f);
                    dust.noGravity = true;
                    dust = Dust.NewDustPerfect(npc.Center, DustID.IceGolem, new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(22.5f)), 0, default, 1.4f);
                    dust.noGravity = true;
                    dust = Dust.NewDustPerfect(npc.Center, DustID.IceGolem, new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(-22.5f)), 0, default, 1.4f);
                    dust.noGravity = true;
                    dust = Dust.NewDustPerfect(npc.Center, DustID.IceGolem, new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(45f)), 0, default, 1.4f);
                    dust.noGravity = true;
                    dust = Dust.NewDustPerfect(npc.Center, DustID.IceGolem, new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(-45f)), 0, default, 1.4f);
                    dust.noGravity = true;
                }
                if (shardTimer >= 80)
                {
                    if (Main.expertMode)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(0, -5).RotatedByRandom(MathHelper.ToRadians(80)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    }
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7).RotatedBy(MathHelper.ToRadians(22.5f)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7).RotatedBy(MathHelper.ToRadians(-22.5f)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7).RotatedBy(MathHelper.ToRadians(45f)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 7).RotatedBy(MathHelper.ToRadians(-45f)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
                    shardTimer = 0;
                    shardCount++;
                }
            }

            if (phaseCount == 2)
            {
                if (npc.Distance(player.Center) >= 480)
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 6;
                }
                else
                {
                    npc.velocity = npc.DirectionTo(player.Center) * 3;
                }
                if (iceCount >= 6)
                {
                    iceCount = 0;
                    iceTimer = 0;
                    phaseCount = 0;
                }
                iceTimer++;
                if (iceTimer >= 90)
                {
                    int projectileSpawnAmount = 3;
                    int rotatRand = Main.rand.Next(360);
                    for (int i = 0; i < projectileSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                        Vector2 projectileVelocity = currentRotation.ToRotationVector2();
                        int damage = 0;
                        int type = ModContent.ProjectileType<IceCubeCenter>();
                        Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity.RotatedBy(rotatRand), type, damage, 1)];
                        proj.ai[0] = 1;
                        if (Main.expertMode || npc.life <= 120000)
                        {
                            proj.ai[0] = 2;
                        }
                        if (npc.life <= 60000)
                        {
                            proj.ai[0] = 4;
                        }
                    }
                    iceTimer = 0;
                    iceCount++;
                }
            }
        }
        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
