using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Buffs;

namespace Thinf.NPCs
{
	public class HellSpirit : ModNPC
	{
		int dashTimer = 0;
		float spawnchance;
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 22;
			npc.lifeMax = 25000;
			npc.damage = 42;
			npc.defense = 45;
			npc.knockBackResist = 0f;
			npc.width = 48;
			npc.height = 80;
			npc.value = Item.buyPrice(0, 40, 2, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit54;
			npc.DeathSound = SoundID.NPCDeath33;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.buffImmune[BuffID.Bleeding] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.netAlways = true;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Nightmare>(), Thinf.MinutesToTicks(12));
		}

        public override void NPCLoot()
		{
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
		}
		public override void AI()
		{
			npc.rotation = MathHelper.ToRadians(0);
			npc.spriteDirection = npc.direction;
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			dashTimer++;
			if (dashTimer >= 60)
            {
				npc.aiStyle = -1;
				if (dashTimer % 40 == 0)
                {
					npc.velocity = npc.DirectionTo(player.Center) * 12;
                }
				if (dashTimer == 320)
                {
					npc.aiStyle = 22;
					dashTimer = -300;
                }
            }
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			if (NPC.downedMoonlord)
            {
				spawnchance = SpawnCondition.Underworld.Chance * 0.01f;
            }

			return spawnchance;
        }
    }
}
