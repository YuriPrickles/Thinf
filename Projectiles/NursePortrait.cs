using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;
using Thinf.NPCs.HypnoKeeper;
using Thinf.NPCs;

namespace Thinf.Projectiles
{
	public class NursePortrait : ModProjectile
	{
		bool canBeThinged = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The only important painting");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 60;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.rotation += 0.04f;
			int dustSpawnAmount = 32;
			for (int i = 0; i < dustSpawnAmount; ++i)
			{
				float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
				Vector2 dustOffset = currentRotation.ToRotationVector2();
				Dust dust = Dust.NewDustPerfect(projectile.Center + dustOffset * 60, DustID.Blood, null, 0, default, 1.5f);
				dust.noGravity = true;
			}
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active && player.Distance(projectile.Center) <= 60)
				{
					if (!canBeThinged)
					{
						TheThing();
						canBeThinged = true;
					}
					
				}
			}
			
		}
		public void TheThing()
		{
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile proj = Main.projectile[k];

                if (proj.active && proj.hostile)
                {
                    proj.active = false;
                }
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active)
                {
                    player.ClearBuff(ModContent.BuffType<Hallucinating>());
                    for (int j = 0; j < Main.maxNPCs; j++)
                    {
                        if (NPC.AnyNPCs(ModContent.NPCType<HypnoKeeper>()))
                        {
                            if (Main.npc[j].active && Main.npc[j].type == ModContent.NPCType<HypnoKeeper>())
                            {
                                NPC npc = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<HypnoKeeper>())];
                                HypnoKeeper hypno = npc.modNPC as HypnoKeeper;
                                hypno.noDrugs = false;
                                hypno.noDrugTimer = Thinf.ToTicks(30);
                            }
                        }
                        if (Main.npc[j].type == ModContent.NPCType<MindDemon>())
                        {
                            Main.npc[j].active = false;
                        }
                    }
					Main.PlaySound(SoundID.Item4);
				}
			}
			projectile.Kill();
			int rand = Main.rand.Next(4);
			switch (rand)
			{
				case 0:
					Main.NewText("Normall Pills taken");
					break;
				case 1:
					Main.NewText("Everything will be fine");
					break;
				case 2:
					Main.NewText("Your mind is cleansed");
					break;
				case 3:
					Main.NewText("Good Luck");
					break;
			}
		}
		public override void Kill(int timeLeft)
		{
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
