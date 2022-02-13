using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Weapons;
using Thinf.Items.Weapons.FarmerWeapons;
using Thinf.Projectiles;

namespace Thinf.NPCs.SmellyBill
{
	public class SmellyBill : ModNPC
	{
		bool doBlazinBlast = false;
		bool drawPentagram = false;
		int phaseTwoRitualTimer = 0;
		int phaseCount = 2;
		int phaseZeroDashTimer = 0;
		int phaseZeroCount = 0;
		int phaseOneBleatTimer = 0;
		public override void SetStaticDefaults()
		{

		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 30000;
			npc.damage = 42;
			npc.defense = 22;
			npc.knockBackResist = 0f;
			npc.width = 118;
			npc.height = 72;
			npc.value = Item.buyPrice(0, 10, 0, 0);
			npc.npcSlots = 1f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			int loot = Main.rand.Next(5);
			switch (loot)
			{
				case 0:
					Item.NewItem(npc.getRect(), ModContent.ItemType<Phonogun>());
					break;
				case 1:
					Item.NewItem(npc.getRect(), ModContent.ItemType<SmellySling>());
					break;
				case 2:
					Item.NewItem(npc.getRect(), ModContent.ItemType<DevilsMatchstick>());
					break;
				case 3:
					Item.NewItem(npc.getRect(), ModContent.ItemType<Goatarang>());
					break;
				case 4:
					Item.NewItem(npc.getRect(), ModContent.ItemType<GoahstStaff>());
					break;
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

			if (phaseZeroDashTimer < 360) npc.spriteDirection = npc.direction;

			if (phaseCount == 0)
			{
				if (phaseZeroCount >= 5)
				{
					phaseCount = 1;
					phaseZeroDashTimer = 0;
					phaseZeroCount = 0;
				}
				phaseZeroDashTimer++;
				if (phaseZeroDashTimer >= 360)
				{
					if (phaseZeroDashTimer == 0)
					{
						Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GoatBleat"));
					}
					if (phaseZeroDashTimer % 7 == 0)
					{
						Projectile.NewProjectileDirect(npc.Center, Vector2.Normalize(player.Center - npc.Center), ProjectileID.DemonSickle, 20, 12);
					}
					if (phaseZeroDashTimer < 362)
					{
						npc.noTileCollide = true;
						npc.noGravity = true;
						npc.velocity.X = npc.DirectionTo(player.Center).X * 16;
					}
					else
					{
						npc.noGravity = false;
						npc.noTileCollide = false;
					}
					npc.velocity.Y = 0;
					if (phaseZeroDashTimer == 420)
					{
						phaseZeroCount++;
						phaseZeroDashTimer = 0;
					}
				}
				else if (npc.velocity.Y == 0)
				{
					BillHop(4);
				}
			}

			if (phaseCount == 1)
			{
				npc.noGravity = true;
				npc.velocity = npc.DirectionTo(player.Center + new Vector2(-200, -200 + Main.rand.Next(-10, 10)));

				phaseOneBleatTimer++;
				if (phaseOneBleatTimer >= 20 && phaseOneBleatTimer % 20 == 0)
				{
					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GoatBleat"));
					Projectile.NewProjectileDirect(npc.Center, Vector2.Normalize(player.Center - npc.Center) * Main.rand.Next(4, 8), ModContent.ProjectileType<BillBleat>(), 35, 12);
					if (phaseOneBleatTimer >= 260)
					{
						phaseCount = 2;
						phaseOneBleatTimer = 0;
					}
				}
			}
			if (phaseCount == 2)
			{
				phaseTwoRitualTimer++;
				if (phaseTwoRitualTimer >= 240)
				{
					drawPentagram = false;
					if (!NPC.AnyNPCs(ModContent.NPCType<Goahst>()))
					{
						doBlazinBlast = false;
						int SpawnAmount = 5;
						for (int i = 0; i < SpawnAmount; ++i)
						{
							float currentRotation = (MathHelper.TwoPi / SpawnAmount) * i;
							Vector2 playerOffset = currentRotation.ToRotationVector2();
							NPC.NewNPC((int)(npc.Center + playerOffset * 50f).X, (int)(npc.Center + playerOffset * 50f).Y, ModContent.NPCType<Goahst>());
						}
					}
					else if (doBlazinBlast)
					{
						if (Main.time % 8 == 0)
						Projectile.NewProjectileDirect(npc.Center, Vector2.Normalize(player.Center - npc.Center) * 7, ProjectileID.Fireball, 50, 3);
					}
					if (phaseTwoRitualTimer >= 300)
					{
						npc.noGravity = false;
						doBlazinBlast = true;
						phaseTwoRitualTimer = 0;
						phaseCount = 0;
					}
				}
				else
				{
					drawPentagram = true;
					npc.velocity = Vector2.Zero;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (drawPentagram) spriteBatch.Draw(mod.GetTexture("NPCs/SmellyBill/Pentagram"), npc.Center - Main.screenPosition, null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 0);
			return true;
		}

		public void BillHop(int horizontalSpeed)
		{
			Player player = Main.player[npc.target];
			npc.velocity.X = horizontalSpeed * npc.direction;
			npc.velocity.Y = -5 + npc.DirectionTo(player.Center).Y * 12;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Blackout, 120);
		}
	}
}
