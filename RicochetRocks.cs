using System.Collections.Generic;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Thinf
{
	public class RicochetRocks : Subworld
	{
		public override int width => 1000;
		public override int height => 1000;

		public override ModWorld modWorld => ModContent.GetInstance<ModNameWorld>();

		public override bool saveSubworld => false;
		public override bool disablePlayerSaving => false;
		public override bool saveModData => false;

		public override List<GenPass> tasks => new List<GenPass>()
	{
		new SubworldGenPass(progress =>
		{
			progress.Message = "Entering the Gatrix";

			Main.worldSurface = Main.maxTilesY - 42;
			Main.rockLayer = Main.maxTilesY;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY));
					if (Main.rand.Next(300) == 0)
                    {
						WorldGen.TileRunner(i, j, 10, 2, TileID.Stone, true);
                    }
				}
			}
		})
	};

		public override void Load()
		{
			Main.dayTime = true;
			Main.time = 27000;
		}
	}
}
