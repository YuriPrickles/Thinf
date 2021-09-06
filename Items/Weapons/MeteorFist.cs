using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class MeteorFist : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Fist");
			Tooltip.SetDefault("Punch enemies with the blast of a meteor");
		}

		public override void SetDefaults()
		{
			item.damage = 21;
			item.crit = (int)0.15f;
			item.magic = true;
			item.mana = 9;
			item.width = 32;
			item.height = 32;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.knockBack = 15;
			item.value = 20000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.useTurn = false;
			item.shoot = mod.ProjectileType("MeteorBall");
			item.shootSpeed = 2f;
		}


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 480);
		}
    }
}