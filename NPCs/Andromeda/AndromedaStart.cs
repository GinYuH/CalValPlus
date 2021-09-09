using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValPlus.NPCs.Andromeda
{
	public class AndromedaStart : ModNPC

	{
		int spawntimer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Booting up");
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.npcSlots = 3f;
			npc.width = 32; //324
			npc.height = 32; //216
			npc.defense = 10;
			npc.lifeMax = 100000;
			npc.boss = true;
			npc.aiStyle = -1; //new
			Main.npcFrameCount[npc.type] = 1; //new
			aiType = -1; //new
			animationType = 10; //new
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.lavaImmune = true;
			npc.behindTiles = false;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			music = MusicID.Boss1;
			npc.dontTakeDamage = true;
			npc.netAlways = true;
		}
		public override void AI()
		{
			spawntimer++;
			npc.velocity.Y = -1;
			if (spawntimer >= 180)
            {
				npc.Transform(ModContent.NPCType<Andromeda>());
			}

		}
	}
}