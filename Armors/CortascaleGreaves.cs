using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Thinf.Projectiles;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class CortascaleGreaves : ModItem
	{
		int cortacount = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("+8% minion damage");
		}

		public override void SetDefaults() {
			item.width = 26;
			item.height = 24;
			item.value = 20000;
			item.rare = ItemRarityID.Green;
			item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemType<CortascaleChestplate>() && head.type == ItemType<CortascaleHelmet>();
		}

		public override void UpdateEquip(Player player) {
			player.minionDamage += 0.08f;
		}
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(mod.ItemType("Cortascale"), 8);recipe.AddIngredient(ItemID.BottledWater, 4);recipe.AddTile(TileID.Anvils); recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}