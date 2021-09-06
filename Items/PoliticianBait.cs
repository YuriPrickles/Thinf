using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.NPCs.Cortal;
using static Terraria.ModLoader.ModContent;
using Thinf.NPCs.SoulCatcher;
using Thinf.NPCs.PrimeMinister;
using Microsoft.Xna.Framework;

namespace Thinf.Items
{
    public class PoliticianBait : ModItem
    {
        int waitingTimer = 0;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Prime Minister, the totally not corrupt King of the Bee Kingdom! Please vote for him in the next election, he will do good to the kingdom!\nDrop this anywhere in the Jungle and wait\nDoesn't actually contain money");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 999;
            item.value = 0;
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {           
            return false;  //you can't spawn this boss multiple times
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!NPC.AnyNPCs(NPCType<PrimeMinister>()) && Main.player[item.owner].ZoneJungle)
            {
                waitingTimer++;
                if (waitingTimer >= 300)
                {
                    NPC.SpawnOnPlayer(Main.player[item.owner].whoAmI, NPCType<PrimeMinister>());
                    Main.NewText("MONEY! GIMME THAT MONEY!", Color.Yellow);
                    waitingTimer = 0;
                }
            }
        }
        public override void AddRecipes()
        {

        }
    }
}
