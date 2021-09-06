using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Potions
{
    public class MixeledPancake : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heals 3 HP\nGo to hell.");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
            item.useStyle = 2;                 //this is how the item is holded when used
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;                 //this is where you set the max stack of item
            item.consumable = true;           //this make that the item is consumable when used
            item.width = 32;
            item.height = 32;
            item.value = 1000;
            item.rare = 1;
            item.healLife = 3;
            item.buffType = BuffID.PotionSickness;    //this is where you put your Buff name
            item.buffTime = Thinf.ToTicks(60);
        }
        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffID.PotionSickness);
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.PotionSickness, Thinf.ToTicks(150));
            player.AddBuff(BuffID.Poisoned, Thinf.ToTicks(15));
            player.AddBuff(BuffID.Stinky, Thinf.MinutesToTicks(15));
            return true;
        }
    }
}