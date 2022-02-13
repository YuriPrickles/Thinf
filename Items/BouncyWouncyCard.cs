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
    public class BouncyWouncyCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Applies the Bouncy Wouncy prefix on a weapon (~32% knockback)\nTo apply, use the card and click the weapon you want to use it on\n'All the Pinkies are going to hate you'");
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
            player.GetModPlayer<MyPlayer>().cardPrefixType = "BouncyWouncy";
            player.AddBuff(BuffType<SelectingWeapon>(), Thinf.MinutesToTicks(60000));
            Main.NewText("Now select any weapon from your inventory!");
            return true;
        }
    }
}
