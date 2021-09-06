using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items;
using static Thinf.MyPlayer;

namespace Thinf.NPCs.Nightmares
{
	public class NightmareTortoise : ModNPC
	{
		int phaseTimer = 0;
		public override void SetStaticDefaults()
		{

		}
		public override void SetDefaults()
		{
			npc.width = 76;
			npc.height = 60;
			npc.aiStyle = 39;
			npc.lifeMax = 27500;
			npc.damage = 260;
			npc.defense = 34;
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 24, 60, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.HitSound = SoundID.NPCHit54;
			npc.DeathSound = SoundID.NPCDeath33;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.netAlways = true;
		}

        public override void NPCLoot()
        {
			Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(3) + 3);
        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (nightmare)
			{
				return 0.16f / 30;
			}
			return 0;
		}
		public override void FindFrame(int frameHeight)
        {
			npc.frame.Y = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()))
			{
				return false;
			}
			return true;
        }
        public override void AI()
		{
			if (npc.life > 12500)
			{
			}
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			if (npc.life <= 12500)
			{
			}
			phaseTimer++;
			if (phaseTimer >= 300)
			{
				npc.noTileCollide = false;
				npc.aiStyle = -1;
				npc.noGravity = true;
				npc.rotation++;
				npc.damage = 420;
				npc.defense = 80;
				npc.velocity = npc.DirectionTo(player.Center) * 7.6f;
				if (phaseTimer == 600)
				{
					npc.noTileCollide = true;
					npc.rotation = MathHelper.ToRadians(0);
					npc.noGravity = false;
					npc.aiStyle = 39;
					npc.damage = 260;
					npc.defense = 34;
					phaseTimer = 0;
                }
			}
			if (phaseTimer == 300)
			{
				Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
			}
			Lighting.AddLight(npc.Center, new Vector3(255, 0, 0) / 255);
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (target.HasBuff(ModContent.BuffType<Nightmare>()))
			{
				target.AddBuff(BuffID.Obstructed, 80);
				target.AddBuff(BuffID.Darkness, Thinf.ToTicks(25));
				target.AddBuff(BuffID.Cursed, 20);
				if (Main.rand.Next(5) == 0)
				{
					target.AddBuff(BuffID.Slow, 40);
				}
			}
		}
	}
}
