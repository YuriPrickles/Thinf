using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs.MintCherry
{
	public class MintCherry : ModNPC
	{
		int cutsceneTimer = 0;
		float rotat = 0;
		int phaseCount = -1;
		int phaseZeroTimer = 0;
		int phaseOneTimer = 0;
		int buffTimer = 0;
		int laserTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cherry");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 10;
			npc.damage = 50;
			npc.defense = 0;
			npc.knockBackResist = 0f;
			npc.width = 58;
			npc.height = 46;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
		}

        public override bool CheckDead()
        {
			npc.life = 10;
			return false;
        }
        public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active || (!NPC.AnyNPCs(ModContent.NPCType<Blizzard.Blizzard>()) && phaseCount != -1))
			{
				npc.active = false;
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			npc.spriteDirection = npc.direction;

			if (NPC.AnyNPCs(ModContent.NPCType<Blizzard.Blizzard>()))
			{
				NPC blizzard = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Blizzard.Blizzard>())];
				if (blizzard.ai[1] == 123)
                {
					npc.active = false;
                }
			}

			if (phaseCount == -1)
			{
				cutsceneTimer++;
				if (cutsceneTimer <= 120)
                {
					npc.velocity.X = npc.DirectionTo(player.Center).X * 2;
                }
				else
                {
					npc.velocity = Vector2.Zero;
                }
				if (cutsceneTimer >= 360)
                {
					phaseCount = 0;
					NPC.NewNPC((int)(player.Center.X - 200 * player.direction), (int)player.Center.Y, ModContent.NPCType<Blizzard.Blizzard>());
                }
			}
			if (phaseCount == 0)
			{
				if (NPC.AnyNPCs(ModContent.NPCType<Blizzard.Blizzard>()))
				{
					NPC blizzard = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Blizzard.Blizzard>())];
					rotat = blizzard.AngleTo(player.Center) + MathHelper.ToRadians(-45);
					npc.velocity = npc.DirectionTo(blizzard.Center + Vector2.One.RotatedBy(rotat) * 80f) * 10;
				}
				phaseZeroTimer++;
				if (phaseZeroTimer >= Thinf.ToTicks(10))
				{
					rotat = 0;
					phaseCount = 1;
					phaseZeroTimer = 0;
				}
			}

			if (phaseCount == 1)
			{
				if (NPC.AnyNPCs(ModContent.NPCType<Blizzard.Blizzard>()))
				{
					NPC blizzard = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Blizzard.Blizzard>())];
					blizzard.ai[0] = 1;
					MintCherryQuickDustLine(npc.Center + new Vector2(16 * -npc.direction, -14), blizzard.Center, 25, Color.White);
					npc.velocity = npc.DirectionTo(blizzard.Center + new Vector2(100, -70)) * 7;
					buffTimer++;
					if (buffTimer >= 20)
					{
						if (blizzard.life <= 75000)
						{
							blizzard.life += 300;
							blizzard.HealEffect(300);
						}
						else
						{
							blizzard.life += 100;
							blizzard.HealEffect(100);
						}
						buffTimer = 0;
					}
				}
				phaseOneTimer++;
				if (phaseOneTimer >= Thinf.ToTicks(10))
				{
					NPC blizzard = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Blizzard.Blizzard>())];
					blizzard.ai[0] = 0;
					phaseCount = 2;
					phaseOneTimer = 0;
				}
			}
			if (phaseCount == 2)
			{
				if (npc.Distance(player.Center) >= 75)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 4;
				}
				laserTimer++;
				if (laserTimer >= 120 && laserTimer % 12 == 0)
				{
					Projectile proj = Projectile.NewProjectileDirect(npc.Center, Vector2.Normalize(player.Center - npc.Center) * 7, ProjectileID.LaserMachinegunLaser, 23, 1);
					proj.magic = false;
					proj.friendly = false;
					proj.hostile = true;
					if (laserTimer >= 260)
					{
						phaseCount = 0;
						laserTimer = 0;
					}
				}
			}
		}
		public static Dust MintCherryQuickDust(Vector2 pos, Color color)
		{
			Dust obj = Main.dust[Dust.NewDust(pos, 0, 0, DustID.Frost)];
			obj.position = pos;
			obj.velocity = Vector2.Zero;
			obj.fadeIn = 0f;
			obj.noLight = false;
			obj.noGravity = true;
			obj.color = color;
			obj.alpha = 128;
			return obj;
		}
		public static void MintCherryQuickDustLine(Vector2 start, Vector2 end, float splits, Color color)
		{
			MintCherryQuickDust(start, color).scale = 0.5f;
			MintCherryQuickDust(end, color).scale = 0.5f;
			float num = 1f / splits;
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				MintCherryQuickDust(Vector2.Lerp(start, end, num2), color).scale = 0.5f;
			}
		}
	}
}
