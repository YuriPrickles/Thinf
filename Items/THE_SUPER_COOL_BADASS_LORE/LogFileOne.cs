using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items.THE_SUPER_COOL_BADASS_LORE
{
    public class LogFileOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prime Minister - Log File 01");
            Tooltip.SetDefault("Log File 01\n" +
                "It's a simple plan. When the Queen dies, we kill all her larva, then make robot copies of what her children might have been.\n" +
                "After that, we use the Hypno Soup on everyone before the fake robots would 'reach maturity'. Hmm... But there's still one problem.\n" +
                "Beekeeper. She's going to find out eventually, and research has proven that Hypno Soup is less effective on humans.\n" +
                "Eh, we'll cross that bridge when we get to it. My favorite anime's starting, and apparently this episode introduces a new character.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 4;
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Tink, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}
