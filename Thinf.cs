using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Thinf.Buffs;
using Thinf.Items;
using Thinf.Items.Accessories;
using Thinf.Items.Placeables;
using Thinf.Items.Tools;
using Thinf.Items.Weapons;
using Thinf.NPCs;
using Thinf.NPCs.Blizzard;
using Thinf.NPCs.Cacterus;
using Thinf.NPCs.Core;
using Thinf.NPCs.Cortal;
using Thinf.NPCs.Herbalgamation;
using Thinf.NPCs.PrimeMinister;
using Thinf.NPCs.SoulCatcher;
using Thinf.NPCs.SpudLord;
using Thinf.NPCs.ThunderCock;
using static Thinf.NPCs.ThunderChick;

namespace Thinf
{
    public class Thinf : Mod
    {
        public int[] dungeonWalls = { WallID.BlueDungeon, WallID.BlueDungeonSlab, WallID.BlueDungeonSlabUnsafe, WallID.BlueDungeonTile, WallID.BlueDungeonTileUnsafe, WallID.BlueDungeonUnsafe, WallID.GreenDungeon, WallID.GreenDungeonSlab, WallID.GreenDungeonSlabUnsafe, WallID.GreenDungeonTile, WallID.GreenDungeonTileUnsafe, WallID.GreenDungeonUnsafe, WallID.PinkDungeon, WallID.PinkDungeonSlab, WallID.PinkDungeonSlabUnsafe, WallID.PinkDungeonTile, WallID.PinkDungeonTileUnsafe, WallID.PinkDungeonUnsafe };
        public int[] forcedNoImmunities = { BuffID.OnFire, BuffID.Poisoned, BuffID.Frostburn, BuffID.CursedInferno, BuffID.Venom, BuffID.BoneJavelin, BuffID.Bleeding, BuffID.ShadowFlame, BuffID.Ichor };
        public static ModHotKey Anvil;
        public static ModHotKey CloseAnvil;
        public static ModHotKey SpiritSocks;
        public static ModHotKey DroneUp;
        public static ModHotKey DroneDown;
        public static ModHotKey DroneLeft;
        public static ModHotKey DroneRight;
        public static ModHotKey DroneSemiAutoAttack;
        public static ModHotKey DroneAbility;
        public static Thinf Instance;
        GameTime _lastUpdateUiGameTime;
        internal UserInterface MyInterface;
        internal AnvilUI MyUI;
        public override void ModifyLightingBrightness(ref float scale)
        {
            if (Main.player[Main.myPlayer].blackout && NPC.AnyNPCs(NPCType("CarrotKing")))
            {
                scale = 0.30f;
            }

            if (Main.player[Main.myPlayer].blackout && NPC.AnyNPCs(NPCType("IchorElemental")))
            {
                scale = 0.60f;
            }
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (!Main.gameMenu && MyPlayer.hereComesTheMoney)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/HereComesTheMoney");
                priority = MusicPriority.BossHigh;
            }
            if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneChestWasteland)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/StorageOverflow");
                priority = MusicPriority.BiomeHigh;
            }
            if (!Main.gameMenu && Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/The_Debuff_Was_Just_Cosmetic_They_Said");
                priority = MusicPriority.BiomeHigh;
            }
            //Checks if the invasion is in the correct spot, if it is, then change the music
            if (ModNameWorld.DungeonArmyUp == true && Main.invasionX == Main.spawnTileX)

            {
                music = MusicID.Dungeon;

            }
        }
        public override void AddRecipes()
        {
            #region Bloodclot recipe stuff
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Moonglow, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.MagicPowerPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.RottenChunk, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.BattlePotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.Vertebrae, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.BattlePotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.Cactus, 1);
            recipe.AddIngredient(ItemID.WormTooth, 1);
            recipe.AddIngredient(ItemID.Stinger, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.ThornsPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.Blinkroot, 1);
            recipe.AddIngredient(ItemID.Feather, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.GravitationPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Bone, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.Shiverthorn, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.TitanPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.CrimsonTigerfish, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.RagePotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Ebonkoi, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.WrathPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Stinkfish, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.StinkPotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddIngredient(ItemID.Moonglow, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.CratePotion);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Daybloom, 1);
            recipe.AddIngredient(ItemID.Moonglow, 1);
            recipe.AddIngredient(ItemID.Blinkroot, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(ModContent.ItemType<Bloodclot>(), 1);
            recipe.AddIngredient(ItemID.Shiverthorn, 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(ItemID.GenderChangePotion);
            recipe.AddRecipe();
            #endregion
        }
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call(
                   "AddMiniBoss",
                   0f,
                   ModContent.NPCType<AcornSpirit>(),
                   this, // Mod
                   "Acorn Spirit",
                   (Func<bool>)(() => ModNameWorld.downedAcornSpirit),
                   null,
                   new List<int> { ItemID.Acorn },
                   new List<int> { ItemID.Aglet, ItemID.Radar, ItemID.ClimbingClaws, ItemID.CordageGuide, ItemID.WoodenBoomerang, ItemID.Blowpipe, ItemID.Spear, ItemID.WandofSparking, ItemID.ThrowingKnife, ItemID.Glowstick, ItemID.Umbrella, ItemID.Wood, ItemID.LivingLoom },
                   $"Chop a bit too much trees.",
                   "The Acorn Spirit calls an Uber.",
                   "Thinf/NPCs/AcornSpiritBC"
                   );

                bossChecklist.Call(
                "AddBoss",
                5.5f,
                ModContent.NPCType<Cacterus>(),
                this, // Mod
                "Cacterus",
                (Func<bool>)(() => ModNameWorld.downedCacterus),
                ModContent.ItemType<Items.CacterusEffigy>(),
                new List<int> { ModContent.ItemType<Items.THE_SUPER_COOL_BADASS_LORE.CacterusBusinessCard>() },
                new List<int> { ModContent.ItemType<Items.Weapons.TrueCactusSword>(), ModContent.ItemType<Items.Weapons.MeteorGun>(), ModContent.ItemType<Items.Weapons.CactusStaff>(), ModContent.ItemType<Items.Weapons.NormalCactusPot>(), ModContent.ItemType<Items.Accessories.FlyingFlower>() },
                $"Use a [i:{ModContent.ItemType<CacterusEffigy>()}] in a Corrupt/Crimson Desert",
                "Cacterus wishes you good luck on your next try...",
                "Thinf/NPCs/Cacterus/Cacterus_BC",
                "Thinf/NPCs/Cacterus/Cacterus_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                3.5f,
                ModContent.NPCType<Cortal>(),
                this, // Mod
                "Cortal",
                (Func<bool>)(() => ModNameWorld.downedCortal),
                ModContent.ItemType<Items.SpecularGem>(),
                new List<int> { ModContent.ItemType<Items.THE_SUPER_COOL_BADASS_LORE.CortalsFin>() },
                new List<int> { ModContent.ItemType<CortalMirror>(), ModContent.ItemType<Cortascale>(), ItemID.SpecularFish, ModContent.ItemType<NeverendingSpecularSoup>(), ModContent.ItemType<RodOfCortal>() },
                $"Use a [i:{ModContent.ItemType<SpecularGem>()}] while submerged in water.",
                "Cortal teleports away...",
                "Thinf/NPCs/Cortal/CortalBC",
                "Thinf/NPCs/Cortal/Cortal_Head_Boss"
                );

                bossChecklist.Call(
                "AddMiniBoss",
                3.6f,
                ModContent.NPCType<CommandoFlashlight>(),
                this, // Mod
                "Commando Flashlight",
                (Func<bool>)(() => ModNameWorld.downedFlashlight),
                null,
                new List<int> { },
                new List<int> { },
                $"He'll come during the night. When? Well, that's for you to figure out.",
                "Commando Flashlight flies away!",
                "Thinf/NPCs/CommandoFlashlight"
                );

                bossChecklist.Call(
                "AddBoss",
                0.5f,
                ModContent.NPCType<ThunderCock>(),
                this, // Mod
                "Thunder Cock",
                (Func<bool>)(() => ModNameWorld.downedThundercock),
                ModContent.ItemType<ThunderChickItem>(),
                new List<int> { ModContent.ItemType<Items.THE_SUPER_COOL_BADASS_LORE.ThunderEgg>() },
                new List<int> { ModContent.ItemType<Battery>(), ModContent.ItemType<BatteryStaff>(), ModContent.ItemType<Climax>(), ModContent.ItemType<Transformer>() },
                $"Kill a [i:{ModContent.ItemType<ThunderChickItem>()}]. Thunder Chicks spawn during the rain.",
                "Thunder Cock returns to its nest...",
                "Thinf/NPCs/ThunderCock/ThunderCock",
                "Thinf/NPCs/ThunderCock/ThunderCock_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                9.5f,
                new List<int>(){
                ModContent.NPCType<FlightKey>(),
                ModContent.NPCType<LightKey>(),
                ModContent.NPCType<NightKey>()
                },
                this, // Mod
                "The Soul Keys",
                (Func<bool>)(() => ModNameWorld.downedSoulKeys),
                ModContent.ItemType<KeyOfSouls>(),
                new List<int> { ModContent.ItemType<Items.Accessories.ChestWastelandMusicBox>() },
                new List<int> { ModContent.ItemType<FragmentOfFlight>(), ModContent.ItemType<FragmentOfLight>(), ModContent.ItemType<FragmentOfNight>() },
                $"Use a [i:{ModContent.ItemType<KeyOfSouls>()}] in the Chest Wasteland. A Chest Wasteland biome will form when 20 chests are present.",
                "The Soul Keys succeed in protecting the Chest Wastelands...",
                new List<string>() { "Thinf/NPCs/LightKey", "Thinf/NPCs/LightKey", "Thinf/NPCs/FlightKey", }
                );

                bossChecklist.Call(
                "AddBoss",
                10.5f,
                ModContent.NPCType<SpudLord>(),
                this, // Mod
                "The Spud Lord",
                (Func<bool>)(() => ModNameWorld.downedSpudLord),
                ModContent.ItemType<PotatoProbe>(),
                new List<int> { ModContent.ItemType<Potato>() },
                new List<int> { ModContent.ItemType<McPickaxe>() },
                $"Use a [i:{ModContent.ItemType<PotatoProbe>()}] in the underground layer. Enrages outside the underground layer.",
                "The Spud Lord burrows back down...",
                "Thinf/NPCs/SpudLord/SpudLord",
                "Thinf/NPCs/SpudLord/SpudLord_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                14.5f,
                ModContent.NPCType<Herbalgamation>(),
                this, // Mod
                "herbalgamaiton",
                (Func<bool>)(() => ModNameWorld.downedHerbalgamation),
                ModContent.ItemType<HerbalgamatedClump>(),
                new List<int> { ModContent.ItemType<CosmicHerbalPiece>() },
                new List<int> { ModContent.ItemType<HerbalCore>() },
                $"[i:{ModContent.ItemType<HerbalgamatedClump>()}]. will get angry outside forest.",
                "Herbalgamation laughs maniacally as they destroy another human.",
                "Thinf/NPCs/Herbalgamation/Herbalgamation",
                "Thinf/NPCs/Herbalgamation/Herbalgamation_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                14.5f,
                new List<int>(){
                ModContent.NPCType<PrimeMinister>(),
                ModContent.NPCType<PrimeMinisterCopter>(),
                ModContent.NPCType<PrimeMinisterTank>(),
                ModContent.NPCType<PrimeMinisterTheManHimself>()
                },
                this, // Mod
                "Prime Minister",
                (Func<bool>)(() => ModNameWorld.downedPM),
                ModContent.ItemType<PoliticianBait>(),
                new List<int> { },
                new List<int> { },
                $"Throw [i:{ModContent.ItemType<PoliticianBait>()}] in the jungle and wait. Politician Bait can be obtained from the Beekeeper. If you lose it, you can always get more.",
                "Prime Minister successfully defeats evil! Vote for him in the 2024 election.",
                "Thinf/NPCs/PrimeMinister/PrimeMinister",
                "Thinf/NPCs/PrimeMinister/PrimeMinisterTheManHimself"
                );


                bossChecklist.Call(
                "AddBoss",
                14.5f,
                ModContent.NPCType<SoulCatcher>(),
                this, // Mod
                "Soul Catcher",
                (Func<bool>)(() => ModNameWorld.downedSoulCatcher),
                ModContent.ItemType<AmuletOfSouls>(),
                new List<int> { },
                new List<int> { ModContent.ItemType<Linimisifrififlium>()},
                $"Use an [i:{ModContent.ItemType<AmuletOfSouls>()}] in the Underworld. Enrages outside the underworld.",
                "Soul Catcher takes another soul. He didn't really like it so let it go.",
                "Thinf/NPCs/SoulCatcher/SoulCatcher",
                "Thinf/NPCs/SoulCatcher/SoulCatcher_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                14.5f,
                ModContent.NPCType<Blizzard>(),
                this, // Mod
                "Blizzard",
                (Func<bool>)(() => ModNameWorld.downedBlizzard),
                ModContent.ItemType<ChillyCube>(),
                new List<int> { },
                new List<int> { ModContent.ItemType<FrozenEssence>() },
                $"Throw a [i:{ModContent.ItemType<ChillyCube>()}] in the Snow Biome. Enrages outside the Snow Biome.",
                "Blizzard goes back to bake some cupcakes.",
                "Thinf/NPCs/Blizzard/Blizzard",
                "Thinf/NPCs/Blizzard/Blizzard_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                15f,
                ModContent.NPCType<Core>(),
                this, // Mod
                "Core",
                (Func<bool>)(() => ModNameWorld.coreDestroyed),
                null,
                new List<int> { },
                new List<int> { },
                "Go to the middle of the world and tell her that nobody loves her.",
                "Core goes back to the middle of the world.",
                "Thinf/NPCs/Core/Core",
                "Thinf/NPCs/Core/Core_Head_Boss"
                );
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            MyInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Thinf: A Description",
                    delegate
                    {
                        MyInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public override void Load()
        {
            On.Terraria.NPC.AddBuff += NPC_AddBuff;
            On.Terraria.WorldGen.TileRunner += WorldGen_TileRunner;
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyUI = new AnvilUI();
                MyUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element

                MyInterface = new UserInterface();
                MyInterface.SetState(MyUI);
            }
            SpiritSocks = RegisterHotKey("Spirit Socks Dash", "F");
            Anvil = RegisterHotKey("Open Renaming Scroll", "L");
            CloseAnvil = RegisterHotKey("Close Renaming Scroll", "Escape");
            DroneUp = RegisterHotKey("Drone Up", "W");
            DroneDown = RegisterHotKey("Drone Down", "S");
            DroneLeft = RegisterHotKey("Drone Left", "A");
            DroneRight = RegisterHotKey("Drone Right", "D");
            DroneSemiAutoAttack = RegisterHotKey("Drone Semi Auto Weapon Attack", "Mouse1");
            DroneAbility = RegisterHotKey("Drone Ability", "Mouse2");

            Filters.Scene["Thinf:HoneyImHome"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(1f, 0.819f, 0.231f).UseOpacity(1f), EffectPriority.High);
            SkyManager.Instance["Thinf:HoneyImHome"] = new ModSky();

            Filters.Scene["Thinf:ChestWasteland"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0, 0, 0).UseOpacity(0.45f), EffectPriority.High);
            SkyManager.Instance["Thinf:ChestWasteland"] = new ModSky();

            Filters.Scene["Thinf:Nightmare"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(Color.DarkRed).UseOpacity(0.85f), EffectPriority.High);
            SkyManager.Instance["Thinf:Nightmare"] = new NightmareSky();

            Filters.Scene["Thinf:CoreDead"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(Color.Black).UseOpacity(0.75f), EffectPriority.High);
            SkyManager.Instance["Thinf:CoreDead"] = new DeadSky();

            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/StorageOverflow"), ModContent.ItemType<ChestWastelandMusicBox>(), ModContent.TileType<Blocks.ChestWastelandMusicBoxTile>());
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/The_Rooted_Menace"), ModContent.ItemType<SpudLordMusicBox>(), ModContent.TileType<Blocks.SpudLordMusicBoxTile>());
        }

        private void WorldGen_TileRunner(On.Terraria.WorldGen.orig_TileRunner orig, int i, int j, double strength, int steps, int type, bool addTile, float speedX, float speedY, bool noYChange, bool overRide)
        {
            if (type == ModContent.TileType<Blocks.TomatoBlockTile>())
            {
                while (WorldGen.TileEmpty(i, j))
                {
                    j++;
                }
                double num = strength;
                float num2 = steps;
                Vector2 vector = default(Vector2);
                vector.X = i;
                vector.Y = j;
                Vector2 vector2 = default(Vector2);
                vector2.X = WorldGen.genRand.Next(-10, 11) * 0.1f;
                vector2.Y = WorldGen.genRand.Next(-10, 11) * 0.1f;
                if (speedX != 0f || speedY != 0f)
                {
                    vector2.X = speedX;
                    vector2.Y = speedY;
                }
                bool flag = type == 368;
                bool flag2 = type == 367;
                while (num > 0.0 && num2 > 0f)
                {
                    if (vector.Y < 0f && num2 > 0f && type == 59)
                    {
                        num2 = 0f;
                    }
                    num = strength * (num2 / steps);
                    num2 -= 1f;
                    int num3 = (int)(vector.X - num * 0.5);
                    int num4 = (int)(vector.X + num * 0.5);
                    int num5 = (int)(vector.Y - num * 0.5);
                    int num6 = (int)(vector.Y + num * 0.5);
                    if (num3 < 1)
                    {
                        num3 = 1;
                    }
                    if (num4 > Main.maxTilesX - 1)
                    {
                        num4 = Main.maxTilesX - 1;
                    }
                    if (num5 < 1)
                    {
                        num5 = 1;
                    }
                    if (num6 > Main.maxTilesY - 1)
                    {
                        num6 = Main.maxTilesY - 1;
                    }
                    for (int k = num3; k < num4; k++)
                    {
                        for (int l = num5; l < num6; l++)
                        {
                            if (!(Math.Abs(k - vector.X) + Math.Abs(l - vector.Y) < strength * 0.5 * (1.0 + WorldGen.genRand.Next(-10, 11) * 0.015)))
                            {
                                continue;
                            }
                            if (type < 0)
                            {
                                if (type == -2 && Main.tile[k, l].active() && (l < WorldGen.waterLine || l > WorldGen.lavaLine))
                                {
                                    Main.tile[k, l].liquid = byte.MaxValue;
                                    if (l > WorldGen.lavaLine)
                                    {
                                        Main.tile[k, l].lava(lava: true);
                                    }
                                }
                                Main.tile[k, l].active(active: false);
                                continue;
                            }
                            if (overRide || !Main.tile[k, l].active())
                            {
                                Tile tile = Main.tile[k, l];
                                bool flag3 = false;
                                flag3 = Main.tileStone[type] && tile.type != 1;
                                if (!TileID.Sets.CanBeClearedDuringGeneration[tile.type])
                                {
                                    flag3 = true;
                                }
                                if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.PinkDungeonBrick || tile.type == TileID.GreenDungeonBrick || dungeonWalls.Contains(tile.wall) || l >= Main.worldSurface && Vector2.Distance(new Vector2(i, j), new Vector2(k, l)) >= 50)
                                {
                                    flag3 = true;
                                }
                                if (!flag3)
                                {
                                    tile.type = (ushort)type;
                                }
                            }
                            if (addTile)
                            {
                                Main.tile[k, l].active(active: true);
                                Main.tile[k, l].liquid = 0;
                                Main.tile[k, l].lava(lava: false);
                            }
                            if (type == 59 && l > WorldGen.waterLine && Main.tile[k, l].liquid > 0)
                            {
                                Main.tile[k, l].lava(lava: false);
                                Main.tile[k, l].liquid = 0;
                            }
                        }
                    }
                    vector += vector2;
                    if (num > 50.0)
                    {
                        vector += vector2;
                        num2 -= 1f;
                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (num > 100.0)
                        {
                            vector += vector2;
                            num2 -= 1f;
                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                            if (num > 150.0)
                            {
                                vector += vector2;
                                num2 -= 1f;
                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                if (num > 200.0)
                                {
                                    vector += vector2;
                                    num2 -= 1f;
                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    if (num > 250.0)
                                    {
                                        vector += vector2;
                                        num2 -= 1f;
                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        if (num > 300.0)
                                        {
                                            vector += vector2;
                                            num2 -= 1f;
                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            if (num > 400.0)
                                            {
                                                vector += vector2;
                                                num2 -= 1f;
                                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                if (num > 500.0)
                                                {
                                                    vector += vector2;
                                                    num2 -= 1f;
                                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    if (num > 600.0)
                                                    {
                                                        vector += vector2;
                                                        num2 -= 1f;
                                                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        if (num > 700.0)
                                                        {
                                                            vector += vector2;
                                                            num2 -= 1f;
                                                            vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            if (num > 800.0)
                                                            {
                                                                vector += vector2;
                                                                num2 -= 1f;
                                                                vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                if (num > 900.0)
                                                                {
                                                                    vector += vector2;
                                                                    num2 -= 1f;
                                                                    vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    vector2.X += WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if (vector2.X > 1f)
                    {
                        vector2.X = 1f;
                    }
                    if (vector2.X < -1f)
                    {
                        vector2.X = -1f;
                    }
                    if (!noYChange)
                    {
                        vector2.Y += WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (vector2.Y > 1f)
                        {
                            vector2.Y = 1f;
                        }
                        if (vector2.Y < -1f)
                        {
                            vector2.Y = -1f;
                        }
                    }
                    else if (type != 59 && num < 3.0)
                    {
                        if (vector2.Y > 1f)
                        {
                            vector2.Y = 1f;
                        }
                        if (vector2.Y < -1f)
                        {
                            vector2.Y = -1f;
                        }
                    }
                    if (type == 59 && !noYChange)
                    {
                        if (vector2.Y > 0.5)
                        {
                            vector2.Y = 0.5f;
                        }
                        if (vector2.Y < -0.5)
                        {
                            vector2.Y = -0.5f;
                        }
                        if (vector.Y < Main.rockLayer + 100.0)
                        {
                            vector2.Y = 1f;
                        }
                        if (vector.Y > Main.maxTilesY - 300)
                        {
                            vector2.Y = -1f;
                        }
                    }
                }
            }
            else
            {
                orig(i, j, strength, steps, type, addTile, speedX, speedY, noYChange, overRide);
            }
        }
        private void NPC_AddBuff(On.Terraria.NPC.orig_AddBuff orig, NPC self, int type, int time, bool quiet)
        {
            if (self.HasBuff(ModContent.BuffType<ImmunityLoss>()) && forcedNoImmunities.Contains(type))
            {
                if (!quiet)
                {
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(53, -1, -1, null, self.whoAmI, type, time);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(54, -1, -1, null, self.whoAmI);
                    }
                }
                int num = -1;
                for (int i = 0; i < 5; i++)
                {
                    if (self.buffType[i] == type)
                    {
                        if (!BuffLoader.ReApply(type, Main.LocalPlayer, time, i) && self.buffTime[i] < time)
                        {
                            self.buffTime[i] = time;
                        }
                        return;
                    }
                }
                while (num == -1)
                {
                    int num2 = -1;
                    for (int j = 0; j < 5; j++)
                    {
                        if (!Main.debuff[self.buffType[j]])
                        {
                            num2 = j;
                            break;
                        }
                    }
                    if (num2 == -1)
                    {
                        return;
                    }
                    for (int k = num2; k < 5; k++)
                    {
                        if (self.buffType[k] == 0)
                        {
                            num = k;
                            break;
                        }
                    }
                    if (num == -1)
                    {
                        self.DelBuff(num2);
                    }
                }
                self.buffType[num] = type;
                self.buffTime[num] = time;
            }
            else
            {
                orig(self, type, time, quiet);
            }
        }

        internal void ShowMyUI()
        {
            MyInterface?.SetState(MyUI);
        }
        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
        }
        public override void Unload()
        {
            MyUI = null;
        }
        public static int ToTicks(int seconds)
        {
            return (seconds * 60);
        }

        public static int MinutesToTicks(int minutes)
        {
            return (minutes * 3600);
        }
        public static void GeneticAmplification(Vector2 spawnPos, int npcType, int? modifiedHealth = null, int? modifiedDamage = null, int? modifiedDefense = null, string modifiedName = null)
        {
            NPC npc = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, npcType)];
            if (modifiedName != null) npc.GivenName = modifiedName;
            if (modifiedDamage != null) npc.damage = (int)modifiedDamage;
            if (modifiedDefense != null) npc.defense = (int)modifiedDefense;
            if (modifiedHealth != null) npc.lifeMax = (int)modifiedHealth;
        }
        public static void NPCGotoPlayer(NPC npc, Player target, float speed)
        {
            npc.velocity = npc.DirectionTo(target.Center) * speed;
        }
        public static void NPCGotoPlayerRandom(NPC npc, Player target, float speed)
        {
            npc.velocity = (npc.DirectionTo(target.Center) * speed).RotatedByRandom(MathHelper.ToRadians(360));
        }
        public static void ProjGotoPlayer(Projectile proj, Player target, float speed)
        {
            proj.velocity = proj.DirectionTo(target.Center) * speed;
        }

        public static Player FindNearestPlayer(float distance, Vector2 origin)
        {
            for (int i = 0; i < Main.maxPlayers; ++i)
            {
                Player player = Main.player[i];
                Player otherPlayer = Main.player[i + 1];
                if (player.active && otherPlayer.active)
                {
                    if (Vector2.Distance(player.Center, origin) < Vector2.Distance(otherPlayer.Center, origin))
                    {
                        return player;
                    }
                    else
                    {
                        return otherPlayer;
                    }
                }
                if (Main.dedServ)
                {
                    if (player.Distance(origin) <= distance)
                    {
                        return player;
                    }
                }
            }
            return Main.LocalPlayer;
        }

        public static void QuickSpawnNPC(NPC npc, int type)
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, type);
        }
        public static void QuickSpawnNPCFromProj(Projectile projectile, int type)
        {
            NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, type);
        }

    }
}