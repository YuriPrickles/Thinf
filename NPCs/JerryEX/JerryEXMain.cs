using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Pets;
using Thinf.Items.Placeables;
using Thinf.Items.Weapons;
using Thinf.Projectiles;

namespace Thinf.NPCs.JerryEX
{
	[AutoloadBossHead]
	public class JerryEXMain : ModNPC
	{
		// THIS IS NOT A STRAWBERRY CREPE COOKIE RIP OFF TRUST ME
		int deadTimer = 0;
		bool secondPhase = false;
		int cutsceneTimer = 0;
		bool isDoingCutscene = false;
		bool summonedArms = false;
		int idleFrameTimer = 0;
		int phaseCount = 0;
		int phaseZeroTimer = 0;
		int phaseOneTimer = 0;
		int phaseOneTomatCount = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jerry EX");
			NPCID.Sets.TrailCacheLength[npc.type] = 11;
			NPCID.Sets.TrailingMode[npc.type] = 0;
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 50000;
			npc.damage = 45;
			npc.defense = 25;
			npc.knockBackResist = 0f;
			npc.width = 80;
			npc.height = 62;
			npc.boss = true;
			npc.value = Item.buyPrice(0, 42, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.netAlways = true;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Jerry_EX");
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
			npc.lifeMax = 69420;
			npc.damage = 53;
        }
        public override bool CheckDead()
		{
			secondPhase = false;
			phaseZeroTimer = 0;
			phaseOneTimer = 0;
			phaseOneTomatCount = 0;
			npc.velocity = Vector2.Zero;
			npc.damage = 0;
			phaseCount = -1;
			npc.life = 1;
			npc.dontTakeDamage = true;
			music = 0;
            return false;
        }
        public override void AI()
		{
			if (phaseCount == -1) //jery dies :(
			{
				npc.velocity = Vector2.Zero;
				npc.ai[0] = -12;
				deadTimer++;
				if (deadTimer == 60)
				{
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/JerryDefeat").WithVolume(1.5f));
				}
				if (deadTimer >= 510)
				{
					npc.life = 0;
					int rand = Main.rand.Next(3);
					switch (rand)
					{
						case 0:
							Item.NewItem(npc.getRect(), ModContent.ItemType<RedAngel>());
							break;
						case 1:
							Item.NewItem(npc.getRect(), ModContent.ItemType<BlasterArm>());
							break;
						case 2:
							Item.NewItem(npc.getRect(), ModContent.ItemType<StaffOfTheRedPainter>());
							break;
						default:
							break;
					}
					Item.NewItem(npc.getRect(), ModContent.ItemType<Tomato>(), Main.rand.Next(100) + 100);
					if (Main.rand.Next(10) == 0)
					{
						Item.NewItem(npc.getRect(), ModContent.ItemType<HolyTomato>(), 1);
					}
				}
			}
			if (npc.life <= npc.lifeMax * 0.45f && !secondPhase && phaseCount != -1)
			{
				npc.dontTakeDamage = true;
				isDoingCutscene = true;
				cutsceneTimer++;
				if (cutsceneTimer == 240)
				{
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/JerrySecondPhase").WithVolume(1.5f));
				}
				if (cutsceneTimer == 520)
				{
					ModNameWorld.downedJerry = true;
					npc.dontTakeDamage = false;
					phaseCount = 0;
					isDoingCutscene = false;
					secondPhase = true;
				}
			}
			if (isDoingCutscene)
			{
				npc.velocity = Vector2.Zero;
				npc.ai[0] = 75;
            }
			if (secondPhase)
			{
				npc.ai[0] = 150;

				Dust dust;
				Vector2 position = npc.Center;
				dust = Dust.NewDustDirect(position, 0, 0, 31, 0f, -2.325581f, 0, new Color(255, 255, 255), 1.162791f);
				dust.fadeIn = 0.8372093f;
			}
			if (!summonedArms)
			{
				Thinf.QuickSpawnNPC(npc, ModContent.NPCType<JerryEXBlasterArm>());
				Thinf.QuickSpawnNPC(npc, ModContent.NPCType<JerryEXPortalArm>());
				summonedArms = true;
			}
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			idleFrameTimer++;
			if (idleFrameTimer == 6)
			{
				npc.frame.Y = GetFrame(0);
			}
			if (idleFrameTimer == 12)
			{
				npc.frame.Y = GetFrame(1);
			}
			if (idleFrameTimer == 18)
			{
				npc.frame.Y = GetFrame(2);
			}
			if (idleFrameTimer == 24)
			{
				npc.frame.Y = GetFrame(3);
				idleFrameTimer = 0;
			}

			if (!isDoingCutscene)
			{
				if (phaseCount != -1)
				{
					npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(player.Center + new Vector2(0, -125)) * 5f, .1f);
					//Above just slowly sets the velocity to go towards the target position.
					if (npc.velocity.Length() < 20f)
						npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop
				}

				if (phaseCount == 0)
				{

					phaseZeroTimer++;
					if (secondPhase)
					{
						if (phaseZeroTimer >= 120 && phaseZeroTimer % 35 == 0)
						{
							Projectile.NewProjectile(npc.Center, new Vector2(20, 0), ModContent.ProjectileType<Tom>(), 28, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(-20, 0), ModContent.ProjectileType<Tom>(), 28, 0);
							if (phaseZeroTimer >= 300)
							{
								phaseZeroTimer = 0;
								phaseCount = 1;
							}
						}
					}
					else
					{
						if (phaseZeroTimer >= 120 && phaseZeroTimer % 60 == 0)
						{
							Projectile.NewProjectile(npc.Center, new Vector2(16, 0), ModContent.ProjectileType<Tom>(), 24, 0);
							Projectile.NewProjectile(npc.Center, new Vector2(-16, 0), ModContent.ProjectileType<Tom>(), 24, 0);
							if (phaseZeroTimer >= 300)
							{
								phaseZeroTimer = 0;
								phaseCount = 1;
							}
						}
					}
				}

				if (phaseCount == 1)
				{
					if (phaseOneTomatCount >= 20)
					{
						phaseCount = 0;
						phaseOneTimer = 0;
						phaseOneTomatCount = 0;
					}
					phaseOneTimer++;
					if (phaseOneTimer >= 4)
					{
						Projectile proj = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-7, -10)), ModContent.ProjectileType<TomatoProj>(), 14, 0);
						proj.hostile = true;
						proj.friendly = false;
						phaseOneTomatCount++;
						phaseOneTimer = 0;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (secondPhase)
			{
				Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
				for (int k = 0; k < npc.oldPos.Length; k++)
				{
					Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
					Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
					spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (Main.expertMode && Main.rand.Next(9) == 0)
			{
				Projectile proj = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-4, -2)), ProjectileID.SaucerScrap, 25, 0);
			}
		}
		private int GetFrame(int framenum)
		{
			return npc.height * framenum;
		}
	}
}
