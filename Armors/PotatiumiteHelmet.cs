using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.MyPlayer;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class PotatiumiteHelmet: ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("30% increased ranged crit chance\nHas a 15% to not consume ammo");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 30;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 14;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemType<PotatiumiteChestplate>() && legs.type == ItemType<PotatiumiteLeggings>();
		}
        public override void UpdateEquip(Player player)
        {
			player.rangedCrit += 30;
			haspotatorangedhelm = true;
        }
        public override void UpdateArmorSet(Player player)
		{
			haspotatorangedsetbonus = true;
			player.setBonus = "40% increased ranged fire rate";
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