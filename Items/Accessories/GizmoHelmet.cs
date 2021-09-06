using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;
using static Thinf.MyPlayer;

namespace Thinf.Items.Accessories
{
    public class GizmoHelmet : ModItem
    {
        int skullSummonTimer = 0;
        int bloodShotTimer = 0;
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 36;
            item.defense = 1;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 1, silver: 24);
            item.rare = ItemRarityID.Green;
        }

        public override void UpdateInventory(Player player)
        {
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(@"When you are hurt, enemies take the same amount of damage
Immunity to Electrified
Increased jump height
Releases slime every few seconds
Grants an infinite Night Owl buff
Effects do not stack with other night vision items
7% increased ranged damage during the night
Any enemies below you onscreen have their debuff immunities disabled
Gives the ability to climb walls
Shoots bursts of crimson energy in random directions behind the player while sliding/grabbing onto walls
You gain +10 defense and +8% damage when below 50% HP
Crits paralyze enemies for 3 seconds
+40 max HP when covered in honey
Summons a floating skull every 16 seconds that rotates around the player
The skull damages enemies and inflicts shadowflames
Right-click to kill every skull and heal the player for 10 HP for each skull
You can have a max amount of 5 skulls");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            hasTransformer = true;
            player.buffImmune[BuffID.Electrified] = true;

            player.jumpSpeedBoost += 4;
            MyPlayer.hasKingSlimeCrown = true;

            player.AddBuff(BuffID.NightOwl, 2);
            if (!Main.dayTime)
            {
                player.rangedDamage *= 1.07f;
            }

            for (int i = 0; i < Main.maxNPCs; ++i)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.type != NPCID.TargetDummy && !npc.friendly && !npc.dontTakeDamage && npc.Center.Y > player.Center.Y && !npc.boss)
                {
                    npc.AddBuff(ModContent.BuffType<ImmunityLoss>(), 2);
                }
            }

            player.spikedBoots += 2;
            if (player.sliding)
            {
                bloodShotTimer++;
                if (bloodShotTimer == 15)
                {
                    Projectile projectile = Main.projectile[Projectile.NewProjectile(player.Center, Vector2.Zero, ProjectileID.RubyBolt, 26, 0, player.whoAmI)];
                    projectile.magic = false;
                    projectile.velocity = projectile.DirectionTo(Main.MouseWorld.RotatedByRandom(MathHelper.ToRadians(360))) * 9f;
                    projectile.velocity *= player.direction;
                    projectile.tileCollide = false;
                    bloodShotTimer = 0;
                }
            }

            if (player.statLife < player.statLifeMax2 * 0.5f)
            {
                player.statDefense += 15;
                player.allDamage *= 1.08f;
            }

            hasQueenStinger = true;
            if (player.HasBuff(BuffID.Honey))
            {
                player.statLifeMax2 += 40;
            }

            if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSkull>()] < 5)
            {
                skullSummonTimer++;
                if (skullSummonTimer >= Thinf.ToTicks(16))
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<FloatingSkull>(), 30, 0, player.whoAmI);
                    skullSummonTimer = 0;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Transformer>());
            recipe.AddIngredient(ModContent.ItemType<KingSlimesCrown>());
            recipe.AddIngredient(ModContent.ItemType<GlassesOfCthulhu>());
            recipe.AddIngredient(ModContent.ItemType<EaterOfSoles>());
            recipe.AddIngredient(ModContent.ItemType<BloodCrawlerGear>());
            recipe.AddIngredient(ModContent.ItemType<NecromancyGuide>());
            recipe.AddIngredient(ModContent.ItemType<CollarboneCollar>());
            recipe.AddIngredient(ModContent.ItemType<RoyalStinger>());
            recipe.AddIngredient(ItemID.AdamantiteBar, 15);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Transformer>());
            recipe.AddIngredient(ModContent.ItemType<KingSlimesCrown>());
            recipe.AddIngredient(ModContent.ItemType<GlassesOfCthulhu>());
            recipe.AddIngredient(ModContent.ItemType<EaterOfSoles>());
            recipe.AddIngredient(ModContent.ItemType<BloodCrawlerGear>());
            recipe.AddIngredient(ModContent.ItemType<NecromancyGuide>());
            recipe.AddIngredient(ModContent.ItemType<CollarboneCollar>());
            recipe.AddIngredient(ModContent.ItemType<RoyalStinger>());
            recipe.AddIngredient(ItemID.TitaniumBar, 15);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}