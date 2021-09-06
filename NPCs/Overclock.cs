using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
	public class Overclock : ModNPC
	{
		float rotat = 0;
		int phaseCount = 0;
		int phaseZeroTimer = 0;
		int phaseOneTimer = 0;
		int phaseTwoTimer = 0;
		int frameNumber = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 7;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 3000;
			npc.damage = 24;
			npc.defense = 4;
			npc.knockBackResist = 0f;
			npc.width = 62;
			npc.height = 62;
			npc.value = Item.buyPrice(0, 12, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
			npc.boss = true;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter++;
			if (npc.frameCounter >= 6)
			{
				npc.frameCounter = 0;
				frameNumber++;
				if (frameNumber >= 7)
				{
					frameNumber = 0;
				}
				npc.frame.Y = frameNumber * (434 / 7);
			}
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}
		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			Main.NewText("How did you find me...");
			if (npc.active)
			{
				Main.sundialCooldown = 0;
				Main.Sundialing();
			}

			if (phaseCount == 0)
			{

			}
		}
	}
}
