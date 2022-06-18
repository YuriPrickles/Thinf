using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs;
using Thinf.NPCs.MotherNature;
using Thinf.NPCs.Raisin;
using Thinf.NPCs.SoulCatcher;
using static Terraria.ModLoader.ModContent;
using static Thinf.ModNameWorld;

namespace Thinf.Items
{
    public class WorldDestroyer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raisin Amulet");
            Tooltip.SetDefault("Hey you should use this item right away!\n(DON'T THROW THIS AWAY! THIS IS IMPORTANT FOR PROGRESSION!)\n-Raisin");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            return (!NPC.AnyNPCs(NPCType<MotherNatureCadillac>()) && !NPC.AnyNPCs(NPCType<MotherNature>()) && !NPC.AnyNPCs(NPCType<Raisin>()));  //you can't spawn this boss multiple times
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (!timeLoop)
                {
                    if (!Main.dedServ)
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/TacoBell"));
                    if (!coreDestroyed)
                    {
                        if (!NPC.downedMoonlord)
                        {
                            if (!NPC.downedGolemBoss)
                            {
                                if (!NPC.downedPlantBoss)
                                {
                                    if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                                    {
                                        if (!downedWall)
                                        {
                                            if (!raisinCutscene)
                                            {
                                                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 75, ModContent.NPCType<Raisin>());
                                            }
                                            else
                                            {
                                                Main.NewText("A concentration of souls inside the Wall Of Flesh collectively disallow the use of the World Destroyer!");
                                            }
                                        }
                                        else
                                        {
                                            Main.NewText("The Mechs are so cool that they completely stop the World Destroyer from functioning!");
                                        }
                                    }
                                    else
                                    {
                                        Main.NewText("The jungle is screaming! Plantera's loud guitar shredding drowns out the power of the World Destroyer!");
                                    }
                                }
                                else
                                {
                                    Main.NewText("Golem's power shields this world!");
                                }
                            }
                            else
                            {
                                Main.NewText("The Moon Lord's phone has blocked access of the World Destroyer!");
                            }
                        }
                        else
                        {
                            Main.NewText("An apple is preventing you from using this item.");
                        }
                    }
                    else
                    {
                        Main.NewText("The Raisin Amulet will erase everything, including you. If you want to survive it, you have to use the Time Looper first.");
                    }
                }
                else
                {
                    Main.NewText("An 8th Gen Cadillac Eldorado blocks your way!");
                    NPC.NewNPC((int)(player.Center.X + 200 * player.direction), (int)(player.Center.Y - 1000), NPCType<MotherNatureCadillac>());
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CadillacEntry"));
                }
            }
            else
            {
                int raisinRand = Main.rand.Next(20);

                switch (raisinRand)
                {
                    case 0:
                        Main.NewText("According to my brochure, people here found ways to harness the hidden power of plants. It was said that they could craft them into deadly weapons.");
                        break;
                    case 1:
                        if (ModNameWorld.downedThundercock)
                        {
                            Main.NewText("I still cannot get over the name she gave to those birds... haha...");
                        }
                        else
                        {
                            Main.NewText("A mutated race of chickens hide in another layer. Their chicks come out during the rain to catch any potential lightning strikes. Just be careful, their mothers are very protective.");
                        }
                        break;
                    case 2:
                        if (NPC.downedBoss1)
                        {
                            Main.NewText("Mother Nature has sent her mercenaries! Good luck dealing with them.");
                        }
                        else
                        {
                            Main.NewText("The Eye of Cthulhu serves as a messenger from one dimension to another, transporting produce to Mother Nature. Killing it can give you enough crops to start your own farm.");
                        }
                        break;
                    case 3:
                        Main.NewText("Using different combinations of potted plants can really help during any fight, whether it be against one, or one hundred.");
                        break;
                    case 4:
                        if (ModNameWorld.downedCacterus)
                        {
                            Main.NewText("No, you can't kill Cacterus. His blessing of pure kindess gives him immortality.");
                        }
                        else
                        {
                            Main.NewText("Part of our plan is to make sure the evil biomes spread. One of her guardians stops it from spreading in the deserts, or, at least it tries to.");
                        }
                        break;
                    case 5:
                        Main.NewText("I think you shouldn't place too much chests in one area. I've heard bad stories about garbage storage systems...");
                        break;
                    case 6:
                        if (Main.hardMode)
                        {
                            if (ModNameWorld.downedBill)
                            {
                                Main.NewText("You know, in hindsight, we should've asked that goat to join us.");
                            }
                            else
                            {
                                Main.NewText("Rumors of a demon goat gaining power have been going around on Twitter. You MUST make sure they don't become more powerful than you.");
                            }
                        }
                        else
                        {
                            Main.NewText("Some slingshots provide extra benefits in certain conditions.");
                        }
                        break;
                    case 7:
                        if (Main.hardMode)
                        {
                            if (!NPC.downedMechBossAny)
                            {
                            }
                            else
                            {
                                if (ModNameWorld.downedJerry)
                                    Main.NewText("Don't worry, soon you'll be a god yourself. We shouldn't have to worry about Jerry anymore.");
                                else
                                    Main.NewText("Hey, you should probably talk to that weird-eyed Ketchup Slime.");
                            }
                        }
                        else
                        {
                            Main.NewText("Special fish reside in the waters of the Tomato Fields.");
                        }
                        break;
                    case 8:
                        if (NPC.downedQueenBee)
                        {
                            Main.NewText("There's gotta be something you can do with that radio...");
                        }
                        else
                        {
                            Main.NewText("Giant bees reside in the Jungle Hives, and are very protective of the larvae. I think those larvae are dead seeing as how they never hatched in my whole time here.");
                        }
                        break;
                    case 9:
                        if (NPC.downedPlantBoss)
                        {
                            Main.NewText("My brochure has it that two powerful plants guard the underground caverns. Only the shrieks of ghosts wrapped inside vegetables can summon them.");
                        }
                        else
                        {
                            Main.NewText("At some point those pink flowers should start appearing...");
                        }
                        break;
                    case 10:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.downedHerbalgamation)
                            {
                                Main.NewText("Herbalgamation's Herbal Cores can be stitched into powerful gear for rangers and planters.");
                            }
                            else
                            {
                                Main.NewText("this message has been intercepted by: herbalgamation");
                                Main.NewText("your mother is on an 80% sale on steam");
                                Main.NewText("come  fight us you loser");
                            }
                        }
                        else
                        {
                            Main.NewText("I have a strange feeling that these herbs are going to be a problem soon.");
                        }
                        break;
                    case 11:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.downedBlizzard)
                            {
                                Main.NewText("Frozen Essence. Melee gear. Sorry for the rushed tip, I'm currently eating some ice cream.");
                            }
                            else
                            {
                                Main.NewText("This is a very important part of our journey: We must kill Mother Nature's daugther, Blizzard.");
                            }
                        }
                        else
                        {
                            Main.NewText("If you're tired of farming for underground evil biome materials, elementals from these biomes can spawn. When defeated, they can reward you with lots of things.");
                        }
                        break;
                    case 12:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.downedSoulCatcher)
                            {
                                Main.NewText("Linimisifrififlium is a material made from pure energy. Perfect for mages.");
                            }
                            else
                            {
                                Main.NewText("I know this sounds like a bad plan, but you HAVE to get hit by a Hell Spirit. It provides temporary access to the Nightmare Layer, letting you interact with Nightmare Creatures.");
                            }
                        }
                        else
                        {
                            Main.NewText("I was supposed to provide a tip here. I couldn't really think of anything, so just remember that this text will be replaced in Post-Moon Lord.");
                        }
                        break;
                    case 13:
                        if (NPC.downedMoonlord)
                        {
                            Main.NewText("A powerful fruit known as the Starfruit was one of my main goals back in the old days. These increase your Max HP when you eat them. If this had a Legendary modifier, it could do so much more.");
                        }
                        else
                        {
                            Main.NewText("Just telling you right now, Life Fruits are not the last health upgrade.");
                        }
                        break;
                    case 14:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.downedPM)
                            {
                                Main.NewText("Hey, you can't use the Political Powers you got from Prime Minsiter. You have to purify them first.");
                            }
                            else
                            {
                                Main.NewText("Beekeeper has something to say to you. I think you should talk to her.");
                            }
                        }
                        else
                        {
                            if (ModNameWorld.downedHypnoKeeper)
                            {
                                Main.NewText("Looks like you freed that girl from that hat thing. She could probably be of use later.");
                            }
                            else
                            {
                                Main.NewText("I sense a mind control device somewhere. What kind of coward uses forceful mind control? I much prefer the old way of just manipulating people.");
                            }
                        }
                        break;
                    case 15:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.coreDestroyed)
                            {
                                Main.NewText("This is it. Left-Click this Amulet for the final battle. We'll finally become gods.");
                            }
                            else
                            {
                                Main.NewText("According to my extensive research, each of Mother Nature's 4 best fighters carry a S'more with them. These S'mores give power to the holder, but their main use is keeping Core's shield up.");
                            }
                        }
                        else
                        {
                            Main.NewText("At some point you're going to have to kill Core in the middle of the world. She has a shield, but I don't know what keeps it up.");
                        }
                        break;
                    case 16:
                        if (NPC.downedMoonlord)
                        {
                            if (ModNameWorld.downedMom)
                            {
                                Main.NewText("She didn't die. But, there has to be a way, right? RIGHT?");
                            }
                            else
                            {
                                Main.NewText("Mother Nature is the only one blocking our path to absorbing this world's power.");
                            }
                        }
                        else
                        {
                            Main.NewText("Zzz...");
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {

        }
    }
}
