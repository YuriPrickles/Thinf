using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.NPCs.PlayerDrones
{
    public class YulanMagnoliaBird : ModNPC
    {
        int frameCountTimer = 0;
        int shotCooldown = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 25;
            npc.damage = 0;
            npc.defense = 200000;
            npc.knockBackResist = 0.1f;
            npc.width = 50;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.friendly = true;
        }

        public override bool CheckActive()
        {
            return false;
        }
        public override void NPCLoot()
        {
            for (int g = 0; g < 7; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(npc.position.X + npc.width / 2 - 24f, npc.position.Y + npc.height / 2 - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
            Player player = Main.player[npc.target];
            player.GetModPlayer<DroneControls>().playerIsControllingDrone = false;
            player.AddBuff(ModContent.BuffType<DroneRecharge>(), Thinf.MinutesToTicks(1));
        }
        public override void AI()
        {
            npc.timeLeft = 2;
            npc.rotation = npc.AngleTo(Main.MouseWorld) - MathHelper.ToRadians(180);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (player.active && !player.dead)
            {
                player.GetModPlayer<DroneControls>().dronePos = npc.position - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);

                player.GetModPlayer<DroneControls>().droneControlled = npc.whoAmI;

                player.GetModPlayer<DroneControls>().droneType = npc.type;


                player.GetModPlayer<DroneControls>().ModifyScreenPosition();
            }
            if (Thinf.DroneCancel.Current)
            {
                npc.StrikeNPC(1000000, 0, 0);
            }
            if (Thinf.DroneUp.JustPressed)
            {
                npc.velocity.Y = 0;
                npc.velocity.Y -= 5.7f;
            }
            else
            {
                if (npc.velocity.Y <= 2)
                {
                    npc.velocity.Y += 0.22f;
                }
            }
            if (Thinf.DroneLeft.Current && npc.velocity.X >= -2)
            {
                npc.velocity.X -= 0.2f;
            }
            if (Thinf.DroneRight.Current && npc.velocity.X <= 2)
            {
                npc.velocity.X += 0.2f;
            }
            shotCooldown++;
            if (shotCooldown >= 44)
            {
                if (Thinf.DroneSemiAutoAttack.JustPressed)
                {
                    Projectile.NewProjectileDirect(npc.Center, Vector2.Normalize(Main.MouseWorld - npc.Center) * 5, ModContent.ProjectileType<YulanBomb>(), 25, 0, player.whoAmI);
                    Main.PlaySound(SoundID.Item66, npc.Center);
                    shotCooldown = 0;
                }
            }

            frameCountTimer++;
            if (frameCountTimer == 6)
            {
                npc.frame.Y = 0;
            }
            if (frameCountTimer == 12)
            {
                npc.frame.Y = 36;
                frameCountTimer = 0;
            }
        }
    }
}
