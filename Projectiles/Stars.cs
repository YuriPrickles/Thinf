using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items.Placeables;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Projectiles
{
    public class Stars : Minion
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.FallingStar;

        public override void SetDefaults()
        {
            projectile.minionSlots = 1;
            projectile.height = 14;
            projectile.width = 14;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        protected float detectionRange = 900f;
        protected float idleRadius = 256f;
        protected float idleVelocity = 16f;
        protected float shootCooldown = 25f;

        public static float Rotation = 0f; //Used for summon rotation

        private int AiState = 0; //0 = idle (not attacking) //1 = found target (start attack) //2 = attacking found target
        private int ID = 0;
        private Vector2 targetPosition; //Found target's position. Default is around player
        private Vector2 targetPositionForProj;

        private float AttackCooldown { get => projectile.ai[0]; set => projectile.ai[0] = value; }
        public override void OnCreated()
        {
            ID = Main.player[projectile.owner].ownedProjectileCounts[projectile.type] + 1;
        }

        public override void Behavior()
        {
            if (AttackCooldown >= 0)
                AttackCooldown--;
            Player player = Main.player[projectile.owner];
            int num = player.ownedProjectileCounts[projectile.type];
            if (num < 1)
                num = 1;
            Rotation += 2.6f / num;
            targetPosition = player.position + (Vector2.One * idleRadius).RotatedBy(MathHelper.ToRadians((Rotation) + ((ID) * (360f / num))));
            bool hasTarget = false;
            float distance = detectionRange * 1.2f;
            for (int i = 0; i < Main.maxNPCs; ++i) //Basic find NPC code
            {
                NPC target = Main.npc[i];
                if (player.Distance(target.position) < distance && target.active && target.CanBeChasedBy() && target.lifeMax > 10 && !target.friendly && target.type != NPCID.TargetDummy && !target.immortal && Collision.CanHitLine(projectile.Center, 4, 4, target.position, 4, 4))
                {
                    hasTarget = true;
                    targetPositionForProj = target.Center;
                }
            }
            Vector2 direction = projectile.DirectionTo(targetPosition); //Find direction to position
            if (hasTarget) //If the projectile actually has a target
                AiState = 1; //Set to found NPC
            else //If projectile does not have a target
                AiState = 0; //Set to idle
            if (AiState != 2) //If projectile is not attacking
            {
                if (AiState == 1) //if projectile has a target
                {
                    if (projectile.Distance(targetPosition) > idleRadius * 1.2f) //This should always run here, but putting here just in case.
                    {
                    }
                    AiState = 2;
                }
            }
            if (AiState == 2)
            {
                if (AttackCooldown <= 0)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(targetPositionForProj) * 6.7f, ModContent.ProjectileType<StarsLaser>(), projectile.damage, projectile.knockBack, projectile.owner);
                    AttackCooldown = shootCooldown + Main.rand.Next(-2, 2);
                }
            }

            if (projectile.Distance(targetPosition) <= detectionRange * 1.2f && projectile.Distance(targetPosition) > idleRadius * 0.8f) //if projectile is within a certain range
            {
                projectile.velocity = Vector2.Lerp(direction * idleVelocity * 0.8f, direction * 1.5f, idleVelocity / Vector2.Distance(projectile.position, targetPosition));
                //Vector2.Lerp is like a slider.
                //You input 2 Vector2 fields, and give it a float. 0f is all the way left (the first one), 1f is all the way right (the second one).
            }
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.starsMinion = false;
            }
            if (modPlayer.starsMinion)  // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
            {
                projectile.timeLeft = 2;
            }
        }
    }
    public class StarsLaser : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly;
        public override void SetDefaults()
        {
            projectile.light = 0.3f;
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            projectile.extraUpdates = 100;
        }
        public override void AI()
        {
            Dust d = Main.dust[Dust.NewDust(projectile.Center, 1, 1, DustID.SpelunkerGlowstickSparkle)];
            d.noGravity = true;
            d.velocity = projectile.velocity * 1.2f;
        }
    }

    public class StarSummon : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.SlimeStaff;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlazer");
            Tooltip.SetDefault("Summon a rapid fire laser shooting star that goes around you\nRequires at least 5 minion slots");
        }
        public override void SetDefaults()
        {
            item.color = new Color(170, 255, 170);
            item.shoot = ModContent.ProjectileType<Stars>();
            item.mana = 10;
            item.useStyle = 1;
            item.useAnimation = 100;
            item.useTime = 20;
            item.damage = 50;
            item.knockBack = 2f;
            item.width = 24;
            item.height = 24;
            item.summon = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(BuffType<Starlazer>(), 18000);
            if (player.whoAmI == Main.myPlayer)
                for (int i = 0; i < 1; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / 5) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 targetPosition = player.position + offset;
                    Projectile.NewProjectile(targetPosition, Vector2.Zero, type, damage, knockBack, player.whoAmI);
                }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentStardust, 15);
            recipe.AddIngredient(ItemType<StarriteBar>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool CanUseItem(Player player)
        {
            if (player.numMinions + 5 <= player.maxMinions)
                return true;
            return false;
        }
    }
}