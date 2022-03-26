using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Prefixes
{
	public class CardPrefix : ModPrefix
	{
		private string sorry = "Sorry clicker class";
		internal static List<byte> CardPrefixes;
		internal float damageMult = 1f;
		internal int critBonus = 0;
		internal float manaMult = 1;
		internal float scaleMult = 1;
		internal float useTimeMult = 1;
		internal float knockbackMult = 1;
		internal float shootSpeedMult = 1;

		public override PrefixCategory Category => PrefixCategory.AnyWeapon;

		public CardPrefix() { }

		public CardPrefix(float damageMult, int critBonus, float manaMult = 0, float scaleMult = 0, float useTimeMult = 0, float knockbackMult = 0, float shootSpeedMult = 0)
		{
			this.damageMult = damageMult;
			this.critBonus = critBonus;
			this.scaleMult = scaleMult;
			this.useTimeMult = useTimeMult;
			this.knockbackMult = knockbackMult;
			this.manaMult = manaMult;
			this.shootSpeedMult = shootSpeedMult;
		}

		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				CardPrefixes = new List<byte>();
				AddCardPrefix(CardPrefixType.Merciless,    1.125f);
				AddCardPrefix(CardPrefixType.Supersonic,   1f, 0, 1f, 1f, 0.8f);
				AddCardPrefix(CardPrefixType.Embiggened,   1f, 0, 1f, 1.30f);
				AddCardPrefix(CardPrefixType.BouncyWouncy, 1f, 0, 1f, 1f, 1f, 1.16f);
				AddCardPrefix(CardPrefixType.Lightspeed,   1f, 0, 1f, 1f, 1f, 1f, 1.125f);
				AddCardPrefix(CardPrefixType.Efficient,    1f, 0, 1.17f);
				AddCardPrefix(CardPrefixType.Accurate,     1f, 10);
			}
			return false;
		}

		public override void Apply(Item item)
		{
			if ((item.accessory || item.damage > 0) && item.maxStack == 1)
			{
				item.damage = (int)(item.damage * damageMult);
				item.shootSpeed = (int)(item.shootSpeed * shootSpeedMult);
				item.mana = (int)(item.mana * manaMult);
				item.useTime = (int)(item.useTime * useTimeMult);
				item.useAnimation = (int)(item.useAnimation * useTimeMult);
				item.crit += critBonus;
				item.knockBack = (int)(item.knockBack * knockbackMult);
				item.scale = (int)(item.scale * scaleMult);
			}
		}

		public override void ModifyValue(ref float valueMult) => valueMult *= 1 * 1.425f;

		public override bool CanRoll(Item item)
		{
			if ((item.damage > 0) && item.maxStack == 1)
			{
				return true;
			}
			return true;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult = this.damageMult;
			critBonus = this.critBonus;
			knockbackMult = this.knockbackMult;
			useTimeMult = this.useTimeMult;
			scaleMult = this.scaleMult;
			shootSpeedMult = this.shootSpeedMult;
			manaMult = this.manaMult;
		}

		private void AddCardPrefix(CardPrefixType prefixType, float damageMult = 1f, int critBonus = 0, float manaMult = 1f, float scaleMult = 1f, float useTimeMult = 1f, float knockbackMult = 1f, float shootSpeedMult = 1f)
		{
			mod.AddPrefix(prefixType.ToString(), new CardPrefix(damageMult, critBonus, manaMult, scaleMult, useTimeMult, knockbackMult, shootSpeedMult));
			CardPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
		}
	}

	public enum CardPrefixType : byte
	{
		Merciless,
		Supersonic,
		Embiggened,
		BouncyWouncy,
		Lightspeed,
		Efficient,
		Accurate,

	}
}