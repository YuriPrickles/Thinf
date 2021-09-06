using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.Cacterus;

namespace Thinf.Projectiles
{
    public class CacterusSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cacterus Spike");
        }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 16;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 9999999;
            projectile.timeLeft = 600;
            projectile.aiStyle = 29;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode || Main.rand.NextBool())
            {
                player.AddBuff(BuffID.Bleeding, 600, true);
            }
            //if (NPC.AnyNPCs(ModContent.NPCType<Cacterus>()))
            //{
            //    if (Main.rand.Next(4) == 0)
            //        Main.NewText("Sorry if that hurts too much!");
            //    if (Main.rand.Next(4) == 1)
            //        Main.NewText("Your dodging sure does need some practice!");
            //    if (Main.rand.Next(4) == 2)
            //        Main.NewText("You can do it!");
            //    if (Main.rand.Next(4) == 3)
            //        Main.NewText("Try circling around me!");
            //}
        }
    }
}