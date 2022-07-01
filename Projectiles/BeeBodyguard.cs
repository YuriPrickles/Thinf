using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Projectiles
{
	public class BeeBodyguard : Minion
	{
		string type = "Gatling";
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
            projectile.minionSlots = 1;
			projectile.height = 18;
			projectile.width = 22;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
		}

		protected float detectionRange = 900f;
		protected float idleVelocity = 7f;
		protected float shootCooldown = 0;

		public static float Rotation = 0f; //Used for summon rotation

		private int AiState = 0; //0 = idle (not attacking) //1 = found target (start attack) //2 = attacking found target
		private int ID = 0;
		private Vector2 targetPosition; //Found target's position. Default is around player
		private Vector2 targetPositionForProj;

		private float AttackCooldown { get => projectile.ai[0]; set => projectile.ai[0] = value; }
		private float timer = 0;
		public override void OnCreated()
		{
			int typerand = Main.rand.Next(3);
			switch (typerand)
			{
				case 0:
					type = "Gatling";
					break;
				case 1:
					type = "Rocket";
					break;
				case 2:
					type = "Shotgun";
					break;
			}
			ID = Main.player[projectile.owner].ownedProjectileCounts[projectile.type] + 1;
		}

		public override void Behavior()
		{
			if (type == "Gatling")
			{
				shootCooldown = 50;
			}
			if (type == "Rocket")
			{
				shootCooldown = 120;
			}
			if (type == "Shotgun")
			{
				shootCooldown = 90;
			}
			int frameSpeed = 5;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			Player player = Main.player[projectile.owner];
			int num = player.ownedProjectileCounts[projectile.type];
			if (num < 1)
				num = 1;
			if (projectile.Distance(player.Center) >= 1700)
			{
				targetPosition.X = (player.Center + new Vector2(23 * projectile.minionPos * -player.direction, -45)).X;
				targetPosition.Y = (player.Center + new Vector2(23 * projectile.minionPos * -player.direction, -45)).Y;
			}
			targetPosition.X = (player.Center + new Vector2(23 * projectile.minionPos * -player.direction, -45)).X;
			targetPosition.Y = (player.Center + new Vector2(23 * projectile.minionPos * -player.direction, -45)).Y;
			bool hasTarget = false;
			float distance = detectionRange * 1.2f;
			for (int i = 0; i < Main.maxNPCs; ++i) //Basic find NPC code
			{
				NPC target = Main.npc[i];
				if (player.Distance(target.position) < distance && target.active && target.CanBeChasedBy() && target.lifeMax > 10 && !target.friendly && target.type != NPCID.TargetDummy && !target.immortal && Collision.CanHitLine(projectile.Center, 4, 4, target.position, 4, 4))
				{
					hasTarget = true;
					targetPositionForProj = target.Center;
				}
			}
			Vector2 direction = projectile.DirectionTo(targetPosition); //Find direction to position
			if (hasTarget) //If the projectile actually has a target
				AiState = 1; //Set to found NPC
			else //If projectile does not have a target
				AiState = 0; //Set to idle
			if (AiState != 2) //If projectile is not attacking
			{
				if (AiState == 1) //if projectile has a target
				{
					AiState = 2;
				}
			}
			if (AiState == 2)
			{
					AttackCooldown--;
				if (type == "Gatling")
				{
					if (AttackCooldown <= 0)
					{
						Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(targetPositionForProj) * 7f, ProjectileID.VenomBullet, (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner);
						Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(targetPositionForProj) * 5f, ProjectileID.VenomBullet, (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner);
						Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(targetPositionForProj) * 3f, ProjectileID.VenomBullet, (int)(projectile.damage * 0.8f), projectile.knockBack, projectile.owner);
						AttackCooldown = shootCooldown + Main.rand.Next(30, 120);
					}
				}
				if (type == "Rocket")
				{
					if (AttackCooldown <= 0)
					{
						Projectile.NewProjectileDirect(projectile.Center, projectile.DirectionTo(targetPositionForProj) * 3f, ProjectileID.RocketIII, projectile.damage * 5, projectile.knockBack, projectile.owner).hostile = false;
						if (AttackCooldown <= 30)
						{
							AttackCooldown = shootCooldown + Main.rand.Next(30, 120);
						}
					}
				}
				if (type == "Shotgun")
				{
					if (AttackCooldown <= 0)
					{
						Projectile.NewProjectile(projectile.Center, (projectile.DirectionTo(targetPositionForProj) * 4f).RotatedByRandom(MathHelper.ToRadians(8)), ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner);
						Projectile.NewProjectile(projectile.Center, (projectile.DirectionTo(targetPositionForProj) * 4f).RotatedByRandom(MathHelper.ToRadians(8)), ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner);
						Projectile.NewProjectile(projectile.Center, (projectile.DirectionTo(targetPositionForProj) * 4f).RotatedByRandom(MathHelper.ToRadians(8)), ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner);
						Projectile.NewProjectile(projectile.Center, (projectile.DirectionTo(targetPositionForProj) * 4f).RotatedByRandom(MathHelper.ToRadians(8)), ProjectileID.Bullet, projectile.damage, projectile.knockBack, projectile.owner);
						AttackCooldown = shootCooldown + Main.rand.Next(30, 120);
					}
				}
			}

			if (projectile.Distance(targetPosition) <= detectionRange * 1.2f) //if projectile is within a certain range
			{
				projectile.velocity = Vector2.Lerp(direction * idleVelocity * 0.8f, direction * 1.5f, idleVelocity / Vector2.Distance(projectile.position, targetPosition));
				//Vector2.Lerp is like a slider.
				//You input 2 Vector2 fields, and give it a float. 0f is all the way left (the first one), 1f is all the way right (the second one).
			}
		}
		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (player.dead)
			{
				modPlayer.beeBodyguardMinion = false;
			}
			if (modPlayer.beeBodyguardMinion)  // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
			{
				projectile.timeLeft = 2;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}
	}

	public class BeeBodyguardStaff : ModItem
	{
		public override string Texture => "Thinf/Items/Weapons/BeeBodyguardStaff";
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a Bee Bodyguard that goes pew pew bam bam");
		}
		public override void SetDefaults()
		{
			item.shoot = ModContent.ProjectileType<BeeBodyguard>();
			item.mana = 10;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.damage = 60;
			item.knockBack = 1f;
			item.width = 24;
			item.height = 24;
			item.summon = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(BuffType<BeeBodyguardBuff>(), 18000);
			position = Main.MouseWorld;
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FlintlockPistol);
			recipe.AddIngredient(ItemType<PoliticalPower>(), 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}