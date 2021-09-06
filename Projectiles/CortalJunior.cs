using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;

namespace Thinf.Projectiles
{
    /*
	 * This minion shows a few mandatory things that make it behave properly. 
	 * Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	 * If the player targets a certain NPC with right-click, it will fly through tiles to it
	 * If it isn't attacking, it will float near the player with minimal movement
	 */
    public class CortalJunior : ModProjectile
	{
		int projrand = 0;
		int type;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cortal Junior");

			// These below are needed for a minion
			// Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
		}

		public sealed override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			// Makes the minion go through tiles freely
			projectile.tileCollide = false;

			// These below are needed for a minion weapon
			// Only controls if it deals damage to enemies on contact (more on that later)
			projectile.friendly = true;
			// Only determines the damage type
			projectile.minion = true;
			// Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			projectile.minionSlots = 0f;
			// Needed so the minion doesn't despawn on collision with enemies or tiles
			projectile.penetrate = -1;
		}

		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles()
		{
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage()
		{
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.setBonus == "")
			{
				player.ClearBuff(mod.BuffType("CortalJunior"));
			}
			if (player.HasBuff(mod.BuffType("CortalJunior")))
			{
				projectile.timeLeft = 2;
			}
			#endregion

			projectile.spriteDirection = player.direction;

			projectile.position = new Vector2(player.position.X, player.position.Y - 75);

			projrand = Main.rand.Next(10);

			if (projrand == 0)
				type = ProjectileID.BallofFrost;
			if (projrand == 1)
				type = ProjectileID.PurpleLaser;
			if (projrand == 2)
				type = ProjectileID.Seed;
			if (projrand == 3)
				type = ProjectileID.DemonScythe;
			if (projrand == 4)
				type = ProjectileID.FallingStar;
			if (projrand == 5)
				type = ProjectileID.Bullet;
			if (projrand == 6)
				type = ProjectileID.ImpFireball;
			if (projrand == 7)
				type = ProjectileID.MagicDagger;
			if (projrand == 8)
				type = ProjectileID.EnchantedBeam;
			if (projrand == 9)
				type = ProjectileID.HornetStinger;

			timer++;
			if (timer >= 120)
			{
				for (int i = 0; i < 200; i++)
				{
					NPC target = Main.npc[i];
					float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
					float shootToY = target.position.Y - projectile.Center.Y;
					float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
					if (distance < 480f && !target.friendly && target.active)
					{
						if (projectile.ai[0] > 10f)
						{
							distance = 3f / distance;
							shootToX *= distance * 5;
							shootToY *= distance * 5;
							int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f); //mod.ProjectileType("Laser") is the projectile it shoots, change it to what you like
							Main.projectile[proj].timeLeft = 300;
							Main.projectile[proj].netUpdate = true;
							projectile.netUpdate = true;
							Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
							projectile.ai[0] = -50f;
							if (timer >= 240)
							timer = 0;
						}
					}
				}

				projectile.ai[0] += 1f;
			}
			// So it will lean slightly towards the direction it's moving
			projectile.rotation = projectile.velocity.X * 0.05f;
		}
	}
}