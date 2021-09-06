using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Glowsword : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Shines light on enemies\nAll enemies are linked to one another and share damage dealt on them");
		}

		public override void SetDefaults()
		{
			item.damage = 500;
			item.crit = 12;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 11;
			item.value = 20000;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.shootSpeed = 10f;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Linimisifrififlium>(), 15);
			recipe.AddIngredient(ItemID.Glowstick, 150);
			recipe.AddIngredient(ItemID.WoodenSword, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override void HoldItem(Player player)
        {
			for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC npc = Main.npc[i];
				if (npc.active && !npc.friendly && !npc.dontTakeDamage)
				{
					Lighting.AddLight(npc.Center, Main.DiscoColor.ToVector3() * 2);
				}
			}
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			for (int i = 0; i < Main.maxNPCs; ++i)
			{
				NPC npc = Main.npc[i];
				if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.whoAmI != target.whoAmI)
				{
					npc.StrikeNPC(damage / 4, 0, 0);
				}
			}
		}
    }
}