using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValPlus.NPCs.OceanicScourge
{
	[AutoloadBossHead]
	public class OceanicScourgeHead : ModNPC
	
	{
		bool TailSpawned = false;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Oceanic Scourge");
		}
		public override void SetDefaults()
		{
			NPC.damage = 78; 
			NPC.npcSlots = 3f;
			NPC.width = 32; //324
			NPC.height = 32; //216
			NPC.defense = 10;
			NPC.lifeMax = 100000;
			NPC.boss = true;
			NPC.aiStyle = 6; //new
			Main.npcFrameCount[NPC.type] = 1; //new
            AIType = -1; //new
            AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.alpha = 255;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.Boss1;
			//base.bossBag = mod.ItemType("UngodlyBag");
			NPC.netAlways = true;
		}
    }
}