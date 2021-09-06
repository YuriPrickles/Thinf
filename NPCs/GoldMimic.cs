using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
	public class GoldMimic : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gold Coin");
			Main.npcFrameCount[npc.type] = 8;
		}
		int frameNumber = 1;
		public override void SetDefaults()
		{
			npc.aiStyle = -1;  //5 is the flying AI
			npc.lifeMax = 1500;   //boss life
			npc.damage = 55;  //boss damage
			npc.defense = 16;    //boss defense
			npc.knockBackResist = 0.6f;
			npc.width = 12;
			npc.height = 16;
			npc.value = Item.buyPrice(0, 1, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(25) == 0)
				Item.NewItem(npc.getRect(), mod.ItemType("OldKey"));
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter++;
			if (npc.frameCounter >= 6)
			{
				npc.frameCounter = 0;
				frameNumber++;
				if (frameNumber >= 8)
				{
					frameNumber = 0;
				}
				npc.frame.Y = frameNumber * (144 / 8);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneChestWasteland ? 25f : 0f;
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			//npc.spriteDirection = -npc.direction;
			Vector2 distance = player.Center - npc.Center;
			if (distance.Length() <= 150 || npc.life < 750)
			{
				npc.aiStyle = 41;
			}
		}
    }
}
