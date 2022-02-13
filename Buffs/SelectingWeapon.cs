using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Prefixes;
using System;

namespace Thinf.Buffs
{
    // Ethereal Flames is an example of a buff that causes constant loss of life.
    // See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
    public class SelectingWeapon : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Selecting Weapon");
            Description.SetDefault("Left-click any weapon in your inventory to apply the prefix!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HeldItem.type != ItemID.None && player.HeldItem.damage > 0)
            {
                byte[] prefixes = CardPrefix.CardPrefixes.ToArray();
                switch (player.GetModPlayer<MyPlayer>().cardPrefixType)
                {
                    case "Merciless":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Merciless.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Merciless' applied to {player.HeldItem.Name}");
                        break;
                    case "Supersonic":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Supersonic.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Supersonic' applied to {player.HeldItem.Name}");
                        break;
                    case "Embiggened":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Embiggened.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Embiggened' applied to {player.HeldItem.Name}");
                        break;
                    case "BouncyWouncy":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.BouncyWouncy.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Bouncy Wouncy' applied to {player.HeldItem.Name}");
                        break;
                    case "Lightspeed":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Lightspeed.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Lightspeed' applied to {player.HeldItem.Name}");
                        break;
                    case "Efficient":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Efficient.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Efficient' applied to {player.HeldItem.Name}");
                        break;
                    case "Accurate":
                        player.HeldItem.SetDefaults(player.HeldItem.type);
                        player.HeldItem.Prefix(mod.PrefixType(CardPrefixType.Accurate.ToString()));
                        player.ClearBuff(ModContent.BuffType<SelectingWeapon>());
                        Main.NewText($"Prefix 'Accurate' applied to {player.HeldItem.Name}");
                        break;
                    case "Default":
                        Main.NewText("Hey hey hey hold on the prefix type in MyPlayer isnt set to anything you moron");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}