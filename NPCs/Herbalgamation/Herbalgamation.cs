using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;

namespace Thinf.NPCs.Herbalgamation
{
    [AutoloadBossHead]
    public class Herbalgamation : ModNPC
    {
        int theBossRevolving = 0;
        int spinTimer = 0;
        int scytheAttack = 0;
        float rotat = MathHelper.ToRadians(0);
        int attackPhase = 1;
        int bulletHeck = 0;
        int edgyNameChanger = 0;
        int edgyNameTimer = 0;
        int dashTimer = 0;
        int waterBoltTimer = 0;
        int mad = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Herbalgamation"); //yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaay
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 10;
            npc.lifeMax = 350000;   //boss life
            npc.damage = 140;  //boss damage
            npc.defense = 90;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 128;
            npc.value = Item.buyPrice(0, 96, 32, 45);
            npc.npcSlots = 7f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Venom] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Never_Going_Back_To_That_Box");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
        }


        public override void BossLoot(ref string name, ref int potionType)
        {
            if (ModNameWorld.downedHerbalgamation)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<OvergrownSmore>());
            }
            potionType = ItemID.SuperHealingPotion;   //boss drops
            ModNameWorld.downedHerbalgamation = true;
            Item.NewItem(npc.getRect(), ModContent.ItemType<HerbalCore>(), Main.rand.Next(20) + 45);
            Item.NewItem(npc.getRect(), ModContent.ItemType<CosmicHerbalPiece>(), Main.rand.Next(12) + 15);
            Item.NewItem(npc.getRect(), ItemID.Daybloom, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Blinkroot, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Waterleaf, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Fireblossom, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Shiverthorn, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Moonglow, Main.rand.Next(10) + 7);
            Item.NewItem(npc.getRect(), ItemID.Deathweed, Main.rand.Next(10) + 7);
            //if (Main.expertMode)
            //{
            //    Main.NewText("Wowie, the boss name glitching out really hurt my eyes...", Color.SlateGray);
            //    Main.NewText("Well, at least that's dealt with... Nice job!", Color.SlateGray);
            //    Main.NewText("We're gonna be so powerful together...", Color.SlateGray);
            //}
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 575000;
            npc.damage = 210;
            npc.defense = 100;
        }

        public override void AI()
        {
            edgyNameTimer++;
            if (edgyNameTimer == 2)
            {
                edgyNameChanger = Main.rand.Next(9);

                if (edgyNameChanger == 0)
                    npc.GivenName = "H  balgam ti n";
                if (edgyNameChanger == 1)
                    npc.GivenName = " e bal  ma io ";
                if (edgyNameChanger == 2)
                    npc.GivenName = " erb  gamat  n";
                if (edgyNameChanger == 3)
                    npc.GivenName = "H r  lg  atio ";
                if (edgyNameChanger == 4)
                    npc.GivenName = "Herbalgamation";
                if (edgyNameChanger == 5)
                    npc.GivenName = "H  ba gama   n";
                if (edgyNameChanger == 6)
                    npc.GivenName = " e ba g mat on";
                if (edgyNameChanger == 7)
                    npc.GivenName = "He ba gam   on";
                if (edgyNameChanger == 8)
                    npc.GivenName = "He  a ga a on";
                edgyNameTimer = 0;
            }

            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                bulletHeck++;
                npc.velocity.Y -= 1;
                if (bulletHeck >= 120)
                {
                    npc.active = false;
                    bulletHeck = 0;
                }
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            scytheAttack++;
            if (attackPhase != 2 && scytheAttack >= 120)
            {
                int projectileSpawnAmount = 8;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 2.5f;
                    int damage = 43;  //projectile damage
                    int type = ProjectileID.DemonSickle;
                    Projectile.NewProjectile(npc.Center + projectileVelocity * 75f, Vector2.Normalize(player.Center - npc.Center) * 3f, type, damage, 1); //code by eldrazi#2385
                }
                scytheAttack = 0;
            }
            if (!MyPlayer.ZoneForest || player.Distance(npc.Center) > 3500)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Red, "Angry", true);
                mad++;
                if (mad >= 30)
                {
                    int projectileSpawnAmount = 16;
                    for (int i = 0; i < projectileSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                        Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 2.5f;
                        int damage = 32;  //projectile damage
                        int type = ProjectileID.DemonSickle;
                        Projectile.NewProjectile(npc.Center + projectileVelocity * 75f, Vector2.Normalize(player.Center - npc.Center) * 3f, type, damage, 1); //code by eldrazi#2385
                    }

                    int projectileSpawnAmount2 = 16;
                    for (int i = 0; i < projectileSpawnAmount2; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount2) * i;
                        Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 4f;
                        int damage = 54;  //projectile damage
                        int typerand = Main.rand.Next(3);
                        int type = ProjectileID.InfernoHostileBolt;
                        switch (typerand)
                        {
                            case 0:
                                type = ProjectileID.InfernoHostileBolt;
                                break;
                            case 1:
                                type = ProjectileID.FrostBlastHostile;
                                break;
                            case 2:
                                type = ProjectileID.Stinger;
                                break;
                        }
                        Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 1)];
                        projectile.tileCollide = false;
                    }
                    mad = 0;
                }
            }
            if (attackPhase == 1)
            {
                dashTimer++;
                if (dashTimer == 300)
                {
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), Color.Lime, "CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), Color.Lime, 'DASH');", true);
                }
                if (dashTimer >= 300)
                {
                    npc.aiStyle = -1;
                    npc.rotation = MathHelper.ToRadians(0);
                    Vector2 moveTo = player.Center - new Vector2(0f, 250f); //This is 200 pixels above the center of the player.
                    float speed = 11f; //make this whatever you want
                    Vector2 move = moveTo - npc.Center; //this is how much your boss wants to move
                    if (move.Length() > 20)
                    {
                        float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y); //fun with the Pythagorean Theorem
                        if (magnitude > speed)
                            move *= speed / magnitude; //this adjusts your boss's speed so that its speed is always constant
                        npc.velocity = move;
                    }
                    if (dashTimer == 420)
                    {

                        npc.velocity = npc.DirectionTo(player.position) * 14f;
                        npc.aiStyle = 10;
                        dashTimer = 0;
                        theBossRevolving++;
                        if (theBossRevolving >= 3)
                        {
                            attackPhase = 2;
                        }
                    }
                }
            }

            if (attackPhase == 2)
            {
                if (theBossRevolving >= 3)
                {
                    npc.aiStyle = -1;
                    rotat += 0.05f;
                    npc.velocity = npc.DirectionTo(player.Center + Vector2.One.RotatedBy(rotat) * 256f) * 14;
                    spinTimer++;
                    bulletHeck++;
                    if (bulletHeck >= 20)
                    {
                        int projectileSpawnAmount = 8;
                        for (int i = 0; i < projectileSpawnAmount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                            Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 2.5f;
                            int damage = 35;  //projectile damage
                            int typerand = Main.rand.Next(3);
                            int type = ProjectileID.InfernoHostileBolt;
                            switch (typerand)
                            {
                                case 0:
                                    type = ProjectileID.InfernoHostileBolt;
                                    break;
                                case 1:
                                    type = ProjectileID.FrostBlastHostile;
                                    break;
                                case 2:
                                    type = ProjectileID.Stinger;
                                    break;
                            }
                            Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 1)];
                            projectile.tileCollide = false;
                            projectile.timeLeft = Thinf.ToTicks(5);
                        }
                        bulletHeck = 0;
                    }
                    if (spinTimer >= 300)
                    {
                        rotat = MathHelper.ToRadians(0);
                        attackPhase = 1;
                        npc.aiStyle = 10;
                        theBossRevolving = 0;
                        spinTimer = 0;
                    }
                }
            }

            if (npc.life <= 180000 && !Main.expertMode)
            {
                waterBoltTimer++;
                if (waterBoltTimer >= 300 && waterBoltTimer % 2 == 0)
                {
                    Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.WaterBolt, 96, 1)];
                    projectile.hostile = true;
                    projectile.friendly = false;
                    projectile.penetrate = 1;
                    projectile.velocity = projectile.DirectionTo(player.Center) * 2f;
                    projectile.timeLeft = Thinf.ToTicks(4);
                    if (waterBoltTimer == 360)
                    {
                        waterBoltTimer = 0;
                    }
                }
            }

            if (npc.life <= 320000 && Main.expertMode)
            {
                waterBoltTimer++;
                if (waterBoltTimer >= 300 && waterBoltTimer % 2 == 0)
                {
                    Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.WaterBolt, 96, 1)];
                    projectile.hostile = true;
                    projectile.friendly = false;
                    projectile.penetrate = 1;
                    projectile.velocity = projectile.DirectionTo(player.Center) * 2f;
                    projectile.timeLeft = Thinf.ToTicks(4);
                    if (waterBoltTimer == 360)
                    {
                        waterBoltTimer = 0;
                    }
                }
            }

            if (Main.expertMode && npc.life <= 56000)
            {
                scytheAttack = 0;
                attackPhase = 0;
                npc.aiStyle = -1;
                rotat += 0.02f;
                npc.velocity = npc.DirectionTo(player.Center + Vector2.One.RotatedBy(rotat) * 256f) * 8;
                bulletHeck++;
                if (bulletHeck >= 60)
                {
                    int projectileSpawnAmount = 4;
                    for (int i = 0; i < projectileSpawnAmount; ++i)
                    {
                        float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                        Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 1.2f;
                        int damage = 23;  //projectile damage
                        int typerand = Main.rand.Next(3);
                        int type = ProjectileID.InfernoHostileBolt;
                        switch (typerand)
                        {
                            case 0:
                                type = ProjectileID.InfernoHostileBolt;
                                break;
                            case 1:
                                type = ProjectileID.FrostBlastHostile;
                                break;
                            case 2:
                                type = ProjectileID.Stinger;
                                break;
                        }
                        Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 1)];
                        projectile.tileCollide = false;
                        projectile.timeLeft = Thinf.ToTicks(5);
                    }
                    bulletHeck = 0;
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                player.AddBuff(BuffID.Venom, 180, true);
            }
            player.AddBuff(BuffID.Slow, 120);
            player.AddBuff(BuffID.OnFire, 180);
            player.AddBuff(BuffID.Frostburn, 180);
        }
    }
}