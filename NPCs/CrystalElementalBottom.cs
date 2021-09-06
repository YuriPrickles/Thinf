using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.NPCs
{
	public class CrystalElementalBottom : ModNPC
	{
		int crystaltime;
		int var1;
		int darkraisoundslikehehasasorethroat;
		int phase2check = 0;
		float spawnchance;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Elemental");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;  //5 is the flying AI
			npc.lifeMax = 1650;   //boss life
			npc.damage = 62;  //boss damage
			npc.defense = 5;    //boss defense
			npc.knockBackResist = 0f;
			npc.width = 66;
			npc.height = 48;
			npc.value = Item.buyPrice(0, 1, 60, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = new Terraria.Audio.LegacySoundStyle(SoundID.Tink, 0);
			npc.DeathSound = new Terraria.Audio.LegacySoundStyle(SoundID.Shatter, 0);
			npc.buffImmune[BuffID.Confused] = true;
			npc.buffImmune[BuffID.Bleeding] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.netAlways = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Confused, 180);
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.position, ItemID.CrystalShard, (Main.rand.Next(40) + 40) / 2);
			Item.NewItem(npc.position, ItemID.SoulofLight, (Main.rand.Next(12) + 6) / 2);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);


			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, ItemID.CrystalStorm);

			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, mod.ItemType("CrystalGoliath"));

			if (Main.rand.Next(15) == 0)
			{
				Item.NewItem(npc.position, ItemID.HolyArrow, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalBullet, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalDart, 999 / 2);
				Item.NewItem(npc.position, ItemID.HolyArrow, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalBullet, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalDart, 999 / 2);
				Item.NewItem(npc.position, ItemID.HolyArrow, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalBullet, 999 / 2);
				Item.NewItem(npc.position, ItemID.CrystalDart, 999 / 2);
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

			crystaltime++;
			if (crystaltime >= 100 && crystaltime % 25 == 0)
			{
				int projectileSpawnAmount = 4;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i + MathHelper.ToRadians(45);
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();

					// Spawn projectile with the velocity, profit.
					float Speed = 12f;  //projectile speed
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
					int damage = 24;  //projectile damage
					int type = ProjectileID.PinkLaser;  //put your projectile
					Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
					Projectile.NewProjectile(npc.Center, projectileVelocity * 1.2f, type, damage, 0, Main.myPlayer); //code by eldrazi#2385
				}
				if (crystaltime >= 140)
				crystaltime = 0;
			}

			Vector2 moveTo = player.Center + new Vector2(0f, 64f); //This is 200 pixels above the center of the player.
			float speed = 5f; //make this whatever you want
			Vector2 move = moveTo - npc.Center; //this is how much your boss wants to move
			if (move.Length() > 7)
			{
				float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y); //fun with the Pythagorean Theorem
				if (magnitude > speed)
				move *= speed / magnitude; //this adjusts your boss's speed so that its speed is always constant
				npc.velocity = move;
			}
			else
            {
				npc.velocity.Y = 0;
            }

			var1++;
			if (var1 >= 240)
			{
				int projectileSpawnAmount = 2;
				for (int i = 0; i < projectileSpawnAmount; ++i)
				{
					float currentRotation = (MathHelper.TwoPi / projectileSpawnAmount) * i + MathHelper.ToRadians(90);
					Vector2 projectileVelocity = currentRotation.ToRotationVector2();

					// Spawn projectile with the velocity, profit.
					float Speed = 12f;  //projectile speed
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
					int damage = 87;  //projectile damage
					int type = ProjectileID.PinkLaser;  //put your projectile
					Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
					Projectile projectile = Projectile.NewProjectileDirect(npc.Center, projectileVelocity * 1.2f, type, damage, 0, Main.myPlayer); //code by eldrazi#2385
					projectile.timeLeft = 70;
				}

				if (var1 == 300)
					var1 = 0;
			}
		}
	}
}
