using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items;
using Thinf.Items.Weapons;
using Thinf.Items.Weapons.FarmerWeapons;

namespace Thinf.NPCs
{
    internal class Chicken : ModNPC
    {
        public bool hasMate = false;
        public bool horny = false;
        public int matingTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chicken");
            Main.npcFrameCount[npc.type] = 4;
            Main.npcCatchable[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Bunny);
            npc.width = 32;
            npc.height = 32;
            npc.damage = 0;
            npc.defense = 5;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            if (!Main.dedServ)
            {
                npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/ChickenScream").WithPitchVariance(.5f).WithVolume(2f);
            }
            npc.npcSlots = 0.5f;
            npc.noGravity = false;
            npc.catchItem = (short)ModContent.ItemType<ChickenItem>();
            npc.aiStyle = 7;
            npc.knockBackResist = 0.1f;
            npc.friendly = true; // We have to add this and CanBeHitByItem/CanBeHitByProjectile because of reasons.
            aiType = NPCID.Bunny;
            animationType = NPCID.Zombie;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.Center, ModContent.ItemType<RawChicken>(), Main.rand.Next(3) + 2);
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileID.Seed)
            {
                npc.townNPC = true;
                horny = true;
                npc.life += 10;
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.netUpdate = true;

            var allNPCs = FindAllNPCs(npc.type, npc.whoAmI);
            if (player.HeldItem == Main.item[ItemID.Seed])
            {
                npc.velocity = npc.DirectionTo(player.Center) * 3f;
                npc.aiStyle = -1;
            }
            else
            {
                npc.aiStyle = 7;
            }

            if (horny && NPC.CountNPCS(npc.type) > 1)
            {
                if (allNPCs.Count > 0)
                {
                    Chicken chicken = Main.npc[Main.rand.Next(allNPCs)].modNPC as Chicken;
                    if (chicken.horny)
                    {
                        hasMate = true;
                        chicken.hasMate = true;
                        npc.spriteDirection = -chicken.npc.direction;
                        if (npc.Hitbox.Intersects(chicken.npc.Hitbox))
                        {
                            npc.velocity = Vector2.Zero;
                            chicken.npc.velocity = Vector2.Zero;
                            npc.aiStyle = -1;
                            chicken.npc.aiStyle = -1;
                            Main.NewText(matingTimer);
                            matingTimer++;
                            if (matingTimer >= Thinf.ToTicks(15))
                            {
                                Item.NewItem(npc.getRect(), ModContent.ItemType<Egg>());
                                Main.NewText("Success");
                                chicken.matingTimer = 0;
                                matingTimer = 0;
                                horny = false;
                                chicken.horny = false;
                                npc.aiStyle = 7;
                                chicken.npc.aiStyle = 7;
                                hasMate = false;
                                chicken.hasMate = false;
                            }
                        }
                        else
                        {
                            chicken.npc.aiStyle = -1;
                            chicken.npc.velocity = chicken.npc.DirectionTo(npc.Center) * 3f;
                            npc.aiStyle = -1;
                            npc.velocity = npc.DirectionTo(chicken.npc.Center) * 3f;
                        }
                    }
                }
            }
        }
        public override string GetChat()
        {
            return "buck buck";
        }
        public static List<int> FindAllNPCs(int type, int whoAmI)
        {
            List<int> count = new List<int>();
            for (int i = 0; i < Main.maxNPCs; ++i)
            {
                NPC check = Main.npc[i];
                if (check.active && check.type == type && !count.Contains(i) && i != whoAmI)
                {
                    count.Add(i);
                }
            }
            return count;
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.43f;
        }


        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -2f);
                    if (Main.rand.NextBool(2))
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale = 1.2f * npc.scale;
                    }
                    else
                    {
                        Main.dust[dust].scale = 0.7f * npc.scale;
                    }
                }
            }
        }

        public override void OnCatchNPC(Player player, Item item)
        {
            item.stack = 1;
        }
        internal class ChickenItem : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Chicken");
            }

            public override void SetDefaults()
            {
                item.useStyle = 1;
                item.autoReuse = true;
                item.useTurn = true;
                item.useAnimation = 15;
                item.useTime = 10;
                item.maxStack = 999;
                item.consumable = true;
                item.width = 30;
                item.height = 30;
                item.noUseGraphic = true;
                item.makeNPC = (short)ModContent.NPCType<Chicken>();
            }
        }
    }
}