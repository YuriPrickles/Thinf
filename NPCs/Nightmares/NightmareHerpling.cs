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
    public class NightmareHerpling : ModNPC
    {
        int tpTimer = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.width = 76;
            npc.height = 54;
            npc.aiStyle = 41;
            npc.lifeMax = 15700;   //boss life
            npc.damage = 140;  //boss damage
            npc.defense = 55;    //boss defense
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 14, 60, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath33;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.netAlways = true;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(2) + 3);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                return false;
            }
            return true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            Lighting.AddLight(npc.Center, new Vector3(255, 0, 0) / 255);
            tpTimer++;
            if (tpTimer >= 300)
            {
                npc.alpha += 5;
                if (npc.alpha >= 255)
                {
                    npc.Center = player.Center + new Vector2(0, -150);
                    Vector2 position = npc.Center;
                    for (int i = 0; i < 40; ++i)
                    {
                        Dust dust = Main.dust[Terraria.Dust.NewDust(position, 76, 54, 109, npc.velocity.X, npc.velocity.Y, 0, new Color(255, 255, 255), 3.421053f)];
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                    tpTimer = 0;
                    npc.alpha = 0;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (target.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                npc.HealEffect(damage);
                npc.life += damage;
                target.AddBuff(BuffID.Obstructed, 20);
                target.AddBuff(BuffID.Ichor, Thinf.MinutesToTicks(2));
                target.AddBuff(BuffID.Bleeding, Thinf.ToTicks(10));
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (nightmare)
            {
                return 0.24f / 30;
            }
            return 0;
        }
    }
}
