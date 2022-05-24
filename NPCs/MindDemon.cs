using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.NPCs
{
	public class MindDemon : ModNPC
	{
		bool hasSetDir = false;
		int spawnDir = 1;
		int spawnPos = Main.rand.Next(-Main.screenHeight / 2, Main.screenHeight / 2);
		int timer = 0;
		bool windUp = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mind Demon");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 7;
			npc.damage = 77;
			npc.defense = 7;
			npc.knockBackResist = 0f;
			npc.width = 136;
			npc.height = 56;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 7f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.dontTakeDamage = true;
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
			if (!hasSetDir)
			{
				int dirRand = Main.rand.Next(2);
				if (dirRand == 0)
				{
					spawnDir = -1;
				}
				else
				{
					spawnDir = 1;
				}
				hasSetDir = true;
			}
			npc.direction = spawnDir;
			timer++;
			if (timer <= 200)
			{
				if (spawnDir == 1)
				{
					npc.Center = new Vector2(Main.screenPosition.X - 24, player.Center.Y + spawnPos);
				}
				else
				{
					npc.Center = new Vector2((Main.screenPosition.X + Main.screenWidth) + 24, player.Center.Y + spawnPos);
				}
				if (timer % 100 == 0)
				{
					Projectile proj1 = Projectile.NewProjectileDirect(npc.Center + new Vector2(38, 0), Vector2.Normalize(player.Center - npc.Center) * 8, ProjectileID.AmethystBolt, 22, 0);
					proj1.tileCollide = false;
					proj1.hostile = true;
					proj1.friendly = false;
					proj1.magic = false;
					Projectile proj2 = Projectile.NewProjectileDirect(npc.Center + new Vector2(38, 0), (Vector2.Normalize(player.Center - npc.Center) * 8).RotatedBy(MathHelper.ToRadians(45)), ProjectileID.AmethystBolt, 22, 0);
					proj1.tileCollide = false;
					proj1.hostile = true;
					proj1.friendly = false;
					proj1.magic = false;
					Projectile proj3 = Projectile.NewProjectileDirect(npc.Center + new Vector2(38, 0), (Vector2.Normalize(player.Center - npc.Center) * 8).RotatedBy(MathHelper.ToRadians(-45)), ProjectileID.AmethystBolt, 22, 0);
					proj1.tileCollide = false;
					proj1.hostile = true;
					proj1.friendly = false;
					proj1.magic = false;
				}
			}
			else
			{	if (spawnDir == 1)
				{
					if (npc.velocity.X > -2 && !windUp)
					{
						npc.velocity.X -= 0.2f;
					}
					else
					{
						npc.velocity.X += 0.8f;
						windUp = true;
					}
				}
				else
				{
					if (npc.velocity.X < 2 && !windUp)
					{
						npc.velocity.X += 0.2f;
					}
					else
					{
						npc.velocity.X -= 0.8f;
						windUp = true;
					}
				}
			}
			if (timer >= Thinf.ToTicks(15))
			{
				npc.active = false;
			}
		}
		//public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		//{
		//    Player player = Main.player[npc.target];
		//    Texture2D texture = Main.npcTexture[npc.type]; //mod.GetTexture("NPCs/Nightmares/NightmareBat");

		//    for (int i = 0; i < 3; i++)
		//    {
		//        float rotation = MathHelper.TwoPi * i / 8f;
		//        Vector2 drawpos = player.Center + (npc.Center - player.Center).RotatedBy(i * MathHelper.ToRadians(360f / 8)) - Main.screenPosition;
		//        if (i != 0)
		//        {
		//            spriteBatch.Draw(texture, drawpos, npc.frame, Color.White * (1f - npc.alpha / 255f), npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		//        }
		//    }
		//    return true;
		//}
	}
}
