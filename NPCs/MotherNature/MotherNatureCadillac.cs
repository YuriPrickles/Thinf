using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.MotherNature
{
	public class MotherNatureCadillac : ModNPC
	{
		int cutsceneTimer = 0;
		bool hasGoneBelowPlayer = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("An 8th Gen Cadillac Eldorado");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 200;
			npc.damage = 0;
			npc.defense = 1;
			npc.knockBackResist = 0f;
			npc.width = 256;
			npc.height = 172;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.dontTakeDamage = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit8;
			npc.DeathSound = SoundID.NPCDeath14;
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

			if (npc.Center.Y < player.Center.Y)
			{
				if (hasGoneBelowPlayer)
				{
					npc.velocity = Vector2.Zero;
				}
				else
				{
					npc.velocity.Y = 2;
				}
			}
			else
            {
				hasGoneBelowPlayer = true;
            }

			cutsceneTimer++;
            switch (cutsceneTimer)
            {
				case 600:
					Thinf.Kaboom(npc.Center);
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/RegularBoom").WithVolume(1.5f));
					Thinf.QuickSpawnNPC(npc, ModContent.NPCType<MotherNature>());
					npc.active = false;
					break;
                default:
                    break;
            }
        }
	}
}
