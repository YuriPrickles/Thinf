using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items;

namespace Thinf.NPCs
{
	public class SpiritMage : ModNPC
	{
		int var1 = 0;
		int var2 = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 3;
			npc.lifeMax = 2500;
			npc.damage = 30;
			npc.defense = 32;
			npc.knockBackResist = 0.8f;
			npc.width = 38;
			npc.height = 54;
			npc.value = Item.buyPrice(0, 0, 90, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.netAlways = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (npc.life < npc.lifeMax)
			{
				npc.HealEffect(300, true);
				npc.life += 300;
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedMoonlord)
			{
				return spawnInfo.player.ZoneDungeon ? 0.2f : 0f;
			}
			return 0f;
		}
		public override void NPCLoot()
        {
			if (Main.rand.Next(2) == 0)
			Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFight>(), Main.rand.Next(3) + 1);
		}

        public override void AI()
		{
			npc.spriteDirection = npc.direction;
			npc.TargetClosest(true);
			Player P = Main.player[npc.target];
			npc.netUpdate = true;
			var1++;
			if (var1 == 90 && Main.rand.Next(2) == 0)
			{
				
				float Speed = 4f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 32;  //projectile damage
				int type = ProjectileID.LostSoulHostile;  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
				Projectile projectile = Main.projectile[Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0)];
				projectile.hostile = true;
				projectile.friendly = false;
				var1 = 0;
			}
			var2++;
			if (var2 == 600)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<DummySpirit>());
				var2 = 0;
			}
		}
	}
}
