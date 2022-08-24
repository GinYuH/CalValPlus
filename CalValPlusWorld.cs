using Microsoft.Xna.Framework;
//using StructureHelper;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;

namespace CalValPlus
{
    public class CalValPlusWorld : ModSystem
    {
		public bool asttttr = false;
		public static bool downedJohnWulfrum = false;
		public static bool jungledialogue = false;
		public static bool helldialogue = false;
		public static bool plaguedialogue = false;
		public static bool spacedialogue = false;
		public static bool hmdialogue = false;
		/*public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int astraltest = tasks.FindIndex(genpass => genpass.Name.Equals("Rust and Dust"));
			if (astraltest != -1)
			{
				asttttr = true;
				tasks.Insert(astraltest + 1, new PassLegacy("Astral Test", delegate (GenerationProgress progress)
				{
					Wolfram();
					progress.Message = "Wulfrum Workshop";
				}));
			}
			int astraltest2 = tasks.FindIndex(genpass => genpass.Name.Equals("Rust and Dust"));
			if (astraltest2 != -1)
			{
				tasks.Insert(astraltest + 2, new PassLegacy("Astral Test2", delegate (GenerationProgress progress)
				{

					Permass();
					progress.Message = "Frozen Stronghold";
				}));
			}
		}*/

		private bool generatedpermastle = true;

		public void Permass()
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
									//success = Generator.GenerateStructure("NPCs/FrozenStrongholdChest", new Point16(i, j - 20), Mod);
								}
							}
						
					}
				}
			}
		}
		public void Wolfram()
		{
			int DungeonDirection = 1;
			if (Main.dungeonX < Main.spawnTileX)
			{
				DungeonDirection = -1;
			}
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
					int i = WorldGen.genRand.Next(200, Main.maxTilesX / 6);
					if (DungeonDirection == 1)
					{
						i = WorldGen.genRand.Next(200, Main.maxTilesX / 6);
					}
					else
					{
						i = WorldGen.genRand.Next((Main.maxTilesX / 6) * 5, Main.maxTilesX - 200);
					}
					int j = 0;
					while (!Main.tile[i, j].HasTile && (double)j < Main.worldSurface)
					{
						j++;
					}
					if ((Main.tile[i, j].TileType == TileID.Grass || Main.tile[i, j].TileType == TileID.CorruptGrass || Main.tile[i, j].TileType == TileID.CrimsonGrass || Main.tile[i, j].TileType == TileID.JungleGrass) && !Main.tile[i, j - 1].HasTile && !Main.tile[i, j - 2].HasTile && !Main.tile[i, j - 3].HasTile && !Main.tile[i, j - 4].HasTile && !Main.tile[i, j - 5].HasTile && !Main.tile[i, j - 6].HasTile && !Main.tile[i, j - 7].HasTile && !Main.tile[i, j - 8].HasTile && !Main.tile[i, j - 9].HasTile && !Main.tile[i, j - 10].HasTile && !Main.tile[i, j - 11].HasTile && !Main.tile[i, j - 12].HasTile && !Main.tile[i, j - 13].HasTile && !Main.tile[i, j - 14].HasTile && !Main.tile[i, j - 15].HasTile)
					{
						j--;
						if (j > 150)
						{
							bool placementOK = true;
							for (int l = i - 20; l < i + 20; l++)
							{
								for (int m = j - 6; m < j + 40; m++)
								{
									if (Main.tile[l, m].HasTile)
									{
										int type = (int)Main.tile[l, m].TileType;
										if (type == TileID.RedBrick || type == TileID.SnowBrick || type == TileID.BlueDungeonBrick || type == TileID.GreenDungeonBrick || type == TileID.PinkDungeonBrick || type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Containers || type == TileID.FakeContainers || type == TileID.Containers2 || type == TileID.FakeContainers2 || type == TileID.LivingWood || type == TileID.LeafBlock || type == TileID.Demonite || type == TileID.Crimtane)
										{
											placementOK = false;
										}
									}
								}
							}
							if (placementOK)
							{
								//success = Generator.GenerateStructure("NPCs/WulfrumWorkshop", new Point16(i, j - 10), Mod);
							}
						}

					}
				}
			}
		}

		public static int permaTiles;

		public override void ResetNearbyTileEffects()
		{
			permaTiles = 0;
		}

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			Mod calamityMod = ModLoader.GetMod("CalamityMod");
			Mod calval = ModLoader.GetMod("CalValEX");
			permaTiles = tileCounts[calamityMod.Find<ModTile>("CryonicBrick").Type] + tileCounts[calval.Find<ModTile>("FrostflakeBrickPlaced").Type];
		}

		public static void UpdateWorldBool()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
		public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
		{
			downedJohnWulfrum = false;
		}
		/*public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound 
		{
			List<string> list = new List<string>();
			if (downedJohnWulfrum)
			{
				list.Add("johnWulfrum");
			}
			TagCompound val = new TagCompound();
			val.Add("downed", (object)list);
			return (TagCompound)(object)val;
		}*/
		public override void LoadWorldData(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("downed");
			downedJohnWulfrum = list.Contains("johnWulfrum");
		}
	}
}