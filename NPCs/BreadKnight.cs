using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
	public class BreadKnight : ModNPC
	{
		bool isShielded = false;
		int shieldDelay = 0;
		int frameTimer = 0;
		int frameCount = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 10;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 21000;
			npc.damage = 110;
			npc.defense = 120;
			npc.knockBackResist = 0.4f;
			npc.width = 52;
			npc.height = 56;
			npc.value = Item.buyPrice(0, 5, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.netAlways = true;
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
			npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center) * 3.4f, .1f);
			//Above just slowly sets the velocity to go towards the target position.
			if (npc.velocity.Length() < 19f)
				npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop

			GetNPCFrame(frameCount);

			if (!isShielded)
			{
				npc.defense = 120;
				npc.knockBackResist = 0.2f;
				frameTimer++;
				if (frameTimer == 6)
				{
					frameTimer = 0;
					frameCount++;
					if (frameCount >= 4)
					{
						frameCount = 0;
					}
				}
			}
			else
			{
				npc.velocity = Vector2.Zero;
				npc.defense = 123123;
				npc.knockBackResist = 0f;
				frameTimer++;
				if (frameTimer == 6)
				{
					frameTimer = 0;
					if (frameCount < 9)
					{
						frameCount++;
					}
				}
			}

			shieldDelay++;
			if (shieldDelay >= Thinf.ToTicks(5))
			{
				if (isShielded)
				{
					frameCount = 0;
					isShielded = false;
					shieldDelay = 0;
				}
				else
				{
					frameCount = 4;
					isShielded = true;
					shieldDelay = 0;
				}
			}
		}
		private void GetNPCFrame(int framenum)
		{
			npc.frame.Y = npc.height * framenum;
			return;
		}
	}
}
