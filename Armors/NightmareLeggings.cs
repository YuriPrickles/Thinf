
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using Thinf.Items;
using Thinf.Blocks;
using Microsoft.Xna.Framework;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class NightmareLeggings : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("20% increased movement speed\n13% increased damage\nGives an infinite hunter effect\n'Wait how does THIS give you the hunter effect?'");
		}

		public override void SetDefaults() {
			item.width = 22;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 30;
		}

		public override void UpdateEquip(Player player) {
			player.moveSpeed += 0.2f;
			player.allDamage *= 1.13f;
			player.AddBuff(BuffID.Hunter, 2);
		}
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor) {
            color = drawPlayer.GetImmuneAlphaPure(Color.Red, shadow);
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<NightmareFuel>(), 15);
			recipe.AddIngredient(ItemID.SoulofFright, 25);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}