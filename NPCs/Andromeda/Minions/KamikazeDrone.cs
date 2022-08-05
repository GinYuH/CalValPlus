using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.NPCs.Andromeda.Minions
{

	public class KamikazeDrone : ModNPC

	{
		public override string Texture => "CalValPlus/NPCs/Andromeda/Minions/AssaultDrone";
		private bool noai = true;
		int lasertimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Assault Drone");
			Main.npcFrameCount[NPC.type] = 8;
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 60; //324
			NPC.height = 110; //216
			NPC.defense = 10;
			NPC.lifeMax = 40000;
			NPC.aiStyle = -1; //new\
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
		}

		public override void AI()
		{
			//npc.spriteDirection = npc.direction;
			if (!NPC.AnyNPCs(ModContent.NPCType<Andromeda>()) && !NPC.AnyNPCs(ModContent.NPCType<ItNPC>()))
			{
				NPC.active = false;
			}
			NPC.TargetClosest();
			if (CalValPlusGlobalNPC.androalive < 0)
			{
				NPC.active = false;
				NPC.netUpdate = true;
			}
			NPC.spriteDirection = -NPC.direction;
			if (noai)
			{
				NPC.ai[1]++;
				if (NPC.ai[1] >= 60)
				{
					NPC.ai[1] = 0;
					NPC.ai[0] = 1;
					noai = false;
				}
			}
			Vector2 vector97 = new Vector2(NPC.Center.X, NPC.Center.Y);
			float num816 = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - vector97.X;
			float num817 = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - vector97.Y;
			if (NPC.ai[0] == 1)
			{
				if (Main.npc[CalValPlusGlobalNPC.androalive].life <= NPC.lifeMax * 0.6f)
				{
					NPC.dontTakeDamage = true;
				}
				NPC.ai[1]++;
				Vector2 position = NPC.Center;
				Vector2 targetPosition = Main.player[NPC.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				float speed = 14f;
				NPC.velocity.X = direction.X * speed + (NPC.ai[1] / 30);
				NPC.velocity.Y = direction.Y * 0.5f;

				if (NPC.ai[1] >= 240)
				{
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[0] = 3;
				}

			}
			if (NPC.ai[0] == 3)
			{
				if (Main.npc[CalValPlusGlobalNPC.androalive].life <= NPC.lifeMax * 0.6f && NPC.ai[2] < 120)
				{
					NPC.dontTakeDamage = true;
				}
				NPC.velocity *= 0.6f;
				NPC.ai[1]++;
				NPC.ai[2]++;
				Vector2 speed;
				speed.X = 0;
				speed.Y = 0;
				
				if (NPC.ai[2] >= 120)
				{
					NPC.dontTakeDamage = false;
					CheckDead();
					NPC.life = 0;
					return;
				}
			}
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}
		public override bool CheckActive() { return false; }

		public override bool CheckDead()
        {
			SoundEngine.PlaySound(SoundID.Item14, NPC.position);
			NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
			NPC.width = NPC.height = 312;
			NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
			NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
			for (int num621 = 0; num621 < 45; num621++)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				dust = Main.dust[Terraria.Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 75, 0f, 0f, 0, new Color(0, 255, 92), 3.907895f)];
				dust.noGravity = true;

			}
			//Projectile.NewProjectile(npc.Center, speed, ProjectileID.InfernoHostileBlast, 120, 0f, 255);
			return true;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 8f / (float)Main.npcFrameCount[NPC.type];

			//Wings
			Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/AssaultDroneGlow").Value);

			int wingtextureheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = ((NPC.ai[0] == 3) || (Main.npc[CalValPlusGlobalNPC.androalive].life <= Main.npc[CalValPlusGlobalNPC.androalive].lifeMax * 0.6)) ? Color.Red : Color.Blue;
			spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale, NPC.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(noai);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			noai = reader.ReadBoolean();
		}
	}
}