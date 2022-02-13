using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Thinf.Projectiles;

namespace Thinf.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class CortascaleHelmet : ModItem
	{
		int cortacount;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("You can see millions of worlds inside this\n+9% minion damage");
		}

		public override void SetDefaults() {
			item.width = 30;
			item.height = 27;
			item.value = 25000;
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ItemType<CortascaleChestplate>() && legs.type == ItemType<CortascaleGreaves>();
		}
        public override void UpdateEquip(Player player)
        {
			player.minionDamage += 0.09f;
        }
        public override void UpdateArmorSet(Player player) {
			player.setBonus = "You have obtained a fraction of Cortal's powers";
			if (player.ownedProjectileCounts[ProjectileType<CortalJunior>()] < 1)
			{
				Projectile.NewProjectile(player.position, new Vector2(0, 0), mod.ProjectileType("CortalJunior"), 25, 5, player.whoAmI);
				player.AddBuff(mod.BuffType("CortalJunior"), 2);
			}

			if (player.setBonus == "")
				player.ClearBuff(mod.BuffType("CortalJunior"));
		}

		public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod); recipe.AddIngredient(mod.ItemType("Cortascale"), 12); recipe.AddIngredient(ItemID.SpecularFish, 5); recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this); recipe.AddRecipe();
		}
	}
}