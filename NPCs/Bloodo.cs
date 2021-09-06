using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Projectiles;

namespace Thinf.NPCs
{
    public class Bloodo : ModNPC
    {
        float rotat = MathHelper.ToRadians(0);
        bool shieldMode = true;
        int shieldHP = 10;
        int shieldDamageIFrame = 0;
        int spewTimer = 0;
        int shieldRespawnTimer = 0;
        int spewTimer2 = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 700;
            npc.damage = 34;
            npc.defense = 6;
            npc.knockBackResist = 0.2f;
            npc.width = 48;
            npc.height = 62;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            npc.boss = true;
            music = MusicID.UndergroundCrimson;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.CrimstoneBlock, Main.rand.Next(60) + 50);
            Item.NewItem(npc.getRect(), ItemID.Vertebrae, Main.rand.Next(8) + 4);
            Item.NewItem(npc.getRect(), ItemID.ViciousMushroom, Main.rand.Next(5) + 5);
            Item.NewItem(npc.getRect(), ModContent.ItemType<BloodCore>());
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (spewTimer < 320 && shieldMode)
            {
                spewTimer2++;
                if (spewTimer2 == 25)
                {
                    Projectile vileSpit = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<BloodBall>(), 22, 0)];
                    vileSpit.velocity = vileSpit.DirectionTo(player.Center) * 7f;
                    vileSpit.ai[0] = 1;
                    spewTimer2 = 0;
                }
            }
            if (npc.velocity != Vector2.Zero && spewTimer < 320)
            {
                npc.rotation = MathHelper.ToRadians(npc.direction * 14);
            }
            if (npc.Distance(player.Center) > 20)
            {
                npc.velocity = npc.DirectionTo(player.Center) * 4f;
            }

            if (shieldMode)
            {
                var fatHitbox = npc.Hitbox;
                fatHitbox.Inflate(50, 50);
                npc.dontTakeDamage = true;
                int dustSpawnAmount = 32;
                for (int i = 0; i < dustSpawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                    Vector2 dustOffset = currentRotation.ToRotationVector2() * 2.5f;
                    Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 50, DustID.ViciousPowder);
                    dust.noGravity = true;
                }
                for (int g = 0; g < Main.maxProjectiles; g++)
                {
                    Projectile projectile = Main.projectile[g];
                    if (projectile.type == ProjectileID.PurificationPowder || projectile.type == ProjectileID.PureSpray)
                    {
                        if (fatHitbox.Intersects(projectile.Hitbox))
                        {
                            shieldDamageIFrame++;
                            if (shieldDamageIFrame == 15)
                            {
                                CombatText.NewText(npc.getRect(), Color.DarkRed, 1);
                                shieldHP--;
                                shieldDamageIFrame = 0;
                            }
                        }
                    }
                }
                if (shieldHP <= 0)
                {
                    shieldMode = false;
                }
            }
            else
            {
                shieldRespawnTimer++;
                if (shieldRespawnTimer == 600)
                {
                    shieldMode = true;
                    shieldHP = 10;
                    shieldDamageIFrame = 0;
                }
                npc.dontTakeDamage = false;
            }
            spewTimer++;
            if (spewTimer >= 320)
            {
                npc.rotation = npc.velocity.ToRotation();
                rotat += 0.1f;
                npc.velocity = npc.DirectionTo(npc.Center + Vector2.One.RotatedBy(rotat) * 320f) * 14;
                if (spewTimer % 4 == 0)
                {
                    Projectile vileSpit = Main.projectile[Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<BloodBall>(), 32, 0)];
                    vileSpit.velocity = Vector2.Normalize(new Vector2(Main.rand.Next(-10, 10), -10)) * 7f;
                }
                if (spewTimer == 640)
                {
                    rotat = MathHelper.ToRadians(0);
                    spewTimer = 0;
                }
            }
        }
    }
}
