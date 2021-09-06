using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Thinf.Items.Weapons
{
	public class DevilsMatchstick : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Cast a burst of flames at the cursor");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 47;
			item.magic = true;
			item.mana = 25;
			item.width = 38;
			item.height = 38;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0.4f;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item73;
			item.autoReuse = true;
            item.shoot = ProjectileID.InfernoFriendlyBlast;
			item.shootSpeed = 0.1f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			speedX = 0;
			speedY = 0;
			position = Main.MouseWorld;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}