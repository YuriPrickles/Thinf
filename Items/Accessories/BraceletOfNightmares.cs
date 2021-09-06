using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Buffs;
using static Thinf.MyPlayer;
namespace Thinf.Items.Accessories
{
	// This file is showcasing inheritance to implement an accessory "type" that you can only have one of equipped
	// It also shows how you can interact with inherited methods
	// Additionally, it takes advantage of ValueTuple to make code more compact

	// First, we create an abstract class that all our exclusive accessories will be based on
	// This class won't be autoloaded by tModLoader, meaning it won't "exist" in the game, and we don't need to provide it a texture
	// Further down below will be the actual items (Green/Yellow Exclusive Accessory)
	public class BraceletOfNightmares : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 24;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 1, silver: 24);
			item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 25);
			recipe.AddIngredient(ItemID.BandofRegeneration, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		// Here we add our accessories, note that they inherit from ExclusiveAccessory, and not ModItem

		public override void SetStaticDefaults()
		{
				Tooltip.SetDefault("Life regen increased by 10HP/s when you have the Nightmare debuff\n6% increased damage\nWhile under the Nightmare debuff, enemies below 1000 HP get a lot of debuffs");
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (nightmare)
			{
				for (int i = 0; i < Main.maxNPCs; ++i)
				{
					NPC npc = Main.npc[i];
					if (npc.life <= 1000 && !npc.friendly && !npc.townNPC && npc.type != NPCID.TargetDummy)
					{
						npc.AddBuff(BuffID.Venom, 120);
						npc.AddBuff(BuffID.Ichor, 120);
						npc.AddBuff(BuffID.OnFire, 120);
						npc.AddBuff(BuffID.ShadowFlame, 120);
						npc.AddBuff(BuffID.CursedInferno, 120);
						npc.AddBuff(BuffID.BetsysCurse, 120);
						npc.AddBuff(BuffID.Frostburn, 120);
						npc.AddBuff(BuffID.DryadsWardDebuff, 120);
						npc.AddBuff(BuffID.Daybreak, 120);
						npc.AddBuff(BuffID.Confused, 120);
						npc.AddBuff(BuffID.BrokenArmor, 120);
						npc.AddBuff(BuffID.Bleeding, 120);
						npc.AddBuff(BuffID.BoneJavelin, 120);
						npc.AddBuff(BuffID.WitheredArmor, 120);
						npc.AddBuff(BuffID.WitheredWeapon, 120);
						npc.AddBuff(BuffID.Poisoned, 120);
						npc.AddBuff(BuffID.Oiled, 120);
						npc.AddBuff(BuffID.Midas, 120);
						npc.AddBuff(ModContent.BuffType<Shocked>(), 120);
						npc.AddBuff(ModContent.BuffType<Disintegrating>(), 120);
					}
				}
				player.lifeRegen = 20;
				player.allDamage *= 1.06f;
			}
		}
	}
}