using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Thinf.ModNameWorld;

namespace Thinf.Items
{
	public class FrenzyCoffee : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Activates Frenzy Mode, making the game harder\nCan only be used in expert mode\nEffects:\nStronger Mobs\nHigher spawnrates\nChanges to boss AI\nMore to come");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Red;
			item.useAnimation = 10;
			item.useTime = 10;
			item.UseSound = SoundID.Item2;
			item.useStyle = 2;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override bool UseItem(Player player)
		{
			if (Main.expertMode && !FrenzyMode)
			{
				FrenzyMode = true;
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.SandyBrown, "It's Frenzy Time!");
			}
			if (FrenzyMode)
            {
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.SandyBrown, "There is no going back, the coffee is here to stay.");
			}
			if (!Main.expertMode)
            {

				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.SandyBrown, "The world cannot handle this much frenziness");
			}
			return true;
		}
	}
}
