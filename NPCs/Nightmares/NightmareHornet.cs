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
    public class NightmareHornet : ModNPC
    {
        int potatoShoot = 0;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            npc.aiStyle = 5;
            npc.lifeMax = 9000;   //boss life
            npc.damage = 69;  //boss damage
            npc.defense = 38;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 62;
            npc.height = 60;
            npc.value = Item.buyPrice(0, 6, 45, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            aiType = NPCID.Crimera;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<Nightmare>()))
            {
                return false;
            }
            return true;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<NightmareFuel>(), Main.rand.Next(4) + 3);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (nightmare)
            {
                return 0.30f / 30;
            }
            return 0;
        }
        public override void AI()
        {
            if (npc.life > 4000)
            {
                npc.alpha += 20;
                if (npc.alpha >= 255)
                {
                    npc.alpha = 0;
                }
            }
            if (npc.life <= 4000)
            {
                npc.noTileCollide = true;
                npc.alpha += 20;
                if (npc.alpha >= 80)
                {
                    npc.alpha = 0;
                }
            }
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Lighting.AddLight(npc.Center, new Vector3(255, 0 ,0) / 255);
            Player player = Main.player[npc.target];
            npc.netUpdate = true;
            potatoShoot++;
            npc.rotation = npc.velocity.ToRotation();
            if (potatoShoot >= 70 && potatoShoot % 40 == 0)
            {
                Vector2 potatoVelocity = Vector2.Normalize(player.Center - npc.Center) * 8;
                Main.PlaySound(SoundID.DD2_LightningBugDeath, npc.Center);
                Projectile projectile = Main.projectile[Projectile.NewProjectile(npc.Center, potatoVelocity.RotatedByRandom(MathHelper.ToRadians(10)), ProjectileID.CultistBossFireBallClone, npc.damage, 6)];
                projectile.tileCollide = false;
                if (potatoShoot == 140)
                {
                    Main.PlaySound(SoundID.NPCDeath52.WithPitchVariance(4), npc.Center);
                    npc.velocity = npc.DirectionTo(player.Center) * 10;
                    potatoShoot = 0;
                }
            }
        }
    }
}
