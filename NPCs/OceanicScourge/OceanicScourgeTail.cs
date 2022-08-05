using Microsoft.Xna.Framework;
using StructureHelper;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace CalValPlus.NPCs.OceanicScourge
{
	internal class OceanicScourgeTail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.Tooltip.SetDefault("AAAAAAA\n"+"Instantly spawns Permafrost's castle\n"+"Avoids chests, sky islands, and the Dungeon\n"+"If the world has no Snow Blocks near the surface, the structure won't spawn");
		}

		public override void SetDefaults()
		{
			base.Item.rare = 1;
			base.Item.autoReuse = false;
			base.Item.useStyle = 4;
			base.Item.useTime = 20;
			base.Item.useAnimation = 20;
		}

		public override bool CanUseItem(Player player)
		{
			float widthScale = Main.maxTilesX / 4200f;
			int numberToGenerate = 1;
			for (int k = 0; k < numberToGenerate; k++)
			{
				bool success = false;
				int attempts = 0;
				while (!success)
				{
					attempts++;
					if (attempts > 1000)
					{
						success = true;
						continue;
					}
					int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
					int j = 0;
					while (!Main.tile[i, j].HasTile && (double)j < Main.worldSurface)
					{
						j++;
					}
					if (Main.tile[i, j].TileType == TileID.SnowBlock && !Main.tile[i, j - 1].HasTile && !Main.tile[i, j - 2].HasTile && !Main.tile[i, j - 3].HasTile && !Main.tile[i, j - 4].HasTile && !Main.tile[i, j - 5].HasTile && !Main.tile[i, j - 6].HasTile && !Main.tile[i, j - 7].HasTile && !Main.tile[i, j - 8].HasTile && !Main.tile[i, j - 9].HasTile && !Main.tile[i, j - 10].HasTile && !Main.tile[i, j - 11].HasTile && !Main.tile[i, j - 12].HasTile && !Main.tile[i, j - 13].HasTile && !Main.tile[i, j - 14].HasTile && !Main.tile[i, j - 15].HasTile)
					{
						j--;
						if (j > 150)
						{
							bool placementOK = true;
							for (int l = i - 150; l < i + 150; l++)
							{
								for (int m = j - 6; m < j + 40; m++)
								{
									if (Main.tile[l, m].HasTile)
									{
										int type = (int)Main.tile[l, m].TileType;
										if (type == TileID.RedBrick || type == TileID.SnowBrick || type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Containers || type == TileID.FakeContainers || type == TileID.Containers2 || type == TileID.FakeContainers2)
										{
											placementOK = false;
										}
									}
								}
							}
							if (placementOK)
							{
								success = Generator.GenerateStructure("NPCs/FrozenStrongholdChest", new Point16(i, j - 20), Mod);
							}
						}

					}
				}
			}
			
			return base.CanUseItem(player);
		}
	}
}
