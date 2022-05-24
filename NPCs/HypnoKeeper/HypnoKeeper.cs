using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.THE_SUPER_COOL_BADASS_LORE;
using Thinf.NPCs.TownNPCs;
using Thinf.Projectiles;

namespace Thinf.NPCs.HypnoKeeper
{
	[AutoloadBossHead]
	public class HypnoKeeper : ModNPC
	{
		int rot2 = 0;
		int positionChangeTimer = -180;
		Vector2 randomPos;
		int attackCount = 0;
		public int attackTimer = -60;
		public bool noDrugs = false;
		public int noDrugTimer = 0;
		int chargeAmount = 0;
		int rotation = 0;
		Vector2 playerAngle;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hypno Keeper");
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 25000;
			npc.damage = 50;
			npc.defense = 30;
			npc.knockBackResist = 0f;
			npc.width = 30;
			npc.height = 50;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
			npc.boss = true;
			animationType = NPCID.Harpy;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Helplessly_Hanging");
		}
        public override void BossLoot(ref string name, ref int potionType)
		{
			ModNameWorld.downedHypnoKeeper = true;
			Item.NewItem(npc.getRect(), ModContent.ItemType<LogFileTwo>());
			potionType = ItemID.BottledHoney;
			if (!NPC.AnyNPCs(ModContent.NPCType<Beekeeper>()))
			{
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Beekeeper>());
			}
		}
        public override void AI()
		{
			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			if (!noDrugs)
			{
				npc.defense = 30;
			}
			else
			{
				npc.defense = 60;
				int dustSpawnAmount = 32;
				for (int i = 0; i < dustSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
					Vector2 dustOffset = currentRotation.ToRotationVector2();
					Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 60, DustID.Shadowflame, null, 0, default, 1.5f);
					dust.noGravity = true;
				}
				for (int i = 0; i < dustSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
					Vector2 dustOffset = currentRotation.ToRotationVector2();
					Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 30, DustID.Shadowflame, null, 0, default, 1.5f);
					dust.noGravity = true;
				}
			}

			if (noDrugTimer > 0)
			{
				noDrugTimer--;
			}
			if (noDrugs)
			{
				positionChangeTimer++;
				if (positionChangeTimer >= 111)
				{
					npc.velocity = npc.DirectionTo(randomPos) * 5f;

					if (positionChangeTimer >= 150)
					{
						npc.velocity = Vector2.Zero;
						positionChangeTimer = Main.rand.Next(-80, -20);
					}
				}
				else if (positionChangeTimer <= 90 && positionChangeTimer > 40)
				{
					randomPos = player.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200));
				}
			}
			else
			{
				positionChangeTimer++;
				if (positionChangeTimer >= 60)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 11f;

					if (positionChangeTimer >= 70)
					{
						chargeAmount++;
						if (chargeAmount >= 5)
						{
							chargeAmount = 0;
						}
						npc.velocity = Vector2.Zero;
						positionChangeTimer = -10 + (10 * chargeAmount);
					}
				}
			}
			switch (attackCount)
			{
				case 0:
					attackTimer++;
					if (attackTimer >= 120)
					{
						ModNameWorld.screenShake = false;
						MyPlayer.shakeType = -1;
						attackCount = 1;
						attackTimer = 0;
						for (int i = 0; i < Main.maxPlayers; i++)
						{
							if (Main.player[i].active)
							{
								Main.player[i].AddBuff(ModContent.BuffType<Hallucinating>(), 999999);
							}
						}
						noDrugs = true;
						Main.NewText("Hallucination!");
					}
					else if (attackTimer > 0)
					{
						MyPlayer.shakeType = 2;
						ModNameWorld.screenShake = true;
					}
					break;
				case 1:
					attackTimer++;
					if (attackTimer % 30 == 0)
					{
						if (noDrugs)
							Thinf.QuickSpawnNPC(npc, ModContent.NPCType<MindDemon>());
					}
					if (attackTimer >= 240)
					{
						attackCount = 2;
						attackTimer = 0;
					}
					break;
				case 2:
					int paintingRand = Main.rand.Next(10);
					attackTimer++;
					if (attackTimer % 20 == 0)
					{
						int type = 0;
						switch (paintingRand)
						{
							case 0:
							case 1:
								type = ModContent.ProjectileType<NursePortrait>();
								break;
							case 2:
							case 3:
							case 4:
								type = ModContent.ProjectileType<DemolitionistPortrait>();
								break;
							case 5:
							case 6:
							case 7:
							case 8:
							case 9:
								type = ModContent.ProjectileType<GuidePortrait>();
								break;
						}
						Projectile proj = Projectile.NewProjectileDirect(new Vector2(player.Center.X + (1 * Main.screenWidth), player.Center.Y + Main.rand.Next(-750, 750)), new Vector2(-4.5f, 0), type, 15, 0);
						if (type == ModContent.ProjectileType<NursePortrait>())
						{
							proj.damage = 0;
						}
						if (type == ModContent.ProjectileType<DemolitionistPortrait>())
						{
							proj.damage = 50;
						}
					}
					if (!noDrugs)
					{
						attackCount = 3;
					}
					if (attackTimer >= 250)
					{
						attackCount = 1;
						attackTimer = 0;
					}
					break;
				case 3:
					attackTimer++;
					if (attackTimer == 60)
					{
						playerAngle = Vector2.Normalize(player.Center - npc.Center);
					}
					if (attackTimer == 90)
					{
						Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f * -1)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f * -1)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
					}
					if (attackTimer >= 60)
					{

					}
					if (attackTimer >= 100)
					{
						if (attackTimer % 50 == 0)
						{
							rotation += Main.rand.Next(20, 45);
							rot2 += Main.rand.Next(-35, -10);
						}
					}
					if (attackTimer % 5 == 0)
					{
						if (attackTimer >= 100 && attackTimer < 150)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f * -1)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f * -1)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
						}
						if (attackTimer == 150)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						}
						if (attackTimer >= 160 && attackTimer < 200)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
						}
						if (attackTimer == 200)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						}
						if (attackTimer >= 210 && attackTimer < 250)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
						}
						if (attackTimer == 250)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 10, ProjectileID.ShadowBeamHostile, 0, 1).tileCollide = false;
						}
						if (attackTimer >= 260 && attackTimer < 300)
						{
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + rotation)) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(22.5f + (rotation * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + (rotation + rot2))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
							Projectile.NewProjectileDirect(npc.Center, playerAngle.RotatedBy(MathHelper.ToRadians(45f + ((rotation + rot2) * -1))) * 7, ProjectileID.EyeBeam, 30, 1).timeLeft = 120;
						}
					}
					if (attackTimer >= 300)
					{
						if (noDrugTimer <= 0)
						{
							attackCount = 0;
							attackTimer = 0;
						}
						else
						{
							attackCount = 3;
						}
						rot2 = 0;
						rotation = 0;
						attackTimer = 0;
					}
					break;
			}
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 0; i < 10000; i++)
			{
				Vector2 drawPos = npc.Center + new Vector2(-2, -1 * (i + 12));
				spriteBatch.Draw(mod.GetTexture("NPCs/HypnoKeeper/HypnoKeeperHatPipe"), drawPos - Main.screenPosition, Color.White);
			}
            return true;
        }
    }
}
