using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.NPCs
{
    public class Detonato : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Detonato");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = 3;  //5 is the flying AI
            npc.lifeMax = 160;   //boss life
            npc.damage = 200;  //boss damage
            npc.defense = 14;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(npc.position, npc.velocity, ProjectileID.DD2OgreSmash, 0, 0, Main.myPlayer);
            npc.life = 0;
            target.AddBuff(BuffID.OnFire, 90);
            Main.PlaySound(4, npc.Center, 43);
        }
    }
}
