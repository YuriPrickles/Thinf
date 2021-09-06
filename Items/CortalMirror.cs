using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Thinf.Items
{
	public class CortalMirror : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cortal Mirror");
			Tooltip.SetDefault("Teleports you to a random location at the cost of 200 mana and health\nLegendary Item!\n \nThis item has a 1/7500 chance of dropping from Cortal");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.maxStack = 1;
			item.value = 50000;
			item.rare = ItemRarityID.Expert;
			item.useAnimation = 90;
			item.useTime = 90;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool CanUseItem(Player player)
		{
			return true;
		}
		public override bool UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.statLife > 200 && player.statMana > 200)
			{
				player.TeleportationPotion();
				player.statLife -= 200;
				player.statMana -= 200;
			}
			else
            {
				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.LightBlue, "The Cortal Mirror doesn't have enough power!");
			}

			Main.PlaySound(SoundID.Drown, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
	}
}
