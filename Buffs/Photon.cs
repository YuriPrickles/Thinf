using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Thinf.MyPlayer;

namespace Thinf.Buffs
{
	public class Photon : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Photon");
			Description.SetDefault("He's been on Adobe Animate for 20 hours");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().photon = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.PhotonPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.PhotonPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}