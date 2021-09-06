using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
    public class FrostLance : ModItem
    {
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Item.staff[item.type] = true;
            Tooltip.SetDefault("An absolute ice-breaker\nDash towards enemies\nIf a non-boss enemy with less than 30 defense survives a hit, they will be left paralyzed\nHitting a non-boss enemy reduces its defense by 5");
        }

        public override void SetDefaults()
        {
            item.damage = 1200;
            item.crit = (int)0.7f;
            item.melee = true;
            item.width = 48;
            item.noMelee = false;
            item.height = 48;
            item.useTime = 50;
            item.useAnimation = 50;
            item.reuseDelay = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3;
            item.value = 5505;
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = ProjectileID.FrostBoltSword;
            item.shootSpeed = 10f;
            item.scale = 2.5f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.velocity = player.DirectionTo(Main.MouseWorld) * 30;
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>(), 15);
            recipe.AddIngredient(ItemID.AdamantiteGlaive);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.defense <= 30 && !target.boss)
            {
                target.aiStyle = -1;
            }
            if (target.defense > 30 && !target.boss)
            {
                target.defense -= 5;
            }
        }
    }
}