using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Thinf.NPCs.MotherNature;
using Thinf.NPCs.SoulCatcher;
using static Terraria.ModLoader.ModContent;
using static Thinf.ModNameWorld;

namespace Thinf.Items
{
    public class WorldDestroyer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("World Destroyer");
            Tooltip.SetDefault("Many powerful bosses stop you from using this item\nUse it to see what boss is stopping you from using it!\n(DON'T THROW THIS AWAY! THIS IS IMPORTANT FOR PROGRESSION!)\n-Raisin");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }
        public override bool CanUseItem(Player player)
        {
            return (!NPC.AnyNPCs(NPCType<MotherNatureCadillac>()) && !NPC.AnyNPCs(NPCType<MotherNature>()));  //you can't spawn this boss multiple times
        }
        public override bool UseItem(Player player)
        {
            if (!timeLoop)
            {
                if (!Main.dedServ)
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/TacoBell"));
                if (!coreDestroyed)
                {
                    if (!NPC.downedMoonlord)
                    {
                        if (!NPC.downedGolemBoss)
                        {
                            if (!NPC.downedPlantBoss)
                            {
                                if (!NPC.downedMechBoss1 && !NPC.downedMechBoss2 && !NPC.downedMechBoss3)
                                {
                                    if (!downedWall)
                                    {
                                        Main.NewText("A concentration of souls inside the Wall Of Flesh collectively disallow the use of the World Destroyer!");
                                    }
                                    else
                                    {
                                        Main.NewText("The Mechs are so cool that they completely stop the World Destroyer from functioning!");
                                    }
                                }
                                else
                                {
                                    Main.NewText("The jungle is screaming, Screaming, SCREAMING!!! Plantera's loud guitar shredding drowns out the power of the World Destroyer!");
                                }
                            }
                            else
                            {
                                Main.NewText("Golem's power shields this world!");
                            }
                        }
                        else
                        {
                            Main.NewText("The Moon Lord has blocked access of the World Destroyer!");
                        }
                    }
                    else
                    {
                        Main.NewText("An apple is preventing you from using this item.");
                    }
                }
                else
                {
                    Main.NewText("The World Destroyer will erase everything, including you. If you want to survive it, you have to use the Time Looper first.");
                }
            }
            else
            {
                Main.NewText("An 8th Gen Cadillac Eldorado blocks your way!");
                NPC.NewNPC((int)(player.Center.X + 200 * player.direction), (int)(player.Center.Y - 1000), NPCType<MotherNatureCadillac>());
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/CadillacEntry"));
            }
            return true;
        }
        public override void AddRecipes()
        {

        }
    }
}
