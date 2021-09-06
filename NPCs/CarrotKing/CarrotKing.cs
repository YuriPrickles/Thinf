using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs.CarrotKing
{
    [AutoloadBossHead]

    public class CarrotKing : ModNPC
    {
        int illusionSpinCounter = 0;
        int illusionSpinCounter2 = 0;
        int illusionSpinCounter3 = 0;
        int illusionSpinCounter4 = 0;
        int illusionSpinCounter5 = 0;
        int illusionSpinCounter6= 0;
        int illusionSpinCounter7 = 0;
        int illusionSpinCounter8 = 0;
        int carrotrand;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Carrot King");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  //5 is the flying AI
            npc.lifeMax = 40000;   //boss life
            npc.damage = 80;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 200;
            npc.height = 200;
            Main.npcFrameCount[npc.type] = 1;    //boss frame/animation 
            npc.value = Item.buyPrice(0, 40, 75, 45);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[24] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/The_Rooted_Menace");
            npc.netAlways = true;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            Item.NewItem(npc.getRect(), mod.ItemType("Drarrot"));
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.4f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.6f);  //boss damage increase in expermode
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            npc.ai[0]++;
            if (npc.ai[0] >= 160)
            {
                if (npc.ai[0] % 4 == 0)
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<CarrotStriker>());
                if (npc.ai[0] == 240)
                {
                    npc.ai[0] = 0;
                    npc.ai[1]++;
                }
            }

            if (npc.ai[1] >= 5 && npc.ai[0] % 20 == 0)
            {
                float Speed = 12f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 32;  //projectile damage
                int type = mod.ProjectileType("EyeBlast");  //put your projectile
                Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f))) + Main.rand.NextFloat(-0.1f, 0.2f);
                Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                if (npc.ai[1] == 8)
                    npc.ai[1] = 0;
            }

            npc.ai[2]++;
            if (npc.ai[2] >= 360)
            {
                carrotrand = Main.rand.Next(2);
                if (carrotrand == 0)
                {
                    NPC.NewNPC((int)player.Center.X + 300, (int)player.Center.Y + 300, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y - 300, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y + 300, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X + 300, (int)player.Center.Y - 300, ModContent.NPCType<CarrotBiter>());
                }
                if (carrotrand == 1)
                {
                    NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 300, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 300, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y, ModContent.NPCType<CarrotBiter>());
                    NPC.NewNPC((int)player.Center.X + 300, (int)player.Center.Y, ModContent.NPCType<CarrotBiter>());
                }
                npc.ai[2] = 0;
            }

            if (!player.ZoneDirtLayerHeight)
            {
                float Speed = 2f;  //projectile speed
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 89;  //projectile damage
                int type = mod.ProjectileType("EyeBlast");  //put your projectile
                Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f))) + Main.rand.NextFloat(-0.1f, 0.2f);
                Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
            }

            if (npc.life <= 20000)
            {
                npc.ai[3]++;
                if (npc.ai[3] >= 600)
                {
                    float Speed = 1f;  //projectile speed
                    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    int damage = 54;  //projectile damage
                    int type = mod.ProjectileType("EyeBlast");  //put your projectile
                    Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
                    float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f))) + Main.rand.NextFloat(-0.1f, 0.2f);
                    Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                    if (npc.ai[3] == 720)
                    {
                        npc.ai[3] = 0;
                        NPC.NewNPC((int)npc.Center.X + 300, (int)npc.Center.Y, ModContent.NPCType<CarrotBiter>());
                    }
                }
            }

            if (Main.expertMode)
            {

            }
        }
        private const int Sphere = 100;
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            //npc.alpha = (int)(255 * 0.35f);
            Player player = Main.player[npc.target];
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter;
                    double rad = deg * (Math.PI / 180);
                    double dist = 200;
                    drawPos.X = npc.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = npc.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter -= 2;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter2;
                    double rad = deg * (Math.PI / 180);
                    double dist = 250;
                    drawPos.X = npc.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = npc.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter2 += 6;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter3;
                    double rad = deg * (Math.PI / 180);
                    double dist = 340;
                    drawPos.X = npc.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = npc.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter3 += 4;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter4;
                    double rad = deg * (Math.PI / 180);
                    double dist = 170;
                    drawPos.X = npc.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = npc.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter4 -= 5;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter5;
                    double rad = deg * (Math.PI / 180);
                    double dist = 200;
                    drawPos.X = player.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = player.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter5 += 2;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter6;
                    double rad = deg * (Math.PI / 180);
                    double dist = 250;
                    drawPos.X = player.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = player.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter6 -= 6;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter7;
                    double rad = deg * (Math.PI / 180);
                    double dist = 340;
                    drawPos.X = player.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = player.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter7 -= 4;
            }
            if (npc.life <= 20000)
            {
                int spawnAmount = 8;
                for (int i = 0; i < spawnAmount; ++i)
                {
                    float currentRotation = (MathHelper.TwoPi / spawnAmount) * i;
                    Vector2 offset = currentRotation.ToRotationVector2() * 2.5f;
                    Vector2 drawPos;
                    double deg = illusionSpinCounter8;
                    double rad = deg * (Math.PI / 180);
                    double dist = 170;
                    drawPos.X = player.Center.X - (int)(Math.Cos(rad) * dist);
                    drawPos.Y = player.Center.Y - (int)(Math.Sin(rad) * dist);

                    spriteBatch.Draw(mod.GetTexture("NPCs/CarrotKing/CarrotKing"), (drawPos) - Main.screenPosition, null, Color.White * (Color.White.A * 0.35f), 0f, new Vector2(Sphere, Sphere), 1f, SpriteEffects.None, 0f);
                }
                illusionSpinCounter8 += 5;
            }
        }
    }
}