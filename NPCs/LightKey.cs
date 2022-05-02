using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Thinf.Items;

namespace Thinf.NPCs
{
    [AutoloadBossHead] //Uncomment this line to enable
    public class LightKey : ModNPC
    {
        public override bool CheckActive()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Key");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 32;
            npc.height = 48;
            npc.knockBackResist = 0f;
            npc.lifeMax = 10000;
            npc.life = 10000;
            npc.defense = 24;
            npc.damage = 26;
            npc.boss = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = 1250000;
            npc.npcSlots = 0.1f;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Soul_Keys");
        }
        public float AttackType { get { return npc.ai[0]; } set { npc.ai[0] = value; } }
        public float Timer { get { return npc.ai[1]; } set { npc.ai[1] = value; } }
        public float Timer2 { get { return npc.ai[2]; } set { npc.ai[2] = value; } }
        public float ExtraFloat { get { return npc.ai[3]; } set { npc.ai[3] = value; } }

        public override void AI()
        {
            npc.netUpdate = true;
            if (AttackType != 1)
                npc.TargetClosest(false);
            if (npc.target < 0 || npc.target == 255 || NoNearbyPlayers(1600f)) //Despawn NPC
            {
                Despawn();
            }
            float[] cooldown = new float[3];
            cooldown[0] = 25f; //I think this is time between laser attacks?
            cooldown[1] = 90f; //Laser shoot cooldown
            cooldown[2] = 15f;
            float ExpertMult = Main.expertMode ? 1.2f : 1f; //Modify first value to increase multiplier during expert.
            Timer++;
            Player player = Main.player[npc.target];

            if (AttackType == 0f)
            {
                if (Timer < cooldown[0] * Main.rand.Next(75, 100) * ExpertMult)
                {
                    if (npc.DistanceSQ(player.position) < 262144f || npc.velocity.LengthSquared() < 16f) //So apparently LengthSQ and DistanceSQ is cheaper on resources than Length and Distance (From tML Discord)
                        npc.velocity += npc.DirectionTo(player.position) * .08f;
                }
                if (npc.velocity.LengthSquared() > 210.25f)
                {
                    npc.velocity = npc.velocity.SafeNormalize(Vector2.Zero) * 14.5f;
                }
                ScreenWrap(player); //this only tries to wrap
                if (Main.expertMode) //This is an Expert-Only part of the attack
                {
                    Timer2++;
                    if (Timer2 * ExpertMult >= cooldown[0])
                    {
                        Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, (npc.velocity * .984f).RotatedByRandom(MathHelper.PiOver4 * .75f), ProjectileID.PurpleLaser, 16, 1.4f, Main.myPlayer)];
                        p.friendly = false;
                        p.hostile = true;
                        p.penetrate = 3;
                        Timer2 = 0f;
                    }
                }
                if (Timer > cooldown[0] * Main.rand.Next(75, 100) * ExpertMult)
                {
                    npc.velocity *= .984f;
                    if (npc.velocity.LengthSquared() < 12f)
                    {
                        AttackType = 1;
                        Timer = -10f;
                        Timer2 = 0f;
                        ExtraFloat = 0f;
                        npc.velocity = Vector2.Zero;
                    }
                }
            }
            if (AttackType == 1f)
            {
                npc.position.Y = player.position.Y - 240f;
                npc.velocity.X += player.position.X < npc.position.X ? -.08f : .08f;
                if (Math.Abs(npc.position.X - player.position.X) < 128f)
                    npc.velocity.X *= .984f;
                Timer2++;
                if (Timer2 * ExpertMult >= cooldown[1])
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Projectile p = Main.projectile[Projectile.NewProjectile(new Vector2(ExtraFloat, npc.position.Y - 360f), new Vector2(0, 3.5f + ((i + 1) * 3.5f)), ProjectileID.PurpleLaser, 36, 1.4f, Main.myPlayer)];
                        //Super high damage, because you can see the projectile a mile away.
                        p.friendly = false;
                        p.hostile = true;
                        p.penetrate = 3;
                    }
                    ExtraFloat = Main.rand.Next(-6, 6) * 10f + player.position.X;
                    Timer2 = 0;
                }
                Vector2 centre = new Vector2(ExtraFloat, player.Center.Y);
                if (Timer % 5 == 0)
                    Dust.QuickBox(centre - new Vector2(24f, 24f), centre + new Vector2(24f, 24f), 1, Color.Pink, null);

                int times = 12;
                if (Timer - ExpertMult > times * cooldown[1] * ExpertMult)
                {
                    ResetVariables();
                    Timer = 0;
                }
            }
            if (AttackType == 2f)
            {
                npc.rotation += MathHelper.ToRadians(Main.rand.NextFloat(3, 9));
                npc.rotation %= MathHelper.TwoPi;
                Timer2++;
                if (Timer2 * ExpertMult >= cooldown[2])
                {
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 1.2f, ProjectileID.PurpleLaser, 40, 3f, Main.myPlayer)];
                    p.friendly = false;
                    p.hostile = true;
                    p.penetrate = -1;
                    Timer2 = 0f;
                }
                int lasers = 60;
                if (Timer > cooldown[2] * lasers)
                {
                    AttackType = 2;
                    Timer = -10f;
                    Timer2 = 0f;
                    ExtraFloat = 0f;
                    npc.velocity = Vector2.Zero;
                }
            }
        }
        public override void NPCLoot()
        {
            if(!NPC.AnyNPCs(ModContent.NPCType<FlightKey>()) && !NPC.AnyNPCs(ModContent.NPCType<NightKey>()))
            {
                Main.NewText("The Chest Wastelands grow stronger.. and messier!", 255, 255, 0); //Bright orange text.
                ModNameWorld.downedSoulKeys = true; //Removed using static. Didn't feel it was needed.
            }

            Item.NewItem(npc.getRect(), ModContent.ItemType<FragmentOfLight>(), Main.rand.Next(10) + 18);
            //You will have to replace ItemID with ModContent.ItemType<>() for this to drop proper item.
        }

        private void ResetVariables()
        {
            AttackType = 0;
            Timer = -10f;
            Timer2 = 0f;
            ExtraFloat = 0f;
            npc.velocity = Vector2.Zero;
        }

        /// <summary>
        /// Causes the NPC to warp or teleport to the opposite side of the screen when it exceeds the boundaries of the player's screen.
        /// </summary>
        /// <param name="player"></param>
        private void ScreenWrap(Player player)
        {
            bool wrapped = false;
            Vector2 newPos = Vector2.Zero;
            if (player.whoAmI == Main.myPlayer)
            {
                float ScreenLeft = Main.screenPosition.X - npc.width + Main.rand.Next(64, 129);
                float ScreenRight = Main.screenPosition.X + Main.screenWidth - Main.rand.Next(64, 129);
                float ScreenTop = Main.screenPosition.Y - npc.height + Main.rand.Next(64, 129);
                float ScreenBottom = Main.screenPosition.Y + Main.screenHeight - Main.rand.Next(64, 129);
                if (npc.position.X > ScreenRight && npc.velocity.X > 0)
                {
                    newPos = new Vector2(ScreenLeft, npc.position.Y);
                    //npc.Teleport(newPos);
                    wrapped = true;
                }
                else if (npc.position.X < Main.screenPosition.X - npc.width - Main.rand.Next(0, 64) && npc.velocity.X <= 0)
                {
                    newPos = new Vector2(ScreenRight, npc.position.Y);
                    //npc.Teleport(newPos);
                    wrapped = true;
                }
                if (npc.position.Y < ScreenTop && npc.velocity.Y < 0)
                {
                    newPos = new Vector2(npc.position.X, ScreenBottom);
                    //npc.Teleport(newPos);
                    wrapped = true;
                }
                else if (npc.position.Y > ScreenBottom && npc.velocity.Y >= 0)
                {
                    newPos = new Vector2(npc.position.X, ScreenTop);
                    //npc.Teleport(newPos);
                    wrapped = true;
                }
            }
            if (wrapped == true)
            {
                for (int i = 0; i < 30; i++)
                {
                    Dust d = Main.dust[Dust.NewDust(newPos, 1, 1, DustID.UndergroundHallowedEnemies)];
                    d.scale = Main.rand.NextFloat(1.1f, 1.3f);
                    d.velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(2.8f, 5.6f);
                    if (i > 20)
                    {
                        d.velocity *= 2.5f;
                        d.scale += .15f;
                    }
                    d.noGravity = true;
                }
                float space = 32f;
                float distance = Vector2.Distance(newPos, npc.position) / space;
                for (int i = 0; i < distance * 2; i++)
                {
                    Vector2 spawnPos = Vector2.Lerp(npc.position, newPos, i / (distance * 2));
                    Dust d = Main.dust[Dust.NewDust(spawnPos - new Vector2(24f, 24f), 48, 48, DustID.UndergroundHallowedEnemies)];
                    d.scale = Main.rand.NextFloat(1.1f, 1.3f);
                    d.velocity = Vector2.Zero;
                    d.noGravity = true;
                }
                npc.position = newPos;
            }
        }
        /// <summary>
        /// Checks for players within a given distance. Returns true if none, returns false if 1 or more exist.
        /// </summary>
        /// <param name="distance">Set a distance to check players from. If none given, or distance is less than 0, will check for all players in world</param>
        /// <returns>Whether or not there are any valid targets nearby for NPC</returns>
        private bool NoNearbyPlayers(float distance = -1f)
        {
            bool noPlayers = true;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (distance >= 0f)
                {
                    if (Vector2.Distance(npc.position, player.position) < distance && player.active && !player.dead)
                        return false; //Short it here if it finds a player and distance is >= 0
                }
                else
                {
                    if (player.active && !player.dead)
                    {
                        noPlayers = true;
                    }
                    else if (!player.active || player.dead)
                    {
                        noPlayers = false;
                    }
                }
            }
            return noPlayers;
        }


        private void Despawn()
        {
            npc.active = false;
            npc.timeLeft = 1;
            for (int i = 0; i < 60; i++)
            {
                Dust d = Main.dust[Dust.NewDust(npc.Center, 1, 1, DustID.UndergroundHallowedEnemies)];
                d.scale = Main.rand.NextFloat(0.95f, 1.1f);
                d.velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(6f, 8f);
                d.noGravity = true;
            }
        }
    }
}