using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;

namespace Thinf.NPCs
{
	internal class ThunderChick : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thunder Chick");
			Main.npcFrameCount[npc.type] = 4;
			Main.npcCatchable[npc.type] = true;
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Bunny);
			npc.width = 32;
			npc.height = 32;
			npc.damage = 1;
			npc.defense = 1234;
			npc.lifeMax = 5;
			npc.HitSound = SoundID.NPCHit1;
			if (!Main.dedServ)
			{
				npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream").WithPitchVariance(.2f).WithVolume(2f);
			}
			npc.npcSlots = 0.5f;
			npc.noGravity = false;
			npc.catchItem = (short)ModContent.ItemType<ThunderChickItem>();
			npc.aiStyle = 7;
			npc.knockBackResist = 0.1f;
			npc.friendly = true; // We have to add this and CanBeHitByItem/CanBeHitByProjectile because of reasons.
			aiType = NPCID.Bunny;
			animationType = NPCID.Zombie;
			npc.dontTakeDamageFromHostiles = true;
		}

        public override void NPCLoot()
        {
			Player player = Main.player[npc.target];
			if (!NPC.AnyNPCs(mod.NPCType("ThunderCock")))
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("ThunderCock"));   //boss spawn
				Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream"), (int)player.position.X, (int)player.position.Y, 0);
			}
		}

        public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayRain.Chance * 0.16f;
		}

        public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
		}

        public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int i = 0; i < 6; i++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -2f);
					if (Main.rand.NextBool(2))
					{
						Main.dust[dust].noGravity = true;
						Main.dust[dust].scale = 1.2f * npc.scale;
					}
					else
					{
						Main.dust[dust].scale = 0.7f * npc.scale;
					}
				}
			}
		}

		public override void OnCatchNPC(Player player, Item item)
		{
			item.stack = 1;

			try
			{
				player.statLife -= 50;
				player.HealEffect(-50, true);
			}
			catch
			{
				return;
			}
		}
		internal class ThunderChickItem : ModItem
		{
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Thunder Chick");
			}

			public override void SetDefaults()
			{
				item.useStyle = 1;
				item.autoReuse = true;
				item.useTurn = true;
				item.useAnimation = 15;
				item.useTime = 10;
				item.maxStack = 999;
				item.consumable = true;
				item.width = 32;
				item.height = 32;
				item.noUseGraphic = true;
				item.makeNPC = (short)ModContent.NPCType<ThunderChick>();
			}
		}
	}
}