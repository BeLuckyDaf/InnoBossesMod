using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InnoBossesMod.Items
{
    public class StrangeInequality : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Inequality");
            Tooltip.SetDefault("This inequality seems unsolvable.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.rare = ItemRarityID.Cyan;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item44;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Mathking.Mathking>());
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<NPCs.Mathking.Mathking>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}