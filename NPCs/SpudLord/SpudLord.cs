using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.FryBiter;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.SpudLord
{
	[AutoloadBossHead]

	public class SpudLord : ModNPC
	{
		List<int> projRes = new List<int>();
		int laserShot = 0;
		int detonatoBombCount = 0;
		int overheatCounter = 200;
		int weakMode = 0;
		int weakTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Spud Lord");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			projRes.Add(ProjectileID.SporeCloud);
			projRes.Add(ProjectileID.Mushroom);
			projRes.Add(ProjectileID.RainbowBack);
			projRes.Add(ProjectileID.RainbowFront);
			projRes.Add(ProjectileID.RainFriendly);
			projRes.Add(ProjectileID.NorthPoleSnowflake);
			projRes.Add(ProjectileID.ToxicCloud);
			projRes.Add(ProjectileID.ToxicCloud2);
			projRes.Add(ProjectileID.ToxicCloud3);
			projRes.Add(ProjectileID.InfernoFriendlyBlast);
			projRes.Add(ProjectileID.MagnetSphereBall);
			projRes.Add(ProjectileID.MagnetSphereBolt);
			npc.aiStyle = -1;  //5 is the flying AI
			npc.lifeMax = 40000;   //boss life
			npc.damage = 50;  //boss damage
			npc.defense = 40;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 200;
			npc.height = 200;
			Main.npcFrameCount[npc.type] = 1;    //boss frame/animation 
			npc.value = Item.buyPrice(0, 40, 75, 45);
			npc.npcSlots = 1f;
			npc.boss = true;  
			npc.lavaImmune = true;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit8;
			npc.DeathSound = SoundID.NPCDeath14;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/The_Rooted_Menace");
			npc.netAlways = true;
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			downedSpudLord = true;
			potionType = ItemID.GreaterHealingPotion;   //boss drops
			Item.NewItem(npc.getRect(), mod.ItemType("McPickaxe"));
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.4f * bossLifeScale);  //boss life scale in expertmode
			npc.damage = (int)(npc.damage * 0.6f);  //boss damage increase in expermode
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.behindTiles = true;
				npc.noGravity = true;
				npc.velocity.Y += 0.5f;
				npc.noTileCollide = true;
			}
			Player P = Main.player[npc.target];
			npc.netUpdate = true;

			laserShot++;
			if (laserShot >= 240 && laserShot % 10 == 0)
			{
				Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DeathLaser, 20, 2)];
				projectile.velocity = projectile.DirectionTo(P.Center) * 12;
				projectile.velocity = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(5));
				if (laserShot == 320)
				laserShot = 0;
			}
			npc.ai[0]++;
			if (npc.ai[0] >= 200)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Floatato>());
				npc.ai[0] = 0;
			}

			npc.ai[1]++;
			if (npc.ai[1] >= 400)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Detonato>());
				npc.ai[1] = 0;
				npc.ai[2]++;
			}

			if (npc.ai[2] == 7)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<FryBiterHead>());
				npc.ai[2] = 0;
			}

			if (!P.ZoneDirtLayerHeight)
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<FryBiterHead>());
			}

			if (npc.life <= 20000)
			{
				npc.defense = 54;
				npc.ai[3]++;
				if (npc.ai[3] >= 120)  // 230 is projectile fire rate
				{
					float Speed = 12f;  //projectile speed
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
					int damage = 24;  //projectile damage
					int type = mod.ProjectileType("BouncyTater");  //put your projectile
					Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
					float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
					npc.ai[3] = 0;
				}
				if (npc.ai[3] == 30 || npc.ai[3] == 90)  // 230 is projectile fire rate
				{

					float Speed = 7f;  //projectile speed
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
					int damage = 32;  //projectile damage
					int type = ProjectileID.DeathLaser;  //put your projectile
					Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
					float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				}
				detonatoBombCount++;
				if (detonatoBombCount == 420)
                {
					Main.NewText("<SPUDOS V.0.1.2.5> READY TO BOMB DETONATOS IN:", Color.Red);
					Main.NewText("<SPUDOS V.0.1.2.5> 3", Color.Red);
				}
				if (detonatoBombCount == 480)
				{
					Main.NewText("<SPUDOS V.0.1.2.5> 2", Color.Red);
				}
				if (detonatoBombCount == 540)
				{
					Main.NewText("<SPUDOS V.0.1.2.5> 1", Color.Red);
				}
				if (detonatoBombCount == 600)
				{
					NPC.NewNPC((int)(P.Center.X + 400), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X - 400), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X + 350), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X - 350), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X + 300), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X - 300), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X + 250), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X - 250), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X + 200), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					NPC.NewNPC((int)(P.Center.X - 200), (int)P.Center.Y - 200, ModContent.NPCType<Detonato>());
					Main.NewText("<SPUDOS V.0.1.2.5> DROPPING DETONATOS", Color.Red);

					detonatoBombCount = -600;
				}
			}

			if (weakMode == 1)
            {
				Main.NewText("<SPUDOS V.0.1.2.5> MAIN SYSTEM IS OVERHEATING, SOME FEATURES MAY NOT FUNCTION WELL", Color.Red);
				weakMode = 2;
			}

			if (weakMode == 2)
			{
				MyPlayer.shakeType = 0;
				npc.defense = 24;
                weakTimer++;
				if (weakTimer % 6 == 0 && weakTimer % 8 != 0)
                {
					Projectile projectile1 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DeathLaser, 20, 2)];
					projectile1.velocity = projectile1.DirectionTo(P.Center) * 12;
					Projectile projectile2 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DeathLaser, 20, 2)];
					projectile2.velocity = projectile2.DirectionTo(P.Center) * 12;
					projectile2.velocity = projectile2.velocity.RotatedBy(MathHelper.ToRadians(16f));
					Projectile projectile3 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DeathLaser, 20, 2)];
					projectile3.velocity = projectile3.DirectionTo(P.Center) * 12;
					projectile3.velocity = projectile3.velocity.RotatedBy(MathHelper.ToRadians(-16f));
				}
				if (weakTimer == 420)
                {
					Main.NewText("<SPUDOS V.0.1.2.5> SYSTEM RESTORED TO NORMAL", Color.Red);
					npc.defense = 54;
					overheatCounter = 200;
					weakMode = 0;
					weakTimer = 0;
				}
            }

			if (Main.expertMode) 
			{
				
			}
		}

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
			if (npc.life <= 20000 && weakMode == 0)
            {
				overheatCounter -= 2;
				if (overheatCounter <= 0)
                {
					weakMode = 1;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (projRes.Contains(projectile.type))
            {
				damage /= 5;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (npc.life <= 20000 && weakMode == 0)
			{
				overheatCounter -= 2;
				if (overheatCounter <= 0)
				{
					weakMode = 1;
				}
			}
		}
        private const int Sphere = 100;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
			if (npc.life <= 20000)
            {
				return false;
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (npc.life <= 20000)
			{
                spriteBatch.Draw(mod.GetTexture("NPCs/SpudLord/SpudBot"), npc.Center - Main.screenPosition, null, new Color(255, overheatCounter, overheatCounter), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
			}
		}
	}      
}