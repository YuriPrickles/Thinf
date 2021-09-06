using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
	public class CortaPortal : ModNPC
	{
		int spewTimer = 0;
		int projRand;
		int projType;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Portal");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;  //5 is the flying AI
			npc.lifeMax = 80;   //boss life
			npc.damage = 1;  //boss damage
			npc.defense = 0;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 32;
			npc.height = 48;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.dontCountMe = true;
			npc.HitSound = SoundID.NPCHit52;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			npc.ai[0]++;
			if (npc.ai[0] < 91)
			{
				npc.alpha += 35;
				if (npc.alpha >= 255)
				{
					npc.alpha = 0;
				}
			}
			else
			{
				npc.alpha = 0;
			}
			npc.rotation = npc.AngleTo(player.Center);
			if (npc.collideX && npc.collideY)
			{
				npc.velocity = npc.DirectionTo(player.Center) * 15;
			}
			else
			{
				npc.velocity = Vector2.Zero;
			}
			spewTimer++;
			if (player.wet)
			{
				if (spewTimer >= 480)
				{
					projRand = Main.rand.Next(5);
					switch (projRand)
					{
						case 0:
							projType = ProjectileID.WoodenArrowHostile;
							break;
						case 1:
							projType = ProjectileID.HarpyFeather;
							break;
						case 2:
							projType = ProjectileID.DeathLaser;
							break;
						case 3:
							projType = ProjectileID.RuneBlast;
							break;
						case 4:
							projType = ProjectileID.SkeletonBone;
							break;
					}
					Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
					if (projType == ProjectileID.SkeletonBone)
					{
						Projectile bone1 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone1.velocity = projectile.DirectionTo(player.Center) * 7;
						bone1.velocity = bone1.velocity.RotatedByRandom(MathHelper.ToRadians(12));
						Projectile bone2 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone2.velocity = projectile.DirectionTo(player.Center) * 7;
						bone2.velocity = bone2.velocity.RotatedByRandom(MathHelper.ToRadians(12));
						Projectile bone3 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone3.velocity = projectile.DirectionTo(player.Center) * 7;
						bone3.velocity = bone3.velocity.RotatedByRandom(MathHelper.ToRadians(12));
					}
					projectile.velocity = projectile.DirectionTo(player.Center) * 11;
					spewTimer = 0;
				}
			}
			if (!player.wet)
			{
				if (spewTimer >= 240)
				{
					projRand = Main.rand.Next(5);
					switch (projRand)
					{
						case 0:
							projType = ProjectileID.JestersArrow;
							break;
						case 1:
							projType = ProjectileID.EyeBeam;
							break;
						case 2:
							projType = ProjectileID.PhantasmalBolt;
							break;
						case 3:
							projType = ProjectileID.SaucerMissile;
							break;
						case 4:
							projType = ProjectileID.BoneGloveProj;
							break;
					}
					Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
					projectile.hostile = true;
					projectile.friendly = false;
					if (projType == ProjectileID.BoneGloveProj)
					{
						Projectile bone1 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone1.velocity = projectile.DirectionTo(player.Center) * 15;
						bone1.velocity = bone1.velocity.RotatedByRandom(MathHelper.ToRadians(12));
						Projectile bone2 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone2.velocity = projectile.DirectionTo(player.Center) * 15;
						bone2.velocity = bone2.velocity.RotatedByRandom(MathHelper.ToRadians(12));
						Projectile bone3 = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, projType, 8, 0)];
						bone3.velocity = projectile.DirectionTo(player.Center) * 15;
						bone3.velocity = bone3.velocity.RotatedByRandom(MathHelper.ToRadians(12));
					}
					projectile.velocity = projectile.DirectionTo(player.Center) * 15;
					spewTimer = 0;
				}
			}
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (NPC.AnyNPCs(ModContent.NPCType<Cortal.Cortal>()))
			{
				if (projectile.penetrate <= 1)
				{
					projectile.penetrate = 2;
				}
				NPC cortal = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Cortal.Cortal>())];
				Main.PlaySound(SoundID.Item6, npc.Center);
				projectile.Center = cortal.Center - new Vector2(100 * cortal.direction, Main.rand.Next(-20, 20));
				projectile.velocity = projectile.DirectionTo(cortal.Center) * 14;
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (NPC.CountNPCS(npc.type) > 1 && npc.ai[0] >= 180)
			{
				var allNPCs = FindAllNPCs(npc.type, npc.whoAmI);
				NPC tpPos = Main.npc[Main.rand.Next(allNPCs)];
				Player player = Main.player[npc.target];
				player.Teleport(tpPos.Center);
				tpPos.ai[0] = 0;
				npc.ai[0] = 0;
			}
		}
		public static List<int> FindAllNPCs(int type, int whoAmI)
		{
			List<int> portalcount = new List<int>();
			for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC portalCheck = Main.npc[i];
				if (portalCheck.active && portalCheck.type == type && !portalcount.Contains(i) && i != whoAmI)
				{
					portalcount.Add(i);
				}
			}
			return portalcount;
		}
	}
}