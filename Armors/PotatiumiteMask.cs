using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.MyPlayer;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class PotatiumiteMask: ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("+3 max minions\n+35% summon damage\n+2 max sentries");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 30;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemType<PotatiumiteChestplate>() && legs.type == ItemType<PotatiumiteLeggings>();
		}
        public override void UpdateEquip(Player player)
        {
			player.maxMinions += 3;
			player.minionDamage *= 1.35f;
            player.maxTurrets += 2;
			player.UpdateMaxTurrets();
        }
        public override void UpdateArmorSet(Player player)
		{
			haspotatosummonsetbonus = true;
			player.setBonus = "When you a minion kills an enemy, lasers burst out from it\nThis only works if the damage is not greater than the enemies max health";
			/* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<Potato>(), 45);
			recipe.AddIngredient(ItemType<PotatiumiteBar>(), 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}