using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.TownNPCs;
using Thinf.Projectiles;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.Beenado
{
	[AutoloadBossHead]
	public class Beenado : ModNPC
	{
		int phaseCount = 0;
		int stingerRainDelay = 0;
		int stingersRained = 0;
		int cooltimer = 0;
		int suckTimer = 0;
		int beeTimer = 0;
		int beeCount = 0;
		int phaseTwoStingSpreadTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beenado"); //yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaay
											   //Main.npcFrameCount[npc.type] = 7;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = 22;
			aiType = NPCID.Pixie;
			npc.lifeMax = 24000;   //boss life
			npc.damage = 0;
			npc.defense = 8;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 512;
			npc.height = 768;
			npc.value = Item.buyPrice(0, 14, 65, 35);
			npc.npcSlots = 1f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			music = MusicID.Boss5; //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cacterus");
			musicPriority = MusicPriority.BossHigh;
			npc.netAlways = true;
		}

		/*public override void FindFrame(int frameHeight)
		{
			npc.frameCounter++;
			if (npc.frameCounter >= 6)
			{
				npc.frameCounter = 0;
				frameNumber++;
				if (frameNumber >= 7)
				{
					frameNumber = 0;
				}
				npc.frame.Y = frameNumber * (840 / 7);
			}
		}*/

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;   //boss drops
			downedBeenado = true;

			if (!NPC.AnyNPCs(ModContent.NPCType<Beekeeper>()))
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Beekeeper>());
			}
			Main.rainTime = 300;
			Main.raining = false;
			Item.NewItem(npc.getRect(), mod.ItemType("SusPlaceholder"));
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.defense = 14;
			npc.life = 36000;
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				cooltimer++;
				npc.velocity.X -= 1;
				if (cooltimer >= 120)
				{
					npc.active = false;
					cooltimer = 0;
				}
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			if (phaseCount == 0)
			{
				npc.damage = 0;
				stingerRainDelay++;
				if (stingerRainDelay == 50)
				{
					for (int i = -8; i <= 8; ++i)
					{
						Projectile.NewProjectile(player.Center + new Vector2(100 * i, -500), new Vector2(0, 9), ProjectileID.Stinger, 25, 5);
					}
					if (stingersRained % 2 == 0)
					{
						Projectile honey = Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ModContent.ProjectileType<HoneySplat>(), 35, 10);
						Thinf.ProjGotoPlayer(honey, player, 4);
					}
					stingerRainDelay = 0;
					stingersRained++;
				}
				if (stingersRained == 10)
				{
					Main.PlaySound(SoundID.ForceRoar, npc.Center, 0);
					stingerRainDelay = 0;
					stingersRained = 0;
					phaseCount = 1;
				}
			}

			if (phaseCount == 1)
			{
				phaseTwoStingSpreadTimer++;
				if (phaseTwoStingSpreadTimer == 120)
				{
					int rotationRand = Main.rand.Next(360);
					int projectileSpawnAmount = 32;
					for (int i = 0; i < projectileSpawnAmount; ++i)
					{
						float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i;
						Vector2 projectileVelocity = currentRotation.ToRotationVector2();
						int damage = 24;  //projectile damage
						int type = ProjectileID.Stinger;  //put your projectile
						Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
						Projectile.NewProjectile(npc.Center, (projectileVelocity * 4).RotatedBy(MathHelper.ToRadians(rotationRand)), type, damage, 0, Main.myPlayer);
					}
					phaseTwoStingSpreadTimer = 0;
				}
				npc.damage = 65;
				if (Main.expertMode)
				{
					npc.damage = 90;
				}
				npc.aiStyle = -1;
				npc.velocity = Vector2.Zero;
				screenShake = true;
				MyPlayer.shakeType = 2;
				suckTimer++;
				if (suckTimer == Thinf.ToTicks(8))
				{
					Main.PlaySound(SoundID.ForceRoar, npc.Center, 0);
				}
				if (suckTimer == Thinf.ToTicks(10))
				{
					for (int i = -12; i <= 12; ++i)
					{
						Projectile.NewProjectile(player.Center + new Vector2(200 * i, -500), new Vector2(0, 5), ModContent.ProjectileType<HoneySplat>(), 25, 5);
					}
					npc.aiStyle = 22;
					suckTimer = 0;
					phaseCount = 2;
				}
				if (player.Center.X < npc.Center.X)
				{
					if (player.velocity.X < 2)
					{
						player.velocity.X += 1f;
					}
				}
				else
				{
					if (player.velocity.X > -2)
					{
						player.velocity.X -= 1f;
					}
				}
			}

			if (phaseCount == 2)
			{
				beeTimer++;
				if (beeTimer == 10)
				{
					Main.NewText("A storm is buzzing!", 175, 75, 255);
					NPC bee = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Bee)];
					bee.lifeMax = 900;
					bee.life = 900;
					bee.scale = 3;
					bee.GivenName = "Storm Buzzer";
					bee.width *= 3;
					bee.height *= 3;
					bee.defense = -20;
					bee.knockBackResist = 1.2f;
					beeTimer = 0;
					beeCount++;
				}
				if (beeCount == 3)
				{
					beeTimer = 0;
					beeCount = 0;
					if (npc.life <= 18000)
					{
						phaseCount = 3;
					}
					else
					{
						phaseCount = 0;
					}
				}
			}

			if (phaseCount == 3)
			{
				npc.damage = 0;
				stingerRainDelay++;
				if (stingerRainDelay == 140)
				{
					for (int i = -8; i <= 8; ++i)
					{
						Projectile proj = Projectile.NewProjectileDirect(player.Center + new Vector2(100 * i, -500), new Vector2(0, 6), ProjectileID.Stinger, 25, 5);
						proj.tileCollide = false;
					}
					if (npc.life <= 7000 && Main.expertMode)
					{
						for (int i = -8; i <= 8; ++i)
						{
							Projectile proj = Projectile.NewProjectileDirect(player.Center + new Vector2(-1000, 100 * i), new Vector2(6, 0), ProjectileID.Stinger, 25, 5);
							proj.tileCollide = false;
						}
						for (int i = -8; i <= 8; ++i)
						{
							Projectile proj = Projectile.NewProjectileDirect(player.Center + new Vector2(1000, 100 * i), new Vector2(-6, 0), ProjectileID.Stinger, 25, 5);
							proj.tileCollide = false;
						}
						for (int i = -8; i <= 8; ++i)
						{
							Projectile proj = Projectile.NewProjectileDirect(player.Center + new Vector2(100 * i, 500), new Vector2(0, -6), ProjectileID.Stinger, 25, 5);
							proj.tileCollide = false;
						}
					}
					stingerRainDelay = 0;
					stingersRained++;
				}
				if (stingersRained == 10)
				{
					stingerRainDelay = 0;
					stingersRained = 0;
					phaseCount = 0;
				}
			}
		}
        public override void BossHeadRotation(ref float rotation)
        {
			rotation = 45;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.type == ProjectileID.HallowStar)
			{
				damage /= 4;
			}
		}
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (phaseCount == 3)
			{
				Main.PlaySound(SoundID.ForceRoar, npc.Center, 0);
				Player player = Main.player[npc.target];
				projectile.hostile = true;
				projectile.friendly = false;
				projectile.penetrate = -1;
				Thinf.ProjGotoPlayer(projectile, player, projectile.velocity.Length());
			}
		}
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.rand.NextBool())
			{
				player.AddBuff(BuffID.Poisoned, 600, true);
			}
		}
	}
}