using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Tools
{
	//ported from my tAPI mod because I don't want to make more artwork
	public class Drarrot : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Can mine Potatiumite and Carrotyx\nThat's it. No funny tooltip.");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
			item.width = 56;
			item.height = 38;
			item.useTime = 7;
			item.useAnimation = 25;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.pick = 201;
			item.tileBoost++;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 6;
			item.value = Item.buyPrice(0, 12, 50, 0);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<Projectiles.Drarrot>();
			item.shootSpeed = 27f;
		}

		public override Vector2? HoldoutOffset()
		{
			return (new Vector2(0, 0)); // You'll need to play a bit with these numbers
		}
	}
}