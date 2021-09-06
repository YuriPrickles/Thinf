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
    public class EnterRicochet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transportamatic-1129");
            Tooltip.SetDefault("Target Location: Ricochet Rocks");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool UseItem(Player player)
        {
            //MyPlayer.insideGatrix = true;
            //MyPlayer.readyToTravel = false;
            Subworld.Enter<RicochetRocks>();
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}
