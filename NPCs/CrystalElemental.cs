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
	public class CrystalElemental : ModNPC
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
			npc.aiStyle = 44;  //5 is the flying AI
			npc.lifeMax = 6600;   //boss life
			npc.damage = 42;  //boss damage
			npc.defense = 10;    //boss defense
			npc.knockBackResist = 0.2f;
			npc.width = 66;
			npc.height = 66;
			npc.value = Item.buyPrice(0, 3, 2, 0);
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
			Item.NewItem(npc.position, mod.ItemType("SusPlaceholder"));
			Main.NewText("how did you even do that");
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

			crystaltime++;
			if (crystaltime >= 100 && crystaltime % 4 == 0)
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
					Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, projectileVelocity * 1.2f, type, damage, 0)];
					projectile.hostile = true;
				}
					if (crystaltime == 140)
					crystaltime = 0;
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
				for (int u = 0; u > 360; ++u)
				{
					npc.rotation++;
				}
				var1 = 0;
			}

			if (npc.life <= 3300)
            {
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("CrystalElementalTop"));
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("CrystalElementalBottom"));
				npc.active = false;
			}
		}

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
			if (damage >= npc.life)
            {
				damage = 1;
            }
			return true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.player.ZoneHoly && Main.hardMode)
			{
				spawnchance = SpawnCondition.Cavern.Chance * 0.005f;
			}

			return spawnchance;
		}
	}
}
