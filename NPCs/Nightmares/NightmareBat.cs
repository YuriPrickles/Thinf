using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Items;
using static Thinf.MyPlayer;

namespace Thinf.NPCs.Nightmares
{
    public class NightmareBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.CaveBat);
            npc.aiStyle = 14;  //5 is the flying AI
            npc.lifeMax = 8500;   //boss life
            npc.damage = 100;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0.04f;
            npc.width = 44;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 5, 232, 90);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.netAlways = true;
            animationType = NPCID.Harpy;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (target.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                target.AddBuff(BuffID.Obstructed, 60);
                target.AddBuff(BuffID.Weak, Thinf.MinutesToTicks(2));
                target.AddBuff(BuffID.Blackout, Thinf.ToTicks(10));
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(1) + 2);
        }
        public override void AI()
        {
            if (npc.life > 5000)
            {
                npc.alpha += 20;
                if (npc.alpha >= 255)
                {
                    npc.alpha = 0;
                }
            }
            if (npc.life <= 5000)
            {
                npc.noTileCollide = true;
                npc.alpha += 20;
                if (npc.alpha >= 80)
                {
                    npc.alpha = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                return false;
            }
            else
            {
                if (npc.life <= 5000)
                {
                    Player player = Main.player[npc.target];
                    Texture2D texture = Main.npcTexture[npc.type]; //mod.GetTexture("NPCs/Nightmares/NightmareBat");

                    for (int i = 0; i < 8; i++)
                    {
                        float rotation = MathHelper.TwoPi * i / 8f;
                        Vector2 drawpos = player.Center + (npc.Center - player.Center).RotatedBy(i * MathHelper.ToRadians(360f / 8)) - Main.screenPosition;
                        if (i != 0)
                        {
                            spriteBatch.Draw(texture, drawpos, npc.frame, Color.White * (1f - npc.alpha / 255f), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                        }
                    }
                }
            }
            return true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (nightmare)
            {
                return 0.50f / 30;
            }
            return 0;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (npc.life <= 5000)
            {
                return false;
            }
            return base.DrawHealthBar(hbPosition, ref scale, ref position);
        }
    }
}
