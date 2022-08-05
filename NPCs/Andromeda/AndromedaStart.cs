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
			NPC.damage = 0;
			NPC.npcSlots = 3f;
			NPC.width = 32; //324
			NPC.height = 32; //216
			NPC.defense = 10;
			NPC.lifeMax = 5500000;
			NPC.boss = true;
			NPC.aiStyle = -1; //new
			Main.npcFrameCount[NPC.type] = 1; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss1;
			NPC.dontTakeDamage = true;
			NPC.netAlways = true;
		}
		public override void AI()
		{
			spawntimer++;
			NPC.velocity.Y = -1;
			if (spawntimer >= 180)
            {
				NPC.Transform(ModContent.NPCType<Andromeda>());
			}

		}
	}
}