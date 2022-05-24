using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Accessories;
using Thinf.Items.Placeables;
using Thinf.Items.Potions;
using Thinf.Items.THE_SUPER_COOL_BADASS_LORE;
using Thinf.Items.Weapons;
using Thinf.NPCs.TownNPCs;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs
{
    public class NpcDrops : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        bool wasCrit;
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.QueenBee)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<LogFileOne>(), 1);
                Item.NewItem(npc.getRect(), ModContent.ItemType<MysteriousRadio>(), 1);
            }
            if (npc.type == NPCID.GoblinSorcerer && Main.rand.Next(25) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ChaosCaster>(), 1);
            }
            if (npc.type == NPCID.DrManFly && Main.rand.Next(40) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ErlenmeyerWarhammer>(), 1);
            }
            if (MyPlayer.dingMode)
            {
                MyPlayer.vanquishStreak++;
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GARY_GET_THE_FUCK_OUT_OF_THAT_IMP_PUNT_OH_NO_HE_CANT_HEAR_US_HE_HAS_AIRPODS_ON_GARY_NOOOOOO"));
                if (!npc.boss)
                {
                    Main.NewText("Vanquished Enemy!");
                    if (MyPlayer.vanquishStreak > 0)
                    {
                        Main.NewText($"Vanquish Streak x{MyPlayer.vanquishStreak}");
                    }
                }
                else
                {
                    Main.NewText("Vanquished Boss!");
                    if (MyPlayer.vanquishStreak > 0)
                    {
                        Main.NewText($"Vanquish Streak x{MyPlayer.vanquishStreak}");
                    }
                }
            }
            if (NPC.downedBoss1 && Main.rand.Next(5) == 0)
            {
                if (!Main.hardMode)
                {
                    if (Main.rand.Next(20) == 0)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                }
                if (Main.hardMode && !NPC.downedMechBossAny)
                {
                    if (npc.lifeMax > 125)
                    {
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                    }
                    else
                    {
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                    }
                }
                if (Main.hardMode && NPC.downedMechBossAny)
                {
                    if (npc.lifeMax > 350)
                    {
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                    }
                    else
                    {
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                    }
                }
                if (Main.hardMode && NPC.downedMoonlord && NPC.downedMechBossAny)
                {
                    if (npc.lifeMax > 1000)
                    {
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem(npc.getRect(), ModContent.ItemType<ChlorophyllBottle>(), 1);
                    }
                }
            }
            if (npc.type == NPCID.WallofFlesh && Main.rand.Next(3) == 0)
            {
                downedWall = true;
                Item.NewItem(npc.getRect(), ModContent.ItemType<GrowerEmblem>(), 1);
            }

            if (npc.type == NPCID.KingSlime && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<KingSlimesCrown>(), 1);
            }

            if (npc.type == NPCID.EyeofCthulhu && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GlassesOfCthulhu>(), 1);
            }

            if (npc.type == NPCID.SkeletronHead && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<CollarboneCollar>(), 1);
            }

            if (npc.type == NPCID.EaterofWorldsHead && Main.rand.Next(5) == 0 && !NPC.AnyNPCs(NPCID.EaterofWorldsBody))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<EaterOfSoles>(), 1);
            }

            if (npc.type == NPCID.BrainofCthulhu && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<BloodCrawlerGear>(), 1);
            }

            if (npc.type == NPCID.DD2DarkMageT1 && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<NecromancyGuide>(), 1);
            }

            if (npc.type == NPCID.QueenBee && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<RoyalStinger>(), 1);
            }

            if (npc.type == NPCID.Spazmatism || npc.type == NPCID.Retinazer && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<EpicGoggles>(), 1);
            }

            if (npc.type == NPCID.SkeletronPrime && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<PrimetimePincer>(), 1);
            }

            if (npc.type == NPCID.TheDestroyer && Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ArmorPlating>(), 1);
            }

            if (npc.type == NPCID.CultistBoss)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<LifeFragment>(), Main.rand.Next(35, 60));
            }

            if (npc.type == NPCID.MoonLordCore && Main.rand.Next(10) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GreatZucchini>(), 1);
            }

            if (npc.type == mod.NPCType("WastelandMimic") && downedSoulKeys)
            {
                int mimicloot;
                mimicloot = Main.rand.Next(4);
                if (mimicloot == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("DemonGauntlet"), 1);
                }

                if (mimicloot == 1)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("BrokenHeroKnife"), 1);
                }

                if (mimicloot == 2)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("PlatinumCross"), 1);
                }

                if (mimicloot == 3)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("ElixirOfLife"), Main.rand.Next(3) + 2);
                }

                if (mimicloot == 4)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("SpaceRobe"), 1);
                }
            }

            if (npc.type == mod.NPCType("SpudLord"))
            {
                if (!spawnOrespud)
                {                                                          //Red  Green Blue
                    Main.NewText("The essence of potatoes has spread throught the dirt", 200, 200, 55);
                    for (int k = 0; k < (int)((double)(WorldGen.rockLayer * Main.maxTilesY) * 40E-05); k++)
                    {
                        int X = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int Y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200);
                        WorldGen.OreRunner(X, Y, WorldGen.genRand.Next(9, 15), WorldGen.genRand.Next(5, 9), (ushort)mod.TileType("PotatiumiteOreTile"));
                    }
                }
                spawnOrespud = true;
            }

            if (npc.type == mod.NPCType("CarrotKing"))
            {
                if (!spawnOrecarrot)
                {
                    Main.NewText("The essence of carrots has spread throught the dirt", 200, 200, 55);
                    for (int k = 0; k < (int)((double)(WorldGen.rockLayer * Main.maxTilesY) * 40E-05); k++)
                    {
                        int X = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int Y = WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200);
                        WorldGen.OreRunner(X, Y, WorldGen.genRand.Next(9, 15), WorldGen.genRand.Next(5, 9), (ushort)mod.TileType("CarrotyxOreTile"));
                    }
                }
                spawnOrecarrot = true;
            }

            if (npc.type == NPCID.Plantera)
            {
                /*Point16 potatogen = new Point16(Main.spawnTileX*16, ((int)Main.rockLayer*16));
				//Point16 potatogen = new Point16(Main.spawnTileX, Main.spawnTileY);
				Main.NewText("A potato has grown in the dirt! (Nothing genereated because worldgen failed misserably again)", 153, 118, 69);
				StructureHelper.StructureHelper.GenerateStructure("Structures/PotatoShrine", potatogen, ModContent.GetInstance<Thinf>());*/
            }

            if (npc.type == NPCID.MeteorHead)
            {
                if (Main.rand.Next(25) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MeteorFist"), 1);
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            wasCrit = crit;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            wasCrit = crit;
        }
        public override bool PreNPCLoot(NPC npc)
        {
            if ((NPC.AnyNPCs(mod.NPCType("Cortal")) || NPC.AnyNPCs(mod.NPCType("LightKey")) || NPC.AnyNPCs(mod.NPCType("NightKey")) || NPC.AnyNPCs(mod.NPCType("FlightKey")) || NPC.AnyNPCs(mod.NPCType("Beenado"))) && npc.boss != true)
            {
                return false;
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!downedWall)
                    Main.NewText("Only more powerful enemies will now drop chlorophyll bottles...");
            }
            if ((npc.type == NPCID.SkeletronPrime || (npc.type == NPCID.Spazmatism && !NPC.AnyNPCs(NPCID.Retinazer) || (npc.type == NPCID.Retinazer && !NPC.AnyNPCs(NPCID.Spazmatism))) || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail))
            {
                if (NPC.downedMechBossAny)
                {
                    return false;
                }
                Main.NewText("There is a God at spawn");
                NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<Jerry>());
                Main.NewText("Only more powerful enemies will drop chlorophyll bottles...");
            }
            if (npc.type == NPCID.MoonLordCore && !NPC.downedMoonlord)
            {
                WorldGen.dropMeteor();
                Main.NewText("Herbs grow stronger...", 133, 255, 122);
                Main.NewText("The Underworld is filled with nightmares...", 133, 7, 7);
                Main.NewText("The meteors are more powerful...", 212, 205, 195);
                Main.NewText("The Beekeeper has a favor for you, assuming she's not dead.", Color.Yellow);
                Main.NewText("Only more powerful enemies will now drop chlorophyll bottles...");
                Main.NewText("Why is there so much text...");
            }
            if (!NPC.downedBoss1)
            {
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Potato>(), 75);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Carrot>(), 75);
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), 50);
                    Main.NewText("The spirits of agriculture have been freed.", 255, 0, 0);
                }
            }
            return true;
        }
    }
}