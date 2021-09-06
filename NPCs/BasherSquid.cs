using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Thinf.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class BasherSquid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Basher Squid");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 48;
			npc.height = 40;
			npc.damage = 40;
			npc.defense = 8;
			npc.lifeMax = 70;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 60f;
			npc.knockBackResist = 0.5f;
			npc.aiStyle = 3;
			animationType = NPCID.Zombie;
			//banner = Item.NPCtoBanner(NPCID.Zombie);
			//bannerItem = Item.BannerToItem(banner);
		}

		public override void AI()
		{
			if (npc.wet)
			{
				aiType = NPCID.CreatureFromTheDeep;
			}
			if (!npc.wet)
			{
				npc.aiStyle = 3;
				int num178 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
				int num179 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
				bool flag5 = true;
				if (Main.tile[num178, num179 - 1].nactive() && (TileLoader.IsClosedDoor(Main.tile[num178, num179 - 1]) || Main.tile[num178, num179 - 1].type == 388) && flag5)
				{
					bool flag23 = false;
					npc.velocity.X = 0.5f * (float)(-npc.direction);
					int num180 = 5;
					if (Main.tile[num178, num179 - 1].type == 388)
					{
						num180 = 2;
					}
					npc.ai[1] += num180;
					npc.ai[2] = 0f;
					if (npc.ai[1] >= 10f)
					{
						flag23 = true;
						npc.ai[1] = 10f;
					}

					WorldGen.KillTile(num178, num179 - 1, fail: true);
					if ((Main.netMode != 1 || !flag23) && flag23 && Main.netMode != 1)
					{

						WorldGen.KillTile(num178, num179 - 1);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, null, 0, num178, num179 - 1);
						}
					}
				}
			}
		}
	}
}

