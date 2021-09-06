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
	public class WastelandMimic : ModNPC
	{
		public override string Texture => "Terraria/NPC_" + NPCID.Mimic;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 24;
			DisplayName.SetDefault("Wasteland Mimic");
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Mimic);
			npc.lifeMax = 1000;   //boss life
			npc.damage = 45;  //boss damage
			npc.defense = 20;    //boss defense
			npc.knockBackResist = 0.4f;
			npc.value = Item.buyPrice(0, 0, 60, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.netAlways = true;
			animationType = NPCID.Mimic;
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player P = Main.player[npc.target];
			npc.netUpdate = true;

			npc.ai[0]++;
			if (npc.ai[0] == 120)
			{
				float Speed = 12f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 20;  //projectile damage
				int type = mod.ProjectileType("DeathCoin");  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				npc.ai[0] = 0;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.GetModPlayer<MyPlayer>().ZoneChestWasteland ? 20f : 0f;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(7) == 0)
			Item.NewItem(npc.getRect(), mod.ItemType("OldKey"));
		}
	}
}
