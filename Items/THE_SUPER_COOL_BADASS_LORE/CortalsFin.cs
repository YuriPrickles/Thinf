using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items.THE_SUPER_COOL_BADASS_LORE
{
    public class CortalsFin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cortal's Fin");
            Tooltip.SetDefault("Specular Fish are known for their ability to teleport, but none can compare to the power\n" +
                "of Cortal, a Specular Fish that grew so big from the amount of food it ate. It's teleporting\n" +
                "abilities grew with it's size, being able to transport anything from other worlds and dimensions.");
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
