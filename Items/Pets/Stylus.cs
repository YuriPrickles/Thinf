using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using Thinf.Projectiles;

namespace Thinf.Items.Pets
{
	public class Stylus : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a Photon to light the way");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.width = 32;
			item.height = 32;
			item.shoot = ModContent.ProjectileType<PhotonPet>();
			item.buffType = ModContent.BuffType<Photon>();
		}

		public override void UpdateEquip(Player player)
		{
		
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}