using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
	public class Jerry : ModProjectile
	{
		int mockTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabySlime);
			projectile.width = 22;
			projectile.height = 30;
			aiType = ProjectileID.BabySlime;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.slime = false; // Relic from aiType
			return true;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<JerryOurLordAndSavior>());
			}
			if (player.HasBuff(ModContent.BuffType<JerryOurLordAndSavior>()))
			{
				projectile.timeLeft = 2;
			}
			projectile.alpha += Main.rand.Next(4, 15) * 2;
			if (projectile.alpha >= 255)
			{
				projectile.alpha = 0;
			}

			mockTimer++;
			if (mockTimer == Thinf.ToTicks(27))
			{
				int textrand = Main.rand.Next(8);
                switch (textrand)
                {
					case 0:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "You could have had a better life if you joined me!")].lifeTime = 420;
						break;
					case 1:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "I'm not giving up on the propechy yet! Just you wait!")].lifeTime = 420;
						break;
					case 2:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "You are a coward, rejecting the help of a god like me!")].lifeTime = 420;
						break;
					case 3:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "Ratio + take this L + heretic + you are a nerd")].lifeTime = 420;
						break;
					case 4:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, $"Only {player.HeldItem.damage} damage? PATHETIC! I can do better than that!")].lifeTime = 420;
						break;
					case 5:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, $"Only {player.statDefense} defense? PATHETIC! One touch from me and you'd die! HAH!")].lifeTime = 420;
						break;
					case 6:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "I only lost because of lag, and because I was holding back my TRUE EPIC POWER!!!")].lifeTime = 420;
						break;
					case 7:
						Main.combatText[CombatText.NewText(projectile.getRect(), Color.OrangeRed, "You'll never succeed without me!")].lifeTime = 420;
						break;
					default:
                        break;
                }
				mockTimer = Main.rand.Next(-Thinf.ToTicks(9), 0);
			}
		}
	}
}