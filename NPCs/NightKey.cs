using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs
{
	// You can tell which Soul Key AI I made -- Lawn
	[AutoloadBossHead]
	public class NightKey : ModNPC
	{
		int timerForSpawn = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Key");
		}
		public override void SetDefaults()
		{
			npc.lifeMax = 10000;   //boss life
			npc.damage = 32;  //boss damage
			npc.defense = 18;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 32;
			npc.height = 58;
			npc.value = Item.buyPrice(0, 25, 0, 0);
			npc.npcSlots = 0.1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath3;
			npc.netAlways = true;
			npc.boss = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Soul_Keys");
		}

		public override void NPCLoot()
		{
			if (!NPC.AnyNPCs(mod.NPCType("FlightKey")) && !NPC.AnyNPCs(mod.NPCType("LightKey")))
			{
				Main.NewText("The Chest Wasteland grows stronger.. and messier!", 255, 255, 0);
				downedSoulKeys = true;
			}
			Item.NewItem(npc.getRect(), ModContent.ItemType<FragmentOfNight>(), Main.rand.Next(10) + 18);
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			Thinf.NPCGotoPlayer(npc, player, 0.8f);

			if (!NPC.AnyNPCs(ModContent.NPCType<Badlock>()))
			{
				timerForSpawn++;
			}
			if (timerForSpawn >= 600)
			{
				if (!NPC.AnyNPCs(ModContent.NPCType<Badlock>()))
				{
					for (int i = 0; i < 50; ++i)
					{
						Dust.NewDust(npc.Center, 50, 50, DustID.Corruption, 0, 0, 0, default, 2);
					}
					int npcSpawnAmount = 4;
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 60;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 120;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 180;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 240;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 300;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 360;
					}
					for (int i = 0; i < npcSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / npcSpawnAmount) * i;
						Vector2 spawnOffset = currentRotation.ToRotationVector2() * 2.5f;
						Vector2 spawnPos = npc.Center + spawnOffset * 30;
						NPC badlock = Main.npc[NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Badlock>())];
						badlock.ai[3] = currentRotation;
						badlock.ai[2] = 420;
					}
					timerForSpawn = 0;
				}
			}

		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.CursedInferno, 240);
			target.AddBuff(BuffID.Ichor, 240);
		}
	}
}
