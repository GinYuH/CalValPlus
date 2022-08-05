using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValPlus
{
	public class CalValPlusGlobalWall : GlobalWall
	{

		public override void KillWall(int i, int j, int type, ref bool fail)
		{
			if (type == ModLoader.GetMod("CalamityMod").Find<ModWall>("CryonicBrickWall").Type && !Main.hardMode)
			{
				fail = true;
			}
			else if (type == ModLoader.GetMod("CalValEX").Find<ModWall>("FrostflakeWallPlaced").Type && !Main.hardMode)
			{
				fail = true;
			}
		}
		public override bool CanExplode(int i, int j, int type)
		{
			if (type == ModLoader.GetMod("CalamityMod").Find<ModWall>("CryonicBrickWall").Type)
			{
				return Main.hardMode;
			}
			else if (type == ModLoader.GetMod("CalValEX").Find<ModWall>("FrostflakeWallPlaced").Type)
			{
				return Main.hardMode;
			}
			return true;
		}
	}
}