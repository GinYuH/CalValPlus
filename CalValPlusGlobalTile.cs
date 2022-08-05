using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValPlus
{
	public class CalValPlusGlobalTile : GlobalTile
	{
		public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
		{
			if (type == ModLoader.GetMod("CalamityMod").Find<ModTile>("CryonicBrick").Type)
			{
				return Main.hardMode;
			}
			return true;
		}
		public override bool CanExplode(int i, int j, int type)
		{
			if (type == ModLoader.GetMod("CalamityMod").Find<ModTile>("CryonicBrick").Type)
			{
				return Main.hardMode;
			}
			return true;
		}
	}
}