using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.TownNPCs         //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
	[AutoloadHead]
	public class Beekeeper : ModNPC
	{
		int selectedButton = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beekeeper");
			NPCID.Sets.ExtraFramesCount[npc.type] = 7;
			NPCID.Sets.AttackFrameCount[npc.type] = 1;
			NPCID.Sets.DangerDetectRange[npc.type] = 500; //this defines the npc danger detect range
			NPCID.Sets.AttackType[npc.type] = 0; //this is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee) 
			NPCID.Sets.AttackTime[npc.type] = 1; //this defines the npc attack speed
			NPCID.Sets.AttackAverageChance[npc.type] = 1;//this defines the npc atack chance
			NPCID.Sets.HatOffsetY[npc.type] = -1; //this defines the party hat position
			Main.npcFrameCount[npc.type] = 23; //this defines how many frames the npc sprite sheet has
		}
		public override bool Autoload(ref string name)
		{
			name = "Beekeeper";
			return mod.Properties.Autoload;
		}
		public override void SetDefaults()
		{
			npc.townNPC = true; //This defines if the npc is a town Npc or not
			npc.friendly = true;  //this defines if the npc can hur you or not()
			npc.width = 40; //the npc sprite width
			npc.height = 58;  //the npc sprite height
			npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
			npc.defense = 50;  //the npc defense
			npc.lifeMax = 400;// the npc life
			npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
			npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
			npc.knockBackResist = 0f;  //the npc knockback resistance
			animationType = NPCID.Guide;  //this copy the guide animation
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money) //Whether or not the conditions have been met for this town NPC to be able to move into town.
		{
			if (NPC.downedQueenBee && downedBeenado)
			{
				return true;
			}
			return false;
		}
		public override bool CheckConditions(int left, int right, int top, int bottom)    //Allows you to define special conditions required for this town NPC's house
		{
			return true;  //so when a house is available the npc will  spawn
		}
		public override string TownNPCName()     //Allows you to give this town NPC any name when it spawns
		{
			int namerand = Main.rand.Next(9);
			switch (namerand)
			{
				case 0:
					return "Vanessa";
				case 1:
					return "Honey";
				case 2:
					return "Suprema";
				case 3:
					return "Chloe";
				case 4:
					return "Rose";
				case 5:
					return "Tulip";
				case 6:
					return "Maria Sibylla";
				case 7:
					return "Alice Gray";
				case 8:
					return "Nadia Waloff";
			}
			return "shitass";
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			switch (selectedButton)
			{
				case 0:
					button2 = "Shop";
					break;
				case 1:
					button2 = "Cover in honey (5 gold)";
					break;
				case 2:
					button2 = "Bee Detection";
					break;
				case 3:
					button2 = "Politician Bait";
					break;
			}
			button = "Cycle Options";
		}
		public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
		{
			Player player = Thinf.FindNearestPlayer(300, npc.Center);
			if (!firstButton)
			{
				if (selectedButton == 0)
				{
					openShop = true;
				}

				if (selectedButton == 1)
				{
					if (player.BuyItem(50000))
					{
						Main.PlaySound(18, player.position, 1);
						player.AddBuff(BuffID.Honey, 36000);
						if (timesCoveredHoney < 3) //0-2
						{
							Main.npcChatText = "Here's your bucket of honey.";
							timesCoveredHoney++;
						}

						if (timesCoveredHoney >= 3 && timesCoveredHoney <= 6)//3-6
						{
							Main.npcChatText = "You really like this, don't you?";
							timesCoveredHoney++;
						}

						if (timesCoveredHoney >= 7 && timesCoveredHoney <= 13)//7-13
						{
							Main.npcChatText = $"I'm disappointed in you, {player.name}.";
							timesCoveredHoney++;
						}

						if (timesCoveredHoney >= 14 && timesCoveredHoney < 20)//14-20
						{
							Main.npcChatText = "I'm starting to get worried...";
							timesCoveredHoney++;
						}

						if (timesCoveredHoney == 20 || timesCoveredHoney == 21)
						{
							Main.npcChatText = "Please stop...";
							timesCoveredHoney++;
						}

						if (timesCoveredHoney > 22)
						{
							Main.npcChatText = "...";
							timesCoveredHoney++;
						}
					}
				}

				if (selectedButton == 2)
				{
					int beecount = NPC.CountNPCS(NPCID.Bee) + NPC.CountNPCS(NPCID.BeeSmall);
					int hornetcount = NPC.CountNPCS(NPCID.Hornet) + NPC.CountNPCS(NPCID.HornetFatty) + NPC.CountNPCS(NPCID.HornetHoney) + NPC.CountNPCS(NPCID.HornetLeafy) + NPC.CountNPCS(NPCID.HornetSpikey) + NPC.CountNPCS(NPCID.HornetStingy) + NPC.CountNPCS(NPCID.LittleHornetFatty) + NPC.CountNPCS(NPCID.LittleHornetHoney) + NPC.CountNPCS(NPCID.LittleHornetLeafy) + NPC.CountNPCS(NPCID.LittleHornetSpikey) + NPC.CountNPCS(NPCID.LittleHornetStingy) + NPC.CountNPCS(NPCID.BigHornetFatty) + NPC.CountNPCS(NPCID.BigHornetHoney) + NPC.CountNPCS(NPCID.BigHornetLeafy) + NPC.CountNPCS(NPCID.BigHornetSpikey) + NPC.CountNPCS(NPCID.BigHornetStingy) + NPC.CountNPCS(NPCID.MossHornet) + NPC.CountNPCS(NPCID.BigMossHornet) + NPC.CountNPCS(NPCID.LittleMossHornet) + NPC.CountNPCS(NPCID.TinyMossHornet) + NPC.CountNPCS(NPCID.GiantMossHornet);
					bool qbcheck = NPC.AnyNPCs(NPCID.QueenBee);
					if (qbcheck)
					{
						Main.npcChatText = $"There are {beecount} bees, {hornetcount} hornets, and one of Queen Bee's daughters is alive right now.";
					}

					if (!qbcheck)
					{
						Main.npcChatText = $"There are {beecount} bees, {hornetcount} hornets, and one of Queen Bee's daughters isn't alive right now.";
					}
				}

				if (selectedButton == 3)
				{
					if (!hasReceivedBait)
					{
						if (!downedPM)
						{
							Item.NewItem(npc.getRect(), ModContent.ItemType<PoliticianBait>());
							hasReceivedBait = true;
							Main.npcChatText = "Throw this in the jungle and kill whatever comes after it.";
						}
						else
						{
							Item.NewItem(npc.getRect(), ModContent.ItemType<PoliticianBait>());
							hasReceivedBait = true;
							Main.npcChatText = "You want to kill him again? Well, I'm not stopping you.";
						}
					}

					if (hasReceivedBait && !Thinf.FindNearestPlayer(210, npc.Center).HasItem(ModContent.ItemType<PoliticianBait>()))
					{
						Item.NewItem(npc.getRect(), ModContent.ItemType<PoliticianBait>());
						Main.npcChatText = "Did you lose the money? God, you're such a moron...";
					}
				}
			}

			if (firstButton)
			{
				selectedButton++;
				if (!NPC.downedMoonlord)
				{
					if (selectedButton > 2)
					{
						selectedButton = 0;
					}
				}
				else
				{
					if (selectedButton > 3)
					{
						selectedButton = 0;
					}
				}
			}
		}
		public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
		{
			if (NPC.downedQueenBee)
			{
				shop.item[nextSlot].SetDefaults(ItemID.BeeHat);
				shop.item[nextSlot].shopCustomPrice = 250000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.BeeShirt);
				shop.item[nextSlot].shopCustomPrice = 250000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.BeePants);
				shop.item[nextSlot].shopCustomPrice = 250000;
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ItemID.HoneyDispenser);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Stinger);
			shop.item[nextSlot].shopCustomPrice = 2500;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.BeeWax);
			shop.item[nextSlot].shopCustomPrice = 5000;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Hive);
			shop.item[nextSlot].shopCustomPrice = 500;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.HoneyBucket);
			shop.item[nextSlot].shopCustomPrice = 75000;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.HiveWand);
			shop.item[nextSlot].shopCustomPrice = 500000;
			nextSlot++;

		}

		public override string GetChat()
		{
			if (!ModNameWorld.hasReceivedBait && NPC.downedMoonlord && !downedPM)
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<PoliticianBait>());
				hasReceivedBait = true;
				return "Psst, there's this guy, and he's really mad at you. I'm also mad at him, so take this bag of fake money and kill him.";
			}
			if (hasReceivedBait && !downedPM && NPC.downedMoonlord && !Thinf.FindNearestPlayer(210, npc.Center).HasItem(ModContent.ItemType<PoliticianBait>()))
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<PoliticianBait>());
				return "Did you lose the money? God, you're such a moron...";
			}
			if (downedPM && Main.rand.Next(5) == 0)
			{
				return "I really appreciate you for destroying Prime Moron's most expensive vehicles. That fat weeb deserved whatver you did to him.";
			}
			int bitchdoctor = NPC.FindFirstNPC(NPCID.WitchDoctor);
			if (bitchdoctor >= 0 && Main.rand.Next(4) == 0)
			{
				return "I hate " + Main.npc[bitchdoctor].GivenName + ". He needs to be killed for what his kind has done.";
			}
			int guide = NPC.FindFirstNPC(NPCID.Guide);
			if (guide >= 0 && Main.rand.Next(4) == 0)
			{
				return "I feel like my friend " + Main.npc[guide].GivenName + " is hiding something from me. I don't know what it is, but when I mention the Underworld he just starts reading all the TileIDs in the world like a damn wiki user.";
			}
			int qb = NPC.FindFirstNPC(NPCID.QueenBee);
			if (qb >= 0 && Main.rand.Next(4) == 0)
			{
				return "The Queen's Daughter is here! Now watch her die like all the others because of assholes like you.";
			}
			int beenado = NPC.FindFirstNPC(ModContent.NPCType<Beenado.Beenado>());
			if (beenado >= 0)
			{
				return "Holy crap, what did Prime Minister do again?";
			}
			switch (Main.rand.Next(9))
			{
				case 0:
					return "Do you know how hard it is to lead a bunch of tiny insects into battle?";
				case 1:
					return "I've tried killing myself with poison, but I keep forgetting that the Queen gave me immunity to it.";
				case 2:
					return "I've been serving at the Kingdom for years as a general yet nobody still trusts me with a position in the government.";
				case 3:
					return "I've lost a total of 4 elections. It doesn't seem a lot but that's like 16 years of depressingly low votes for me.";
				case 4:
					return "Did you know that we repurpose old beehives into grenades? It's like a trojan horse, but with bees in it, and it's not a horse, nor a gift.";
				case 5:
					return "I remember the Great Jungle War of 1992. We killed every single Lihzahrd we saw, even the women and the children. No Lihzahrd deserves to be happy.";
				case 6:
					return "The moment I received the election results last year I knew there was no hope for the kingdom.";
				case 7:
					return "Life is unfair. Your dreams will never come true. All hope is lost. Give up. With those four sentences I scared about 5 goblin children.";
				case 8:
					return "What are you talking about? The jungle is an absolutely great place!";
				default:
					return "What's your opinion on jazz? I absolutely love it.";
			}
		}
		public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of this town NPC attack
		{
			damage = 20;  //npc damage
			knockback = 2f;   //npc knockback
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
		{
			cooldown = 80;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
		{
			projType = ProjectileID.Beenade;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 3f;
			// randomOffset = 4f;

		}
	}
}