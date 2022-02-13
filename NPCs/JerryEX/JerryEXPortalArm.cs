using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs.JerryEX
{
    public class JerryEXPortalArm : ModNPC
    {
        // THIS IS NOT A STRAWBERRY CREPE COOKIE RIP OFF TRUST ME

        int phaseCount = 0;
        int phaseZeroTimer = 0;
        int movementTimer = 0;
        int movedir = 1;
        int phaseOneTimer = 0;
        Vector2 destination;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cannon Arm");
            Main.npcFrameCount[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.aiStyle = -1;
            npc.lifeMax = 50000;
            npc.damage = 45;
            npc.defense = 25;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 54;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.netAlways = true;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
            }
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            if (NPC.AnyNPCs(ModContent.NPCType<JerryEXMain>()))
            {
                NPC jerry = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<JerryEXMain>())];


                npc.velocity += Vector2.Lerp(npc.velocity, npc.DirectionTo(destination) * 9f, .1f);
                //Above just slowly sets the velocity to go towards the target position.
                if (npc.velocity.Length() < 20f)
                    npc.velocity *= .5f; //Just some value to make it slow down, but not immediately stop

                if (jerry.ai[0] == 150)
                {
                    Dust dust;
                    Vector2 position = npc.Center;
                    dust = Dust.NewDustDirect(position, 0, 0, 31, 0f, -2.325581f, 0, new Color(255, 255, 255), 1.162791f);
                    dust.fadeIn = 0.8372093f;
                }
                if (jerry.ai[0] != 75 && jerry.ai[0] != -12)
                {
                    if (phaseCount == 0)
                    {
                        npc.rotation = npc.AngleTo(player.Center) + MathHelper.ToRadians(-90);
                        destination = jerry.Center + new Vector2(-100, -45);
                        movementTimer++;
                        if (movementTimer >= 15)
                        {
                            destination += new Vector2(20, 0) * movedir;
                            movementTimer = 0;
                            movedir *= -1;
                        }
                        phaseZeroTimer++;
                        if (phaseZeroTimer == 600)
                        {
                            if (jerry.ai[0] == 150)
                            {
                                NPC tomatoMan = Main.npc[NPC.NewNPC((int)npc.Center.X + 50, (int)npc.Center.Y, ModContent.NPCType<TomatoBat>())];
                                tomatoMan.GivenName = "Angel of death";
                                tomatoMan = Main.npc[NPC.NewNPC((int)npc.Center.X - 50, (int)npc.Center.Y, ModContent.NPCType<TomatoBat>())];
                                tomatoMan.GivenName = "Angel of death";
                            }
                        }
                        if (phaseZeroTimer >= 1000)
                        {
                            if (!Main.dedServ)
                                Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/JerryPortalShot").WithVolume(1.5f));
                            phaseZeroTimer = 0;
                            phaseCount = 1;
                        }
                    }

                    if (phaseCount == 1)
                    {
                        phaseOneTimer++;
                        if (phaseOneTimer >= 160)
                        {
                            npc.velocity.X = 4 * npc.direction;
                            npc.rotation = npc.velocity.X * 0.05f;
                            if (jerry.ai[0] == 150)
                            {
                                if (phaseOneTimer % 60 == 0)
                                {
                                    NPC tomatoMan = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<RoyalTomatoMan>())];
                                }
                            }
                            else
                            {
                                if (phaseOneTimer % 80 == 0)
                                {
                                    NPC tomatoMan = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<RoyalTomatoMan>())];
                                }
                            }
                            if (phaseOneTimer >= 400)
                            {
                                phaseOneTimer = 0;
                                phaseCount = 0;
                            }
                        }
                        else
                        {
                            npc.rotation = 0;
                            npc.velocity = Vector2.Zero;
                        }
                    }
                }
            }
            else
            {
                npc.active = false;
            }
        }
        private int GetFrame(int framenum)
        {
            return npc.height * framenum;
        }
    }
}
