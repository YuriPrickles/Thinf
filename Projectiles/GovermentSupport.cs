using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;

namespace Thinf.Projectiles
{
    /*
	 * This minion shows a few mandatory things that make it behave properly. 
	 * Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	 * If the player targets a certain NPC with right-click, it will fly through tiles to it
	 * If it isn't attacking, it will float near the player with minimal movement
	 */
    public class GovernmentSupport : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[projectile.type] = 2;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            // These below are needed for a minion
            // Denotes that this projectile is a pet or minion
            Main.projPet[projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 68;
            // Makes the minion go through tiles freely
            projectile.tileCollide = false;

            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            projectile.friendly = true;
            // Only determines the damage type
            projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            projectile.minionSlots = 3f;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            projectile.penetrate = -1;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
        }

        private float healTime = 0f;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            int dustSpawnAmount = 24;
            for (int i = 0; i < dustSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                Vector2 dustOffset = currentRotation.ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(projectile.Center + dustOffset * 240, DustID.Firework_Green);
                dust.noGravity = true;
            }

            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<GovermentSupportBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<GovermentSupportBuff>()))
            {
                projectile.timeLeft = 2;
            }
            #endregion


            healTime++;
            if (healTime >= 180)
            {
                for (int i = 0; i < Main.maxPlayers; ++i)
                {
                    Player healTarget = Main.player[i];
                    int amountToHeal = healTarget.statLifeMax2 / 60;
                    if (healTarget.active && !healTarget.dead && healTarget.Distance(projectile.Center) <= 240 && (player.statLife + amountToHeal < player.statLifeMax2))
                    {
                        healTarget.HealEffect(amountToHeal);
                        healTarget.statLife += amountToHeal;
                    }
                }
                for (int i = 0; i < 18; i++)
                {
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, -10, 1, 0, default);
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, -8, 1, 0, default);
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, -6, 1, 0, default);
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, 6, 1, 0, default);
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, 8, 1, 0, default);
                    Dust.NewDustDirect(projectile.Center + new Vector2(0, -35), 5, 1, DustID.Clentaminator_Green, 10, 1, 0, default);
                }
                healTime = 0;
            }

            #region General behavior
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Fix overlap with other minions
                Projectile other = Main.projectile[i];
                if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
                {
                    if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
                    else projectile.velocity.X += overlapVelocity;

                    if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
                    else projectile.velocity.Y += overlapVelocity;
                }
            }
            #endregion



            #region Movement

            // Default movement parameters (here for attacking)
            float speed = 8f;
            float inertia = 20f;

            // Minion doesn't have a target: return to player and idle
            if (distanceToIdlePosition > 600f)
            {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 60f;
            }
            else
            {
                // Slow down the minion if closer to the player
                speed = 4f;
                inertia = 80f;
            }
            if (distanceToIdlePosition > 20f)
            {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (projectile.velocity == Vector2.Zero)
            {
                // If there is a case where it's not moving at all, give it a little "poke"
                projectile.velocity.X = -0.15f;
                projectile.velocity.Y = -0.05f;
            }
            #endregion

            #region Animation and visuals
            // So it will lean slightly towards the direction it's moving
            projectile.rotation = projectile.velocity.X * 0.05f;

            // This is a simple "loop through all frames from top to bottom" animation
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

            // Some visuals here
            Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
            #endregion
        }
    }
}