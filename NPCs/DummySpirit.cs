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
	public class DummySpirit : ModNPC
	{
		int var1 = 0;
		int healTimer = 0;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 3000;
			npc.damage = 0;
			npc.defense = 0;
			npc.knockBackResist = 0f;
			npc.width = 18;
			npc.height = 36;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.lavaImmune = false;
			npc.noGravity = false;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit54;
			npc.DeathSound = SoundID.NPCDeath52;
			npc.netAlways = true;
			npc.alpha = 127;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			
		}

        public override void AI()
        {
			npc.spriteDirection = npc.direction;
			npc.TargetClosest(true);
			Player P = Main.player[npc.target];
			npc.netUpdate = true;
			var1++;
			healTimer++;
			if (healTimer >= 30)
			{
				for (int i = 0; i < Main.maxNPCs; ++i)
				{
					NPC target = Main.npc[i];
					if (target.active && !target.friendly && !target.dontTakeDamage && target.Distance(npc.Center) <= 1000 && target.type != ModContent.NPCType<DummySpirit>())
					{
						if (Main.rand.Next(15) == 0)
                        {
							target.life += 500;
							target.HealEffect(500);
							CombatText text = Main.combatText[CombatText.NewText(target.getRect(), Color.Lime, "Mega Dummy Heal!")];
							text.lifeTime = 240;
						}
                        else
						{
							target.life += 10;
							target.HealEffect(10);
						}
					}
				}
				healTimer = 0;
			}
			if (var1 == 500)
            {
				
				float Speed = 12f;  //projectile speed
				Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				int damage = 3;  //projectile damage
				int type = ProjectileID.DD2DarkMageBolt;  //put your projectile
				Main.PlaySound(98, (int)npc.position.X, (int)npc.position.Y, 17);
				float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
				Projectile projectile = Main.projectile[Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0)];
				projectile.hostile = true;
				projectile.friendly = false;
				var1 = 0;
			}
        }
    }
}
