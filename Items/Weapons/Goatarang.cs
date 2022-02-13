using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Goatarang : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throw a fireball spewing boomerang\n'Mehhhhh'");
		}

		public override void SetDefaults()
		{
			item.damage = 54;
			item.crit = 0;
			item.melee = true;
			item.width = 20;
			item.height = 32;
			item.useStyle = 1;
			item.knockBack = 11;
			item.useTime = 14;
			item.useAnimation = 14;
			item.value = 20000;
			item.rare = ItemRarityID.Red;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/GoatBleat");
			item.autoReuse = true;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<GoatarangProj>();
			item.shootSpeed = 19f;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.channel = true;
		}


        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<GoatarangProj>()] == 0;
        }
    }
}