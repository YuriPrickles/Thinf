using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using static Thinf.ModNameWorld;

namespace Thinf.NPCs.TownNPCs         //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
	public class MobileSprout : ModNPC
	{
		int frameTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tired Mobile Sprout");
			Main.npcFrameCount[npc.type] = 2; //this defines how many frames the npc sprite sheet has
		}
		public override void SetDefaults()
		{
			npc.friendly = true;  //this defines if the npc can hur you or not()
			npc.width = 38; //the npc sprite width
			npc.height = 34;  //the npc sprite height
			npc.aiStyle = -1; //this is the npc ai style, 7 is Pasive Ai
			npc.damage = 17;
			npc.defense = 12;  //the npc defense
			npc.lifeMax = 79;// the npc life
			npc.dontTakeDamage = true;
			npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
			npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
			npc.knockBackResist = 0f;  //the npc knockback resistance
		}

        public override void AI()
        {
			npc.velocity.Y += 0.5f;
			frameTimer++;
			if (frameTimer == 15)
            {
				npc.frame.Y = 0;
			}
			if (frameTimer == 30)
			{
				npc.frame.Y = 34;
				frameTimer = 0;
			}
		}
        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Talk";
		}
		public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
		{
			Player player = Main.player[Player.FindClosest(npc.Center, 10000, 10000)];
            if (firstButton)
			{
				if (player.statManaMax2 > 20)
				{
					Main.npcChatText = $"Tired Mobile Sprout used PSI Magnet a!\nDrained 20 max mana from {player.name}!";
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/TrollMagnet").WithVolume(1.5f));
					player.statManaMax -= 20;
					player.AddBuff(BuffID.Silenced, Thinf.MinutesToTicks(60));
				}
                else
				{
					Main.npcChatText = $":D";
				}
            }

        }
		public override string GetChat()
		{
			return "plant noises";
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedSlimeKing && !spawnInfo.player.ZoneDungeon && spawnInfo.player.ZoneOverworldHeight && Main.dayTime && !NPC.AnyNPCs(npc.type))
			{
				return SpawnCondition.OverworldDaySlime.Chance * 0.02f;
			}
			return 0;
		}
	}
}