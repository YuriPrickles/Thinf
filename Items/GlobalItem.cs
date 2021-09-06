using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;

namespace ExampleMod.Items
{

    public class ThinfGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            //if (MyPlayer.hasFrostSavageHelmetMeleeSizeBuff && item.melee)
            //{
            //    item.scale *= 1.3f;
            //}
            if (item.type == ItemID.EmptyBucket)
            {
                item.damage = 1;
            }
        }

        public override void OnCraft(Item item, Recipe recipe)
        {
            if (item.type == ItemID.SlimeChandelier)
            {
                Player player = Main.player[item.owner];

                player.AddBuff(BuffID.Slow, 600);
                player.AddBuff(BuffID.Slimed, 600);
            }
        }
    }
}