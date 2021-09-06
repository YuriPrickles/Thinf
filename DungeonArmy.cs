using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Thinf.NPCs;

namespace Thinf
{
	public class DungeonArmy
	{
		//Initializing an Array that can be used in any file
		public static int[] invaders = {
			NPCID.BlueArmoredBones,
			NPCID.BlueArmoredBonesMace,
			NPCID.BlueArmoredBonesNoPants,
			NPCID.BlueArmoredBonesSword,
			NPCID.RustyArmoredBonesAxe,
			NPCID.RustyArmoredBonesFlail,
			NPCID.RustyArmoredBonesSword,
			NPCID.RustyArmoredBonesSwordNoArmor,
			NPCID.HellArmoredBones,
			NPCID.HellArmoredBonesMace,
			NPCID.HellArmoredBonesSpikeShield,
			NPCID.HellArmoredBonesSword,
			NPCID.SkeletonCommando,
			NPCID.SkeletonSniper,
			NPCID.TacticalSkeleton,
			NPCID.Necromancer,
			NPCID.NecromancerArmored,
			NPCID.RaggedCaster,
			NPCID.RaggedCasterOpenCoat,
			NPCID.DiabolistRed,
			NPCID.DiabolistWhite,
			NPCID.BoneLee,
			NPCID.GiantCursedSkull,
			NPCID.Paladin,
			ModContent.NPCType<FallenHero>(),
			ModContent.NPCType<DemomanSkeleton>(),
			ModContent.NPCType<SpiritMage>(),
		};

		//Setup for an Invasion After setting up
		public static void StartCustomInvasion()
		{
			//Set to no invasion if one is present
			if (Main.invasionType != 0 && Main.invasionSize == 0)
			{
				Main.invasionType = 0;
			}

			//Once it is set to no invasion setup the invasion
			if (Main.invasionType == 0)
			{
				//Checks amount of players for scaling
				int numPlayers = 0;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
					{
						numPlayers++;
					}
				}
				if (numPlayers > 0)
				{
					//Invasion setup
					Main.invasionType = -1; //Not going to be using an invasion that is positive since those are vanilla invasions
					ModNameWorld.DungeonArmyUp = true;
					Main.invasionSize = 200 * numPlayers;
					Main.invasionSizeStart = Main.invasionSize;
					Main.invasionProgress = 0;
					Main.invasionProgressIcon = 0 + 3;
					Main.invasionProgressWave = 0;
					Main.invasionProgressMax = Main.invasionSizeStart;
					Main.invasionWarn = 300; //This doesn't really matter, as this does not work, I like to keep it here anyways
					if (Main.rand.Next(1) == 0)
					{
						Main.invasionX = 0.0; //Starts invasion immediately rather than wait for it to spawn
						return;
					}
					Main.invasionX = (double)Main.maxTilesX; //Set the initial starting location of the invasion to max tiles
				}
			}
		}

		//Text for messages and syncing
		public static void CustomInvasionWarning()
		{
			String text = "Dungeon Army";
			if (Main.invasionX == (double)Main.spawnTileX)
			{
				text = "The Dungeon Army is invading!";
			}
			if (Main.invasionSize <= 0)
			{
				text = "The Dungeon Army's bones have been rattled!";
			}
			if (Main.netMode == 0)
			{
				Main.NewText(text, 175, 75, 255, false);
				return;
			}
			if (Main.netMode == 2)
			{
				//Sync with net
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
			}
		}

		//Updating the invasion
		public static void UpdateCustomInvasion()
		{
			//If the custom invasion is up
			if (ModNameWorld.DungeonArmyUp)
			{
				//End invasion if size less or equal to 0
				if (Main.invasionSize <= 0)
				{
					ModNameWorld.DungeonArmyUp = false;
					CustomInvasionWarning();
					Main.invasionType = 0;
					Main.invasionDelay = 0;
				}

				//Do not do the rest if invasion already at spawn
				if (Main.invasionX == (double)Main.spawnTileX)
				{
					return;
				}

				//Update when the invasion gets to Spawn
				float moveRate = (float)Main.dayRate;

				//If the invasion is greater than the spawn position
				if (Main.invasionX > (double)Main.spawnTileX)
				{
					//Decrement invasion x as to "move them"
					Main.invasionX -= (double)moveRate;

					//If less than the spawn pos, set invasion pos to spawn pos and warn players that invaders are at spawn
					if (Main.invasionX <= (double)Main.spawnTileX)
					{
						Main.invasionX = (double)Main.spawnTileX;
						CustomInvasionWarning();
					}
					else
					{
						Main.invasionWarn--;
					}
				}
				else
				{
					//Same thing as the if statement above, just it is from the other side
					if (Main.invasionX < (double)Main.spawnTileX)
					{
						Main.invasionX += (double)moveRate;
						if (Main.invasionX >= (double)Main.spawnTileX)
						{
							Main.invasionX = (double)Main.spawnTileX;
							CustomInvasionWarning();
						}
						else
						{
							Main.invasionWarn--;
						}
					}
				}
			}
		}

		//Checks the players' progress in invasion
		public static void CheckCustomInvasionProgress()
		{
			//Not really sure what this is
			if (Main.invasionProgressMode != 2)
			{
				Main.invasionProgressNearInvasion = false;
				return;
			}

			//Checks if NPCs are in the spawn area to set the flag, which I do not know what it does
			bool flag = false;
			Player player = Main.player[Main.myPlayer];
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			int num = 5000;
			int icon = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active)
				{
					icon = 0;
					int type = Main.npc[i].type;
					for (int n = 0; n < invaders.Length; n++)
					{
						if (type == invaders[n])
						{
							Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value))
							{
								flag = true;
								break;
							}
						}
					}
				}
			}
			Main.invasionProgressNearInvasion = flag;
			int progressMax3 = 1;

			//If the custom invasion is up, set the max progress as the initial invasion size
			if (ModNameWorld.DungeonArmyUp)
			{
				progressMax3 = Main.invasionSizeStart;
			}

			//If the custom invasion is up and the enemies are at the spawn pos
			if (ModNameWorld.DungeonArmyUp && (Main.invasionX == (double)Main.spawnTileX))
			{
				//Shows the UI for the invasion
				Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);
			}

			//Syncing start of invasion
			foreach (Player p in Main.player)
			{
				NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
			}
		}
	}
}