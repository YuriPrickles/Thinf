using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Thinf
{
	// This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
	public class FarmerClass : ModPlayer
	{
		public static FarmerClass ModPlayer(Player player)
		{
			return player.GetModPlayer<FarmerClass>();
		}

		// Vanilla only really has damage multipliers in code
		// And crit and knockback is usually just added to
		// As a modder, you could make separate variables for multipliers and simple addition bonuses
		public float farmerDamageAdd;
		public float farmerDamageMult = 1f;
		public float farmerKnockback;
		public int farmerCrit;
		public float farmerSpeed;
		public override void ResetEffects()
		{
			ResetVariables();
		}

		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			farmerDamageAdd = 0f;
			farmerDamageMult = 1f;
			farmerKnockback = 3f;
			farmerCrit = 0;
			farmerSpeed = 1f;
		}
	}
}