using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Pets;
using Thinf.Items.Placeables;
using Thinf.Items.Potions;

namespace Thinf.NPCs
{
	public class KetchupSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 2;
		}
		public override void SetDefaults()
		{
			npc.scale = Main.rand.NextFloat(1, 4);
			npc.aiStyle = 1;
			npc.lifeMax = (int)(40 * npc.scale);
			npc.damage = (int)(10 * npc.scale);
			npc.defense = 10;    //boss defense
			npc.knockBackResist = 0.5f;
			npc.width = 28;
			npc.height = 38;
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.npcSlots = 1f;
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.netAlways = true;
			animationType = NPCID.BlueSlime;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Slow, 180);
			if (Main.expertMode)
            {
				target.AddBuff(BuffID.Weak, 600);
			}
        }
        public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), Main.rand.Next(3) + 3);
			if (Main.rand.Next(10) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Ketchup>(), Main.rand.Next(3) + 1);
			if (Main.rand.Next(50) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<HolyTomato>(), 1);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss1)
			{
				return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneTomatoTown ? 1.2f : 0f;
			}
			return 0;
		}

		public override void AI()
		{
			for (int i = 0; i < 5; ++i)
            {
				Dust dust;
				dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0f, 0f, 100, new Color(255, 255, 255), 1.25f)];
				dust.noGravity = true;
			}
		}
    }
}
