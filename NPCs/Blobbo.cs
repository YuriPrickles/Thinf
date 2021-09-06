using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;

namespace Thinf.NPCs
{
    public class Blobbo : ModNPC
    {
        bool shieldMode = true;
        int shieldHP = 10;
        int shieldDamageIFrame = 0;
        int spinTimer = 0;
        int shieldRespawnTimer = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 700;
            npc.damage = 14;
            npc.defense = 6;
            npc.knockBackResist = 0.2f;
            npc.width = 30;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            npc.boss = true;
            music = MusicID.UndergroundCorruption;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.EbonstoneBlock, Main.rand.Next(60) + 50);
            Item.NewItem(npc.getRect(), ItemID.RottenChunk, Main.rand.Next(8) + 4);
            Item.NewItem(npc.getRect(), ItemID.VileMushroom, Main.rand.Next(5) + 5);
            Item.NewItem(npc.getRect(), ModContent.ItemType<BlobCore>());
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (npc.velocity != Vector2.Zero && spinTimer < 280)
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
                    Dust dust = Dust.NewDustPerfect(npc.Center + dustOffset * 50, DustID.VilePowder);
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
                                CombatText.NewText(npc.getRect(), Color.MediumPurple, 1);
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
            spinTimer++;
            if (spinTimer >= 280)
            {
                npc.rotation = npc.velocity.ToRotation();
                npc.velocity = npc.DirectionFrom(player.Center).RotatedBy(8) * 8;
                if (spinTimer % 10 == 0)
                {
                    NPC vileSpit = Main.npc[NPC.NewNPC((int)(npc.Center.X + npc.velocity.X), (int)(npc.Center.Y + npc.velocity.Y), NPCID.VileSpit)];
                    vileSpit.velocity = vileSpit.DirectionTo(player.Center) * 7f;
                    vileSpit.damage = 12;
                }
                if (spinTimer == 410)
                {
                    spinTimer = 0;
                }
            }
        }
    }
}
