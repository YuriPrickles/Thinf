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
	public class CursedFlameElemental : ModNPC
	{
		int Cursedtime;
		int var1;
		int var2;
		int frameNumber = 0;
		int dash1;
		int dash2;
		float spawnchance;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Flame Elemental");
			Main.npcFrameCount[npc.type] = 7;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = 44;  //5 is the flying AI
			npc.lifeMax = 6600;   //boss life
			npc.damage = 42;  //boss damage
			npc.defense = 10;    //boss defense
			npc.knockBackResist = 0.2f;
			npc.width = 92;
			npc.height = 94;
			npc.value = Item.buyPrice(0, 3, 2, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit54;
			npc.DeathSound = SoundID.NPCDeath33;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.Bleeding] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.netAlways = true;
		}
		public override void FindFrame(int frameHeight)
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
				npc.frame.Y = frameNumber * (812 / 7);
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.CursedInferno, 240);
		}

        public override void NPCLoot()
        {
			Item.NewItem(npc.position, ItemID.CursedFlame, Main.rand.Next(40) + 40);
			Item.NewItem(npc.position, ItemID.SoulofNight, Main.rand.Next(12) + 6);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);
			Item.NewItem(npc.position, ItemID.Heart);


			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, ItemID.CursedFlames);

			if (Main.rand.Next(15) == 0)
				Item.NewItem(npc.position, ItemID.ClingerStaff);

			if (Main.rand.Next(15) == 0)
			{
				Item.NewItem(npc.position, ItemID.CursedArrow, 999);
				Item.NewItem(npc.position, ItemID.CursedBullet, 999);
				Item.NewItem(npc.position, ItemID.CursedDart, 999);
				Item.NewItem(npc.position, ItemID.CursedArrow, 999);
				Item.NewItem(npc.position, ItemID.CursedBullet, 999);
				Item.NewItem(npc.position, ItemID.CursedDart, 999);
				Item.NewItem(npc.position, ItemID.CursedArrow, 999);
				Item.NewItem(npc.position, ItemID.CursedBullet, 999);
				Item.NewItem(npc.position, ItemID.CursedDart, 999);
				Item.NewItem(npc.position, ItemID.CursedArrow, 999);
				Item.NewItem(npc.position, ItemID.CursedBullet, 999);
				Item.NewItem(npc.position, ItemID.CursedDart, 999);
			}
		}
		public override void AI()
		{
			npc.rotation = MathHelper.ToDegrees(0);

			npc.TargetClosest(true);
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
			}
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			Cursedtime++;
			npc.spriteDirection = npc.direction;
			if (Cursedtime >= 140 && Cursedtime % 10 == 0)
			{
				float Speed = 2f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 46;  //projectile damage
				int type = ProjectileID.CursedFlameHostile;  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
				Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				if (Cursedtime == 350)
					Cursedtime = 0;
			}

			if (npc.life <= 3300)
            {
				npc.defense = 5;
				npc.damage = 87;
				var2++;
				if (var2 >= 200)
                {
					float Speed = 2f;  //projectile speed
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
					int damage = 32;  //projectile damage
					int type = ProjectileID.EyeFire;  //put your projectile
					Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
					float rotation = MathHelper.ToDegrees(Main.rand.Next(360));
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
					if (var2 == 400)
					var2 = 0;
				}
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			if (spawnInfo.player.ZoneCorrupt && Main.hardMode)
            {
				spawnchance = SpawnCondition.Cavern.Chance * 0.005f;
            }

			return spawnchance;
        }
    }
}
