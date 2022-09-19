using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using CalValPlus.NPCs.Hypnos;
using CalamityMod.Items.Pets;
using CalamityMod.Tiles.DraedonSummoner;

namespace CalValPlus
{
	public class CalValPlusGlobalTile : GlobalTile
	{
		public override void RightClick(int i, int j, int type)
        {
			if (type == ModContent.TileType<CodebreakerTile>() && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<BloodyVein>() && NPC.CountNPCS(ModContent.NPCType<Draedon>()) <= 0)
            {
				NPC.NewNPC(new Terraria.DataStructures.EntitySource_TileBreak(i, j), (int)Main.LocalPlayer.Center.X, (int)(Main.LocalPlayer.Center.Y - 1200), ModContent.NPCType<Draedon>());
            }
        }
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