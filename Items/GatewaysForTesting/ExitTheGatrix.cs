using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.Cortal;
using static Terraria.ModLoader.ModContent;
using Thinf.NPCs.SoulCatcher;
using SubworldLibrary;

namespace Thinf.Items.GatewaysForTesting
{
    public class LeaveTheGatrix : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Leave the Gatrix!\nFor testing purposes only.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        public override bool UseItem(Player player)
        {
            Subworld.Exit();
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}
