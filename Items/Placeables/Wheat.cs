using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.FarmerClass;
using System.Collections.Generic;
using System.Linq;

namespace Thinf.Items.Placeables
{
    public class Wheat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used up when hitting an enemy\nCan also be used to bake stuff!");
        }
        public virtual void SafeSetDefaults()
        {
            item.damage = 31;
            item.UseSound = SoundID.Item1;
            item.useTime = 14;
            item.useAnimation = 14;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Green;
            item.consumable = true;
            item.maxStack = 999;
            item.useTurn = true;
        }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            // all vanilla damage types must be false for custom damage types to work
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += ModPlayer(player).farmerDamageAdd;
            mult *= ModPlayer(player).farmerDamageMult;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            // Adds knockback bonuses
            knockback += ModPlayer(player).farmerKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            // Adds crit bonuses
            crit += ModPlayer(player).farmerCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.text = damageValue + " plant " + damageWord;
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Dust dust;
            Vector2 position = target.position;
            for (int k = 0; k < 30; ++k)
            {
                dust = Main.dust[Dust.NewDust(position, 30, 30, 232, 0f, 0f, 0, new Color(255, 255, 255), 1.381579f)];
            }
            item.stack -= 1;
        }
    }
}