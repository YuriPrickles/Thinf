using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;

namespace Thinf.NPCs
{
    public class CherryBomber : ModNPC
    {
        int groundedTimer = 0;
        int grenadeCount = 0;
        bool isExploding = false;
        int boomTimer = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50;
            npc.damage = 11;
            npc.defense = 8;
            npc.knockBackResist = 0.6f;
            npc.width = 32;
            npc.height = 26;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
        }
        public override bool CheckDead()
        {
            npc.life = 1;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            isExploding = true;
            return false;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<Cherry>(), Main.rand.Next(3) + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1 && !spawnInfo.player.ZoneDungeon && spawnInfo.player.ZoneOverworldHeight && Main.dayTime)
            {
                return SpawnCondition.OverworldDaySlime.Chance * 0.04f;
            }
            return 0;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (!isExploding)
            {
                if (npc.velocity.Y == 0)
                {
                    groundedTimer++;
                    npc.velocity.X = 0;
                    if (groundedTimer == 60)
                    {
                        grenadeCount++;
                        Jump(3, 7);
                        if (grenadeCount % 2 == 0)
                        {
                            for (int i = 0; i < Main.rand.Next(2) + 2; ++i)
                            {
                                Projectile projectile = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-4, 4), -3), ProjectileID.Grenade, 15, 0, Main.myPlayer);
                                projectile.hostile = true;
                                projectile.friendly = false;
                            }
                        }
                        if (grenadeCount >= 6)
                        {
                            for (int i = 0; i < Main.rand.Next(3) + 3; ++i)
                            {
                                Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-4, 4), -3), ProjectileID.SmokeBomb, 0, 0, Main.myPlayer);
                            }
                            grenadeCount = 0;
                        }
                        groundedTimer = 0;
                    }
                }
            }
            else
            {
                npc.noGravity = true;
                npc.velocity = Vector2.Zero;
                boomTimer++;
                //if (boomTimer >= 40 && npc.scale <= 2.4f)
                //{
                //    npc.scale += 0.04f;
                //}
                if (boomTimer == 90)
                {
                    npc.damage = 35;
                    npc.alpha = 255;
                    var oldCenter = npc.Center;
                    npc.height *= 10;
                    npc.width *= 10;
                    npc.Center = oldCenter;
                }
                if (boomTimer == 95)
                {
                    for (int i = 0; i < Main.maxPlayers; ++i)
                    {
                        if (Main.player[i].active && Main.player[i].Hitbox.Intersects(npc.Hitbox))
                        {
                            Main.player[i].AddBuff(BuffID.Burning, 180);
                        }
                    }
                    int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    npc.NPCLoot();
                    Projectile.NewProjectileDirect(npc.Center, Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 0, Main.myPlayer);
                    Main.PlaySound(SoundID.Item62, npc.Center);
                    npc.active = false;
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            if (isExploding && boomTimer % 10 == 0)
            {
                spriteBatch.Draw(mod.GetTexture("NPCs/CherryBomberButHesGonnaFuckingExplode"), npc.position - Main.screenPosition, Color.White);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (isExploding && boomTimer % 10 == 0)
            {
                return false;
            }
            return true;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return !isExploding;
        }
        private void Jump(int horizontalSpeed, int verticalSpeed)
        {
            npc.velocity.X = horizontalSpeed * npc.direction;
            npc.velocity.Y -= verticalSpeed;
        }
    }
}