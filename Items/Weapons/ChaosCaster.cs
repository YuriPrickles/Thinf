using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class ChaosCaster : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots chaos balls that go through tiles");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 24;
			item.magic = true;
			item.mana = 7;
			item.width = 34;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = ProjectileType<ChaosBallers>();
			item.shootSpeed = 4f;
		}
	}
}