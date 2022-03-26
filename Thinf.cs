using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using static Terraria.Main;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Thinf.Blocks;
using Thinf.Buffs;
using Thinf.Items;
using Thinf.Items.Accessories;
using Thinf.Items.Placeables;
using Thinf.Items.Tools;
using Thinf.Items.Weapons;
using Thinf.NPCs;
using Thinf.NPCs.Blizzard;
using Thinf.NPCs.Cacterus;
using Thinf.NPCs.CarrotKing;
using Thinf.NPCs.Core;
using Thinf.NPCs.Cortal;
using Thinf.NPCs.Herbalgamation;
using Thinf.NPCs.JerryEX;
using Thinf.NPCs.PrimeMinister;
using Thinf.NPCs.SoulCatcher;
using Thinf.NPCs.SpudLord;
using Thinf.NPCs.ThunderCock;
using Thinf.Projectiles;
using static Thinf.NPCs.ThunderChick;
using System.Diagnostics;

namespace Thinf
{
    public class Thinf : Mod
    {
        public int[] dungeonWalls = { WallID.BlueDungeon, WallID.BlueDungeonSlab, WallID.BlueDungeonSlabUnsafe, WallID.BlueDungeonTile, WallID.BlueDungeonTileUnsafe, WallID.BlueDungeonUnsafe, WallID.GreenDungeon, WallID.GreenDungeonSlab, WallID.GreenDungeonSlabUnsafe, WallID.GreenDungeonTile, WallID.GreenDungeonTileUnsafe, WallID.GreenDungeonUnsafe, WallID.PinkDungeon, WallID.PinkDungeonSlab, WallID.PinkDungeonSlabUnsafe, WallID.PinkDungeonTile, WallID.PinkDungeonTileUnsafe, WallID.PinkDungeonUnsafe };
        public int[] forcedNoImmunities = { BuffID.OnFire, BuffID.Poisoned, BuffID.Frostburn, BuffID.CursedInferno, BuffID.Venom, BuffID.BoneJavelin, BuffID.Bleeding, BuffID.ShadowFlame, BuffID.Ichor };
        public static ModHotKey Anvil;
        public static ModHotKey CloseAnvil;
        public static ModHotKey SpiritSocks;
        public static ModHotKey DroneCancel;
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
            if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneTomatoTown)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/SleepyTomatoCity");
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
                //bossChecklist.Call(
                //   "AddMiniBoss",
                //   0f,
                //   ModContent.NPCType<AcornSpirit>(),
                //   this, // Mod
                //   "Acorn Spirit",
                //   (Func<bool>)(() => ModNameWorld.downedAcornSpirit),
                //   null,
                //   new List<int> { ItemID.Acorn },
                //   new List<int> { ItemID.Aglet, ItemID.Radar, ItemID.ClimbingClaws, ItemID.CordageGuide, ItemID.WoodenBoomerang, ItemID.Blowpipe, ItemID.Spear, ItemID.WandofSparking, ItemID.ThrowingKnife, ItemID.Glowstick, ItemID.Umbrella, ItemID.Wood, ItemID.LivingLoom },
                //   $"Say 'I hate trees' 4 times into the chat while holding a [i:{ItemID.IronAxe}] or [i:{ItemID.LeadAxe}]",
                //   "The Acorn Spirit calls an Uber.",
                //   "Thinf/NPCs/AcornSpiritBC"
                //   );

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
                $"Will ambush you during the night.",
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
                7.5f,
                ModContent.NPCType<JerryEXMain>(),
                this, // Mod
                "Jerry EX",
                (Func<bool>)(() => ModNameWorld.downedJerry),
                ModContent.ItemType<JerryRemote>(),
                new List<int> { },
                new List<int> { ModContent.ItemType<Tomato>()},
                $"After defeating any mech boss, reject Jerry and go to the Tomato Fields or use a [i:{ModContent.ItemType<JerryRemote>()}] in the Tomato Fields.",
                "And never come back!",
                "Thinf/NPCs/JerryEXBIG",
                "Thinf/NPCs/ThunderCock/JerryEXMain_Head_Boss"
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
                10.500000000001f,
                ModContent.NPCType<CarrotKing>(),
                this, // Mod
                "The Carrot King",
                (Func<bool>)(() => ModNameWorld.downedSpudLord),
                ModContent.ItemType<Carrotrahedron>(),
                new List<int> { ModContent.ItemType<Items.Placeables.Carrot>() },
                new List<int> { ModContent.ItemType<Items.Tools.Drarrot>() },
                $"Use a [i:{ModContent.ItemType<Carrotrahedron>()}] in the underground layer. Enrages outside the underground layer.",
                "The Carrot King digs back into the ground, in style.",
                "Thinf/NPCs/CarrotKing/CarrotKing",
                "Thinf/NPCs/CarrotKing/CarrotKing_Head_Boss"
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
                "Reply hazy try again later",
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
                "Thinf/NPCs/PrimeMinister/PrimeMinisterBC",
                "Thinf/NPCs/PrimeMinister/PrimeMinister_Head_Boss"
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
                new List<int> { ModContent.ItemType<Linimisifrififlium>() },
                $"Use an [i:{ModContent.ItemType<AmuletOfSouls>()}] in the Underworld. Enrages outside the underworld.",
                "Soul Catcher takes another soul. He didn't really like it.",
                "Thinf/NPCs/SoulCatcher/SoulCatcherBC",
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
                "Blizzard disappears as fast as she came!",
                "Thinf/NPCs/Blizzard/BlizzardBC",
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
                $"Go to the middle of the world and insult her. You can also use a [i:{ModContent.ItemType<CorePlushie>()}]",
                "LeftShift LeftShift LeftShift LeftShift LeftShift",
                "Thinf/NPCs/Core/CoreBC",
                "Thinf/NPCs/Core/Core_Head_Boss"
                );

                bossChecklist.Call(
                "AddBoss",
                15.1f,
                ModContent.NPCType<Overclock>(),
                this, // Mod
                "Time Loop",
                (Func<bool>)(() => ModNameWorld.timeLoop),
                null,
                new List<int> { },
                new List<int> { },
                $"Not a boss, just checking if you used the [i:{ModContent.ItemType<TimeLooper>()}]. Don't forget to use it, it's important for progression! If you trashed it, then you'll have to beat Core again.",
                "Place Holder Text Sex Among Us Balls",
                "Thinf/NPCs/Core/CoreBC",
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
            Anvil = RegisterHotKey("Open Renaming Scroll", "L");
            CloseAnvil = RegisterHotKey("Close Renaming Scroll", "Escape");
            SpiritSocks = RegisterHotKey("Spirit Socks Dash", "F");
            DroneCancel = RegisterHotKey("Drone Cancel", "Q");
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

        public static void TomatoIslandHouse(int i, int j)
        {
            byte type = (byte)ModContent.TileType<TomatoBlockTile>();
            byte wall = WallID.Dirt;
            Vector2 vector = new Vector2(i, j);
            int num = 1;
            if (WorldGen.genRand.Next(2) == 0)
            {
                num = -1;
            }
            int num12 = WorldGen.genRand.Next(7, 12);
            int num16 = WorldGen.genRand.Next(5, 7);
            vector.X = i + (num12 + 2) * num;
            for (int k = j - 15; k < j + 30; k++)
            {
                if (Main.tile[(int)vector.X, k].active())
                {
                    vector.Y = k - 1;
                    break;
                }
            }
            vector.X = i;
            int num17 = (int)(vector.X - (float)num12 - 1f);
            int num18 = (int)(vector.X + (float)num12 + 1f);
            int num19 = (int)(vector.Y - (float)num16 - 1f);
            int num20 = (int)(vector.Y + 2f);
            if (num17 < 0)
            {
                num17 = 0;
            }
            if (num18 > Main.maxTilesX)
            {
                num18 = Main.maxTilesX;
            }
            if (num19 < 0)
            {
                num19 = 0;
            }
            if (num20 > Main.maxTilesY)
            {
                num20 = Main.maxTilesY;
            }
            for (int l = num17; l <= num18; l++)
            {
                for (int m = num19 - 1; m < num20 + 1; m++)
                {
                    if (m != num19 - 1 || (l != num17 && l != num18))
                    {
                        Main.tile[l, m].active(active: true);
                        Main.tile[l, m].liquid = 0;
                        Main.tile[l, m].type = (ushort)ModContent.TileType<TomatoBlockTile>();
                        Main.tile[l, m].wall = 0;
                        Main.tile[l, m].halfBrick(halfBrick: false);
                        Main.tile[l, m].slope(0);
                    }
                }
            }
            num17 = (int)(vector.X - (float)num12);
            num18 = (int)(vector.X + (float)num12);
            num19 = (int)(vector.Y - (float)num16);
            num20 = (int)(vector.Y + 1f);
            if (num17 < 0)
            {
                num17 = 0;
            }
            if (num18 > Main.maxTilesX)
            {
                num18 = Main.maxTilesX;
            }
            if (num19 < 0)
            {
                num19 = 0;
            }
            if (num20 > Main.maxTilesY)
            {
                num20 = Main.maxTilesY;
            }
            for (int n = num17; n <= num18; n++)
            {
                for (int num21 = num19; num21 < num20; num21++)
                {
                    if ((num21 != num19 || (n != num17 && n != num18)) && Main.tile[n, num21].wall == 0)
                    {
                        Main.tile[n, num21].active(active: false);
                        Main.tile[n, num21].wall = WallID.Dirt;
                    }
                }
            }
            int num22 = i + (num12 + 1) * num;
            int num2 = (int)vector.Y;
            for (int num3 = num22 - 2; num3 <= num22 + 2; num3++)
            {
                Main.tile[num3, num2].active(active: false);
                Main.tile[num3, num2 - 1].active(active: false);
                Main.tile[num3, num2 - 2].active(active: false);
            }

            WorldGen.PlaceTile(num22, num2, 10, mute: true, forced: false, -1, 2);
            num22 = i + (num12 + 1) * -num - num;
            for (int num4 = num19; num4 <= num20 + 1; num4++)
            {
                Main.tile[num22, num4].active(active: true);
                Main.tile[num22, num4].liquid = 0;
                Main.tile[num22, num4].type = (ushort)ModContent.TileType<TomatoBlockTile>();
                Main.tile[num22, num4].wall = WallID.Dirt;
                Main.tile[num22, num4].halfBrick(halfBrick: false);
                Main.tile[num22, num4].slope(0);
            }
            int contain = 0;
            int num5 = 12;
            if (num5 > 2)
            {
                num5 = WorldGen.genRand.Next(3);
            }
            switch (num5)
            {
                case 0:
                    contain = ModContent.ItemType<JerryRemote>();
                    break;
                case 1:
                    contain = ModContent.ItemType<JerryRemote>();
                    break;
                case 2:
                    contain = ModContent.ItemType<JerryRemote>();
                    break;
            }
            WorldGen.AddBuriedChest(i, num2 - 5, ModContent.ItemType<JerryRemote>(), true);
            int num6 = i - num12 / 2 + 1;
            int num7 = i + num12 / 2 - 1;
            int num8 = 1;
            if (num12 > 10)
            {
                num8 = 2;
            }
            int num9 = (num19 + num20) / 2 - 1;
            for (int num10 = num6 - num8; num10 <= num6 + num8; num10++)
            {
                for (int num11 = num9 - 1; num11 <= num9 + 1; num11++)
                {
                    Main.tile[num10, num11].wall = WallID.Dirt;
                }
            }
            for (int num13 = num7 - num8; num13 <= num7 + num8; num13++)
            {
                for (int num14 = num9 - 1; num14 <= num9 + 1; num14++)
                {
                    Main.tile[num13, num14].wall = WallID.Dirt;
                }
            }
            int num15 = i + (num12 / 2 + 1) * -num;
            WorldGen.PlaceTile(num15, num20 - 1, 14, mute: true, forced: false, -1, 2);
            WorldGen.PlaceTile(num15 - 2, num20 - 1, 15, mute: true, forced: false, 0, 3);
            Main.tile[num15 - 2, num20 - 1].frameX += 18;
            Main.tile[num15 - 2, num20 - 2].frameX += 18;
            WorldGen.PlaceTile(num15 + 2, num20 - 1, 15, mute: true, forced: false, 0, 3);
            int i2 = num17 + 1;
            int j2 = num19;
            WorldGen.PlaceTile(i2, j2, 91, mute: true, forced: false, -1, 0);
            i2 = num18 - 1;
            j2 = num19;
            WorldGen.PlaceTile(i2, j2, 91, mute: true, forced: false, -1, 0);
            if (num > 0)
            {
                i2 = num17;
                j2 = num19 + 1;
            }
            else
            {
                i2 = num18;
                j2 = num19 + 1;
            }
            WorldGen.PlaceTile(i2, j2, 91, mute: true, forced: false, -1, 0);
        }
        public static void TomatoFloatingIsland(int i, int j)
        {
            double num = WorldGen.genRand.Next(100, 150);
            double num12 = num;
            float num22 = WorldGen.genRand.Next(20, 30);
            int num32 = i;
            int num43 = i;
            int num47 = i;
            int num48 = j;
            Vector2 vector = default(Vector2);
            vector.X = i;
            vector.Y = j;
            Vector2 vector2 = default(Vector2);
            vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            while (vector2.X > -2f && vector2.X < 2f)
            {
                vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            }
            vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
            while (num > 0.0 && num22 > 0f)
            {
                num -= (double)WorldGen.genRand.Next(4);
                num22 -= 1f;
                int num50 = (int)((double)vector.X - num * 0.5);
                int num52 = (int)((double)vector.X + num * 0.5);
                int num3 = (int)((double)vector.Y - num * 0.5);
                int num5 = (int)((double)vector.Y + num * 0.5);
                if (num50 < 0)
                {
                    num50 = 0;
                }
                if (num52 > Main.maxTilesX)
                {
                    num52 = Main.maxTilesX;
                }
                if (num3 < 0)
                {
                    num3 = 0;
                }
                if (num5 > Main.maxTilesY)
                {
                    num5 = Main.maxTilesY;
                }
                num12 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                float num6 = vector.Y + 1f;
                for (int k = num50; k < num52; k++)
                {
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        num6 += (float)WorldGen.genRand.Next(-1, 2);
                    }
                    if (num6 < vector.Y)
                    {
                        num6 = vector.Y;
                    }
                    if (num6 > vector.Y + 2f)
                    {
                        num6 = vector.Y + 2f;
                    }
                    for (int l = num3; l < num5; l++)
                    {
                        if (!((float)l > num6))
                        {
                            continue;
                        }
                        float num53 = Math.Abs((float)k - vector.X);
                        float num7 = Math.Abs((float)l - vector.Y) * 3f;
                        if (Math.Sqrt(num53 * num53 + num7 * num7) < num12 * 0.4)
                        {
                            if (k < num32)
                            {
                                num32 = k;
                            }
                            if (k > num43)
                            {
                                num43 = k;
                            }
                            if (l < num47)
                            {
                                num47 = l;
                            }
                            if (l > num48)
                            {
                                num48 = l;
                            }
                            Main.tile[k, l].active(active: true);
                            Main.tile[k, l].type = 189;
                            WorldGen.SquareTileFrame(k, l);
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-20, 21) * 0.05f;
                if (vector2.X > 1f)
                {
                    vector2.X = 1f;
                }
                if (vector2.X < -1f)
                {
                    vector2.X = -1f;
                }
                if ((double)vector2.Y > 0.2)
                {
                    vector2.Y = -0.2f;
                }
                if ((double)vector2.Y < -0.2)
                {
                    vector2.Y = -0.2f;
                }
            }
            int num8 = num32;
            int num10;
            for (num8 += WorldGen.genRand.Next(5); num8 < num43; num8 += WorldGen.genRand.Next(num10, (int)((double)num10 * 1.5)))
            {
                int num9 = num48;
                while (!Main.tile[num8, num9].active())
                {
                    num9--;
                }
                num9 += WorldGen.genRand.Next(-3, 4);
                num10 = WorldGen.genRand.Next(4, 8);
                int num11 = 189;
                if (WorldGen.genRand.Next(4) == 0)
                {
                    num11 = 196;
                }
                for (int m = num8 - num10; m <= num8 + num10; m++)
                {
                    for (int n = num9 - num10; n <= num9 + num10; n++)
                    {
                        if (n > num47)
                        {
                            float num54 = Math.Abs(m - num8);
                            float num13 = Math.Abs(n - num9) * 2;
                            if (Math.Sqrt(num54 * num54 + num13 * num13) < (double)(num10 + WorldGen.genRand.Next(2)))
                            {
                                Main.tile[m, n].active(active: true);
                                Main.tile[m, n].type = (ushort)num11;
                                WorldGen.SquareTileFrame(m, n);
                            }
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(80, 95);
            num12 = num;
            num22 = WorldGen.genRand.Next(10, 15);
            vector.X = i;
            vector.Y = num47;
            vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            while (vector2.X > -2f && vector2.X < 2f)
            {
                vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            }
            vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
            while (num > 0.0 && num22 > 0f)
            {
                num -= (double)WorldGen.genRand.Next(4);
                num22 -= 1f;
                int num49 = (int)((double)vector.X - num * 0.5);
                int num51 = (int)((double)vector.X + num * 0.5);
                int num2 = num47 - 1;
                int num4 = (int)((double)vector.Y + num * 0.5);
                if (num49 < 0)
                {
                    num49 = 0;
                }
                if (num51 > Main.maxTilesX)
                {
                    num51 = Main.maxTilesX;
                }
                if (num2 < 0)
                {
                    num2 = 0;
                }
                if (num4 > Main.maxTilesY)
                {
                    num4 = Main.maxTilesY;
                }
                num12 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                float num14 = vector.Y + 1f;
                for (int num15 = num49; num15 < num51; num15++)
                {
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        num14 += (float)WorldGen.genRand.Next(-1, 2);
                    }
                    if (num14 < vector.Y)
                    {
                        num14 = vector.Y;
                    }
                    if (num14 > vector.Y + 2f)
                    {
                        num14 = vector.Y + 2f;
                    }
                    for (int num16 = num2; num16 < num4; num16++)
                    {
                        if ((float)num16 > num14)
                        {
                            float num55 = Math.Abs((float)num15 - vector.X);
                            float num17 = Math.Abs((float)num16 - vector.Y) * 3f;
                            if (Math.Sqrt(num55 * num55 + num17 * num17) < num12 * 0.4 && Main.tile[num15, num16].type == 189)
                            {
                                Main.tile[num15, num16].type = (ushort)ModContent.TileType<TomatoBlockTile>();
                                WorldGen.SquareTileFrame(num15, num16);
                            }
                        }
                    }
                }
                vector += vector2;
                vector2.X += (float)WorldGen.genRand.Next(-20, 21) * 0.05f;
                if (vector2.X > 1f)
                {
                    vector2.X = 1f;
                }
                if (vector2.X < -1f)
                {
                    vector2.X = -1f;
                }
                if ((double)vector2.Y > 0.2)
                {
                    vector2.Y = -0.2f;
                }
                if ((double)vector2.Y < -0.2)
                {
                    vector2.Y = -0.2f;
                }
            }
            int num18 = num32;
            num18 += WorldGen.genRand.Next(5);
            while (num18 < num43)
            {
                int num19 = num48;
                while ((!Main.tile[num18, num19].active() || Main.tile[num18, num19].type != 0) && num18 < num43)
                {
                    num19--;
                    if (num19 < num47)
                    {
                        num19 = num48;
                        num18 += WorldGen.genRand.Next(1, 4);
                    }
                }
                if (num18 >= num43)
                {
                    continue;
                }
                num19 += WorldGen.genRand.Next(0, 4);
                int num20 = WorldGen.genRand.Next(2, 5);
                int num21 = 189;
                for (int num23 = num18 - num20; num23 <= num18 + num20; num23++)
                {
                    for (int num24 = num19 - num20; num24 <= num19 + num20; num24++)
                    {
                        if (num24 > num47)
                        {
                            float num56 = Math.Abs(num23 - num18);
                            float num25 = Math.Abs(num24 - num19) * 2;
                            if (Math.Sqrt(num56 * num56 + num25 * num25) < (double)num20)
                            {
                                Main.tile[num23, num24].type = (ushort)num21;
                                WorldGen.SquareTileFrame(num23, num24);
                            }
                        }
                    }
                }
                num18 += WorldGen.genRand.Next(num20, (int)((double)num20 * 1.5));
            }
            for (int num26 = num32 - 20; num26 <= num43 + 20; num26++)
            {
                for (int num27 = num47 - 20; num27 <= num48 + 20; num27++)
                {
                    bool flag = true;
                    for (int num28 = num26 - 1; num28 <= num26 + 1; num28++)
                    {
                        for (int num29 = num27 - 1; num29 <= num27 + 1; num29++)
                        {
                            if (!Main.tile[num28, num29].active())
                            {
                                flag = false;
                            }
                        }
                    }
                    if (flag)
                    {
                        Main.tile[num26, num27].wall = 73;
                        WorldGen.SquareWallFrame(num26, num27);
                    }
                }
            }
            for (int num30 = num32; num30 <= num43; num30++)
            {
                int num31;
                for (num31 = num47 - 10; !Main.tile[num30, num31 + 1].active(); num31++)
                {
                }
                if (num31 >= num48 || Main.tile[num30, num31 + 1].type != 189)
                {
                    continue;
                }
                if (WorldGen.genRand.Next(10) == 0)
                {
                    int num33 = WorldGen.genRand.Next(1, 3);
                    for (int num34 = num30 - num33; num34 <= num30 + num33; num34++)
                    {
                        if (Main.tile[num34, num31].type == 189)
                        {
                            Main.tile[num34, num31].active(active: false);
                            Main.tile[num34, num31].liquid = byte.MaxValue;
                            Main.tile[num34, num31].lava(lava: false);
                            WorldGen.SquareTileFrame(num30, num31);
                        }
                        if (Main.tile[num34, num31 + 1].type == 189)
                        {
                            Main.tile[num34, num31 + 1].active(active: false);
                            Main.tile[num34, num31 + 1].liquid = byte.MaxValue;
                            Main.tile[num34, num31 + 1].lava(lava: false);
                            WorldGen.SquareTileFrame(num30, num31 + 1);
                        }
                        if (num34 > num30 - num33 && num34 < num30 + 2 && Main.tile[num34, num31 + 2].type == 189)
                        {
                            Main.tile[num34, num31 + 2].active(active: false);
                            Main.tile[num34, num31 + 2].liquid = byte.MaxValue;
                            Main.tile[num34, num31 + 2].lava(lava: false);
                            WorldGen.SquareTileFrame(num30, num31 + 2);
                        }
                    }
                }
                if (WorldGen.genRand.Next(5) == 0)
                {
                    Main.tile[num30, num31].liquid = byte.MaxValue;
                }
                Main.tile[num30, num31].lava(lava: false);
                WorldGen.SquareTileFrame(num30, num31);
            }
            int num35 = WorldGen.genRand.Next(4);
            for (int num36 = 0; num36 <= num35; num36++)
            {
                int num37 = WorldGen.genRand.Next(num32 - 5, num43 + 5);
                int num38 = num47 - WorldGen.genRand.Next(20, 40);
                int num39 = WorldGen.genRand.Next(4, 8);
                int num40 = 189;
                if (WorldGen.genRand.Next(2) == 0)
                {
                    num40 = 196;
                }
                for (int num41 = num37 - num39; num41 <= num37 + num39; num41++)
                {
                    for (int num42 = num38 - num39; num42 <= num38 + num39; num42++)
                    {
                        float num57 = Math.Abs(num41 - num37);
                        float num44 = Math.Abs(num42 - num38) * 2;
                        if (Math.Sqrt(num57 * num57 + num44 * num44) < (double)(num39 + WorldGen.genRand.Next(-1, 2)))
                        {
                            Main.tile[num41, num42].active(active: true);
                            Main.tile[num41, num42].type = (ushort)num40;
                            WorldGen.SquareTileFrame(num41, num42);
                        }
                    }
                }
                for (int num45 = num37 - num39 + 2; num45 <= num37 + num39 - 2; num45++)
                {
                    int num46;
                    for (num46 = num38 - num39; !Main.tile[num45, num46].active(); num46++)
                    {
                    }
                    Main.tile[num45, num46].active(active: false);
                    Main.tile[num45, num46].liquid = byte.MaxValue;
                    WorldGen.SquareTileFrame(num45, num46);
                }
            }
        }

        public static void Kaboom(Vector2 position)
        {
            Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<Kaboom>(), 0, 0);
        }
    }
}