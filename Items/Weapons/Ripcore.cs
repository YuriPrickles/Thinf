using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class Ripcore : ModItem
	{
		int ripcoreTimer = 0;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Constantly increasing attack speed and damage while swinging\nTRUE MELEE!!!!!");
		}

		public override void SetDefaults()
		{
			item.damage = 460;
			item.crit = 10;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 20000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.scale = 2;
			item.useTurn = true;
			item.shootSpeed = 10f;
			item.channel = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.Venom, Thinf.ToTicks(3));
		}

        public override bool UseItem(Player player)
        {
			ripcoreTimer++;
			if (ripcoreTimer >= 2 && item.useTime != 4)
            {
				item.useTime -= 2;
				item.useAnimation -= 2;
				ripcoreTimer = 0;
			}
            return true;
        }

        public override void HoldItem(Player player)
        {
			if (!Main.mouseLeft)
			{
				item.useTime = 28;
				item.useAnimation = 28;
				ripcoreTimer = 0;
			}
		}
    }
}