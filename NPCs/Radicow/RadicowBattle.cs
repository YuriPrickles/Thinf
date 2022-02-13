using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.GatewaysForTesting;
using Thinf.NPCs.Bounties;

namespace Thinf.NPCs.Radicow       //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    [AutoloadHead]
    public class RadicowBattle : ModNPC
    {
        int frameNumber = 0;
        int laserBlastTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Battle Cow");
            Main.npcFrameCount[npc.type] = 6;
        }
        //public override bool Autoload(ref string name)
        //{
        //	name = "StarPrince";
        //	return mod.Properties.Autoload;
        //}
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                frameNumber++;
                if (frameNumber >= 6)
                {
                    frameNumber = 0;
                }
                npc.frame.Y = frameNumber * (480 / 6);
            }
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;  //this defines if the npc can hur you or not()
            npc.width = 88; //the npc sprite width
            npc.height = 80;  //the npc sprite height
            npc.aiStyle = -1; //this is the npc ai style, 7 is Pasive Ai
            npc.defense = 25;  //the npc defense
            npc.lifeMax = 50000;// the npc life
            npc.HitSound = SoundID.NPCHit4;  //the npc sound when is hit
            npc.DeathSound = SoundID.NPCDeath14;  //the npc sound when he dies
            npc.knockBackResist = 0.0f;  //the npc knockback resistanc
            npc.noTileCollide = true;
        }


        public override void AI()
        {
            npc.spriteDirection = -npc.direction;
            Player player = Main.player[Player.FindClosest(npc.Center, 10000, 10000)];
            if (NPC.AnyNPCs(ModContent.NPCType<Poltergate>()) || NPC.AnyNPCs(ModContent.NPCType<Pinball>()))
            {
                #region If hunting Poltergate the Pussyclart
                if (NPC.AnyNPCs(ModContent.NPCType<Poltergate>()))
                {
                    NPC poltergate = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Poltergate>())];
                    if (poltergate.ai[0] == 1)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    if (poltergate.ai[0] == 0)
                    {
                        npc.velocity = npc.DirectionTo(poltergate.Center) * 2f;
                        laserBlastTimer++;
                        if (laserBlastTimer >= 120 && laserBlastTimer % 7 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(poltergate.Center - npc.Center) * 5, ProjectileID.GreenLaser, 50, 0, player.whoAmI);
                            if (laserBlastTimer >= 300)
                            {
                                laserBlastTimer = 0;
                            }
                        }
                    }

                    if (poltergate.ai[0] == 3)
                    {
                        npc.velocity = npc.DirectionTo(poltergate.Center) * 5f;
                        laserBlastTimer++;
                        if (laserBlastTimer >= 120 && laserBlastTimer % 4 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(poltergate.Center - npc.Center) * 6, ProjectileID.GreenLaser, 50, 0);
                            if (laserBlastTimer >= 300)
                            {
                                laserBlastTimer = 0;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                npc.velocity = npc.DirectionTo(player.Center + new Vector2(-50 * player.direction, -50)) * 3;
            }
        }
        

        public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<Poltergate>()))
            {
                return "Well? What are you waiting for?";
            }
            return "WHAT ARE YOU DOING? KEEP SHOOTING!";
        }

        public override string TownNPCName()
        {
            return "Radicow";
        }
    }
}