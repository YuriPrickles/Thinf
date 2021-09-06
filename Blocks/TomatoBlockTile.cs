using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Items.Placeables;
using Thinf.NPCs;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Blocks
{
	public class TomatoBlockTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = false;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileMerge[Type][TileID.Sand] = true;
			minPick = 100;
			AddMapEntry(new Color(135, 0, 0));
		}

        public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance)
        {
			if (Main.rand.Next(4) == 0)
			{
				NPC.NewNPC((int)new Vector2(i, j).ToWorldCoordinates().X, (int)new Vector2(i, j).ToWorldCoordinates().Y, NPCType<TomatoBat>());
			}

			if (Main.rand.Next(4) == 0)
			{
				NPC.NewNPC((int)new Vector2(i, j).ToWorldCoordinates().X, (int)new Vector2(i, j).ToWorldCoordinates().Y, NPCType<KetchupSlime>());
			}
		}
        public override bool CanExplode(int i, int j)
		{
			return true;
		}
	}
}