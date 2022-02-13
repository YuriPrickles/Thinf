using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Accessories;
using Thinf.Items.THE_SUPER_COOL_BADASS_LORE;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.Cortal
{
    [AutoloadBossHead]
    public class Cortal : ModNPC
    {
        int frameNumber = 0;
        int portalSpawnTimer = 0;
        int phaseCount = 0;
        int phaseTwoTeleportDashCounter = 0;
        int maxPortalSpawn = 3;
        int maxPortalRand = 3;
        int isDead = 0;
        int cutsceneTimer = 0;
        int cutsceneDone = 0;
        bool bossLootnt = true;


        //int[] timer = new int[2];
        int[] imageTimer = new int[2] { 0, 12 };
        List<FadeImage> images = new List<FadeImage>();
        public override string Texture => "Thinf/NPCs/Cortal/Cortal";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cortal"); //yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaay
            Main.npcFrameCount[npc.type] = 6;
            NPCID.Sets.TrailCacheLength[npc.type] = 12;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 16;
            npc.lifeMax = 3600;   //boss life
            npc.damage = 21;  //boss damage
            npc.defense = 8;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 150;
            npc.height = 156;
            npc.value = Item.buyPrice(0, 2, 32, 30);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.Item6;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TeleportsBehindYou");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                frameNumber++;
                if (frameNumber >= 6)
                {
                    frameNumber = 0;
                }
                npc.frame.Y = frameNumber * (936 / 6);
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!bossLootnt)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<RodOfCortal>());
                if (Main.expertMode)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<NeverendingSpecularSoup>());

                if (Main.rand.Next(7500) == 0)
                    Item.NewItem(npc.getRect(), ModContent.ItemType<CortalMirror>());

                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem(npc.getRect(), ItemID.MagicMirror);
                    Item.NewItem(npc.getRect(), ItemID.IceMirror);
                }

                Item.NewItem(npc.getRect(), ItemID.RecallPotion, 5 + Main.rand.Next(8));
                Item.NewItem(npc.getRect(), ItemID.SpecularFish, 12 + Main.rand.Next(13));
                Item.NewItem(npc.getRect(), ModContent.ItemType<Cortascale>(), 10 + Main.rand.Next(9));
                Item.NewItem(npc.getRect(), ItemID.LesserHealingPotion, 8 + Main.rand.Next(9));
                Item.NewItem(npc.getRect(), ModContent.ItemType<CortalsFin>());
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 5300;  //boss life scale in expertmode
            npc.damage = 30;  //boss damage increase in expermode
        }
        public override bool CheckDead()
        {
            npc.life = npc.lifeMax;
            npc.alpha = 0;
            npc.damage = 0;
            npc.aiStyle = -1;
            npc.velocity = Vector2.Zero;
            npc.dontTakeDamage = true;
            music = 0;
            isDead = 1;
            if (cutsceneDone == 1)
            {
                return true;
            }
            return false;
        }
        public override void AI()
        {

            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.alpha += 5;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
            Player player = Main.player[npc.target];
            if (!player.wet)
            {
                maxPortalRand = 7;
                maxPortalSpawn = 7;
            }
            else
            {
                maxPortalSpawn = 3;
                maxPortalRand = 3;
            }
            npc.netUpdate = true;
            npc.spriteDirection = -npc.direction;
            if (isDead == 1)
            {
                portalSpawnTimer = 0;
                phaseCount = 0;
                phaseTwoTeleportDashCounter = 0;
                cutsceneTimer++;
                if (cutsceneTimer == 90)
                {
                    Main.NewText("Hey hold on I left the stove on", Color.Teal);
                }
                if (cutsceneTimer == 180)
                {
                    for (int k = 0; k < 65; ++k)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 dustPos = npc.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(dustPos, npc.width, npc.height, 15, 0f, 0f, 0, new Color(255, 255, 255), 1.5f)];
                    }

                    downedCortal = true;

                    Main.NewText("Cortal disappears!", 175, 75, 255);
                    if (Main.rand.Next(5) == 0 && Main.hardMode && player.ZoneHoly && !player.wet && Main.dayTime && player.ZoneRockLayerHeight && Main.eclipse && Main.invasionType == InvasionID.PirateInvasion)
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<RodOfCortal>());
                    }
                    if (Main.expertMode)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<NeverendingSpecularSoup>());

                    if (Main.rand.Next(7500) == 0)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<CortalMirror>());

                    if (Main.rand.Next(5) == 0)
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<EmergencyEscape>());
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        if (!player.ZoneSnow)
                        {
                            Item.NewItem(npc.getRect(), ItemID.MagicMirror);
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.IceMirror);
                        }
                    }

                    Item.NewItem(npc.getRect(), ItemID.RecallPotion, 5 + Main.rand.Next(8));
                    Item.NewItem(npc.getRect(), ItemID.SpecularFish, 12 + Main.rand.Next(13));
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Cortascale>(), 10 + Main.rand.Next(9));
                    Item.NewItem(npc.getRect(), ItemID.LesserHealingPotion, 8 + Main.rand.Next(9));
                    Item.NewItem(npc.getRect(), ModContent.ItemType<CortalsFin>());
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/GasterFade"));
                }
                if (cutsceneTimer >= 180)
                {
                    npc.alpha += 8;
                    if (npc.alpha >= 255)
                    {
                        npc.life = 0;
                        cutsceneDone = 1;
                    }
                }
            }
            if (phaseCount == 0)
            {
                npc.noTileCollide = false;
                portalSpawnTimer++;
                if (portalSpawnTimer >= 400)
                {
                    for (int i = 0; i < Main.rand.Next(maxPortalRand) + maxPortalSpawn; ++i)
                    {
                        NPC position = Main.npc[NPC.NewNPC((int)(npc.Center.X + Main.rand.Next(-500, 500)), (int)(npc.Center.Y + (Main.rand.Next(-500, 500))), ModContent.NPCType<CortaPortal>())];
                        for (int k = 0; k < 80; ++k)
                        {
                            Dust dust;
                            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                            Vector2 dustPos = position.Center;
                            dust = Main.dust[Terraria.Dust.NewDust(dustPos, 30, 30, 15, 0f, 0f, 0, new Color(255, 255, 255), 1.513158f)];
                        }
                    }
                    portalSpawnTimer = 0;
                    phaseCount = 1;
                }
            }

            if (phaseCount == 1)
            {
                npc.aiStyle = -1;
                npc.noTileCollide = true;
                npc.alpha += 4;
                if (npc.alpha >= 205)
                {
                    PositionLoop:
                    Vector2 tpPos = new Vector2(Main.rand.Next(-800, 800), Main.rand.Next(-800, 800));
                    Vector2 realTpPos = player.Center + tpPos;
                    if (realTpPos.Length() < 250 || Framing.GetTileSafely(realTpPos).active() || realTpPos.X < 1 || realTpPos.Y < 1)
                    {
                        goto PositionLoop;
                    }
                    npc.Center = realTpPos;
                    for (int k = 0; k < 65; ++k)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 dustPos = npc.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(dustPos, npc.width, npc.height, 15, 0f, 0f, 0, new Color(255, 255, 255), 1.5f)];
                    }
                    npc.alpha = 0;
                    npc.velocity = npc.DirectionTo(player.Center) * 7;
                    phaseTwoTeleportDashCounter++;
                }
                if (phaseTwoTeleportDashCounter == 7)
                {
                    npc.aiStyle = 16;
                    phaseCount = 0;
                    phaseTwoTeleportDashCounter = 0;
                    npc.alpha = 0;
                }
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.velocity.X < 0 && npc.direction == 1)
            {
                damage = 0;
            }
            if (projectile.velocity.X > 0 && npc.direction == -1)
            {
                damage = 0;
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Player player = Main.player[npc.target];
            if (projectile.velocity.X < 0 && npc.direction == 1)
            {
                npc.life++;
                projectile.active = false;
            }
            if (projectile.velocity.X > 0 && npc.direction == -1)
            {
                npc.life++;
                projectile.active = false;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (imageTimer[0]++ >= imageTimer[1])
            {
                imageTimer[0] = 0;
                images.Add(new FadeImage(npc.Center, new Vector2(Main.rand.Next(-9, 9), Main.rand.Next(-9, 9))));
            }
            if (images.Count > 0)
            {
                for (int a = 0; a < images.Count; a++)
                {
                    FadeImage img = images[a];
                    img.Update();
                    if (img.dead)
                        images.Remove(img);
                    spriteBatch.Draw(Main.npcTexture[npc.type], img.pos - Main.screenPosition, npc.frame, drawColor * img.scale, 0f, new Vector2(npc.frame.Width / 2, npc.frame.Height / 2), 1, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                    //Main.NewText($"{img.scale}");
                }
            }
            return true;
        }
        public class FadeImage
        {
            public Vector2 pos;
            Vector2 velocity;
            Vector2 newVel;
            int lifetime;
            public float scale = 1;
            //float[] velChange = new float[2];
            public bool dead = false;
            public FadeImage(Vector2 p, Vector2 vel) { pos = p; velocity = vel; newVel = new Vector2(vel.X + Main.rand.Next(-6, 6), vel.Y + Main.rand.Next(-6, 6)); }
            public void Update()
            {
                float max = 89.9f;
                if (!dead)
                {
                    int increment = (int)Math.Round(Math.Sqrt(Math.Abs(velocity.X * velocity.Y)));
                    lifetime += increment == 0 ? 2 : increment;
                    pos += velocity;
                    float minus = lifetime / max;
                    //Main.NewText(minus);
                    scale = 1f - minus;
                    velocity = new Vector2(MathHelper.Lerp(newVel.X, velocity.X, scale), MathHelper.Lerp(newVel.Y, velocity.Y, scale));
                }
                if (lifetime >= max)
                    dead = true;
            }
        }
    }
}