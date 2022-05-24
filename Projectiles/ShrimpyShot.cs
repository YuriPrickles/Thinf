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
	public class ShrimpyShot : ModProjectile
	{
		int npcToIgnore = -1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shrimpy Shot");     //The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 14;               //The width of projectile hitbox
			projectile.height = 2;              //The height of projectile hitbox
			
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			projectile.light = 0.1f;            //How much light emit around the projectile
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < Main.maxNPCs; i++)
            {
				NPC npc = Main.npc[i];

				if (npc.active && npc.whoAmI != npcToIgnore && !npc.friendly && npc.Distance(projectile.Center) <= 80)
                {
					npc.StrikeNPC(projectile.damage / 3, projectile.knockBack, 0);
                }
            }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			npcToIgnore = target.whoAmI;
		}
	}
}
