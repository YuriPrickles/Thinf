using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Accessories;
using Thinf.Items.DroneRemotes;
using Thinf.Items.Placeables;
using Thinf.Items.THE_SUPER_COOL_BADASS_LORE;
using Thinf.Items.Weapons;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.Cacterus
{
    [AutoloadBossHead]
    public class Cacterus : ModNPC
    {
        int hasBeenDefeated = 0;
        int amazingCutsceneTimer = 180;
        int secondPhaseTextBool = 0;
        int textnum = 0;
        int wheat = 0;
        int wallOfSpike = 0;
        int frameNumber = 0;
        int cooltimer = 0;
        int var1 = 0;
        int var2 = 0;
        int var3 = 0;
        int var4 = 0;
        int loot;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cacterus"); //yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaay
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 5200;   //boss life
            npc.damage = 30;  //boss damage
            npc.defense = 10;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 100;
            npc.height = 100;
            npc.value = Item.buyPrice(0, 1, 10, 45);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            animationType = NPCID.Harpy;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cacterus");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
        }

        //public override void FindFrame(int frameHeight)
        //{
        //	npc.frameCounter++;
        //	if (npc.frameCounter >= 6)
        //	{
        //		npc.frameCounter = 0;
        //		frameNumber++;
        //		if (frameNumber >= 7)
        //		{
        //			frameNumber = 0;
        //		}
        //		npc.frame.Y = frameNumber * (840 / 7);
        //	}
        //}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 7680;  //boss life scale in expertmode
            npc.damage = 54;  //boss damage increase in expermode
        }

        public override bool CheckDead()
        {
            npc.damage = 0;
            npc.dontTakeDamage = true;
            hasBeenDefeated = 1;
            npc.life = 5200;
            return false;
        }

        public override void AI()
        {
            npc.noTileCollide = true;
            npc.rotation = MathHelper.ToRadians(0);
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                cooltimer++;
                npc.velocity.Y -= 1;
                if (cooltimer >= 120)
                {
                    Main.NewText("<Cacterus> You can do better next time, I believe in you!");
                    npc.active = false;
                    cooltimer = 0;
                }
            }
            Player P = Main.player[npc.target];
            npc.netUpdate = true;
            if (npc.Distance(P.Center) >= 120)
            {
                npc.velocity = npc.DirectionTo(Vector2.Lerp(npc.Center, P.Center, 0.35f)) * 4;
            }
            if (npc.life == 5200 && hasBeenDefeated == 1)
            {
                music = 0;
                npc.velocity = Vector2.Zero;
                npc.aiStyle = 10;
                npc.velocity /= 2;
                wheat = 0;
                wallOfSpike = 0;
                frameNumber = 0;
                cooltimer = 0;
                var1 = 0;
                var2 = 0;
                var3 = 0;
                var4 = 0;
                amazingCutsceneTimer++;
                if (amazingCutsceneTimer == 240)
                {
                    Main.NewText("<Cacterus> Wow! You're a really skilled fighter!");
                }
                if (amazingCutsceneTimer == 480 && !WorldGen.crimson)
                {
                    Main.NewText("<Cacterus> Well, I'll be leaving soon now, I have an important job of keeping the corruption out of the deserts!");
                }
                if (amazingCutsceneTimer == 480 && WorldGen.crimson)
                {
                    Main.NewText("<Cacterus> Well, I'll be leaving soon now, I have an important job of keeping the crimson out of the deserts!");
                }
                if (amazingCutsceneTimer == 720)
                {
                    downedCacterus = true;

                    Main.NewText($"Cacterus leaves you a gift as a reward for you great skill...", 177, 255, 43);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<CacterusBusinessCard>());
                    Item.NewItem(npc.getRect(), ItemID.HealingPotion, 8 + Main.rand.Next(10));

                    if (Main.expertMode)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<FlyingFlower>());

                    loot = Main.rand.Next(5);

                    if (loot == 0)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<TrueCactusSword>());
                    if (loot == 1)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<CactusGun>());
                    if (loot == 2)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<CactusStaff>());
                    if (loot == 3)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<NormalCactusPot>());
                    if (loot == 4)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<CatcusSeed>(), 3 + Main.rand.Next(3));
                    if (Main.rand.Next(5) == 0)
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<PricklyRemote>());
                    }

                }
                if (amazingCutsceneTimer >= 720)
                {
                    npc.velocity.Y -= 5f;
                }
                if (amazingCutsceneTimer >= 840)
                {
                    npc.active = false;
                    Projectile.NewProjectile(P.Center, npc.velocity, ProjectileID.ConfettiGun, 0, 0);
                }
            }

            var1++;
            if (var1 >= 35)  // 230 is projectile fire rate
            {
                float Speed = 12f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 17;  //projectile damage
                int type = mod.ProjectileType("CacterusSpike");  //put your projectile
                Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                var1 = 0;
                var3++;
            }


            var1 += 0;
            if (npc.life <= 2300)
            {  //when the boss has less than 70 health he will do the charge attack
                npc.aiStyle = 10;
                var2++;
                if (secondPhaseTextBool != 1)
                {
                    Main.NewText("<Cacterus> You're doing great! Keep up the good work, Cuz I'm going a bit hard on you now!");
                    secondPhaseTextBool = 1;
                }
            }
            if (var2 >= 100)
            {
                npc.velocity.X *= 5.98f;
                npc.velocity.Y *= 5.98f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                {
                    float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                    npc.velocity.X = (float)(Math.Cos(rotation) * 24) * -1;
                    npc.velocity.Y = (float)(Math.Sin(rotation) * 24) * -1;
                }
                var2 = 0;

            }
            if (var3 >= 6)
            {
                int projectileSpawnAmount = 16;
                for (int i = 0; i < projectileSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
                    Vector2 projectileVelocity = currentRotation.ToRotationVector2();

                    // Spawn projectile with the velocity, profit.
                    float Speed = 12f;  //projectile speed
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 24;  //projectile damage
                    int type = mod.ProjectileType("CacterusSpike");  //put your projectile
                    Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                    Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 0, Main.myPlayer); //code by eldrazi#2385
                    var3++;
                }
                if (var3 >= 41)
                {
                    var3 = 0;
                    if (Main.expertMode)
                    {
                        var4 = 1;
                    }
                }
            }

            if (Main.expertMode)
            {
                if (var4 == 1)
                {
                    wheat++;
                    wallOfSpike++;
                    if (wheat >= 1)  // 230 is projectile fire rate
                    {
                        float Speed = 4f;  //projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        int damage = 17;  //projectile damage
                        int type = mod.ProjectileType("CacterusSpike");  //put your projectile
                        Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                        float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                        if (wallOfSpike == 100)
                        {
                            var4 = 0;
                            wallOfSpike = 0;
                        } // thanks jopojelly from tml modding team
                    }
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode || Main.rand.NextBool())
            {
                player.AddBuff(BuffID.Bleeding, 600, true);
            }
        }
    }
}