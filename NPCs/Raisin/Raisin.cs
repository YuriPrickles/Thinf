using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.Raisin
{
	public class Raisin : ModNPC
	{
		string text = "cum";
		int frameTimer = 0;
		int frameCount = 0;
		int cutsceneTimer = -200;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raisin");
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 15000;
			npc.damage = 0;
			npc.defense = 15000;
			npc.knockBackResist = 0f;
			npc.width = 34;
			npc.height = 54;
			npc.value = Item.buyPrice(0, 1, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.dontTakeDamage = true;
			npc.HitSound = SoundID.Item27;
			npc.friendly = true;
			npc.DeathSound = new LegacySoundStyle(SoundID.Shatter, 0);
			npc.netAlways = true;
		}

		public override void AI()
		{
			npc.spriteDirection = npc.direction;
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			GetNPCFrame(frameCount);

			frameTimer++;
			if (frameTimer == 6)
			{
				frameTimer = 0;
				frameCount++;
				if (frameCount >= 4)
				{
					frameCount = 0;
				}
			}
			cutsceneTimer++;
			switch (cutsceneTimer)
			{
				case -100:
					text = "Hey, thanks for progressing the lore!";
					break;
				case 100:
					text = "I NEED YOUR HELP. I need to travel back in time.";
					break;
				case 300:
					text = "And to do that we must first absorb the energy of this world.";
					break;
				case 700:
					text = "My amulet can do exactly that, but it's gonna need enough power to rival the power of this world.";
					break;
				case 1100:
					text = "So... are you going to help me?";
					break;
				case 1400:
					text = "Oh, no need to answer. I know you do. Plus, dialogue choice UI hasn't been implemented yet.";
					break;
				case 1800:
					text = "I'll always be in this amulet, so if you need some advice, just pull the string for 20 different sentences!";
					break;
				case 2400:
					npc.active = false;
					ModNameWorld.raisinCutscene = true;
					break;
				default:
					break;
			}
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.DrawString(Main.fontItemStack, $"{text}", npc.Center - Main.screenPosition + new Vector2(-50, -50), Color.BlueViolet);
		}
		private void GetNPCFrame(int framenum)
		{
			npc.frame.Y = npc.height * framenum;
			return;
		}
	}
}
