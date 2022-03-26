using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;

namespace Thinf.NPCs.Blizzard
{
	[AutoloadBossHead]
	public class Blizzard : ModNPC
	{
		int idleFrameTimer = 0;
		int phaseCount = -1;
		int knifeTimer = 0;
		int knifeCount = 0;
		int shardTimer = 0;
		int shardCount = 0;
		int iceTimer = 0;
		int iceCount = 0;
		int cutsceneTimer = 0;
		bool didCutscene = false;
		bool willDieHorribly = false;
		int shakeTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 9;
		}
		public override void SetDefaults()
		{
			npc.boss = true;
			npc.lifeMax = 200000;
			npc.damage = 175;
			npc.defense = 40;
			npc.knockBackResist = 0f;
			npc.width = 60;
			npc.height = 76;
			npc.value = Item.buyPrice(2, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.dontTakeDamage = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.Item48;
			npc.DeathSound = SoundID.Item27;
			npc.netAlways = true;
			npc.alpha = 255;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cold_Slice");
		}
		public override bool CheckDead()
		{
			if (npc.HasBuff(BuffID.OnFire) || npc.HasBuff(BuffID.CursedInferno))
			{
				willDieHorribly = true;
			}
			npc.velocity = Vector2.Zero;
			npc.ai[1] = 123;
			npc.life = 1;
			npc.dontTakeDamage = true;
			phaseCount = -1240;
			npc.damage = 0;
			npc.boss = false;
			music = 0;
			return didCutscene;
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
			ModNameWorld.downedBlizzard = true;
			Item.NewItem(npc.getRect(), ModContent.ItemType<FrozenEssence>(), Main.rand.Next(20) + 35);
			Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(12) + 15);
		}
		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{

		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Chilled, Thinf.ToTicks(5));
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				int projectileSpawnAmount = 24;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();
					Projectile.NewProjectile(npc.Center, projectileVelocity * 9, ModContent.ProjectileType<SnowGlobeBallTwo>(), 0, 0);
				}
				npc.active = false;
			}
			Player player = Main.player[npc.target];

			Lighting.AddLight(npc.Center, Color.LightBlue.ToVector3());
			if (phaseCount == -1240)
			{
				shakeTimer++;
				if (shakeTimer == 120)
				{
					npc.position.X += 4;
				}
				if (shakeTimer == 124)
				{
					npc.position.X -= 8;
				}
				if (shakeTimer >= 128)
				{
					npc.position.X += 4;
					shakeTimer = 0;
				}
				cutsceneTimer++;
				switch(cutsceneTimer)
				{
					case 1:
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "Oh dear")].lifeTime = 239;
						break;
					case 240:
						if (willDieHorribly)
						{
							npc.frame.Y = GetFrame(6);
						}
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "I have reached my limit")].lifeTime = 360;
						break;
					case 600:
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "Screw you, and your stupid dodging skills")].lifeTime = 420;
						break;
					case 1020:
						if (willDieHorribly)
						{
							npc.frame.Y = GetFrame(7);
						}
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "If the combat here was turn-based...")].lifeTime = 360;
						break;
					case 1380:
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "I would've totally kicked your ass...")].lifeTime = 360;
						break;
					case 1800:
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, "...so... uh...")].lifeTime = 420;
						break;
					case 2160:
						if (willDieHorribly)
						{
							npc.frame.Y = GetFrame(8);
						}
						Main.combatText[CombatText.NewText(npc.getRect(), Color.CornflowerBlue, $@"Mom's gonna tear you into pieces")].lifeTime = 480;
						break;
				}

				if (cutsceneTimer >= 2160)
				{
					npc.alpha++;
					if (npc.alpha == 265)
					{
						npc.life = 0;
						if (willDieHorribly)
						{
							Main.NewText("Blizzard has been melted!", 175, 75, 255);

							//if (Main.expertMode)
							//{
							//    Main.NewText("Do you think we can pick up the melted ice cream and eat it?", Color.SlateGray);
							//    Main.NewText("Does the 5-second rule apply?", Color.SlateGray);
							//    Main.NewText("Man, we just destroyed someone and I'm already hungry.", Color.SlateGray);
							//}
						}
						else
						{
							Main.NewText("Blizzard has been defeated!", 175, 75, 255);
							//if (Main.expertMode)
							//{
							//    Main.NewText("Nice", Color.SlateGray);
							//}
						}
						Item.NewItem(npc.getRect(), ItemID.SuperHealingPotion, 8 + Main.rand.Next(9));
						if (ModNameWorld.downedBlizzard)
						{
							Item.NewItem(npc.getRect(), ModContent.ItemType<FrostySmore>());
						}
						ModNameWorld.downedBlizzard = true;
						Item.NewItem(npc.getRect(), ModContent.ItemType<FrozenEssence>(), Main.rand.Next(20) + 35);
						Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(12) + 15);
					}
				}
			}
			if (npc.ai[0] == 1) //Check if buffed by mint choco
			{
				npc.damage = 200;
				npc.defense = 50;
			}
			else
			{
				npc.damage = 175;
				npc.defense = 30;
			}
			npc.netUpdate = true;
			npc.spriteDirection = -npc.direction;

			if (phaseCount == -1)
			{
				npc.alpha -= 2;
				if (npc.alpha <= 0)
				{
					npc.dontTakeDamage = false;
					phaseCount = 0;
				}
			}
			if (phaseCount != 1 && phaseCount != -1240)
			{
				for (int i = 0; i < 7; ++i)
				{
					Dust dust = Dust.NewDustDirect(npc.position + new Vector2(0, 60), 80, 8, DustID.Snow, npc.velocity.X * 0.2f, 3, 100);

					dust.noGravity = true;

					dust.scale *= 2.5f;
					dust.velocity *= 0.9f;
				}
				idleFrameTimer++;
				if (idleFrameTimer == 6)
				{
					npc.frame.Y = GetFrame(0);
				}
				if (idleFrameTimer >= 12)
				{
					npc.frame.Y = GetFrame(1);
					idleFrameTimer = 0;
				}
			}
			else if (phaseCount != -1240)
			{
				idleFrameTimer++;
				if (idleFrameTimer == 6)
				{
					npc.frame.Y = GetFrame(2);
				}
				if (idleFrameTimer == 12)
				{
					npc.frame.Y = GetFrame(3);
				}
				if (idleFrameTimer == 18)
				{
					npc.frame.Y = GetFrame(4);
				}
				if (idleFrameTimer == 24)
				{
					npc.frame.Y = GetFrame(5);
					idleFrameTimer = 0;
				}
			}
			if (phaseCount == 0)
			{
				if (npc.Distance(player.Center) >= 480)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 6;
				}
				else
				{
					npc.velocity = npc.DirectionTo(player.Center) * 3;
				}
				if (knifeCount >= 5)
				{
					knifeTimer = 0;
					knifeCount = 0;
					phaseCount = 1;
				}
				knifeTimer++;
				if (knifeTimer == 120)
				{
					Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<IceKnife>(), 75, 3)];
					proj.timeLeft = 1;
					proj.velocity = proj.DirectionTo(player.Center) * 12f;
					float numberProjectiles;
					if (Main.expertMode)
					{
						numberProjectiles = 3 + Main.rand.Next(3);
					}
					else
					{
						numberProjectiles = 3 + Main.rand.Next(2);
					}
					float rotation = MathHelper.ToRadians(45);
					proj.position += Vector2.Normalize(proj.velocity) * 45f;
					if (npc.ai[0] == 1) //Check if buffed by mint choco
					{
						for (int i = 0; i < numberProjectiles; i++)
						{
							Vector2 perturbedSpeed = proj.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1.5f;
							Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), 90, 3, player.whoAmI);
						}
					}
					else
					{
						for (int i = 0; i < numberProjectiles; i++)
						{
							Vector2 perturbedSpeed = proj.velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1.5f;
							Projectile.NewProjectile(npc.Center, perturbedSpeed, ModContent.ProjectileType<IceKnife>(), 78, 3, player.whoAmI);
						}
					}
					Main.PlaySound(SoundID.Item28, npc.Center);
					knifeTimer = 0;
					knifeCount++;
				}
			}

			if (phaseCount == 1)
			{
				npc.velocity = npc.DirectionTo(player.Center + new Vector2(0, -200)) * 2;

				if (shardCount >= 25)
				{
					shardCount = 0;
					shardTimer = 0;
					phaseCount = 2;
				}
				shardTimer++;
				if (shardTimer >= 11)
				{
					if (Main.expertMode)
					{
						Projectile.NewProjectile(npc.Center, new Vector2(0, -5).RotatedByRandom(MathHelper.ToRadians(80)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
					}
					Projectile.NewProjectile(npc.Center, new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(60)), ModContent.ProjectileType<BlizzardShard>(), 35, 2);
					shardTimer = 0;
					shardCount++;
				}
				Projectile.NewProjectile(npc.Center, new Vector2(0, 15), ModContent.ProjectileType<Frostburst>(), 1, 2);
			}

			if (phaseCount == 2)
			{
				if (npc.Distance(player.Center) >= 480)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 6;
				}
				else
				{
					npc.velocity = npc.DirectionTo(player.Center) * 3;
				}
				if (iceCount >= 8)
				{
					iceCount = 0;
					iceTimer = 0;
					phaseCount = 0;
				}
				iceTimer++;
				if (iceTimer >= 90)
				{
					int damage = 0;
					int type = ModContent.ProjectileType<IceCubeCenter>();
					Projectile proj = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Normalize(player.Center - npc.Center) * 3, type, damage, 1)];
					proj.ai[0] = 1;
					if (Main.expertMode || npc.life <= 100000)
					{
						proj.ai[0] = 2;
					}
					if (npc.life <= 50000)
					{
						proj.ai[0] = 4;
					}
					iceTimer = 0;
					iceCount++;
				}
			}
		}
		private int GetFrame(int framenum)
		{
			return npc.height * framenum;
		}
	}
}
