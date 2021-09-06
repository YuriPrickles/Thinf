using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.NPCs
{
	public class PoliticianBee : ModNPC
	{
		public override string Texture => "Terraria/NPC_" + NPCID.Bee;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Bee);
			npc.lifeMax = 1000;   //boss life
			npc.damage = 45;  //boss damage
			npc.defense = 24;    //boss defense
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			npc.scale = 2;
			animationType = NPCID.Bee;
		}

		public override void AI()
		{
			npc.timeLeft = 2;
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			npc.rotation = npc.velocity.ToRotation();
			if (NPC.AnyNPCs(ModContent.NPCType<PrimeMinister.PrimeMinister>()))
            {
				NPC primeMinister = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<PrimeMinister.PrimeMinister>())];
				npc.ai[3] += 0.04f;
				npc.velocity = npc.DirectionTo(primeMinister.Center + Vector2.One.RotatedBy(npc.ai[3]) * 120f) * 7;
			}
			else
            {
				npc.active = false;
            }
		}
	}
}
