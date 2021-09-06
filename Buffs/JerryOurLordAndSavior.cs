using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;
using static Thinf.MyPlayer;

namespace Thinf.Buffs
{
	public class JerryOurLordAndSavior : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Jerry");
			Description.SetDefault("All hail our Lord and Savior");
			Main.buffNoSave[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MyPlayer>().jerry = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Jerry>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Jerry>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}