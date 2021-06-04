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
using Terraria.World.Generation;

namespace CalValPlus
{
    public class CalValPlusWorld : ModWorld
    {
		public static Vector2 AstralTest;

		public static Vector2 AstralTest2;

		public bool asttttr = false;
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int astraltest = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
			if (astraltest != -1)
			{
				asttttr = true;
				tasks.Insert(astraltest + 1, new PassLegacy("Astral Test", delegate (GenerationProgress progress)
				{
					GenerateStuff();
					progress.Message = "Astral Test";
				}));
			}
			int astraltest2 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
			if (astraltest2 != -1)
			{
				tasks.Insert(astraltest + 2, new PassLegacy("Astral Test2", delegate (GenerationProgress progress)
				{
					GenerateStuff();
					progress.Message = "Astral Test2";
				}));
			}
		}

		private int SnowX(bool xshit = false)
		{
			if (xshit)
			{
				for (int x = Main.maxTilesX; x > 0; x--)
				{
					for (int y = 0; y < Main.worldSurface; y++)
					{
						if (WorldGen.InWorld(x, y))
							if (Main.tile[x, y].type == TileID.SnowBlock) return x;
					}
				}
			}
			else
			{
				for (int x = 0; x < Main.maxTilesX; x++)
				{
					for (int y = 0; y < Main.worldSurface; y++)
					{
						if (WorldGen.InWorld(x, y))
							if (Main.tile[x, y].type == TileID.SnowBlock) return x;
					}
				}
			}
			return 0;
		}

		private bool generatedpermastle = true;

		
		/*private void Castle()
		{
			float widthScale = (Main.maxTilesX / 4200f);
			int numberToGenerate = WorldGen.genRand.Next(1, (int)(2f * widthScale));
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
					int i = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
					if (i <= Main.maxTilesX / 2 - 50 || i >= Main.maxTilesX / 2 + 50)
					{
						int j = 0;
						while (!Main.tile[i, j].active() && (double)j < Main.worldSurface)
						{
							j++;
						}
						if (Main.tile[i, j].type == TileID.SnowBlock)
						{
							j--;
							if (j > 150)
							{
								bool placementOK = true;
								for (int l = i - 4; l < i + 4; l++)
								{
									for (int m = j - 6; m < j + 20; m++)
									{
										if (Main.tile[l, m].active())
										{
											int type = (int)Main.tile[l, m].type;
											if (type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Cloud || type == TileID.RainCloud)
											{
												placementOK = false;
											}
										}
									}
								}
								if (placementOK)
								{
									Generator.GenerateStructure("NPCs/Permacastle", new Point16(i, k), mod);
								}
							}
						}
					}
				}
			}
		}*/
		public void GenerateStuff()
		{
			/*int DungeonDirection = 1;
			if (Main.dungeonX < Main.spawnTileX)
			{
				DungeonDirection = -1;
			}*/
			if (asttttr)
			{
				int DungeonDirection = 1;
				if (Main.dungeonX < Main.spawnTileX)
				{
					DungeonDirection = -1;
				}
				Vector2 permapos = new Vector2(Main.spawnTileX + Main.maxTilesX / 8 * DungeonDirection, Main.spawnTileY / 2 + Main.maxTilesY / 12  + 5);
				//Generator.GenerateStructure("NPCs/Permacastle", new Point16((int)permapos.X, (int)permapos.Y), base.mod, false, false);
				//Generator.GenerateStructure("NPCs/Permacastle1", new Point16((int)permapos.X, (int)permapos.Y), base.mod, false, false);
				//Generator.GenerateStructure("NPCs/Permacastle", new Point16((int)permapos.X, (int)permapos.Y), base.mod, false, false);


				Vector2 AstralPosition = new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2);
				Generator.GenerateStructure("NPCs/AstralTest2", new Point16((int)AstralPosition.X, (int)AstralPosition.Y), base.mod, false, false);

				/*Vector2 AstralPosition2 = new Vector2(Main.spawnTileX + Main.maxTilesX / 2, Main.maxTilesY / 2);
				Generator.GenerateStructure("NPCs/AstralTest2", new Point16((int)AstralPosition2.X, (int)AstralPosition2.Y), base.mod, false, false);*/


				int left = SnowX();
				int right = SnowX(true);
				int width = right - left;

				int center = left + width / 2;
				Generator.GenerateStructure("NPCs/Permacastle", new Point16(center, (int)permapos.Y), mod);
			}
		}
	}
}