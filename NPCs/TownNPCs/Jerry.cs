using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.TownNPCs         //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
	public class Jerry : ModNPC
	{
		int alphatimer = 0;
		bool hasRejected = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jerry");
			Main.npcFrameCount[npc.type] = 2; //this defines how many frames the npc sprite sheet has
		}
		public override void SetDefaults()
		{
			npc.townNPC = true; //This defines if the npc is a town Npc or not
			npc.friendly = false;  //this defines if the npc can hur you or not()
			npc.width = 44; //the npc sprite width
			npc.height = 30;  //the npc sprite height
			npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
			npc.defense = 50;  //the npc defense
			npc.lifeMax = 400;// the npc life
			npc.dontTakeDamage = true;
			npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
			npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
			npc.knockBackResist = 0f;  //the npc knockback resistance
			animationType = NPCID.BlueSlime;  //this copy the guide animation
		}

        public override void AI()
        {
			if (hasRejected)
            {
				npc.velocity = new Vector2(0, -0.07f);
				alphatimer++;
				if (alphatimer >= 4)
				{
					npc.alpha++;
					if (npc.alpha >= 255)
					{
						npc.active = false;
					}
				}
            }
        }
        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Reject";
			button2 = "Reject and Mock";
		}
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
        {
            if (firstButton && !hasRejected)
            {
                hasRejected = true;
                hasRejectedJerry = true;
                Main.npcChatText = "Pathetic. You will suffer like all of them did.";
                Main.NewText("Looks like you've been banned from the Tomato Fields.");
                Main.NewText("Reason: Having your own opinion");
            }
            else
			{
				hasRejected = true;
				hasRejectedJerry = true;
				Main.npcChatText = "(Dialogue has been censored due to intense profanity)";
				Main.NewText("Looks like you've been banned from the Tomato Fields.");
				Main.NewText("Reason: I HATE YOU SO MUCH");
			}

        }
		public override string GetChat()
		{
			if (hasRejected)
            {
				return "Pathetic. You will suffer like all of them did.";
            }
			return "Hello, would you like to join me in my mission to cover the world in Ketchup?";
		}
	}
}