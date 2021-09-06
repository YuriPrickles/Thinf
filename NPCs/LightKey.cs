using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs
{
	[AutoloadBossHead]
	public class LightKey : ModNPC
	{
		int corn;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light Key");
		}
		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.EnchantedSword);
			npc.lifeMax = 17500;   //boss life
			npc.damage = 32;  //boss damage
			npc.defense = 18;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 32;
			npc.height = 58;
			npc.value = Item.buyPrice(0, 25, 0, 0);
			npc.npcSlots = 0.1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath3;
			npc.netAlways = true;
			npc.boss = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Soul_Keys");
		}
		public override void NPCLoot()
		{
			if (!NPC.AnyNPCs(mod.NPCType("NightKey")) && !NPC.AnyNPCs(mod.NPCType("FlightKey")))
            {
				Main.NewText("The Chest Wasteland grows stronger and messier!", 255, 255, 0);
				downedSoulKeys = true;
            }
			Item.NewItem(npc.getRect(), mod.ItemType("FragmentOfLight"), Main.rand.Next(10) + 18);
		}
		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player P = Main.player[npc.target];
			npc.netUpdate = true;

			corn++;
			if (corn%40 == 0)
            {
				float Speed = 7f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 17;  //projectile damage
				int type = ProjectileID.PinkLaser;  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				if (corn == 800)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.IlluminantBat);
					corn = 0;
				}
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Confused, 120);
			target.AddBuff(BuffID.Slow, 120);
		}
	}
}
