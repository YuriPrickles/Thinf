using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.Blocks;
using Thinf.Items.Placeables;
using Thinf.NPCs.Core;

namespace Thinf.Items
{
    public class CorePlushie : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left-Click to summon Core\nRight-Click to play a little song!\n");
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 30;
            item.maxStack = 1;
            item.value = 0;
            item.rare = ItemRarityID.Cyan;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            return ModNameWorld.downedBlizzard && ModNameWorld.downedPM && ModNameWorld.downedSoulCatcher && ModNameWorld.downedHerbalgamation;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                int rand = Main.rand.Next(10);
                switch (rand)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        if (!Main.dedServ)
                            Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CorePlushieSong1").WithPitchVariance(.7f));
                        break;
                    case 7:
                    case 8:
                        if (!Main.dedServ)
                            Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CorePlushieSong3").WithPitchVariance(.7f));
                        break;
                    case 9:
                        if (!Main.dedServ)
                            Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CorePlushieSong2").WithPitchVariance(.7f));
                        break;
                }
            }
            else
            {
                if (NPC.AnyNPCs(ModContent.NPCType<Core>()))
                {
                    NPC core = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Core>())];
                    core.Center = player.Center - new Vector2(0, 125);
                    Main.combatText[CombatText.NewText(core.getRect(), Color.LightSalmon, "Wow you had to disturb me")].lifeTime = 240;
                }
                else
                {
                    NPC.NewNPC((int)player.position.X, (int)(player.position.Y - 200), ModContent.NPCType<Core>());
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Linimisifrififlium>());
            recipe.AddIngredient(ModContent.ItemType<HerbalCore>());
            recipe.AddIngredient(ModContent.ItemType<FrozenEssence>());
            recipe.AddIngredient(ModContent.ItemType<PoliticalPower>());
            recipe.AddTile(ModContent.TileType<StarforgeTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
