
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class BeeRocket : ModProjectile
	{

		public override string Texture => "Terraria/Projectile_" + ProjectileID.RocketIV;
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GrenadeIII);
			aiType = ProjectileID.GrenadeIII;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.timeLeft = Thinf.ToTicks(5);
			projectile.penetrate = 1;
		}
		
        public override void AI()
        {
			projectile.velocity.Y += 0.3f;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item14, projectile.Center);
			projectile.Kill();
            return true;
        }
        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item14, projectile.Center);
			NPC npc = Main.npc[NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, NPCID.Bee)];
            npc.GivenName = "Political Bee";
			npc.life = 500;
			npc.lifeMax = 500;
			npc.knockBackResist = 0;
			npc.damage = 50;
		}
    }
}