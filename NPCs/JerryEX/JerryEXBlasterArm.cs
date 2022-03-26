using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.NPCs.JerryEX
{
	public class JerryEXBlasterArm : ModNPC
	{
		// THIS IS NOT A STRAWBERRY CREPE COOKIE RIP OFF TRUST ME

		int projectileTypeShooting = ProjectileID.BulletDeadeye;
		int phaseCount = 0;
		int phaseZeroTimer = -120;
		int movementTimer = 0;
		int movedir = 1;
		Vector2 destination;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cannon Arm");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.dontTakeDamage = true;
			npc.aiStyle = -1;
			npc.lifeMax = 50000;
			npc.damage = 45;
			npc.defense = 25;
			npc.knockBackResist = 0f;
			npc.width = 40;
			npc.height = 54;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.buffImmune[BuffID.Frostburn] = true;
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

			if (NPC.AnyNPCs(ModContent.NPCType<JerryEXMain>()))
			{
				NPC jerry = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<JerryEXMain>())];

				destination = jerry.Center + new Vector2(100, -45);
				movementTimer++;
				if (movementTimer >= 15)
				{
					destination += new Vector2(20, 0) * movedir;
					movementTimer = 0;
					movedir *= -1;
				}

				npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(destination) * 9f, .1f);
				//Above just slowly sets the velocity to go towards the target position.
				if (npc.velocity.Length() < 20f)
					npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop

				if (jerry.ai[0] == 150)
				{
					Dust dust;
					Vector2 position = npc.Center;
					dust = Dust.NewDustDirect(position, 0, 0, 31, 0f, -2.325581f, 0, new Color(255, 255, 255), 1.162791f);
					dust.fadeIn = 0.8372093f;
				}
				npc.rotation = npc.AngleTo(player.Center) + MathHelper.ToRadians(-90);
				if (jerry.ai[0] != 75 && jerry.ai[0] != -12)
				{
					if (phaseCount == 0)
					{

						phaseZeroTimer++;
						if (projectileTypeShooting == ProjectileID.BulletDeadeye)
						{
							if (phaseZeroTimer == 60)
							{
								if (!Main.dedServ)
									Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/JerryCannonArmKetchup").WithVolume(1.5f));
							}
							if (jerry.ai[0] == 150)
							{
								if (phaseZeroTimer >= 120 && phaseZeroTimer % 18 == 0)
								{
									Projectile proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 6).RotatedByRandom(MathHelper.ToRadians(65)), projectileTypeShooting, 22, 0);
									proj.hostile = true;
									proj.friendly = false;
									proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 6).RotatedByRandom(MathHelper.ToRadians(65)), projectileTypeShooting, 22, 0);
									proj.hostile = true;
									proj.friendly = false;
									proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 6).RotatedByRandom(MathHelper.ToRadians(65)), projectileTypeShooting, 22, 0);
									proj.hostile = true;
									proj.friendly = false;
									if (phaseZeroTimer >= 300)
									{
										projectileTypeShooting = ProjectileID.RocketSkeleton;
										phaseZeroTimer = 0;
									}
								}
							}
							else if (phaseZeroTimer >= 120 && phaseZeroTimer % 30 == 0)
							{
								Projectile proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 5).RotatedByRandom(MathHelper.ToRadians(45)), projectileTypeShooting, 19, 0);
								proj.hostile = true;
								proj.friendly = false;
								proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 5).RotatedByRandom(MathHelper.ToRadians(45)), projectileTypeShooting, 19, 0);
								proj.hostile = true;
								proj.friendly = false;
								proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 5).RotatedByRandom(MathHelper.ToRadians(45)), projectileTypeShooting, 19, 0);
								proj.hostile = true;
								proj.friendly = false;
								if (phaseZeroTimer >= 300)
								{
									projectileTypeShooting = ProjectileID.RocketSkeleton;
									phaseZeroTimer = 0;
								}
							}
						}
						if (projectileTypeShooting == ProjectileID.RocketSkeleton)
						{
							if (phaseZeroTimer == 60)
							{
								if (!Main.dedServ)
									Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/JerryCannonArmRocket").WithVolume(1.5f));
							}
							if (jerry.ai[0] == 150)
							{
								if (phaseZeroTimer >= 100 && phaseZeroTimer % 100 == 0)
								{
									Projectile proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 2), projectileTypeShooting, 50, 0);
									proj.hostile = true;
									proj.friendly = false;
									proj.Size *= 5f;
									proj.scale *= 5f;
									if (phaseZeroTimer >= 900)
									{
										projectileTypeShooting = ProjectileID.BulletDeadeye;
										phaseZeroTimer = 0;
									}
								}
							}
							else if (phaseZeroTimer >= 150 && phaseZeroTimer % 150 == 0)
							{
								Projectile proj = Projectile.NewProjectileDirect(npc.Center, (Vector2.Normalize(player.Center - npc.Center) * 3), projectileTypeShooting, 40, 0);
								proj.hostile = true;
								proj.friendly = false;
								proj.Size *= 3;
								proj.scale *= 3;
								if (phaseZeroTimer >= 900)
								{
									projectileTypeShooting = ProjectileID.BulletDeadeye;
									phaseZeroTimer = 0;
								}
							}
						}
					}
				}
			}
			else
			{
				npc.active = false;
			}
		}
		private int GetFrame(int framenum)
		{
			return npc.height * framenum;
		}
	}
}
