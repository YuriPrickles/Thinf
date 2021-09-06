using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Thinf.Buffs;
using Thinf.Items;
using Thinf.Items.Pets;
using Thinf.Items.Potions;
using Thinf.NPCs;
using Thinf.NPCs.Nightmares;
using Thinf.Projectiles;
using static Thinf.ModNameWorld;
using static Thinf.NPCs.GlobalNPCs;

namespace Thinf
{
    public class MyPlayer : ModPlayer
    {
        public bool coreDeadBuff;
        public int ambushTimer = 0;
        public int moneyTimer = 0;
        public bool hasMoneyNecklace = false;
        public int moneyMeter = 0;
        public static bool hereComesTheMoney = false;
        public static bool insideGatrix = false;
        public static bool readyToTravel = false;
        public static bool hasFrostSavageHelmetMeleeSpeedBuff = false;
        public static int vanquishStreak = 0;
        public static bool hasEmergencyEscape = false;
        public static bool dingMode = false;
        public bool hasNightmareChestplate = false;
        public bool politicallyDying = false;
        public bool starsMinion = false;
        public bool roseDefense = false;
        bool roseDefenseReal = false;
        public bool spinachStrength = false;
        bool spinachStrengthReal = false;
        int slimeTimer = 0;
        public static int treesChopped = 0;
        public static bool hasTransformer;
        public static bool hasKingSlimeCrown;
        public static bool hasQueenStinger;
        public static bool drankelixir = false;
        int elixtime = 0;
        public bool ZoneChestWasteland = false;
        public bool ZoneTomatoTown = false;
        public static bool hasdemonglove;
        public static bool spacerobeworn;
        public static bool angelhalo;
        public static bool ZoneForest;
        public static bool haspotatorangedhelm;
        public static bool haspotatorangedsetbonus;
        public static bool haspotatosummonsetbonus;
        public static bool highOnChlorophyll;
        public bool photon;
        public bool jerry;
        int afkcount = 0;
        int rickrolltimer = 239;
        bool rickrollcheck = false;
        public static bool nightmare = false;
        public const int maxStarfruits = 5;
        public int starfruits;
        public int TimerForShake;
        public static int shakeType;
        float intensity;

        public override void ResetEffects()
        {
            coreDeadBuff = false;
            hasMoneyNecklace = false;
            hereComesTheMoney = false;
            hasEmergencyEscape = false;
            hasFrostSavageHelmetMeleeSpeedBuff = false;
            politicallyDying = false;
            starsMinion = false;
            spinachStrengthReal = spinachStrength;
            roseDefenseReal = roseDefense;
            hasTransformer = false;
            hasKingSlimeCrown = false;
            highOnChlorophyll = false;
            nightmare = false;
            haspotatosummonsetbonus = false;
            haspotatorangedsetbonus = false;
            photon = false;
            spacerobeworn = false;
            hasdemonglove = false;
            angelhalo = false;
            player.statLifeMax2 += starfruits * 50;
            jerry = false;
        }
        public override void ModifyScreenPosition()
        {
            if (screenShake == true && shakeType == 0)
            {
                TimerForShake++;
                float intensity = 10f;
                if (TimerForShake >= 1)
                {
                    Main.screenPosition += new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    Main.screenPosition -= new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    if (TimerForShake == 420) //higher the number the longer shake will do
                    {
                        screenShake = false;
                        TimerForShake = 0;
                    }
                }
            }
            if (screenShake && shakeType == 1) //Soul Catcher
            {
                TimerForShake++;
                intensity = 15f;
                if (TimerForShake >= 1)
                {
                    Main.screenPosition += new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    Main.screenPosition -= new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    intensity *= 0.96f;
                    if (TimerForShake == 300) //higher the number the longer shake will do
                    {
                        intensity = 10;
                        screenShake = false;
                        TimerForShake = 0;
                    }
                }
            }
            if (screenShake && shakeType == 2) //Beenado Suck
            {
                TimerForShake++;
                intensity = 0.4f;
                if (TimerForShake >= 1)
                {
                    Main.screenPosition += new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    Main.screenPosition -= new Vector2(Main.rand.NextFloat(intensity), Main.rand.NextFloat(intensity));
                    intensity *= 0.99f;
                    if (TimerForShake == Thinf.ToTicks(1)) //higher the number the longer shake will do
                    {
                        intensity = 0.1f;
                        screenShake = false;
                        TimerForShake = 0;
                    }
                }
            }
        }
        public override void OnEnterWorld(Player player)
        {
            //if (DateTime.Now.Month != 4 && DateTime.Now.Day != 1)
            //{
            //    int stringRand = Main.rand.Next(9);
            //    string enterWorldText = "an error happpened with this thing, hold on";
            //    switch (stringRand)
            //    {
            //        case 0:
            //            enterWorldText = "You better put me in the intro!";
            //            break;
            //        case 1:
            //            enterWorldText = "Shoutout to HeckingEpicGamer who has been warned as much as the fingers on your hands";
            //            break;
            //        case 2:
            //            enterWorldText = "How are ya";
            //            break;
            //        case 3:
            //            enterWorldText = "Whatever you're farming for, you'll never get it.";
            //            break;
            //        case 4:
            //            enterWorldText = "I was gonna crash the game, but you didnt have Omniswing on, so you're fine.";
            //            break;
            //        case 5:
            //            enterWorldText = "One day the mod will be featured in a Gameraiders101 video...";
            //            break;
            //        case 6:
            //            enterWorldText = "Join the server! too lazy to add the link so just click the mods homepage";
            //            break;
            //        case 7:
            //            enterWorldText = $"Here we have {player.name}, entering the world. What will he do now?";
            //            break;
            //        case 8:
            //            enterWorldText = "I messed up\nThe text\nreally badly\nPut me out of my misery";
            //            break;
            //    }

            //    Main.NewText(enterWorldText, Color.LightSkyBlue);
            //}
            ModContent.GetInstance<Thinf>().MyInterface.SetState(null);
            rickrollcheck = true;
        }
        public override void PreUpdate()
        {
            #region Rickroll for April 1
            if (rickrollcheck)
            {
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
                {
                    if (rickrolltimer < 2401)
                    {
                        rickrolltimer++;
                    }

                    switch (rickrolltimer)
                    {
                        case 240:
                            Main.NewTextMultiline(@"We're no strangers to love
You know the rules and so do I
A full commitment's what I'm thinking of
You wouldn't get this from any other guy
I just wanna tell you how I'm feeling
Gotta make you understand", false, new Color(3, 252, 165));
                            break;
                        case 480:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                        case 720:
                            Main.NewTextMultiline(@"We've known each other for so long
Your heart's been aching but you're too shy to say it
Inside we both know what's been going on
We know the game and we're gonna play it
And if you ask me how I'm feeling
Don't tell me you're too blind to see", false, new Color(3, 252, 165));
                            break;
                        case 960:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                        case 1200:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                        case 1440:
                            Main.NewTextMultiline(@"Never gonna give, never gonna give
(Give you up)", false, new Color(3, 252, 165));
                            break;
                        case 1680:
                            Main.NewTextMultiline(@"We've known each other for so long
Your heart's been aching but you're too shy to say it
Inside we both know what's been going on
We know the game and we're gonna play it
I just wanna tell you how I'm feeling
Gotta make you understand", false, new Color(3, 252, 165));
                            break;
                        case 1920:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                        case 2160:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                        case 2400:
                            Main.NewTextMultiline(@"Never gonna give you up
Never gonna let you down
Never gonna run around and desert you
Never gonna make you cry
Never gonna say goodbye
Never gonna tell a lie and hurt you", false, new Color(3, 252, 165));
                            break;
                    }
                }
            }
            #endregion

            /*Mod omniswing = ModLoader.GetMod("OmniSwing");
			if (omniswing != null)
			{
				player.Hurt(PlayerDeathReason.ByCustomReason("Uninstall Omniswing you lazy goose"), 99999999, 0);
			}*/
            if (FrenzyMode)
            {
                if (player.wet || player.HasBuff(BuffID.Wet))
                {
                    player.AddBuff(BuffID.Wet, Thinf.ToTicks(10));
                    player.moveSpeed -= 4;
                    player.accRunSpeed -= 6;
                }

                if (player.statLife < player.statLifeMax / 5)
                {
                    player.AddBuff(BuffID.Weak, 60);
                }
            }
            if (player.HasItem(ModContent.ItemType<PerfectPancake>()))
            {
                hasPerfectPancake = true;
            }
            else
            {
                hasPerfectPancake = false;
            }
            if (drankelixir)
            {
                elixtime++;
                if (elixtime > 300)
                {
                    player.immune = false;
                    drankelixir = false;
                    elixtime = 0;
                }
                else
                {
                    player.immune = true;
                }
            }
        }

        public void Ambush()
        {
            Main.NewText("AMBUSH!");
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<CommandoFlashlight>());
            for (int i = 0; i < 50; ++i)
            {
                Projectile projectile = Projectile.NewProjectileDirect(player.Center + new Vector2(Main.rand.Next(-1000, 1000), Main.rand.Next(-1000, -800)), new Vector2(1, Main.rand.Next(2, 4)), ModContent.ProjectileType<Brightbolt>(), 4, 3);
                projectile.penetrate = 1;
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (hasEmergencyEscape)
            {
                for (int i = 0; i < Main.maxNPCs; ++i)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && !npc.dontTakeDamage)
                    {
                        npc.active = false;
                    }
                }
                player.statLife = 1;
                player.Spawn();
                Main.NewText("Thank you for using Cortal's Emergency Escape service. ;)");
                for (int d = 0; d < 70; d++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
                }
                return false;
            }
            if (dingMode)
            {
                vanquishStreak = 0;
                Main.NewText("Vanquish Streak Broken!");
            }
            return true;
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = Main.item[Item.NewItem(player.Center, ModContent.ItemType<ThingThatCausesDingSounds>())];
            items.Add(item);
        }
        public override void PostUpdate()
        {
            if (coreDestroyed)
            {
                player.AddBuff(ModContent.BuffType<CoreDestroyed>(), 2);
            }
            ambushTimer++;
            if (ambushTimer >= 600)
            {
                if (downedCortal && Main.rand.Next(80) == 0 && !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<CommandoFlashlight>()) && !downedFlashlight)
                {
                    Ambush();
                }
                ambushTimer = 0;
            }
            if (moneyMeter >= 200)
            {
                hereComesTheMoney = true;
            }
            if (hereComesTheMoney)
            {
                moneyTimer++;
                if (moneyTimer >= Thinf.ToTicks(60))
                {
                    Main.NewText("Money mode has ended!");
                    moneyMeter = 0;
                    hereComesTheMoney = false;
                    moneyTimer = 0;
                }
            }
            if (roseDefenseReal)
            {
                player.statDefense += 25;
            }
            if (spinachStrengthReal)
            {
                player.allDamage *= 1.20f;
            }
            if (hasKingSlimeCrown)
            {
                slimeTimer++;
                if (slimeTimer == 360)
                {
                    for (int i = 0; i < 8; ++i)
                    {
                        Projectile slimeSpike = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileID.SpikedSlimeSpike, 20, 2)];
                        slimeSpike.owner = player.whoAmI;
                        slimeSpike.hostile = false;
                        slimeSpike.friendly = true;
                        slimeSpike.timeLeft = 300;
                        slimeSpike.velocity = (new Vector2(7, 0)).RotatedByRandom(360);
                    }
                    slimeTimer = 0;
                }
            }
            if (nightmare)
            {
                Main.UseHeatDistortion = true;
            }
            if (!player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert && !player.ZoneGlowshroom && !player.ZoneMeteor && !player.ZoneBeach && !player.ZoneDesert && player.ZoneOverworldHeight)
            {
                ZoneForest = true;
            }
            else
            {
                ZoneForest = false;
            }
            if (player.velocity == new Vector2(0))
            {
                afkcount++;
                if (afkcount == 86400)
                {
                    Item.NewItem(player.Center, ModContent.ItemType<Stylus>());
                    Main.NewText("You procastinated for a day! Here's a cool reward.", 255, 255, 0);
                    afkcount = 0;
                }
            }
            else
            {
                afkcount = 0;
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (politicallyDying)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 64;
            }
        }
        public override TagCompound Save()
        {
            return new TagCompound {
                {"starfruits", starfruits},
            };
        }

        public override void Load(TagCompound tag)
        {
            starfruits = tag.GetInt("starfruits");
        }

        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (nurse.life <= 60 && Main.expertMode)
            {
                chatText = "My arms hurt, I can't heal you right now.";
                return false;
            }
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
        }
        public override void UpdateBiomes()
        {
            ZoneChestWasteland = ModNameWorld.ChestWasteland >= 80;
            ZoneTomatoTown = ModNameWorld.TomatoTown >= 100;
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (haspotatorangedhelm)
            {
                return Main.rand.NextFloat() >= -0.15f;
            }
            return true;
        }
        public override float MeleeSpeedMultiplier(Item item)
        {
            if (hasFrostSavageHelmetMeleeSpeedBuff && item.melee)
            {
                return 1.2f;
            }
            return 1;
        }
        public override float UseTimeMultiplier(Item item)
        {
            if (item.ranged && hasNightmareChestplate)
            {
                return 1.36f;
            }
            if (item.ranged && haspotatorangedsetbonus)
            {
                return 1.4f;
            }
            return 1;
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.HasBuff(ModContent.BuffType<GhostMode>()))
            {
                return false;
            }
            if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()) && damageSource.SourceNPCIndex >= 0 && (Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareBat>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareHornet>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareTortoise>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareFeederBody>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareFeederTail>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareFeederHead>() || Main.npc[damageSource.SourceNPCIndex].type == ModContent.NPCType<NightmareHerpling>()))
            {
                return false;
            }
            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == mod.NPCType("ThunderCock"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " got pecked.");
            }

            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == mod.NPCType("ThunderCock") && player.electrified)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " got shocked by the cock.");
            }

            if (damageSource.SourceProjectileType >= 0 && Main.projectile[damageSource.SourceProjectileType].type == mod.ProjectileType("ThunderShot"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " got shocked by the cock.");
            }

            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == mod.NPCType("Cortal"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " died to Cortal.");
            }

            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == mod.NPCType("Cacterus"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " needed more practice.");
            }

            if (damageSource.SourceProjectileType >= 0 && Main.projectile[damageSource.SourceProjectileType].type == mod.ProjectileType("CacterusSpike"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " needed more practice");
            }

            if (damageSource.SourceNPCIndex >= 0 && Main.npc[damageSource.SourceNPCIndex].type == mod.NPCType("Detonato"))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " got spudowwed.");
            }
            if (spacerobeworn)
            {
                float Speed = 11f;  //projectile speed
                Vector2 vector8 = new Vector2(player.position.X + (player.width / 2), player.position.Y + (player.height / 2));
                int projdamage = 45;  //projectile damage
                int type = ProjectileID.HallowStar;  //put your projectile
                Main.PlaySound(98, (int)player.position.X, (int)player.position.Y, 17);
                int stars = 13;
                for (int i = 0; i < stars; i++)
                {
                    Projectile projectile = Projectile.NewProjectileDirect(vector8, Vector2.Zero, type, projdamage, 2f, player.whoAmI);
                    projectile.velocity = new Vector2(0f, -6f).RotatedByRandom(MathHelper.ToRadians(180f));
                }
            }
            return true;
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
            {
                return;
            }
            if (liquidType == 0 && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneTomatoTown && Main.rand.NextBool(3))
            {
                caughtType = ModContent.ItemType<TomatoFish>();
            }

            if (liquidType == 0 && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneTomatoTown && Main.rand.NextBool(12))
            {
                caughtType = ModContent.ItemType<Lycofin>();
            }
        }


        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("Thinf:HoneyImHome", NPC.AnyNPCs(mod.NPCType("Beenado")), player.Center);
            player.ManageSpecialBiomeVisuals("Thinf:ChestWasteland", Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneChestWasteland, player.Center);
            player.ManageSpecialBiomeVisuals("Thinf:Nightmare", nightmare, player.Center);
            player.ManageSpecialBiomeVisuals("Thinf:CoreDead", coreDeadBuff, player.Center);
        }



        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (hereComesTheMoney)
            {
                if (damage >= target.life)
                {
                    if (!target.boss && !target.immortal && !target.friendly)
                    {
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                    }
                    else
                    {
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                    }
                }
                else
                {
                    if (!target.immortal && !target.friendly)
                        Item.NewItem(target.getRect(), ItemID.SilverCoin);
                }
            }
            if (hasMoneyNecklace && moneyMeter <= 100 && !target.immortal && !target.friendly)
            {
                moneyMeter++;
            }
            if (hasQueenStinger && crit && !target.HasBuff(ModContent.BuffType<Paralyzed>()) && !target.boss)
            {
                target.AddBuff(ModContent.BuffType<Paralyzed>(), Thinf.ToTicks(3));
            }
            if (item.melee == true)
            {
                if (hasdemonglove)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        target.AddBuff(mod.BuffType("Disintegrating"), 300);
                    }
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (hereComesTheMoney)
            {
                if (damage >= target.life)
                {
                    if (!target.boss)
                    {
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.SilverCoin);
                    }
                    else
                    {
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                        Item.NewItem(player.Center + new Vector2(Main.rand.Next(-100, 100), -200), ItemID.GoldCoin);
                    }
                }
                else
                {
                    Item.NewItem(target.getRect(), ItemID.SilverCoin);
                }
            }
            if (hasMoneyNecklace && moneyMeter <= 200)
            {
                moneyMeter++;
            }
            if (proj.minion && haspotatosummonsetbonus)
            {
                if (damage >= target.life && damage < target.lifeMax)
                {
                    for (int i = 0; i < 32; ++i)
                    {
                        if (i % 8 == 0)
                        {
                            Projectile laser = Projectile.NewProjectileDirect(target.Center, Vector2.Zero, ProjectileID.DeathLaser, 70, 0, player.whoAmI);
                            laser.velocity = (laser.DirectionTo(player.Center) * 12f).RotatedByRandom(MathHelper.ToRadians(360));
                            laser.hostile = false;
                            laser.friendly = true;
                        }
                    }
                }
            }
            if (hasQueenStinger && Main.rand.Next(14) == 0 && !target.HasBuff(ModContent.BuffType<Paralyzed>()) && !target.boss)
            {
                target.AddBuff(ModContent.BuffType<Paralyzed>(), Thinf.ToTicks(3));
            }
            if (proj.melee == true)
            {
                if (hasdemonglove)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        target.AddBuff(mod.BuffType("Disintegrating"), 300);
                    }
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (hasTransformer)
            {
                npc.StrikeNPC(damage, 0, 0);
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (player.HasBuff(ModContent.BuffType<GhostMode>()))
            {
                a *= 0.25f;
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Thinf.Anvil.JustPressed)
            {
                if (player.HasItem(ModContent.ItemType<ScrollOfRenaming>()))
                {
                    ModContent.GetInstance<Thinf>().MyInterface.SetState(ModContent.GetInstance<Thinf>().MyUI);
                }
                else
                {
                    Main.NewText("You do not have the Scroll of Renaming.", Color.OrangeRed);
                }
            }
            if (Thinf.CloseAnvil.JustPressed)
            {
                ModContent.GetInstance<Thinf>().MyInterface.SetState(null);
            }
            base.ProcessTriggers(triggersSet);
        }
    }
}
