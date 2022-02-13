using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{

	public class StrawberryShifterLifeFruit : ModNPC
	{
		int startTimer = 0;
		int frameCount = 0;
		int frameTimer = 0;
		int phaseCount = -2;
		int spewTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life Friut?");
			Main.npcFrameCount[npc.type] = 20;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 1200;
			npc.damage = 90;
			npc.defense = 10;
			npc.knockBackResist = 0.1f;
			npc.width = 24;
			npc.height = 26;
			npc.value = Item.buyPrice(0, 12, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
			npc.scale = 3;
		}
		public override void NPCLoot()
		{

		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			npc.spriteDirection = -npc.direction;
			GetNPCFrame(frameCount);

			if (phaseCount == -2)
            {
				startTimer++;
				if (startTimer == 240)
                {
					phaseCount = -1;
                }
            }
			if (phaseCount == -1)
			{
				frameTimer++;
				if (frameTimer == 60 && frameCount < 19)
				{
					frameTimer = 0;
					frameCount++;
				}
				if (frameCount >= 19)
                {
					phaseCount = 0;
                }
			}
			if (phaseCount == 0)
			{
				npc.velocity = npc.DirectionTo(player.Center + new Vector2(0, -100) + player.velocity) * 4f;
				spewTimer++;
				if (spewTimer == 200)
				{
					int length = Main.rand.Next(3) + 4;
					for (int i = 0; i < length; i++)
					{
						Projectile.NewProjectileDirect(npc.Center, new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians((i * length) * 10)) * length / 2, ProjectileID.SpikyBallTrap, 45, 0);
					}
					spewTimer = 0;
				}
			}

		}

        private void GetNPCFrame(int framenum)
		{
			npc.frame.Y = (int)(npc.height / npc.scale * framenum);
			return;
		}
	}
}