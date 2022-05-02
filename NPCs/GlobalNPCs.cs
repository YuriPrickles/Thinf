using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Placeables;
using Thinf.NPCs.PlayerDrones;
using Thinf.NPCs.PrimeMinister;
using Thinf.NPCs.TownNPCs;
using static Terraria.ModLoader.ModContent;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs
{
	public class PositionResetter : ModPlayer
	{
		public Vector2 resetPos;
		//other instance or somethin awa
	}
	public class GlobalNPCs : GlobalNPC
	{
		Vector2 playerPos;
		public override bool InstancePerEntity => true;
		public static bool hasPerfectPancake = false;
		public bool Shocked;
		public bool Disintegrating;
		public bool ItBurns;
		public bool paralyzed;
		public bool Manapush;

		public override void ResetEffects(NPC npc)
		{
			ItBurns = false;
			Disintegrating = false;
			Shocked = false;
			paralyzed = false;
		}

		public override bool PreAI(NPC npc)
		{
			Player player = Main.player[npc.target];
			DroneControls dronePlayer = player.GetModPlayer<DroneControls>();
			if (player.active && !player.dead)
			{
				if (dronePlayer.playerIsControllingDrone)
				{
					player.Center = Main.npc[dronePlayer.droneControlled].Center;
				}
			}
			if (hasPerfectPancake)
			{
				if (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon || npc.type == NPCID.RedDevil || npc.type == NPCID.RedDevil || npc.type == NPCID.Hellbat)
				{ // yes this is a helltaker reference
					return false;
				}
				if (npc.type == NPCID.FireImp)
				{
					return false;
				}
				if (npc.type == NPCID.BurningSphere)
				{
					return false;
				}
			}
			else
			{
				if (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon || npc.type == NPCID.RedDevil || npc.type == NPCID.RedDevil || npc.type == NPCID.Hellbat)
				{
					return true;
				}
				if (npc.type == NPCID.FireImp)
				{
					return true;
				}
				if (npc.type == NPCID.BurningSphere)
				{
					return true;
				}
			}
			return base.PreAI(npc);
		}
		public override void PostAI(NPC npc)
		{
			Player player = Main.player[npc.target];
			DroneControls dronePlayer = player.GetModPlayer<DroneControls>();
			if (player.active && !player.dead)
			{
				PositionResetter positionResetter = player.GetModPlayer<PositionResetter>();
				if (dronePlayer.playerIsControllingDrone)
				{
					player.Center = positionResetter.resetPos;
					dronePlayer.hasUsedDrone = false;
				}
			}
		}
		public override void AI(NPC npc)
		{
			if (paralyzed)
			{
				npc.velocity = Vector2.Zero;
			}
			if (hasPerfectPancake)
			{
				if (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon || npc.type == NPCID.RedDevil || npc.type == NPCID.RedDevil || npc.type == NPCID.Hellbat)
				{
					npc.AddBuff(BuffID.Lovestruck, 4);
					npc.friendly = true;
					npc.damage = 0;
					npc.aiStyle = 24;
				}
				if (npc.type == NPCID.FireImp)
				{
					npc.AddBuff(BuffID.Lovestruck, 4);
					npc.damage = 0;
					npc.friendly = true;
				}
				if (npc.type == NPCID.BurningSphere)
				{
					npc.active = false;
				}
			}
			if (NPC.AnyNPCs(NPCID.MoonLordCore) && (npc.type == NPCID.Stylist || npc.type == NPCType<StarPrince.StarPrince>()))
			{
				npc.dontTakeDamage = true;
			}
			else
			{
				if (npc.type == NPCID.Stylist || npc.type == NPCType<StarPrince.StarPrince>())
				{
					npc.dontTakeDamage = false;
				}
			}
		}
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			//if (player.GetModPlayer<MyPlayer>().coreDeadBuff)
			//{
			//    spawnRate = 0;
			//    maxSpawns = 0;
			//}
			player.GetModPlayer<MyPlayer>().spinachStrength = false;
			player.GetModPlayer<MyPlayer>().roseDefense = false;
			if (FrenzyMode)
			{
				spawnRate /= 3;
				maxSpawns = 900000;
			}
		}
		public override void SetDefaults(NPC npc)
		{
			#region Chest Wasteland NPC Buffs (Post-Soul Keys)
			if (npc.type == mod.NPCType("WastelandMimic") && downedSoulKeys)
			{
				npc.life = 2500;
				npc.lifeMax = 2500;
				npc.damage = 65;
				npc.defense = 26;
			}

			if (npc.type == mod.NPCType("BarrelMimic") && downedSoulKeys)
			{
				npc.life = 3800;
				npc.lifeMax = 3800;
				npc.damage = 132;
				npc.defense = 35;
			}

			if (npc.type == mod.NPCType("MoneyTroughMimic") && downedSoulKeys)
			{
				npc.life = 1500;
				npc.lifeMax = 1500;
				npc.damage = 60;
				npc.defense = 20;
			}

			if (npc.type == mod.NPCType("CopperMimic") && downedSoulKeys)
			{
				npc.life = 500;
				npc.lifeMax = 500;
				npc.damage = 32;
				npc.defense = 14;
			}

			if (npc.type == mod.NPCType("SilverMimic") && downedSoulKeys)
			{
				npc.life = 1250;
				npc.lifeMax = 1250;
				npc.damage = 63;
				npc.defense = 20;
			}

			if (npc.type == mod.NPCType("GoldMimic") && downedSoulKeys)
			{
				npc.life = 2100;
				npc.lifeMax = 2100;
				npc.damage = 72;
				npc.defense = 24;
			}
			#endregion

			if (npc.type == NPCID.QueenBee)
			{
				npc.GivenName = "Queen Bee's Daughter";
			}
			if (FrenzyMode)
			{
				npc.life *= 2;
				npc.lifeMax *= 2;
				npc.damage *= 2;
				npc.lifeRegen = 4;
			}

			if (NPC.downedMoonlord && npc.type == NPCID.MeteorHead)
			{
				npc.lifeMax = 5000;
				npc.life = 5000;
				npc.damage = 70;
				npc.defense = 45;
			}
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (Disintegrating)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 4)
				{
					damage = 4;
				}
			}


			if (ItBurns)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 500;
				if (damage < 10)
				{
					damage = 10;
				}
			}

			if (Shocked)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}

				if (npc.wet)
				{
					npc.lifeRegen -= 18;
					if (damage < 1)
					{
						damage = 1;
					}
				}
				else
				{
					npc.lifeRegen -= 9;
					if (damage < 1)
					{
						damage = 1;
					}
				}
				npc.stepSpeed -= 10;
			}
		}

		public override bool? CanHitNPC(NPC npc, NPC target)
		{
			if ((npc.type == NPCID.Bee || npc.type == NPCID.BeeSmall) && (target.type == NPCType<PrimeMinisterTank>() || target.type == NPCType<PrimeMinisterCopter>() || target.type == NPCType<PrimeMinister.PrimeMinister>() || target.type == NPCID.QueenBee || target.type == NPCType<Beekeeper>()))
			{
				return false;
			}
			return null;
		}
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.WitchDoctor)
			{
				if (NPC.downedBoss1)
				{
					shop.item[nextSlot].SetDefaults(ItemID.FlowerBoots);
					shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50, 0);
					nextSlot++;
				}
				if (WorldGen.crimson)
				{
					shop.item[nextSlot].SetDefaults(ItemType<ThingMadeOutOfBlood>());
					shop.item[nextSlot].shopCustomPrice = 100000;
					nextSlot++;
				}
				if (!WorldGen.crimson)
				{
					shop.item[nextSlot].SetDefaults(ItemType<EyeballOnAPlate>());
					shop.item[nextSlot].shopCustomPrice = 100000;
				}
			}
			if (type == NPCID.Dryad && NPC.downedBoss1)
			{
				shop.item[nextSlot].SetDefaults(ItemType<TomatoSeeds>());
				shop.item[nextSlot].shopCustomPrice = 5000;
				nextSlot++;
				shop.item[nextSlot].SetDefaults(ItemType<WheatSeeds>());
				shop.item[nextSlot].shopCustomPrice = 15000;
				nextSlot++;
			}

			if (type == NPCID.Merchant && NPC.downedBoss1)
			{
				shop.item[nextSlot].SetDefaults(ItemID.Seed);
				shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 5, 0);
				nextSlot++;
			}
		}
	}
}
