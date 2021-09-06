using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.GatewaysForTesting;

namespace Thinf.NPCs.Radicow       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{

    public class Radicow : ModNPC
    {
        int frameNumber = 0;
        int selectedButton = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radicow");
            NPCID.Sets.DangerDetectRange[npc.type] = 450;
            NPCID.Sets.AttackType[npc.type] = 1;
            NPCID.Sets.AttackTime[npc.type] = 11;
            NPCID.Sets.AttackAverageChance[npc.type] = 1;
            Main.npcFrameCount[npc.type] = 6;
            //NPCID.Sets.ExtraFramesCount[npc.type] = 7;
            //NPCID.Sets.AttackFrameCount[npc.type] = 1;
            //NPCID.Sets.HatOffsetY[npc.type] = 0; //this defines the party hat position
        }
        //public override bool Autoload(ref string name)
        //{
        //	name = "StarPrince";
        //	return mod.Properties.Autoload;
        //}
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                frameNumber++;
                if (frameNumber >= 6)
                {
                    frameNumber = 0;
                }
                npc.frame.Y = frameNumber * (480 / 6);
            }
        }
        public override void SetDefaults()
        {
            npc.townNPC = true; //This defines if the npc is a town Npc or not
            npc.friendly = true;  //this defines if the npc can hur you or not()
            npc.width = 88; //the npc sprite width
            npc.height = 80;  //the npc sprite height
            npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
            npc.defense = 25;  //the npc defense
            npc.lifeMax = 50000;// the npc life
            npc.HitSound = SoundID.NPCHit4;  //the npc sound when is hit
            npc.DeathSound = SoundID.NPCDeath14;  //the npc sound when he dies
            npc.knockBackResist = 0.0f;  //the npc knockback resistanc
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

        public override void SetChatButtons(ref string button, ref string button2)  //Allows you to set the text for the buttons that appear on this town NPC's chat window. 
        {
            switch (selectedButton)
            {
                case 0:
                    button2 = "Bounty List";
                    break;
                case 1:
                    button2 = "Hunt Bounty";
                    break;
                case 2:
                    button2 = "Info about Bounty (depends on the lure you have)";
                    break;
            }
            button = "Cycle Options";
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = -npc.direction;
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open this NPC's shop.
        {
            if (firstButton)
            {
                selectedButton++;
                if (selectedButton > 2)
                {
                    selectedButton = 0;
                }
            }
            if (!firstButton)
            {
                if (selectedButton == 0)
                {
                    Main.npcChatText = "If you wanna help me find these bounties, we gotta make something to lure them out.";
                    Main.NewText($"BOUNTY 1: Poltergate [Lure using [i:{ModContent.ItemType<SplinterTea>()}]]");
                }
                if (selectedButton == 1)
                {
                    Player player = Main.player[npc.target];
                    if (player.HasItem(ModContent.ItemType<SplinterTea>()))
                    {
                        if (MyPlayer.readyToTravel)
                        {
                            Main.npcChatText = "Are ya ready to go?";
                        }
                        else
                        {
                            MyPlayer.readyToTravel = true;
                            Main.npcChatText = "This Splinter Tea is perfect for luring out Polterghast! Take this Transportamatic and let's beat the hell out of this dude!";
                            player.QuickSpawnItem(ModContent.ItemType<EnterTheGatrix>());
                        }
                    }
                }
                if (selectedButton == 2)
                {
                    Player player = Main.player[npc.target];
                    if (player.HasItem(ModContent.ItemType<SplinterTea>()))
                    {
                        Main.npcChatText = "Ah, Poltergate. This little prick is the weakest of all the bounties I'm assigned, but he definitely is the most elusive. He's gotten away from me more times than he should.";
                    }
                }
            }
        }

        public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
        {
            if (Main.bloodMoon && Main.rand.Next(3) == 1)
            {
                return "Dude, the ladies look like they want to commit arson.";
            }
            if (npc.homeless)
            {
                return "I'm lookin' for a bounty. Last place my tracker saw him was here. Have you seen any ghosts with fences?";
            }
            int guideNPC = NPC.FindFirstNPC(NPCID.Stylist);
            if (guideNPC >= 0 && Main.rand.Next(4) == 0)
            {
                return $"{Main.npc[guideNPC].GivenName} seems like the type of guy to spontaneously combust when confronted about taxes.";
            }
            switch (Main.rand.Next(6))    //this are the messages when you talk to the npc
            {
                case 0:
                    return "What's up little man?";
                case 1:
                    return "I think I've got a lead on one of my bounties. Wanna come?";
                case 2:
                    return "I've heard a lot of stories about this place. I wanted to go here but there was a barrier preventing anything from entering your atmosphere.";
                case 3:
                    return "The texture of the grass is good, the taste is somewhat lacking though, your planet's grass is a 6/10.";
                case 4:
                    return "How you doin'?";
                case 5:
                    return "Bounty hunting is pretty fun. You should try it sometime!";
                default:
                    return "Moo.";
            }
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of this town NPC attack
        {
            damage = 35;  //npc damage
            knockback = 2f;   //npc knockback
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
        {
            cooldown = 4;
            randExtraCooldown = 0;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
        {
            projType = ProjectileID.GreenLaser;
            attackDelay = 0;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
        {
            multiplier = 4f;
            // randomOffset = 4f;

        }
    }
}