using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class CactusStaff : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots piercing spikes");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.magic = true;
			item.mana = 15;
			item.width = 48;
			item.height = 48;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = false; //so the item's animation doesn't do damage
			item.knockBack = 25;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = ProjectileType<MagicCactspike>();
			item.shootSpeed = 16f;
		}
	}
}