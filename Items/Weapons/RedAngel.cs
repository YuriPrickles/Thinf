using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class RedAngel : ModItem
    {
        int chargeCount = 0;
        public override void SetStaticDefaults() //obligatory stickbug o--/--/-
        {
            Tooltip.SetDefault("Strike down bolts of ketchup at enemies\nEvery 15 melee hits heals you for 10 HP\nLyco 12:15 'And so He pulled the sword from the ground, and ketchup fell from the skies.'");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.crit = 1;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2;
            item.value = 20000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item117;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = ModContent.ProjectileType<KetchupLaser>();
            item.shootSpeed = 10f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; ++i)
            {
                position = new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-350, -250));
                Projectile.NewProjectileDirect(new Vector2(Main.MouseWorld.X + position.X, Main.MouseWorld.Y - Main.screenHeight), Vector2.Normalize(Main.MouseWorld - new Vector2(Main.MouseWorld.X + position.X, Main.MouseWorld.Y - Main.screenHeight)) * 6, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
            if (target.active && !target.friendly && !target.immortal)
            {
                chargeCount++;
                if (chargeCount >= 15)
                {
                    player.statLife += 10;
                    player.HealEffect(10);
                    chargeCount = 0;
                }
            }
		}
    }
}