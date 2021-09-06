using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
    public class Scimiterror : ModItem
    {
        int chargeCount = 0;
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Hurting enemies with the sword fills up a charge that releases an aura of nightmares\nThe aura completly removes enemy defense and damages for half an enemy's current HP.\nDamages 4% of current HP for bosses\n'All your dreams are their nightmares'");
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.crit = (int)0.5f;
            item.melee = true;
            item.width = 36;
            item.height = 44;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = 20000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.scale = 1.75f;
        }
        public override void HoldItem(Player player)
        {
            if (chargeCount == 25)
            {
                for (int i = 0; i < Main.maxNPCs; ++i)
                {
                    NPC npc = Main.npc[i];

                    if (npc.Distance(player.Center) < 380)
                    {
                        for (int f = 0; f < 50; f++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Asphalt, 0, 0, 0, default)];
                            dust.noGravity = true;
                        }
                        if (!npc.boss)
                        {
                            npc.defense = 0;
                            npc.StrikeNPC(npc.life / 2, 0, 0);
                        }
                        else
                        {
                            npc.StrikeNPC(npc.life / 25, 0, 0);
                        }
                    }
                }
                chargeCount = 0;
            }
            int dustSpawnAmount = 24;
            for (int i = 0; i < dustSpawnAmount; ++i)
            {
                float currentRotation = (MathHelper.TwoPi / dustSpawnAmount) * i;
                Vector2 dustOffset = currentRotation.ToRotationVector2() * 2.5f;
                Dust dust = Dust.NewDustPerfect(player.Center + dustOffset * (chargeCount * 6), DustID.Asphalt);
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            chargeCount++;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 15);
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddIngredient(ItemID.CopperBroadsword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 15);
            recipe.AddIngredient(ItemID.SoulofNight, 20);
            recipe.AddIngredient(ItemID.TinBroadsword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}