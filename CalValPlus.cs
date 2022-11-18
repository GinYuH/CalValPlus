using Terraria.ModLoader;
using Terraria;
using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace CalValPlus
{
	public class CalValPlus : Mod
	{
		public override void PostSetupContent()
		{
			{
				Mod bossChecklist;
				ModLoader.TryGetMod("BossChecklist", out bossChecklist);
				if (bossChecklist != null)
				{
					bossChecklist.Call(new object[12]
				{
				"AddBoss",
				0.5f,
				ModContent.NPCType<NPCs.JohnWulfrum.JohnWulfrum>(),
				this,
				"John Wulfrum",
				(Func<bool>)(() => CalValPlusWorld.downedJohnWulfrum),
				ModContent.ItemType<Items.AndromedaSummon>(),
				null,
				new List<int>
				{
					ModLoader.GetMod("CalamityMod").Find<ModItem>("WulfrumShard").Type,
					ModLoader.GetMod("CalamityMod").Find<ModItem>("EnergyCore").Type,
					ModLoader.GetMod("CalValEX").Find<ModItem>("WulfrumHelipack").Type,
					ModLoader.GetMod("CalValEX").Find<ModItem>("WulfrumKeys").Type
				},
				$"Find a Wulfrum Workshop and find a way to open the [i:{ModContent.ItemType<Items.AndromedaSummon>()}] or use a [i:{ModContent.ItemType<Items.AndromedaSummon>()}]",
				"The maniacal tinkerer finally found a weak opponent",
				null
				});
				}
			}
		}
	}
}