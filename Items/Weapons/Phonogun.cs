using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Items.Weapons
{
	public class Phonogun : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a stream of 'music'\n'What moron would put goat sounds in a phonograph?'");
		}

		public override void SetDefaults()
		{
			item.damage = 42;
			item.crit = 2;
			item.ranged= true;
			item.width = 64;
			item.height = 28;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Phonogun");
			item.autoReuse = true;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 5f;
			item.useAmmo = AmmoID.Bullet;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), ProjectileID.TiedEighthNote, damage, knockBack, item.owner);
			proj.magic = false;
			proj.ranged = true;
			return false;
        }
    }
}
