using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
    public class ThornApartProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorn Apart");
        }

        public override void SetDefaults()
        {
            projectile.width = 128;
            projectile.height = 128;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.alpha = 0;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.scale = 1;
            projectile.light = 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MyPlayer>().thornApartCharge < 100)
                player.GetModPlayer<MyPlayer>().thornApartCharge++;
            else
            {
                if (player.GetModPlayer<MyPlayer>().thornApartShots < 5)
                player.GetModPlayer<MyPlayer>().thornApartShots++;
                player.GetModPlayer<MyPlayer>().thornApartCharge = 0;
            }

        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            //// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            //Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //projectile.direction = projOwner.direction;
            //projectile.Center = projOwner.Center;
            //projOwner.heldProj = projectile.whoAmI;
            //         projOwner.itemTime = projOwner.itemAnimation;
            //         projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            //         projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
            //         projectile.rotation += 0.5f * projOwner.direction;
            //// When we reach the end of the animation, we can kill the spear projectile
            //if (projOwner.itemAnimation == 0)
            //{
            //	projectile.Kill();
            //}
            //// Offset by 90 degrees here
            //if (projectile.spriteDirection == -1)
            //{
            //	projectile.rotation -= MathHelper.ToRadians(90f);
            //}


            Player projOwner = Main.player[projectile.owner];
            float num = 48;
            float num2 = 2;
            float quarterPI = -(float)Math.PI / 4;

            float scaleFactor = 2;
            Vector2 relativePoint = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            if (/*projOwner.itemAnimation == 0 || */projOwner.dead)
            {
                projectile.Kill();
            }

            int sign = Math.Sign(projectile.velocity.X);

            projectile.velocity = new Vector2(sign, 0f);

            if (projectile.ai[0] == 0f)
            {
                projectile.rotation = new Vector2(sign, 0f - projOwner.gravDir).ToRotation() + quarterPI + (float)Math.PI;
                if (projectile.velocity.X < 0f)
                {
                    projectile.rotation -= (float)Math.PI / 2;
                }
            }

            projectile.ai[0] += 1f;

            projectile.rotation += (float)Math.PI * 2f * num2 / num * sign;

            bool isDone = projectile.ai[0] == num / 2f;

            if (projectile.ai[0] >= num || (isDone && !projOwner.controlUseItem))
            {
                projectile.Kill();
                projOwner.reuseDelay = 2;
            }
            else if (isDone)
            {
                Vector2 mouse = Main.MouseWorld;
                int dir = (projOwner.DirectionTo(mouse).X > 0f) ? 1 : -1;
                if (dir != projectile.velocity.X)
                {
                    projOwner.ChangeDir(dir);
                    projectile.velocity = new Vector2(dir, 0f);
                    projectile.netUpdate = true;
                    projectile.rotation -= (float)Math.PI;
                }
            }
            float rotationValue = projectile.rotation - (float)Math.PI / 4f * sign;
            Vector2 positionVector = (rotationValue + (sign == -1 ? (float)Math.PI : 0f)).ToRotationVector2() * (projectile.ai[0] / num) * scaleFactor;

            projectile.position = relativePoint - projectile.Size / 2f;
            projectile.position += positionVector;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;

            projOwner.ChangeDir(projectile.direction);
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = 2;
            projOwner.itemAnimation = 2;
            projOwner.itemRotation = MathHelper.WrapAngle(projectile.rotation);
        }
    }
}