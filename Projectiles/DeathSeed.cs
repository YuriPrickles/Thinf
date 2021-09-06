
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	public class DeathSeed : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.Seed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Seed");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Seed);
			aiType = ProjectileID.Seed;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{ 
			if (target.FullName == "Zombie" || target.FullName == "Zombie Elf" || target.FullName == "Raincoat Zombie" || target.FullName == "Zombie Eskimo" || target.FullName == "Blood Zombie" || target.FullName == "Spore Zombie" || target.FullName == "Spore Skeleton" || target.FullName == "Skeleton" || target.FullName == "Hoplite" || target.FullName == "Bone Serpent" || target.FullName == "Doctor Bones" || target.FullName == "Cursed Skull" || target.FullName == "Angry Bones" || target.FullName == "Dark Caster" || target.FullName == "Tim" || target.FullName == "Ghost" || target.FullName == "Undead Viking" || target.FullName == "Undead Miner" || target.FullName == "Armored Skeleton" || target.FullName == "Skeleton Archer" || target.FullName == "Blue Armored Bones" || target.FullName == "Rusty Armored Bones" || target.FullName == "Hell Armored Bones" || target.FullName == "Possessed Armor" || target.FullName == "Wraith" || target.FullName == "Rune Wizard" || target.FullName == "Mummy" || target.FullName == "Light Mummy" || target.FullName == "Dark Mummy" || target.FullName == "Armored Viking" || target.FullName == "Ghoul" || target.FullName == "Vile Ghoul" || target.FullName == "Tainted Ghoul" || target.FullName == "Bone Lee" || target.FullName == "Necromancer" || target.FullName == "Ragged Caster" || target.FullName == "Diabolist" || target.FullName == "Skeleton Sniper" || target.FullName == "Tactical Skeleton" || target.FullName == "Skeleton Commando" || target.FullName == "Giant Cursed Skull" || target.FullName == "Paladin" || target.FullName == "The Groom" || target.FullName == "The Bride" || target.FullName == "Dreamer Ghoul" || target.FullName == "Frankenstein" || target.FullName == "The Possessed" || target.FullName == "Eyezor" || target.FullName == "Reaper" || target.FullName == "Scarecrow" || target.FullName == "Poltergeist" || target.FullName == "Headless Horseman")
			{
				damage *= 3;
			}
		}
	}
}