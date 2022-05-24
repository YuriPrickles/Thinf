using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items.THE_SUPER_COOL_BADASS_LORE
{
    public class LogFileTwo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prime Minister - Log File 02");
            Tooltip.SetDefault("Log File 02\n" +
                "Finally, a solution to the Beekeeper problem! Instead of releasing the Hypno Soup as a sort of aerosol, which is less effective for her,\n" +
                "we inject the Hypno Soup directly into her brain! With the new Hypno Hat my team has made, all that's left is to get her head inside it.\n" +
                "This is all coming together nicely, and after this, I can sit back, relax, and watch as I own every single part of this kingdom!\n" +
                "Oh, the fun I'll have, finally, I can live a happy life, where no one, and I MEAN NO ONE, is against me. Anyways, the season finale of my\n" +
                "favorite anime is starting, and I'm not really looking forward to anything good, seeing as how they screwed up the past 4 episodes.\n");
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
