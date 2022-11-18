using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ModLoader;
using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria.ID;
using StructureHelper;

namespace CalValPlus.Subworlds
{
    public class StiltVillage : Subworld
	{
		public override string Name => "StiltVillage";

		public override int Width => 1750;
		public override int Height => 750;
		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;
		public override bool NormalUpdates => false;

        public int variantWorld;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new SubworldGenPass(delegate
			{
				Main.dayTime = false;
				Main.time = 0;
				Main.worldSurface = 600.0;
				Main.rockLayer = Main.maxTilesY;
				SubworldSystem.hideUnderworld = true;
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					for (int j = Main.maxTilesY / 2 + 2; j < Main.maxTilesY; j++)
					{
						Tile tile = Main.tile[i, j];
						tile.LiquidAmount = 255;
						tile.LiquidType = LiquidID.Water;
					}
					// Buildings
					if (i % 40 == 0 && i < (int)(Main.maxTilesX * 0.60f) - 40 && i > (int)(Main.maxTilesX * 0.40f))
					{
						Tile anchor = Main.tile[i, Main.maxTilesY / 2];
						anchor.HasTile = true;

						string house = "Subworlds/NapkinHouse";

						int choice = Main.rand.Next(10);
						int yoffset = 14;
						switch (choice)
                        {
							case 0:
							case 1:
							case 2:
							case 3:
								house = "Subworlds/NapkinHouse";
								break;
							case 4:
							case 5:
								house = "Subworlds/BathHouse";
								yoffset += 4;
								break;
							case 6:
							case 7:
								house = "Subworlds/StorageHouse";
								yoffset += 4;
								break;
							case 8:
							default:
								house = "Subworlds/RichHouse";
								yoffset += 14;
								break;
						}
						StructureHelper.Generator.GenerateStructure(house, new Terraria.DataStructures.Point16(i, Main.maxTilesY / 2 - yoffset), Mod);

					}
					// Platform
					if (Main.tile[i, Main.maxTilesY / 2].TileType != (ushort)ModContent.TileType<CalamityMod.Tiles.Abyss.AcidwoodTile>() && i < (int)(Main.maxTilesX * 0.70f) && i > (int)(Main.maxTilesX * 0.30f))
					{
						Tile sand = Main.tile[i, Main.maxTilesY / 2];
						sand.HasTile = true;
						sand.TileType = (ushort)ModContent.TileType<AcidwoodPlatformTile>();
					}
				}
			})
		};
		public override void Load()
		{
			ModTypeLookup<Subworld>.Register(this);
		}
	}
}