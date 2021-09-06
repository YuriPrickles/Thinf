using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Thinf.MyPlayer;

namespace Thinf.Waters
{
    public class Ketchup : ModWaterStyle
    {
        public override bool ChooseWaterStyle()
        {
            return Main.LocalPlayer.GetModPlayer<MyPlayer>().ZoneTomatoTown;    //this is where u choose where the custom water/waterfalls will appear. it will appear in base of backgrounds so add your surface background name. 
        }

        public override int ChooseWaterfallStyle()
        {
            return mod.GetWaterfallStyleSlot("KetchupWaterfall");   //this is the waterfall style
        }

        public override int GetSplashDust()
        {
            return DustID.Blood;   //this is the water splash dust
        }

        public override int GetDropletGore()
        {
            return GoreID.WaterDripBlood;     //this is the water droplet
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public override Color BiomeHairColor()
        {
            return Color.Red;
        }
    }
}