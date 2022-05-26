using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class BoneLeafProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Leaf");     //The English name of the projectile
        }

        public override void SetDefaults()
        {
            projectile.width = 32;               //The width of projectile hitbox
            projectile.height = 18;              //The height of projectile hitbox
            projectile.friendly = true;         //Can the projectile deal damage to enemies?
            projectile.hostile = false;         //Can the projectile deal damage to the player?
            projectile.magic = false;
            projectile.minion = false;
            projectile.melee = false;
            projectile.thrown = false;
            projectile.ranged = false;
            projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft = 180;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
            projectile.tileCollide = true;          //Can the projectile collide with tiles?
            aiType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            if (Main.mouseRight)
            {
                projectile.Kill();
                int dustSpawnAmount = 64;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(projectile.Center + dustOffset * 16 * 8, DustID.Bone, null, 0, default, 0.4f);
                    dust.noGravity = true;
                }
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.Distance(projectile.Center) <= 16 * 8)
                    {
                        npc.StrikeNPC((int)(projectile.damage * Main.rand.NextFloat(0.85f, 1.15f)), 0, 0);
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustDirect(projectile.position, 20, 20, DustID.Bone, 0, 0, 0, default, Main.rand.NextFloat(0.5f, 2.5f));
            }
            Main.PlaySound(SoundID.NPCHit2);
        }
    }
}
