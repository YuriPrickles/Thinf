using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf;
using Thinf.Projectiles;

namespace ExampleMod.Items
{

    public class ThinfGlobalItem : GlobalItem
    {
        public byte merciless;
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        int[] seedTypes = {
            ProjectileID.Seed,
            ModContent.ProjectileType<GhostSeed>(),
            ModContent.ProjectileType<FireSeed>(),
            ModContent.ProjectileType<FrostSeed>(),
            ModContent.ProjectileType<CosmoSeed>(),
            ModContent.ProjectileType<DeathSeed>()
        };
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            ThinfGlobalItem myClone = (ThinfGlobalItem)base.Clone(item, itemClone);
            myClone.merciless = merciless;
            return myClone;
        }
        public override void SetDefaults(Item item)
        {
            //if (MyPlayer.hasFrostSavageHelmetMeleeSizeBuff && item.melee)
            //{
            //    item.scale *= 1.3f;
            //}
            if (item.type == ItemID.EmptyBucket)
            {
                item.damage = 1;
            }
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<MyPlayer>().seedsAreCarrots && seedTypes.Contains(type))
            {
                Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY) / 2, ModContent.ProjectileType<CarrotChip>(), damage, knockBack, player.whoAmI);
                return false;
            }
            return true;
        }
        public override void OnCraft(Item item, Recipe recipe)
        {
            if (item.type == ItemID.SlimeChandelier)
            {
                Player player = Main.player[item.owner];

                player.AddBuff(BuffID.Slow, 600);
                player.AddBuff(BuffID.Slimed, 600);
            }
        }
    }
}