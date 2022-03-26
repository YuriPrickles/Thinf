using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Thinf.Items.Ammo;
using Thinf.Items.Placeables;
using Thinf.Items.Potions;
using Thinf.Items.Weapons;

namespace Thinf.NPCs.TownNPCs
{
	public class Carolla : ModNPC
	{
		// Time of day for traveller to leave (6PM)
		public const double despawnTime = 48600.0;

		// the time of day the traveler will spawn (double.MaxValue for no spawn)
		// saved and loaded with the world in ExampleWorld
		public static double spawnTime = double.MaxValue;

		// The list of items in the traveler's shop. Saved with the world and reset when a new traveler spawns
		public static List<Item> shopItems = new List<Item>();

		public static NPC FindNPC(int npcType) => Main.npc.FirstOrDefault(npc => npc.type == npcType && npc.active);

		public static void UpdateTravelingMerchant()
		{
			NPC traveler = FindNPC(ModContent.NPCType<Carolla>()); // Find an Explorer if there's one spawned in the world
			if (traveler != null && (!Main.dayTime || Main.time >= despawnTime) && !IsNpcOnscreen(traveler.Center)) // If it's past the despawn time and the NPC isn't onscreen
			{
				// Here we despawn the NPC and send a message stating that the NPC has despawned
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(traveler.FullName + " left the game", 50, 125, 255);
				else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(traveler.FullName + " left the game"), new Color(50, 125, 255));
				traveler.active = false;
				traveler.netSkip = -1;
				traveler.life = 0;
				traveler = null;
			}

			// Main.time is set to 0 each morning, and only for one update. Sundialling will never skip past time 0 so this is the place for 'on new day' code
			if (Main.dayTime && Main.time == 0)
			{
				// insert code here to change the spawn chance based on other conditions (say, npcs which have arrived, or milestones the player has passed)
				// You can also add a day counter here to prevent the merchant from possibly spawning multiple days in a row.

				// NPC won't spawn today if it stayed all night
				if (traveler == null/* && Main.rand.NextBool(4)*/)
				{ // 4 = 25% Chance
				  // Here we can make it so the NPC doesnt spawn at the EXACT same time every time it does spawn
					spawnTime = GetRandomSpawnTime(5400, 8100); // minTime = 6:00am, maxTime = 7:30am
				}
				else
				{
					spawnTime = double.MaxValue; // no spawn today
				}
			}

			// Spawn the traveler if the spawn conditions are met (time of day, no events, no sundial)
			if (traveler == null && CanSpawnNow())
			{
				int newTraveler = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<Carolla>(), 1); // Spawning at the world spawn
				traveler = Main.npc[newTraveler];
				traveler.homeless = true;
				traveler.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
				traveler.netUpdate = true;
				shopItems = CreateNewShop();

				// Prevents the traveler from spawning again the same day
				spawnTime = double.MaxValue;

				// Annouce that the traveler has spawned in!
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText("??? joined the game", Color.Yellow);
			}
		}

		private static bool CanSpawnNow()
		{
			// can't spawn if any events are running
			if (Main.eclipse || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
				return false;

			// can't spawn if the sundial is active
			if (Main.fastForwardTime)
				return false;

			// can spawn if daytime, and between the spawn and despawn times
			return false;
			//return Main.dayTime && Main.time >= spawnTime && Main.time < despawnTime;
		}

		private static bool IsNpcOnscreen(Vector2 center)
		{
			int w = NPC.sWidth + NPC.safeRangeX * 2;
			int h = NPC.sHeight + NPC.safeRangeY * 2;
			Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
			foreach (Player player in Main.player)
			{
				// If any player is close enough to the traveling merchant, it will prevent the npc from despawning
				if (player.active && player.getRect().Intersects(npcScreenRect)) return true;
			}
			return false;
		}

		public static double GetRandomSpawnTime(double minTime, double maxTime)
		{
			// A simple formula to get a random time between two chosen times
			return (maxTime - minTime) * Main.rand.NextDouble() + minTime;
		}

		public static List<Item> CreateNewShop()
		{
			// create a list of item ids
			var itemIds = new List<int>();

			// For each slot we add a switch case to determine what should go in that slot
			switch (Main.rand.Next(2))
			{
				case 0:
					itemIds.Add(ModContent.ItemType<BoxyPotion>());
					break;
				default:
					itemIds.Add(ModContent.ItemType<PackingPeanut>());
					break;
			}

			switch (Main.rand.Next(3))
			{
				case 0:
					itemIds.Add(ModContent.ItemType<LoxxePainting>());
					break;
				case 1:
					itemIds.Add(ModContent.ItemType<LoxxePainting>());
					break;
				default:
					itemIds.Add(ModContent.ItemType<LoxxePainting>());
					break;
			}

			switch (Main.rand.Next(4))
			{
				case 0:
					itemIds.Add(ModContent.ItemType<RulerOfEverything>());
					break;
				case 1:
					itemIds.Add(ModContent.ItemType<RulerOfEverything>());
					break;
				case 2:
					itemIds.Add(ModContent.ItemType<RulerOfEverything>());
					break;
				default:
					itemIds.Add(ModContent.ItemType<RulerOfEverything>());
					break;
			}

			// conver to a list of items
			var items = new List<Item>();
			foreach (int itemId in itemIds)
			{
				Item item = new Item();
				item.SetDefaults(itemId);
				items.Add(item);
			}
			return items;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Hooded Person");
			Main.npcFrameCount[npc.type] = 2;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true; // This will be changed once the NPC is spawned
			npc.friendly = true;
			npc.width = 58;
			npc.height = 58;
			npc.aiStyle = -1;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 2370;
			npc.dontTakeDamage = false;
			npc.knockBackResist = 0.5f;
		}

		public static TagCompound Save()
		{
			return new TagCompound
			{
				["spawnTime"] = spawnTime,
				["shopItems"] = shopItems
			};
		}

		public static void Load(TagCompound tag)
		{
			spawnTime = tag.GetDouble("spawnTime");
			shopItems = tag.Get<List<Item>>("shopItems");
		}

		public override void HitEffect(int hitDirection, double damage)
		{

		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return false; // This should always be false, because we spawn in the Travleing Merchant manually
		}

		public override string TownNPCName()
		{
			switch (Main.rand.Next(4))
			{
				case 0:
					return "?";
				case 1:
					return "??";
				case 2:
					return "???";
				default:
					return "????";
			}
		}

		public override string GetChat()
		{
			switch (Main.rand.Next(8))
			{
				case 0:
					return "Ah... Doesn't it feel nice to take a break every once in a while?";
				case 1:
					return "Potions, Decor, Weapons, anything I find in my room is for sale!";
				case 2:
					return "Sometimes, even with a kingdom to rule and a castle to live in, it's nice to remember where we started...";
				case 3:
					return "Well, I won't be staying here for long, so make your purchases while you can!";
				case 4:
					return "I never knew there were more of her kind... Though you sure do have a different artstyle from her.";
				case 5:
					return "Heh, I don't think you can resell my stock. Too otherworldly even for this world.";
				case 6:
					return "It's a good thing I have plot armor! I would've been dead by now!";
				case 7:
					return "I'm not supposed to be helping you, but my sister's sick, and her only hope is to find a Life Stew...";
			}
			return "Do you see the small vent on the floor, Gregory?";
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");

			button2 = "Give Life Stew";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
			else
            {
				Main.npcChatText = $"Life Stew is a hard thing to make since lots of rare stuff are needed. You'd need [i/s7:{ItemID.LifeFruit}], [i/s24:{ItemID.GreaterHealingPotion}],[i/s14:{ItemID.LifeCrystal}],[i/s17:{ItemID.Prismite}],[i/s45:{ItemID.PixieDust}],[i/s34:{ItemID.BowlofSoup}],[i/s19:{ModContent.ItemType<Corn>()}],[i/s76:{ModContent.ItemType<Carrot>()}].";
            }
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			foreach (Item item in shopItems)
			{
				// We dont want "empty" items and unloaded items to appear
				if (item == null || item.type == ItemID.None)
					continue;

				shop.item[nextSlot].SetDefaults(item.type);
				nextSlot++;
			}
		}

		public override void AI()
		{
			npc.homeless = true; // Make sure it stays homeless
		}


	}
}