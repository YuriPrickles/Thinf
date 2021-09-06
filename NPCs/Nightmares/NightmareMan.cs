using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using static Thinf.MyPlayer;

namespace Thinf.NPCs.Nightmares
{
	public class NightmareMan : ModNPC
	{
		public override void SetStaticDefaults()
		{

		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 1000;
			npc.damage = 100;
			npc.defense = 20;
			npc.knockBackResist = 0.1f;
			npc.width = 48;
			npc.height = 48;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = true;
			npc.HitSound = SoundID.NPCHit54;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Weak, Thinf.MinutesToTicks(2));
			target.AddBuff(BuffID.Blackout, Thinf.ToTicks(10));
		}
		public override void AI()
		{
			Lighting.AddLight(npc.Center, (new Vector3(255, 75, 75)) / 255);
			if (NPC.AnyNPCs(ModContent.NPCType<SoulCatcher.SoulCatcher>()))
            {
				NPC soulCatcher = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<SoulCatcher.SoulCatcher>())];
				npc.velocity = npc.DirectionTo(soulCatcher.Center) * 1.1230231254f;
				if (npc.Hitbox.Intersects(soulCatcher.Hitbox))
				{
					npc.life = 0;
					soulCatcher.life += 5000;
					soulCatcher.HealEffect(5000);
                }
            }
			else
            {
				npc.life = 0;
            }
		}
	}
}
