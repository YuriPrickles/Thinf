using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items;

namespace Thinf.NPCs.PrimeMinister
{
	public class PrimeMinisterTheManHimself : ModNPC
	{
		int talkTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 4;
			DisplayName.SetDefault("Prime Minister");
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Bee);
			npc.width = 18;
			npc.height = 22;
			npc.aiStyle = -1;
			npc.dontTakeDamage = true;
			npc.boss = true;
			npc.immortal = true;
			npc.lifeMax = 1;
			npc.damage = 0;
			npc.defense = 20;
			npc.knockBackResist = 0.0f;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			animationType = NPCID.Bee;
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
			potionType = ItemID.SuperHealingPotion;
        }
        public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			npc.spriteDirection = npc.direction;
			talkTimer++;
			switch(talkTimer)
			{
				case 120:
					Main.NewText("<Prime Minister> I CAN'T BEELIEVE IT.", Color.Yellow);
					break;
				case 300:
					Main.NewText("<Prime Minister> I'M GOING TO LEAVE NOW.", Color.Yellow);
					break;
				case 480:
					Main.NewText("<Prime Minister> I CAN'T MISS THE SEASON FINALE OF 'Seijika to koi ni'", Color.Yellow);
					break;
				case 800:
					Main.NewText("<Prime Minister> THE STORY'S REALLY GETTING GOOD", Color.Yellow);
					break;
			}
			if (talkTimer >= 960)
            {
				Item.NewItem(npc.getRect(), ItemID.GoldCoin);
				npc.velocity.Y -= 0.5f;
            }
			if (talkTimer >= 1200)
			{
				if (ModNameWorld.downedPM)
				{
					Item.NewItem(npc.getRect(), ModContent.ItemType<Smore>());
				}
				Item.NewItem(npc.getRect(), ModContent.ItemType<CorruptedPoliticalPower>(), 45 + Main.rand.Next(15));
				ModNameWorld.downedPM = true;
				Main.NewText("Prime Minister flies away like a coward!", 175, 75, 255);
				//if (Main.expertMode)
				//{
				//	Main.NewText("Though can we really blame him?", Color.SlateGray);
				//	Main.NewText("WE destroyed 3 armored vehicles belonging to him.", Color.SlateGray);
				//	Main.NewText("Without any more tricks up his sleeve, what can he really do?", Color.SlateGray);
				//}
				npc.active = false;
            }
		}
	}
}
