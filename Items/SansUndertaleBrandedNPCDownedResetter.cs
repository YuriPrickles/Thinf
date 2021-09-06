using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Items.Placeables;
using static Thinf.ModNameWorld;

namespace Thinf.Items
{
	public class SansUndertaleBrandedNPCDownedResetter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sans Undertale Branded NPC Downed Bool Resetter");
			Tooltip.SetDefault("Resets NPC Downed bools\nSomething");
		}

		public override void SetDefaults()
		{
			item.width = 29;
			item.height = 30;
			item.maxStack = 1;
			item.value = 0;
			item.rare = ItemRarityID.Cyan;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.scale = 5;
			item.consumable = false;
		}
		public override bool UseItem(Player player)
		{
			downedThundercock = false;
			downedSpudLord = false;
			downedDungeonArmy = false;
			downedCacterus = false;
			downedCortal = false;
			downedBeenado = false;
			NPC.downedAncientCultist = false;
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			NPC.downedBoss3 = false;
			NPC.downedQueenBee = false;
			NPC.downedGoblins = false;
			NPC.downedPirates = false;
			NPC.downedClown = false;
			NPC.downedGolemBoss = false;
			NPC.downedChristmasTree = false;
			NPC.downedChristmasSantank = false;
			NPC.downedChristmasIceQueen = false;
			NPC.downedFishron = false;
			NPC.downedFrost = false;
			NPC.downedHalloweenTree = false;
			NPC.downedHalloweenKing = false;
			NPC.downedMechBoss1 = false;
			NPC.downedMechBoss2 = false;
			NPC.downedMechBoss3 = false;
			NPC.downedMechBossAny = false;
			NPC.downedMoonlord = false;
			NPC.downedPlantBoss = false;
			NPC.downedSlimeKing = false;
			NPC.downedTowerNebula = false;
			NPC.downedTowerSolar = false;
			NPC.downedTowerStardust = false;
			NPC.downedTowerVortex = false;
			Main.NewText("All downed bools have been reset!");
			Main.PlaySound(SoundID.DD2_WyvernDeath, (int)player.position.X, (int)player.position.Y);
			return true;
		}
	}
}
