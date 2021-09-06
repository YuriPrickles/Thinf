using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Thinf.Items.Placeables;
using static Thinf.MyPlayer;
using Thinf.Items;
using Thinf.Blocks;
using Microsoft.Xna.Framework;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class NightmareHelmet: ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("18% increased ranged damage\n25% ranged crit chance");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 24;
			item.value = 10000;
			item.rare = ItemRarityID.Red;
			item.defense = 25;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemType<NightmareChestplate>() && legs.type == ItemType<NightmareLeggings>();
		}
        public override void UpdateEquip(Player player)
        {
			player.rangedDamage *= 1.18f;
			player.rangedCrit += 25;
        }
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "50% increased movement speed";
			player.moveSpeed += 0.5f;
		}

		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			color = drawPlayer.GetImmuneAlphaPure(Color.Red, shadow);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<NightmareFuel>(), 10);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddTile(TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}