using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;

namespace CalValPlus.NPCs
{

	public class ItNPC : ModNPC

	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("It");
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 68; //324
			NPC.height = 44; //216
			NPC.defense = 2;
			NPC.lifeMax = 2000000;
			NPC.aiStyle = -1; //new
			Main.npcFrameCount[NPC.type] = 1; //new
			AIType = -1; //new
			AnimationType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.dontTakeDamage = false;
			NPC.scale = 0.5f;
			NPC.DR_NERD(0.999999f);
		}
		public override void AI()
		{
			CalValPlusGlobalNPC.androalive = NPC.whoAmI;
			/*Mod clamMod =
				ModLoader.GetMod(
					"CalamityMod");
			clamMod.Call(ModContent.NPCType<ItNPC>(), 0.9999f);*/
		}
	}
}