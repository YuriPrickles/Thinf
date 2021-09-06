using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Placeables;
using Thinf.Items.Potions;
using Thinf.Items.Weapons;
using Thinf.Items.Weapons.FarmerWeapons;

namespace Thinf.NPCs
{
	internal class Cow : ModNPC
	{
		public bool hasMate = false;
		public bool horny = false;
		public int matingTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cow");
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Bunny);
			npc.width = 48;
			npc.height = 48;
			npc.damage = 0;
			npc.defense = 15;
			npc.lifeMax = 100;
			npc.HitSound = SoundID.NPCHit1;
			if (!Main.dedServ)
			{
				npc.DeathSound = SoundID.NPCDeath1;
			}
			npc.npcSlots = 0.5f;
			npc.noGravity = false;
			npc.aiStyle = 7;
			npc.knockBackResist = 0.1f;
			npc.friendly = true; // We have to add this and CanBeHitByItem/CanBeHitByProjectile because of reasons.
			aiType = NPCID.Bunny;
		}

		public override void NPCLoot()
		{
			//Item.NewItem(npc.Center, ModContent.ItemType<RawChicken>(), Main.rand.Next(3) + 2);
			Item.NewItem(npc.getRect(), ItemID.Leather, Main.rand.Next(4) + 3);
			Item.NewItem(npc.getRect(), ModContent.ItemType<Steak>(), Main.rand.Next(3) + 3);
		}

		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			if (item.type == ModContent.ItemType<Wheat>())
			{
				npc.townNPC = true;
				horny = true;
				npc.life += 30;
			}
			if (item.type == ItemID.EmptyBucket)
            {
				npc.life++;
				item.stack--;
				player.QuickSpawnItem(ModContent.ItemType<MilkBucket>());
            }
		}
		public override void AI()
		{
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			var allNPCs = FindAllNPCs(npc.type, npc.whoAmI);

			if (horny && NPC.CountNPCS(npc.type) > 1)
			{
				if (allNPCs.Count > 0)
				{
					Cow chicken = Main.npc[Main.rand.Next(allNPCs)].modNPC as Cow;
					if (chicken.horny)
					{
						hasMate = true;
						chicken.hasMate = true;
						npc.spriteDirection = -chicken.npc.direction;
						if (npc.Hitbox.Intersects(chicken.npc.Hitbox))
						{
							npc.velocity = Vector2.Zero;
							chicken.npc.velocity = Vector2.Zero;
							npc.aiStyle = -1;
							chicken.npc.aiStyle = -1;
							Main.NewText(matingTimer);
							matingTimer++;
							if (matingTimer >= Thinf.ToTicks(15))
							{
								NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Cow>());
								Main.NewText("Success");
								chicken.matingTimer = 0;
								matingTimer = 0;
								horny = false;
								chicken.horny = false;
								npc.aiStyle = 7;
								chicken.npc.aiStyle = 7;
								hasMate = false;
								chicken.hasMate = false;
							}
						}
						else
						{
							chicken.npc.aiStyle = -1;
							chicken.npc.velocity = chicken.npc.DirectionTo(npc.Center) * 3f;
							npc.aiStyle = -1;
							npc.velocity = npc.DirectionTo(chicken.npc.Center) * 3f;
						}
					}
				}
			}
		}
		public override string GetChat()
		{
			return "moo";
		}
		public static List<int> FindAllNPCs(int type, int whoAmI)
		{
			List<int> count = new List<int>();
			for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC check = Main.npc[i];
				if (check.active && check.type == type && !count.Contains(i) && i != whoAmI)
				{
					count.Add(i);
				}
			}
			return count;
		}
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.43f;
		}


		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				for (int i = 0; i < 6; i++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -2f);
					if (Main.rand.NextBool(2))
					{
						Main.dust[dust].noGravity = true;
						Main.dust[dust].scale = 1.2f * npc.scale;
					}
					else
					{
						Main.dust[dust].scale = 0.7f * npc.scale;
					}
				}
			}
		}
	}
}