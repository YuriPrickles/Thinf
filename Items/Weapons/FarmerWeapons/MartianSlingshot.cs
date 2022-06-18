using Terraria.ModLoader;
using Terraria.ID;
using Thinf.Projectiles;
using static Terraria.ModLoader.ModContent;
using Thinf.Blocks;
using Terraria;
using static Thinf.FarmerClass;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Thinf.Items.Weapons.FarmerWeapons
{
	public class MartianSling : ModItem
	{
		int missileCooldown = 0;
		int tickTimer = 0;
		bool drawText = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Martian Slingshot 055");
			Tooltip.SetDefault("Shoots homing seeds\nHit enemies with seeds to tag them\nRight-Click to shoot missiles at every tagged enemy");
		}
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults()
		{
			item.damage = 56;
			item.UseSound = null;
			item.shoot = ProjectileID.Seed;
			item.noMelee = true;
			item.shootSpeed = 20f;
			item.useTime = 5;
			item.useAnimation = 5;
			item.reuseDelay = 5;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 32;
			item.height = 32;
			item.rare = ItemRarityID.Blue;
			item.maxStack = 1;
			item.consumable = false;
			item.useAmmo = ItemID.Seed;
		}
		public override bool UseItem(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			drawText = true;
		}
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Player player = Main.player[item.owner];
			if (drawText)
			{
				if (missileCooldown > 0)
                {
					tickTimer++;
					if (tickTimer >= 60)
                    {
						missileCooldown--;
						tickTimer = 0;
                    }
					spriteBatch.DrawString(Main.fontItemStack, $"Missile Cooldown: {missileCooldown}", player.Center - Main.screenPosition + new Vector2(-25, 75), Color.CornflowerBlue);

				}
				if (player.GetModPlayer<MyPlayer>().cantMissileTags.Count > 0)
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Tagged Enemies: {player.GetModPlayer<MyPlayer>().cantMissileTags.Count}", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.CornflowerBlue);
				}
				else
				{
					spriteBatch.DrawString(Main.fontItemStack, $"Tagged Enemies: NONE", player.Center - Main.screenPosition + new Vector2(-25, 50), Color.CadetBlue);
				}
			}
		}
		public override void UpdateInventory(Player player)
		{
			drawText = false;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override float UseTimeMultiplier(Player player)
		{
			return ModPlayer(player).farmerSpeed;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, -2);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<MyPlayer>().cantMissileTags.Count > 0 && missileCooldown == 0)
				{
					foreach (int index in player.GetModPlayer<MyPlayer>().cantMissileTags)
					{
						Projectile proj = Projectile.NewProjectileDirect(player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 7, ModContent.ProjectileType<TagMissile>(), item.damage * 10, 0, player.whoAmI);
						TagMissile missile = proj.modProjectile as TagMissile;
						missile.taggedNPC = index;
					}
					missileCooldown = 30;
				}
				else if (missileCooldown == 0)
				{
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/BooWomp"));
					Main.combatText[CombatText.NewText(player.getRect(), Color.Teal, "No tagged enemies!")].lifeTime = 180;
				}
				else
				{
					if (!Main.dedServ)
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/BooWomp"));
					Main.combatText[CombatText.NewText(player.getRect(), Color.Teal, "Missile on cooldown!")].lifeTime = 180;
				}
				return false;
			}
			//if (player.altFunctionUse == 2)
			//         {
			//	return false;
			//         }
			Main.PlaySound(SoundID.Item97);
			Projectile projectile = Main.projectile[item.shoot];
			type = ModContent.ProjectileType<MartianSeed>();
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			// all vanilla damage types must be false for custom damage types to work
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}

		// As a modder, you could also opt to make these overrides also sealed. Up to the modder
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			add += ModPlayer(player).farmerDamageAdd;
			mult *= ModPlayer(player).farmerDamageMult;
		}

		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			// Adds knockback bonuses
			knockback += ModPlayer(player).farmerKnockback;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			// Adds crit bonuses
			crit += ModPlayer(player).farmerCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Get the vanilla damage tooltip
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				// We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
				// So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				// Change the tooltip text
				tt.text = damageValue + " plant " + damageWord;
			}
		}
	}
}