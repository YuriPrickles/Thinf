using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Projectiles
{
	public class MartianSeed : ModProjectile
	{

		public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Seed);
			aiType = ProjectileID.Seed;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();

			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 180f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
		}
		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 14f)
			{
				vector *= 14f / magnitude;
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[projectile.owner];

			//if it kills a tagged npc, remove the npc.whoAmI from the list of tags
			if (target.life <= damage)
			{
				if (player.GetModPlayer<MyPlayer>().cantMissileTags.Contains(target.whoAmI))
				{
					player.GetModPlayer<MyPlayer>().cantMissileTags.Remove(target.whoAmI);
				}
			}//otherwise, add the tag to the list if it hasnt already
			else if (!player.GetModPlayer<MyPlayer>().cantMissileTags.Contains(target.whoAmI))
			{
				player.GetModPlayer<MyPlayer>().cantMissileTags.Add(target.whoAmI);
			}
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item94, projectile.position);
		}
	}
}
