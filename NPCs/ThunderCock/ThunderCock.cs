using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Accessories;
using Thinf.Items.THE_SUPER_COOL_BADASS_LORE;
using Thinf.Items.Weapons;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.ThunderCock
{
    [AutoloadBossHead]
    public class ThunderCock : ModNPC
    {
        int dashtimer = 0;
        int lightningOrbTimer = 0;
        int lightningShotTimer = 0;
        int laserSpreadTimer = 0;
        int cooltimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Cock"); //yaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaay
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lifeMax = 1600;   //boss life
            npc.damage = 13;  //boss damage
            npc.defense = 7;    //boss defense
            npc.knockBackResist = 0.8f;
            npc.width = 108;
            npc.height = 104;
            npc.value = Item.buyPrice(0, 1, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            if (!Main.dedServ)
            {
                npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream").WithPitchVariance(.2f).WithVolume(2f);
            }
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Rock_Solid");
            musicPriority = MusicPriority.BossHigh;
            npc.netAlways = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            int loot = Main.rand.Next(4);
            potionType = ItemID.LesserHealingPotion;   //boss drops

            downedThundercock = true;
            Item.NewItem(npc.getRect(), ModContent.ItemType<ThunderEgg>());
            if (Main.rand.Next(5) == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Transformer>());
            }
            if (!Main.expertMode)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Battery>(), Main.rand.Next(13) + 14);
            }
            else
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Battery>(), Main.rand.Next(21) + 24);
            }

            if (loot == 0)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Climax>());
            }
            if (loot == 1)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<BatteryStaff>());
            }
            if (loot == 2)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<BatteryBlaster>());
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2400;
            npc.damage = 34;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                cooltimer++;
                npc.velocity.Y += 1;
                if (cooltimer >= 120)
                {
                    npc.active = false;
                    cooltimer = 0;
                }
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            npc.spriteDirection = npc.direction;

            if (npc.spriteDirection == -1)
            {
                npc.rotation = npc.AngleTo(player.Center) + MathHelper.ToRadians(-180);
            }
            else
            {
                npc.rotation = npc.AngleTo(player.Center);
            }
            dashtimer++;
            if (dashtimer >= 360)
            {
                npc.rotation = npc.velocity.ToRotation() + MathHelper.ToRadians(-180 * -npc.direction);
                if (dashtimer == 301)
                {
                    Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream"), player.Center, 0);
                }
                npc.velocity = new Vector2(0, -14);
                if (dashtimer >= 420)
                {
                    dashtimer = 0;
                }
            }
            else
            {
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(0, 300)) * 7;
            }
            lightningOrbTimer++;
            if (lightningOrbTimer >= 480)
            {
                Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream"), player.Center, 0);
                int damage = 6;
                if (Main.expertMode)
                {
                    damage = 14;
                }
                int type = ProjectileID.CultistBossLightningOrb;
                Projectile proj = Main.projectile[Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y - 300), Vector2.Zero, type, damage, 0f, 0)];
                proj.alpha = 0;
                proj.ai[0] = 121;
                lightningOrbTimer = 0;
            }
            if (lightningOrbTimer >= 360)
            {
                int dustSpawnAmount = 32;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y - 300) + dustOffset * 80, DustID.Electric, null, 0, default, 0.7f);
                    dust.noGravity = true;
                }
            }

            lightningShotTimer++;
            if (lightningShotTimer >= 180)
            {
                float Speed = 9f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 9;
                if (Main.expertMode)
                {
                    damage = 18;
                }
                int type = mod.ProjectileType("ThunderShot");  //put your projectile
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                lightningShotTimer = 0;
            }

            if (Main.expertMode)
            {
                laserSpreadTimer++;
                if (laserSpreadTimer >= 60 && laserSpreadTimer % 25 == 0)
                {
                    float Speed = 4f;  //projectile speed
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 12;  //projectile damage
                    int type = ProjectileID.EyeBeam;  //put your projectile
                    Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                    float rotation = Main.rand.Next(360);
                    Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    if (laserSpreadTimer == 180)
                        laserSpreadTimer = -60;
                }
            }
        }
    }
}