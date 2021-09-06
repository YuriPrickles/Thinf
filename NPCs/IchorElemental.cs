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
	public class IchorElemental : ModNPC
	{
		int ichortime;
		int var1;
		int darkraisoundslikehehasasorethroat;
		int phase2check = 0;
		float spawnchance;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Elemental");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 44;  //5 is the flying AI
			npc.lifeMax = 6600;   //boss life
			npc.damage = 42;  //boss damage
			npc.defense = 10;    //boss defense
			npc.knockBackResist = 0.2f;
			npc.width = 100;
			npc.height = 92;
			npc.value = Item.buyPrice(0, 3, 2, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit13;
			npc.DeathSound = SoundID.NPCDeath33;
			npc.buffImmune[BuffID.Ichor] = true;
			npc.buffImmune[BuffID.Bleeding] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.netAlways = true;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Ichor, 600);

			if (phase2check == 0)
			npc.alpha = 255;

			darkraisoundslikehehasasorethroat = Main.rand.Next(4);
			if (darkraisoundslikehehasasorethroat == 0)
			{
				npc.position = target.position + new Vector2(75, 75);
			}

			if (darkraisoundslikehehasasorethroat == 1)
			{
				npc.position = target.position - new Vector2(225, 225);
			}

			if (darkraisoundslikehehasasorethroat == 2)
			{
				npc.position = target.position + new Vector2(-225, 75);
			}

			if (darkraisoundslikehehasasorethroat == 3)
			{
				npc.position = target.position + new Vector2(-225, 225);
			}
		}

        public override void NPCLoot()
        {
			Item.NewItem(npc.position, ItemID.Ichor, Main.rand.Next(40) + 40);
			Item.NewItem(npc.position, ItemID.SoulofNight, Main.rand.Next(12) + 6);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);


			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, ItemID.GoldenShower);

			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, ItemID.Bladetongue);

			if (Main.rand.Next(15) == 0)
			{
				Item.NewItem(npc.position, ItemID.IchorArrow, 999);
				Item.NewItem(npc.position, ItemID.IchorBullet, 999);
				Item.NewItem(npc.position, ItemID.IchorDart, 999);
				Item.NewItem(npc.position, ItemID.IchorArrow, 999);
				Item.NewItem(npc.position, ItemID.IchorBullet, 999);
				Item.NewItem(npc.position, ItemID.IchorDart, 999);
				Item.NewItem(npc.position, ItemID.IchorArrow, 999);
				Item.NewItem(npc.position, ItemID.IchorBullet, 999);
				Item.NewItem(npc.position, ItemID.IchorDart, 999);
				Item.NewItem(npc.position, ItemID.IchorArrow, 999);
				Item.NewItem(npc.position, ItemID.IchorBullet, 999);
				Item.NewItem(npc.position, ItemID.IchorDart, 999);
			}
		}
		public override void AI()
		{
			if (npc.alpha > 0)
			{
				npc.alpha--;
			}

			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;

			ichortime++;
			if (ichortime >= 100 && ichortime % 10 == 0)
			{
				float Speed = 12f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 32;  //projectile damage
				int type = ProjectileID.GoldenShowerHostile;  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
				Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				if (ichortime == 140)
					ichortime = 0;
			}

			var1++;
			if (var1 >= 200)
			{
				Vector2 moveTo = player.Center;
				float speed = 10f;
				Vector2 move = moveTo - npc.Center;
				float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
				move *= speed / magnitude;
				npc.velocity = move;
				for (int u = 0; u > 50; ++u)
				{

				}
				var1 = 0;
			}

			if (npc.life <= 3300)
            {
				player.AddBuff(BuffID.Blackout, 2);
				phase2check = 1;
				npc.defense = 20;
				npc.damage = 64;
				npc.alpha = 127;
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(npc.position, npc.width, npc.height, 170, 0f, 0f, 100, new Color(255, 255, 255), 1.25f)];
				dust.noGravity = true;
				dust.fadeIn = 100f;
				dust.noLight = true;
				Projectile proj = Main.projectile[Projectile.NewProjectile(0, 0, 0, 0, ProjectileID.GoldenShowerHostile, 42, 0f, 0, 0, 0)];
				proj.alpha = 255;
				proj.aiStyle = 0;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.player.ZoneCrimson && Main.hardMode)
			{
				spawnchance = SpawnCondition.Cavern.Chance * 0.005f;
			}

			return spawnchance;
		}
	}
}
