using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
	public class RulerOfEverything : ModItem
	{
		public override void SetStaticDefaults() //obligatory stickbug o--/--/-
		{
			Tooltip.SetDefault("Minions will target any enemy this hits\nEnemy also receives Ichor and Betsy's Curse\n'In another world, this attack deals 2 x 14 damage and focuses all enemies on the party member hit. Party member also receives 5% Armor Break for 2 turns.'");
		}

		public override void SetDefaults()
		{
			item.damage = 55;
			item.crit = 24;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = Item.buyPrice(0, 15, 0, 0);
			//item.shoot = ProjectileID.ToxicCloud2;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.scale = 2;
			item.useTurn = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (player.MinionAttackTargetNPC != target.whoAmI)
			{
				target.AddBuff(BuffID.Ichor, 60);
				target.AddBuff(BuffID.BetsysCurse, 60);
				player.MinionAttackTargetNPC = target.whoAmI;
			}
		}
    }
}