using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items.THE_SUPER_COOL_BADASS_LORE
{
    public class ThunderEgg : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Egg");
            Tooltip.SetDefault("Thunder Cocks are very protective of their little ones. You probably know by now.\n" +
                "These strange creatures began as regular chickens, living their lives, eating, clucking, and laying eggs.\n" +
                "When the corruption and the crimson came, they lost their source of food, and had no choice but to eat batteries and\n" +
                "other stuff with electricity with them. I don't know why they chose that, there's like a much better food source in the\n" +
                "corruption than some guy's auto repair shop.");
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
