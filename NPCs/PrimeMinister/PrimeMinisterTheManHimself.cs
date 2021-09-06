using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

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
					Main.NewText("<Prime Minister> ...", Color.Yellow);
					break;
				case 200:
					Main.NewText("<Prime Minister> I'LL BE BACK!", Color.Yellow);
					break;
				case 360:
					Main.NewText("<Prime Minister> BYE.", Color.Yellow);
					break;
				case 520:
					Main.NewText("<Prime Minister> YOU PEASANT.", Color.Yellow);
					break;
			}
			if (talkTimer >= 600)
            {
				Item.NewItem(npc.getRect(), ItemID.GoldCoin);
				npc.velocity.Y -= 0.5f;
            }
			if (talkTimer >= 720)
			{
				ModNameWorld.downedPM = true;
				Main.NewText("Prime Minister flies away like a coward!", 175, 75, 255);
				npc.active = false;
            }
		}
	}
}
