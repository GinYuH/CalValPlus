using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.NPCs.Andromeda;
using System;
using CsvHelper.TypeConversion;

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
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 1;
			Item.rare = 11;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		public override bool? UseItem(Player player)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, player.position);
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
	}
}