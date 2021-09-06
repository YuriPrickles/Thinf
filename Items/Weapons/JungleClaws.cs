using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class JungleClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Claws");
			Tooltip.SetDefault("Tear through grass and stuff");
		}

		public override void SetDefaults()
		{
			item.damage = 70;
			item.crit = (int)0.11f;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 1;
			item.knockBack = 0;
			item.value = 20000;
			item.rare = ItemRarityID.LightPurple;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FetidBaghnakhs);
			recipe.AddIngredient(ItemID.FeralClaws);
			recipe.AddIngredient(ItemID.JungleSpores, 25);
			recipe.AddIngredient(ItemID.Stinger, 15);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 20);
			recipe.AddIngredient(ItemID.VialofVenom, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.Venom, 240);
		}
	}
}