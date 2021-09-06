using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.Items.Potions;

namespace Thinf.NPCs
{
	public class TomatoBat : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 5;
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);
			npc.aiStyle = 14;  //5 is the flying AI
			npc.lifeMax = 85;   //boss life
			npc.damage = 45;  //boss damage
			npc.defense = 20;    //boss defense
			npc.knockBackResist = 0.04f;
			npc.width = 44;
			npc.height = 32;
			npc.value = Item.buyPrice(0, 0, 0, 90);
			npc.npcSlots = 1f;
			npc.lavaImmune = false;
			npc.noGravity = true;
			npc.noTileCollide = false;
			npc.netAlways = true;
			animationType = NPCID.CaveBat;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Obstructed, 180);
        }
        public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), Main.rand.Next(4) + 5);
			if (Main.rand.Next(7) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Ketchup>(), Main.rand.Next(3) + 3);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss1)
			{
				return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneTomatoTown ? 0.91f : 0f;
			}
			return 0;
		}

		public override void AI()
		{

		}
    }
}
