using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.NPCs.Andromeda;

namespace CalValPlus.Items
{
	public class AndromedaSummon : ModItem
	{
		public override string Texture => "CalValPlus/NPCs/Andromeda/Minions/AndromedaTurret";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Andromeda Spawner");
			Tooltip.SetDefault("Summons the Martian Star Destroyer");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 18;
			item.maxStack = 1;
			item.rare = 11;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = false;
		}

		public override bool UseItem(Player player)
		{
			Main.PlaySound(1, (int)player.position.X, (int)player.position.Y, 0);
			if (Main.netMode != 1)
			{
				NPC.SpawnOnPlayer(player.whoAmI, (ModContent.NPCType<AndromedaStart>()));
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (NPC.AnyNPCs(ModContent.NPCType<AndromedaStart>()) || NPC.AnyNPCs(ModContent.NPCType<Andromeda>()))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			Mod calmod = ModLoader.GetMod("CalamityMod");
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(calmod.ItemType("DubiousPlating"), 5);
				recipe.AddIngredient(calmod.ItemType("MysteriousCircuitry"), 5);
				recipe.AddIngredient(calmod.ItemType("PowerCell"), 200);
				recipe.AddIngredient(calmod.ItemType("HellcasterFragment"), 5);
				recipe.AddTile(calmod.TileType("DraedonsForge"));
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}