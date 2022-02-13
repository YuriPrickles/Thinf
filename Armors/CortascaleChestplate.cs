using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class CortascaleChestplate : ModItem
	{
        int cortacount = 0;

        public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			Tooltip.SetDefault("+12% summon damage");
		}

		public override void SetDefaults() {
			item.width = 26;
			item.height = 26;
			item.value = 30000;
			item.rare = ItemRarityID.Green;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player) 
		{
            player.minionDamage += 0.12f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ItemType<CortascaleHelmet>() && legs.type == ItemType<CortascaleGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);recipe.AddIngredient(mod.ItemType("Cortascale"), 24);recipe.AddIngredient(ItemID.SpecularFish, 6);recipe.AddIngredient(ItemID.RecallPotion, 2);recipe.AddTile(TileID.Anvils); recipe.SetResult(this); recipe.AddRecipe();
        }
	}
}