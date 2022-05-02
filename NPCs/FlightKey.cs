using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
//SHOUT OUT TO STARDUST FOR MAKING THIS WOOHOO
namespace Thinf.NPCs
{
    [AutoloadBossHead] //Uncomment this line. Removed it for testing because I was using my mod for it. :P
    public class FlightKey : ModNPC
    {
        public override bool CheckActive()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flight Key"); //Decided this sounded a bit better.
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1; //Completely custom AI. If left at default, this slows the NPC's X-velocity
            npc.width = 30; //32 -> 30
            npc.height = 52; //58 -> 52
            npc.knockBackResist = 0f; //Removes ability for player to knock back.
            npc.damage = 30; //32 -> 30
            npc.defense = 25; //18 -> 25
            npc.value = 1250000; //Item.BuyPrice(0,25,0,0) = 125000000
            npc.npcSlots = 0.1f; //0.1f -> 5.3f
            //npcSlots represent how much this npc counts towards the npc spawn limit.
            //See the link below for a list of vanilla NPC's, their npcSlots value, among other things.
            //https://docs.google.com/spreadsheets/d/1y5_y1ptnlcPhcxh6jCOsMApX1W6FI3l8duHtEaLqkro/edit#gid=1969633944
            npc.lavaImmune = true; //Immune to lava
            npc.noGravity = true; //No gravity
            npc.noTileCollide = true; //No tile collide
            npc.HitSound = SoundID.NPCHit4; //Kept these the same
            npc.DeathSound = SoundID.NPCDeath3; //Kept these the same
            npc.netAlways = true; //Technically unnecessary, as this is a Boss NPC -- which is automatically synced across all players ANYWAY
            //See link here for more information on NPC fields
            //https://github.com/tModLoader/tModLoader/wiki/NPC-Class-Documentation#npcslots
            npc.boss = true;
            npc.lifeMax = 10000; //17500 -> 10000
            //This is doubled in Expert Mode (35000 -> 20000)
            //Reworked AI will compensate for difficulty loss in HP reduction.
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Soul_Keys");
            //Testing in my own mod, thus don't have this track. Swap commented lines to make this proper music
        }
        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<LightKey>()) && !NPC.AnyNPCs(ModContent.NPCType<NightKey>()))
            {
                Main.NewText("The Chest Wastelands grow stronger.. and messier!", 255, 255, 0); //Bright orange text.
                ModNameWorld.downedSoulKeys = true; //Removed using static. Didn't feel it was needs.
            }

            Item.NewItem(npc.getRect(), ModContent.ItemType<FragmentOfFlight>(), Main.rand.Next(10) + 18);
            //You will have to replace ItemID with ModContent.ItemType<>() for this to drop proper item.
        }
        private float Timer { get => npc.ai[0]; set => npc.ai[0] = value; }
        private float AttackType { get => npc.ai[1]; set => npc.ai[1] = value; }
        private float ExtraFloat { get => npc.ai[2]; set => npc.ai[2] = value; }
        private float CheckOnce { get => npc.ai[3]; set => npc.ai[3] = value; }
        //Defining this just as an extra Vector2 I can assign and use if I so desire
        public override void AI()
        {
            npc.TargetClosest(false); //Rotation will be handled manually.
            if (npc.target < 0 || npc.target == 255 || NoNearbyPlayers(1600f)) //Despawn NPC
            {
                Despawn();
            }

            Player player = Main.player[npc.target]; //Generic player definition
            npc.netUpdate = true; //Should be true anyway I think?
            float[] cooldownTimer = new float[3];

            cooldownTimer[0] = 8f; //Attack 1 (timer)
            cooldownTimer[1] = 160f; //Attack 2 (dash cd)
            cooldownTimer[2] = 15f; //Attack 3

            Timer++; //Generic timer using npc.ai[] array
            if (AttackType == 0)
            {
                if (CheckOnce % 2 == 0)
                {
                    ExtraFloat = Main.rand.NextFloat(0f, 360f);
                    CheckOnce++;
                }
                npc.rotation += MathHelper.ToRadians(18f); //Should be 3 rotations in 1 sec
                npc.rotation %= MathHelper.TwoPi; //Just make sure rotation isn't like 1800 degrees
            }
            if (Timer % cooldownTimer[0] == 0 && AttackType == 0)
            {
                float waitTime = 68f;
                if (Timer < waitTime)
                {
                    int maxRays = 4 + (int)((npc.lifeMax - npc.life) / (npc.lifeMax * .25f)); //If I've done this right, 4 rays to start, 8 when it reaches 0 hp
                    if (npc.life <= npc.lifeMax * .05f)
                        maxRays = 10;
                    for (int i = 0; i < maxRays; i++)
                    {
                        int offset = 1;
                        int length = (int)waitTime + (int)cooldownTimer[0] + offset;
                        length -= 10;
                        for (int iteration = offset; iteration < length; iteration++)
                        {
                            float rotation = MathHelper.ToRadians((((360f / maxRays) * i) + ExtraFloat) % 360f);
                            float spacing = 24f;
                            Vector2 spawnPos = npc.Center + Vector2.One.RotatedBy(rotation) * (iteration * spacing);
                            Dust d = Main.dust[Dust.NewDust(spawnPos, 1, 1, DustID.SapphireBolt, 0, 0, 0, Color.Cyan)];
                            d.noGravity = true;
                            d.velocity = Vector2.Zero;
                            //Main.NewText($"Part {iteration}, spawn at {spawnPos}. NPC is at {npc.Center}.");
                        }
                    }
                }
                else if (Timer >= waitTime)
                {
                    int maxRays = 4 + (int)((npc.lifeMax - npc.life) / (npc.lifeMax * .25f)); //If I've done this right, 4 rays to start, 8 when it reaches 0 hp
                    if (npc.life <= npc.lifeMax * .05f)
                        maxRays = 10;
                    if (Main.expertMode)
                    {
                        maxRays = 6 + (int)((npc.lifeMax - npc.life) / (npc.lifeMax * .25f)); //If I've done this right, 4 rays to start, 8 when it reaches 0 hp
                        if (npc.life <= npc.lifeMax * .05f)
                            maxRays = 15;
                    }
                    for (int i = 0; i < maxRays; i++)
                    {
                        float rotation = MathHelper.ToRadians((((360f / maxRays) * i) + ExtraFloat) % 360f);
                        float speed = 16f;
                        Vector2 velocity = Vector2.One.RotatedBy(rotation) * speed;
                        Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, velocity, ProjectileID.HarpyFeather, npc.damage, 1.2f, Main.myPlayer)];
                        //I know I give shit to Main.myPlayer
                        //But turns out
                        //Hostile projectiles are supposed to have that
                        //As their owner
                        //Weird right?
                        p.hostile = true;
                        p.friendly = false;
                    }
                    CheckOnce++;
                    Timer = -25f;
                    ExtraFloat = 0f;
                    if (CheckOnce > Main.rand.Next(new int[] { 2, 4 }))
                    {
                        Timer = -10f;
                        CheckOnce = 0f;
                        ExtraFloat = 0f;
                        npc.velocity = Vector2.Zero;
                        AttackType = 1;
                        for (int i = 0; i < 60; i++)
                        {
                            Dust d = Main.dust[Dust.NewDust(npc.Center, 1, 1, DustID.SapphireBolt, default, default, default, Color.Cyan)];
                            d.velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(6f, 8f);
                            d.noGravity = true;
                        }
                    }
                }
            }
            if (AttackType == 1)
            {
                if (npc.velocity.LengthSquared() > 2f)
                {
                    Dust d = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.SapphireBolt, default, default, default, Color.Cyan, Main.rand.NextFloat(1.1f, 1.3f))];
                    d.velocity = npc.velocity * 1.2f;
                    d.noGravity = true;
                    npc.rotation = npc.velocity.ToRotation() + MathHelper.PiOver2;
                    npc.velocity *= 0.984f;
                    if (Timer % 20 == 0)
                    {
                        Projectile p1 = Main.projectile[Projectile.NewProjectile(npc.Center, npc.velocity.RotatedBy(MathHelper.PiOver2).SafeNormalize(Vector2.Zero) * 4.6f, ProjectileID.HarpyFeather, 13, 1.2f, Main.myPlayer)];
                        Projectile p2 = Main.projectile[Projectile.NewProjectile(npc.Center, npc.velocity.RotatedBy(3f * MathHelper.PiOver2).SafeNormalize(Vector2.Zero) * 4.6f, ProjectileID.HarpyFeather, 13, 1.2f, Main.myPlayer)];
                        p1.hostile = true;
                        p2.hostile = true;
                        p1.friendly = false;
                        p2.friendly = false;
                    }
                }
                else if (npc.velocity.LengthSquared() > 1f && npc.velocity.LengthSquared() <= 2f)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Dust d = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, DustID.SapphireBolt, default, default, default, Color.Cyan, Main.rand.NextFloat(1.1f, 1.3f))];
                        d.velocity = npc.velocity * Main.rand.NextFloat(8f, 12f);
                        d.noGravity = true;
                    }
                    npc.velocity = Vector2.Zero;
                }
                npc.rotation = (player.position - npc.position).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.PiOver2;
            }
            if (Timer % cooldownTimer[1] == 0 && AttackType == 1)
            {
                if (Timer % (cooldownTimer[1] * Main.rand.Next(2, 4)) == 0 && npc.velocity.LengthSquared() < 1)
                {
                    NPC n = Main.npc[NPC.NewNPC(npc.position.ToPoint().X + Main.rand.Next(-32, 32), npc.position.ToPoint().Y + Main.rand.Next(-32, 32), Main.rand.Next(new int[] { NPCID.Harpy, NPCID.Harpy, NPCID.Harpy/*, NPCID.WyvernHead*/ }))];
                    n.target = npc.target;
                    n.SpawnedFromStatue = true;
                    n.takenDamageMultiplier = 1.1f;
                    if (n.type == NPCID.Harpy)
                    {
                        n.GivenName = "Flimsy Harpy";
                        n.damage = 15;
                        n.lifeMax = 40;
                        n.life = 40;
                        n.defense = 20;
                        n.noTileCollide = true;
                    }
                    //else
                    //{
                    //    n.GivenName = "Wyvern";
                    //    n.damage = 30;
                    //    n.lifeMax = 100;
                    //    n.life = 100;
                    //    n.defense = 35;
                    //}
                }
                int dashCount = 3;
                if (npc.velocity.LengthSquared() < .25f)
                {
                    float speed = 8f;
                    npc.velocity = (player.position - npc.position).SafeNormalize(Vector2.Zero) * speed;
                    if (Timer > cooldownTimer[1] * (dashCount - 1))
                    {
                        Timer = -10f;
                        CheckOnce = 0f;
                        ExtraFloat = 0f;
                        npc.velocity = Vector2.Zero;
                        AttackType = 2;
                        npc.velocity = Vector2.Zero;
                    }
                }
            }
            if (AttackType == 2)
            {
                if (CheckOnce == 0)
                {
                    Timer = -80f;
                    ExtraFloat = Main.rand.NextFloat(0f, 360f);
                    CheckOnce++;
                }
                if (Timer < 0 && Timer % -10 == 0)
                {
                    float rotation = MathHelper.ToRadians(ExtraFloat % 360f);
                    ExtraFloat += 45f;
                    Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, (player.Center - npc.Center).SafeNormalize(Vector2.Zero).RotatedBy(rotation) * 8.4f, ProjectileID.HarpyFeather, 9, 1.2f, Main.myPlayer)];
                    p.friendly = false;
                    p.hostile = true;
                }
                if (Timer == 0)
                    ExtraFloat = 1f;
                npc.rotation = MathHelper.Pi + (npc.velocity.X * .01f);
                npc.velocity.X += npc.position.X < player.position.X ? .04f : -.04f;
                npc.velocity.X *= Math.Abs(npc.position.X - player.position.X) < 48f ? .99f : 1f;
                if (YVel3(player) && Timer - (cooldownTimer[2] * ExtraFloat) >= cooldownTimer[2])
                {
                    ExtraFloat += 1f;
                    for (int i = 0; i < 5; i++)
                    {
                        float rotation = MathHelper.ToRadians(-20f + (i * 10f));
                        Projectile p = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(0f, 1f).SafeNormalize(Vector2.Zero).RotatedBy(rotation) * 8.4f, ProjectileID.HarpyFeather, 9, 1.2f, Main.myPlayer)];
                        p.friendly = false;
                        p.hostile = true;
                    }
                }
                if (Timer > 600) //10 sec
                    ResetVariables();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode || Main.rand.NextFloat() <= .33f)
            {
                target.AddBuff(BuffID.VortexDebuff, Main.expertMode ? 300 : 150);
                //BuffID.VortexBuff is Distorted -- makes player fly up/down randomly.
                target.AddBuff(BuffID.Featherfall, Main.expertMode ? 360 : 210);
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
                Dust d = Main.dust[Dust.NewDust(npc.Center, 1, 1, DustID.SapphireBolt, default, default, default, Color.Cyan)];
                d.velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(6f, 8f);
                d.noGravity = true;
            }
        }

        private void ResetVariables()
        {
            Timer = -10f;
            CheckOnce = 0f;
            AttackType = 0;
            ExtraFloat = 0f;
            npc.velocity = Vector2.Zero;
        }

        private bool YVel3(Player player)
        {
            npc.velocity.Y += .18f;
            //            if (npc.velocity.Y < 0)
            //                npc.velocity.Y *= .9f;
            if (npc.position.Y > player.position.Y - 112f)
            {
                npc.velocity.Y = -4f - Main.rand.NextFloat(0f, 2f);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}