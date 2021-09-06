using Terraria.ModLoader;
using Terraria.ID;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Thinf.Blocks;
using Terraria;
using static Thinf.FarmerClass;
using System.Collections.Generic;
using System.Linq;

namespace Thinf.Items.Weapons
{
	public class Egg : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The miracle of life\nThrow to have a chance to get a chicken or some egg yolk!");
		}
		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public override void SetDefaults()
		{
			item.ranged = true;
			item.damage = 12;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<EggProj>();
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shootSpeed = 7f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 999;
			item.consumable = true;
		}
	}
}