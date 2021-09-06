using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class ChaosOrbStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fighting Spirit");
            Tooltip.SetDefault("Shoots souls that deal a lot of debuffs when you are near them");
            Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
        }

        public override void SetDefaults()
        {
            item.damage = 95;
            item.magic = true;
            item.mana = 40;
            item.width = 34;
            item.height = 34;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.NPCDeath39;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<FightingSpirit>();
            item.shootSpeed = 17f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreStaff);
            recipe.AddIngredient(ItemID.RazorbladeTyphoon);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFight>(), 15);
            recipe.AddTile(TileID.MythrilAnvil); recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}