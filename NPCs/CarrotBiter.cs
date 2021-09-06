using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
	public class CarrotBiter : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carrot Biter");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 5;  //5 is the flying AI
			npc.lifeMax = 380;   //boss life
			npc.damage = 64;  //boss damage
			npc.defense = 21;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 64;
			npc.height = 32;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit38;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.buffImmune[24] = true;
			npc.netAlways = true;

		}

		public override void AI()
		{
			Player player = Main.player[npc.target];
			Vector2 moveTo = player.Center; //This player is the same that was retrieved in the targeting section.

			if (npc.ai[0] <= 0f) //Checks whether the NPC is ready to start another charge.
			{
				float speed = 10f;
				Vector2 move = moveTo - npc.Center;
				float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
				move *= speed / magnitude;
				npc.velocity = move;
				npc.ai[0] = 55f;
			}
			npc.ai[0] -= 1f; //So you can keep track of how long the NPC has been charging.
		}
	}
}
