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
	public class Badlock : ModNPC
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.aiStyle = -1;
			npc.lifeMax = 500;   //boss life
			npc.damage = 65;  //boss damage
			npc.defense = 30;    //boss defense
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 0f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.dontCountMe = true;
			npc.netAlways = true;
		}

        public override void NPCLoot()
        {
			if (Main.expertMode && Main.rand.Next() == 4)
			{
				for (int i = 0; i < 3; i++)
				{
					float rotation = MathHelper.ToRadians((((360f / 3) * i)) % 360f);
					float speed = 4f;
					Vector2 velocity = Vector2.One.RotatedBy(rotation) * speed;
					Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, velocity, ProjectileID.DesertDjinnCurse, npc.damage, 1.2f, Main.myPlayer)];
					p.hostile = true;
					p.friendly = false;
				}
			}
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

			if (NPC.AnyNPCs(ModContent.NPCType<NightKey>()))
            {
				NPC key = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<NightKey>())];
				npc.ai[3] += 0.04f;
				npc.velocity = npc.DirectionTo(key.Center + Vector2.One.RotatedBy(npc.ai[3]) * npc.ai[2]) * (npc.ai[2] / 30);
			}
			else
            {
				npc.active = false;
            }
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.CursedInferno, 120);
			target.AddBuff(BuffID.Ichor, 240);
		}
	}
}
