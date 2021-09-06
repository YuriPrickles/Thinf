using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Thinf.Blocks;
using Thinf.NPCs.Core;

namespace Thinf
{
    public class ModNameWorld : ModWorld
    {
        public static bool hasReceivedBait = false;
        public static bool screenShake = false;
        public static bool downedAcornSpirit;
        public static bool downedThundercock;
        public static bool downedCacterus;
        public static bool downedCortal;
        public static bool downedSoulKeys;
        public static bool downedSpudLord;
        public static bool downedBeenado;
        public static bool downedSoulCatcher;
        public static bool downedHerbalgamation;
        public static bool downedFlashlight = false;
        public static bool downedPM = false;
        public static bool downedBlizzard = false;
        public static bool downedWall;
        public static bool coreDestroyed = false;
        public static bool coreRestored = false;
        public static bool spawnOrespud = false;
        public static bool spawnOrecarrot = false;
        public static int ChestWasteland = 0;
        public static int TomatoTown = 0;
        public static bool DungeonArmyUp = false;
        public static bool downedDungeonArmy = false;
        public static int timesCoveredHoney = 0;
        public static bool FrenzyMode = false;

        public override void Initialize()
        {
            coreRestored = false;
            downedBlizzard = false;
            downedFlashlight = false;
            downedPM = false;
            hasReceivedBait = false;
            downedAcornSpirit = false;
            downedWall = false;
            FrenzyMode = false;
            downedThundercock = false;
            downedCacterus = false;
            downedSoulCatcher = false;
            downedCortal = false;
            downedSoulKeys = false;
            downedSpudLord = false;
            downedBeenado = false;
            Main.invasionSize = 0;
            DungeonArmyUp = false;
            downedDungeonArmy = false;
            coreDestroyed = false;
        }
        public override void PostWorldGen()
        {
            NPC npc = Main.npc[NPC.NewNPC((Main.maxTilesX / 2) * 16, (Main.maxTilesY / 2) * 16, ModContent.NPCType<Core>(), 0, 0f, 0f, 0f, 0f, 255)];
        }
        public override void TileCountsAvailable(int[] tileCounts)
        {
            Mod magicStorage = ModLoader.GetMod("MagicStorage");
            if (magicStorage == null)
            {
                ChestWasteland = tileCounts[TileID.Containers];
            }
            else
            {
                ChestWasteland = tileCounts[TileID.Containers] + tileCounts[magicStorage.TileType("StorageUnit")] + tileCounts[magicStorage.TileType("StorageHeart")];
            }
            TomatoTown = tileCounts[ModContent.TileType<TomatoBlockTile>()];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Beaches"));
            if (genIndex == -1)
            {
                return;
            }
            tasks.Insert(genIndex + 1, new PassLegacy("Tomato Generation", delegate (GenerationProgress progress)
            {
                progress.Message = "Generating an ungodly amount of tomatoes";
                for (int i = 0; i < Main.maxTilesX / 2000; i++)
                {

                    int X = WorldGen.genRand.Next(1, Main.dungeonX + 300);
                    if (Main.dungeonX > Main.maxTilesX / 2)
                    {
                        X = Main.dungeonX + 100;
                    }

                    if (Main.dungeonX < Main.maxTilesX / 2)
                    {
                        X = Main.dungeonX - 100;
                    }
                    int Y = Main.dungeonY - 10;
                    int TileType = mod.TileType("TomatoBlockTile");

                    WorldGen.TileRunner(X, Y, 100, 35, TileType, false, 0f, 0f, true, true);
                    WorldGen.TileRunner(X + 20, Y, 100, 35, TileType, false, 0f, 0f, true, true);
                    WorldGen.TileRunner(X - 20, Y, 100, 35, TileType, false, 0f, 0f, true, true);

                }

            }));
        }
        //Save downed data
        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedFlashlight) downed.Add("Flashlight");
            if (downedBlizzard) downed.Add("Blizzard");
            if (coreDestroyed) downed.Add("Core");
            if (downedPM) downed.Add("PM");
            if (downedDungeonArmy) downed.Add("DungeonArmy");
            if (downedWall) downed.Add("WallOfFlesh");
            if (downedAcornSpirit) downed.Add("AcornSpirit");
            if (downedSoulCatcher) downed.Add("SoulCatcher");
            if (downedCacterus) downed.Add("Cacterus");
            if (downedThundercock) downed.Add("ThundercockandballTorture");
            if (downedCortal) downed.Add("Cortal");
            if (downedSoulKeys) downed.Add("SoulKeys");
            if (downedSpudLord) downed.Add("SpudLord");
            if (downedBeenado) downed.Add("Beenado");
            if (downedHerbalgamation) downed.Add("HerbBoss");
            return new TagCompound {
                {"downed", downed}
            };
        }

        //Load downed data
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedFlashlight = downed.Contains("Flashlight");
            downedBlizzard = downed.Contains("Blizzard");
            downedPM = downed.Contains("PM");
            coreDestroyed = downed.Contains("Core");
            downedDungeonArmy = downed.Contains("DungeonArmy");
            downedWall = downed.Contains("WallOfFlesh");
            downedCacterus = downed.Contains("Cacterus");
            downedSoulCatcher = downed.Contains("SoulCatcher");
            downedThundercock = downed.Contains("ThundercockandballTorture");
            downedCortal = downed.Contains("Cortal");
            downedAcornSpirit = downed.Contains("AcornSpirit");
            downedSoulKeys = downed.Contains("SoulKeys");
            downedSpudLord = downed.Contains("SpudLord");
            downedBeenado = downed.Contains("Beenado");
            downedHerbalgamation = downed.Contains("HerbBoss");
        }

        //Sync downed data
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedDungeonArmy;
            writer.Write(flags);
        }

        //Sync downed data
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedDungeonArmy = flags[0];
        }
        //Allow to update invasion while game is running
        public override void PostUpdate()
        {
            if (DungeonArmyUp)
            {
                if (Main.invasionX == Main.spawnTileX)
                {
                    //Checks progress and reports progress only if invasion at spawn
                    DungeonArmy.CheckCustomInvasionProgress();
                }
                //Updates the custom invasion while it heads to spawn point and ends it
                DungeonArmy.UpdateCustomInvasion();
            }
        }
    }
}
