using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.Cortal;
using static Terraria.ModLoader.ModContent;
using Thinf.NPCs.SoulCatcher;
using Microsoft.Xna.Framework;
using Thinf.Buffs;

namespace Thinf.Items
{
    public class AccurateCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Applies the Acccurate prefix on a weapon (~20% critical strike chance)\nTo apply, use the card and click the weapon you want to use it on\n'The formula's pretty simple: You hit a 100 damage, you have my attention'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 1;
            item.value = 10;
            item.rare = ItemRarityID.LightRed;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<MyPlayer>().cardPrefixType = "Accurate";
            player.AddBuff(BuffType<SelectingWeapon>(), Thinf.MinutesToTicks(60000));
            Main.NewText("Now select any weapon from your inventory!");
            return true;
        }
    }
}
