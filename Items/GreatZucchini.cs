using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Thinf.Blocks;
using Thinf.Buffs;
using Microsoft.Xna.Framework;

namespace Thinf.Items
{
	public class GreatZucchini : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("When played: Transform all non-boss enemies into 1 [i:4]/1 [i:58] Zombies with no abilities.\n20 minute cooldown\n'Presto change-o! Now you see a powerful enemy. Now you don't!'");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 32;
			item.maxStack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = false;
		}
		public override bool UseItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<ZucchiniCooldown>(), Thinf.MinutesToTicks(20));

			Main.NewText("Presto change-o! Now you see a powerful enemy. Now you don't!", Color.Green);
			for (int i = 0; i < Main.maxNPCs; ++i)
            {
				NPC npc = Main.npc[i];
                if (npc.active && !npc.boss && !npc.friendly && ! npc.dontTakeDamage && !npc.dontCountMe && !npc.immortal)
				{
					Projectile.NewProjectile(npc.Center, new Vector2(0, -5), ProjectileID.ConfettiGun, 0, 0);
					npc.CloneDefaults(NPCID.Zombie);
					npc.type = NPCID.Zombie;
                    npc.damage = 1;
					npc.life = 1;
					npc.GivenName = "Zombie";
                }
            }
			return true;
		}
        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(ModContent.BuffType<ZucchiniCooldown>());
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Meteorite, 20);
			recipe.AddTile(ModContent.TileType<StarforgeTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
