using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.NPCs.Andromeda.Minions
{

	public class AssaultDrone : ModNPC

	{
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
			NPC.lifeMax = 20000;
			NPC.aiStyle = -1; //new
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
			NPC.dontTakeDamage = false;
		}
		
		public override void AI()
		{
			NPC.TargetClosest();
			if (CalValPlusGlobalNPC.androalive < 0)
			{
				NPC.active = false;
				NPC.netUpdate = true;
			}
			NPC.spriteDirection = -NPC.direction;
			if (noai)
            {
				NPC.ai[0] = 1;
				noai = false;
            }
			if (NPC.ai[0] == 1)
			{
				{
					NPC.ai[1]++;
					Vector2 vector97 = new Vector2(NPC.Center.X, NPC.Center.Y);
					float num816 = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - vector97.X;
					float num817 = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - vector97.Y;
					float num818 = (float)Math.Sqrt(num816 * num816 + num817 * num817);
					if (num818 > 90f)
					{
						num818 = 8f / num818;
						num816 *= num818;
						num817 *= num818;
						NPC.velocity.X = (NPC.velocity.X * 15f + num816) / 16f;
						NPC.velocity.Y = (NPC.velocity.Y * 15f + num817) / 16f;
						return;
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < 8f)
					{
						NPC.velocity.Y *= 1.05f;
						NPC.velocity.X *= 1.05f;
					}
					if (Main.netMode != 1 && NPC.ai[1] >= 180)
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 2;
					}
				}
			}
			if (NPC.ai[0] == 2)
			{
				float Xdist = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X);
				float speedMult = 0.45f;
				float maxSpeed = 7f;
				NPC.velocity.X += maxSpeed;
				if (NPC.velocity.X > maxSpeed)
				{
					NPC.velocity.X = maxSpeed;
				}
				if (NPC.velocity.X < 0f - maxSpeed)
				{
					NPC.velocity.X = 0f - maxSpeed;
				}
				int distancemin = 200;
				if (NPC.Center.X < Main.player[NPC.target].Center.X && NPC.ai[2] < 0f && Xdist > (float)-200)
				{
					NPC.ai[2] = 0f;
				}
				if (NPC.Center.X > Main.player[NPC.target].Center.X && NPC.ai[2] > 0f && Xdist > (float)distancemin)
				{
					NPC.ai[2] = 0f;
				}
				float Ydist = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				if (Ydist < -50f)
				{
					NPC.velocity.Y -= 0.2f;
				}
				if (Ydist > 100f)
				{
					NPC.velocity.Y += 0.2f;
				}
				if (NPC.velocity.Y > 8f)
				{
					NPC.velocity.Y = 8f;
				}
				if (NPC.velocity.Y < -8f)
				{
					NPC.velocity.Y = -8f;
				}
				NPC.ai[1]++;
				if (NPC.ai[1] >= 120)
				{
					NPC.ai[0] = 3;
					NPC.ai[1] = 0;
				}
			}
			if (NPC.ai[0] == 3)
			{
				Vector2 direction;
				direction.X = NPC.direction;
				direction.Y = 0;
				NPC.ai[1]++;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0;
				if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[1] == 50)
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, direction, ModContent.ProjectileType<Projectiles.AndromedaDeathray>(), 80, 0f, Main.myPlayer);
				}
				if (NPC.ai[1] >= 230)
                {
					NPC.ai[1] = 0;
					NPC.ai[0] = 1;
				}
			}
		}
		public override bool CheckActive() { return false; }
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.15f;
			NPC.frameCounter %= Main.npcFrameCount[NPC.type];
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 8f / (float)Main.npcFrameCount[NPC.type];

			//Wings
			Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/AssaultDroneGlow").Value);

			int wingtextureheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale, NPC.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
	}
}