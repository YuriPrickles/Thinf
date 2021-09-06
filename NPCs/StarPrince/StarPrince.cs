using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.NPCs.StarPrince         //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
	[AutoloadHead]
	public class StarPrince : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Prince");
			Main.npcFrameCount[npc.type] = 24; //this defines how many frames the npc sprite sheet has
			NPCID.Sets.ExtraFramesCount[npc.type] = 7;
			NPCID.Sets.AttackFrameCount[npc.type] = 1;
			NPCID.Sets.DangerDetectRange[npc.type] = 450; //this defines the npc danger detect range
			NPCID.Sets.AttackType[npc.type] = 1; //this is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee) 
			NPCID.Sets.AttackTime[npc.type] = 11; //this defines the npc attack speed
			NPCID.Sets.AttackAverageChance[npc.type] = 1;//this defines the npc atack chance
			NPCID.Sets.HatOffsetY[npc.type] = 0; //this defines the party hat position
		}
		public override bool Autoload(ref string name)
		{
			name = "StarPrince";
			return mod.Properties.Autoload;
		}
		public override void SetDefaults()
		{
			npc.townNPC = true; //This defines if the npc is a town Npc or not
			npc.friendly = true;  //this defines if the npc can hur you or not()
			npc.width = 40; //the npc sprite width
			npc.height = 56;  //the npc sprite height
			npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
			npc.defense = 25;  //the npc defense
			npc.lifeMax = 500;// the npc life
			npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
			npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
			npc.knockBackResist = 0.5f;  //the npc knockback resistance
			animationType = NPCID.Guide;  //this copy the guide animation
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money) //Whether or not the conditions have been met for this town NPC to be able to move into town.
		{
			if (NPC.downedMoonlord)  //so after the EoC is killed
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
			switch (WorldGen.genRand.Next(9))
			{
				case 0:
					return "Siruis";
				case 1:
					return "Poalrsi";
				case 2:
					return "Ornion";
				case 3:
					return "Darstust";
				case 4:
					return "Androdema";
				case 5:
					return "Supernova";
				case 6:
					return "Celesta";
				case 7:
					return "Awrawra";
				case 8:
					return "Galactose";
				default:
					return "Grasscutter";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)  //Allows you to set the text for the buttons that appear on this town NPC's chat window. 
		{
			button = "Shop";   //this defines the buy button name
			button2 = "Tell Bedtime Story";
		}
		public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open this NPC's shop.
		{
			if (firstButton)
			{
				openShop = true;   //so when you click on buy button opens the shop
			}
			else
            {
				Main.npcChatText = "Snap back to reality";//https://pastebin.com/A7G0NXQM
				Main.LocalPlayer.ClearBuff(ModContent.BuffType<Nightmare>());
            }
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(mod.ItemType("Starforge"));
			nextSlot++;
			if (NPC.downedFishron)
			{
				shop.item[nextSlot].SetDefaults(ItemID.Goldfish);
				nextSlot++;
			}
			if (Main.moonPhase == 2)
			{
				shop.item[nextSlot].SetDefaults(ItemID.EnchantedSword);
				shop.item[nextSlot].shopCustomPrice = 100000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemID.EnchantedNightcrawler);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ItemID.ShinePotion);
			shop.item[nextSlot].shopCustomPrice = 23568;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FallenStar);
			shop.item[nextSlot].shopCustomPrice = 10000;
			shop.item[nextSlot].newAndShiny = true;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentStardust);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentVortex);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentSolar);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentNebula);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("Gamblestone"));
			shop.item[nextSlot].shopCustomPrice = 750000;
			nextSlot++;

		}

		public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
		{
			if (Main.bloodMoon && Main.rand.Next(3) == 1)
            {
				return "Long ago, I made a device that painted the moon red as a prank for Moon Lord. I lost the key to turn it off and now it activates every now and again.";
            }

			int wizardNPC = NPC.FindFirstNPC(NPCID.Wizard);
			if (wizardNPC >= 0 && Main.rand.Next(4) == 0)
			{
				return "I've heard that " + Main.npc[wizardNPC].GivenName + " is a fan of stars. I think we'll get along pretty nicely.";
			}
			int stylistNPC = NPC.FindFirstNPC(NPCID.Stylist);
			if (stylistNPC >= 0 && Main.rand.Next(4) == 0) //has 1 in 3 chance to show this message
			{
				return "Hey, do you know what kind of gift " + Main.npc[stylistNPC].GivenName + " likes? Asking for a friend.";
			}
			switch (Main.rand.Next(10))    //this are the messages when you talk to the npc
			{
				case 0:
					return "Me and the Moon Lord are besties. The majority of my kingdom didn't really like it.";
				case 1:
					return "I see you want my nice oven...";
				case 2:
					return "You suck at building.";
				case 3:
					return "No fair! Why are YOU the main character?";
				case 4:
					return "I've never seen a single alchemist in this world other than you. Not like that's a bad thing.";
				case 5:
					return "No one has ever beaten the Moon Lord. In checkers.";
				case 6:
					return "You are going to need a lot of Starrite if you choose to be a Summoner. Lucky for you I have one of the ingredients for sale!";
				case 7:
					return "You have to collect Herbal Cores if you continue as a Ranger.";
				case 8:
					return "You'll need to create Asteroid Bars if you want to make Mage gear.";
				case 9:
					return "You'll need lots of Souls of Fight if you pursue the path of a Warrior.";
				default:
					return "Imagine not having 20000 health. ";

			}
		}
		public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of this town NPC attack
		{
			damage = 100;  //npc damage
			knockback = 2f;   //npc knockback
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
		{
			cooldown = 5;
			randExtraCooldown = 0;
		}
		
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
		{
			projType = ProjectileID.FallingStar;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 10f;
			// randomOffset = 4f;

		}
	}
}