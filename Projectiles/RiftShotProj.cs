using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Terraria.Audio;
using System.Collections.Generic;

namespace Thinf.Projectiles
{
	public class RiftShotProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rift Shot");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 16;               //The width of projectile hitbox
			projectile.height = 2;              //The height of projectile hitbox
			
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			projectile.light = 0.4f;            //How much light emit around the projectile
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.tileCollide = false;
			var allNPCs = FindAllNPCs();
			if (allNPCs.Count > 1 )
            {
				LoopIfNPCIsTheSame:
				NPC tpPos = Main.npc[Main.rand.Next(allNPCs)];
				if (tpPos.whoAmI != target.whoAmI)
				{
					projectile.Center = tpPos.Center - (new Vector2(75 * tpPos.direction, 0));
				}
				else
                {
					goto LoopIfNPCIsTheSame;
                }

				projectile.velocity = projectile.DirectionTo(tpPos.Center) * 9;
                for (int i = 0; i < 50; i++)
                {
					Dust.NewDust(projectile.Center, 40, 40, DustID.MagicMirror, 0, 0, 0, default, 1.4f);
				}
			}
		}
		public static List<int> FindAllNPCs()
		{
			List<int> npcCount = new List<int>();
			for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC check = Main.npc[i];
				if (check.active && !check.friendly && !check.townNPC && !check.dontTakeDamage && check.type != NPCID.TargetDummy)
				{
					npcCount.Add(i);
				}
			}
			return npcCount;
		}
	}
}
