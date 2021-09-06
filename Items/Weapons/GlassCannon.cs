using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Projectiles;

namespace Thinf.Items.Weapons
{
    public class GlassCannon : ModItem
    {
        int chargeTimer = 0;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoot glass shards that stick to the ground and explode\nUses sand as ammo");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.crit = 12;
            item.ranged = true;
            item.width = 52;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.reuseDelay = 0;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2;
            item.value = 120000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item27;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<GlassShard>(); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 7;
            item.useAmmo = AmmoID.Sand;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sandgun);
            recipe.AddIngredient(ItemID.Glass, 100);
            recipe.AddIngredient(ItemID.GlassKiln);
            recipe.AddIngredient(ItemID.SoulofFright, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        // What if I wanted an inaccurate gun? (Chain Gun)
        // Inaccurate Gun style: Single Projectile, Random spread 
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}*/

        // Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);

        }

        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?
        /*Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;*/

        // How can I get a "Clockwork Assault Rifle" effect?
        // 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
        /*	The following changes to SetDefaults()
			item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override bool ConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

        // How can I shoot 2 different projectiles at the same time?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

        // How can I choose between several projectiles randomly?
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
    }
}
