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
			Main.npcFrameCount[npc.type] = 8;
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.npcSlots = 0f;
			npc.width = 60; //324
			npc.height = 110; //216
			npc.defense = 10;
			npc.lifeMax = 20000;
			npc.aiStyle = -1; //new
			aiType = -1; //new
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.lavaImmune = true;
			npc.behindTiles = false;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.dontTakeDamage = false;
		}
		
		public override void AI()
		{
			npc.TargetClosest();
			if (CalValPlusGlobalNPC.androalive < 0)
			{
				npc.active = false;
				npc.netUpdate = true;
			}
			npc.spriteDirection = -npc.direction;
			if (noai)
            {
				npc.ai[0] = 1;
				noai = false;
            }
			if (npc.ai[0] == 1)
			{
				{
					npc.ai[1]++;
					Vector2 vector97 = new Vector2(npc.Center.X, npc.Center.Y);
					float num816 = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - vector97.X;
					float num817 = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - vector97.Y;
					float num818 = (float)Math.Sqrt(num816 * num816 + num817 * num817);
					if (num818 > 90f)
					{
						num818 = 8f / num818;
						num816 *= num818;
						num817 *= num818;
						npc.velocity.X = (npc.velocity.X * 15f + num816) / 16f;
						npc.velocity.Y = (npc.velocity.Y * 15f + num817) / 16f;
						return;
					}
					if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 8f)
					{
						npc.velocity.Y *= 1.05f;
						npc.velocity.X *= 1.05f;
					}
					if (Main.netMode != 1 && npc.ai[1] >= 180)
					{
						npc.ai[1] = 0;
						npc.ai[0] = 2;
					}
				}
			}
			if (npc.ai[0] == 2)
			{
				float Xdist = Math.Abs(npc.Center.X - Main.player[npc.target].Center.X);
				float speedMult = 0.45f;
				float maxSpeed = 7f;
				npc.velocity.X += maxSpeed;
				if (npc.velocity.X > maxSpeed)
				{
					npc.velocity.X = maxSpeed;
				}
				if (npc.velocity.X < 0f - maxSpeed)
				{
					npc.velocity.X = 0f - maxSpeed;
				}
				int distancemin = 200;
				if (npc.Center.X < Main.player[npc.target].Center.X && npc.ai[2] < 0f && Xdist > (float)-200)
				{
					npc.ai[2] = 0f;
				}
				if (npc.Center.X > Main.player[npc.target].Center.X && npc.ai[2] > 0f && Xdist > (float)distancemin)
				{
					npc.ai[2] = 0f;
				}
				float Ydist = Main.player[npc.target].position.Y - (npc.position.Y + npc.height);
				if (Ydist < -50f)
				{
					npc.velocity.Y -= 0.2f;
				}
				if (Ydist > 100f)
				{
					npc.velocity.Y += 0.2f;
				}
				if (npc.velocity.Y > 8f)
				{
					npc.velocity.Y = 8f;
				}
				if (npc.velocity.Y < -8f)
				{
					npc.velocity.Y = -8f;
				}
				npc.ai[1]++;
				if (npc.ai[1] >= 120)
				{
					npc.ai[0] = 3;
					npc.ai[1] = 0;
				}
			}
			if (npc.ai[0] == 3)
			{
				Vector2 direction;
				direction.X = npc.direction;
				direction.Y = 0;
				npc.ai[1]++;
				npc.velocity.X = 0;
				npc.velocity.Y = 0;
				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] == 50)
				{
					Projectile.NewProjectile(npc.position, direction, ModContent.ProjectileType<Projectiles.AndromedaDeathray>(), 80, 0f, Main.myPlayer);
				}
				if (npc.ai[1] >= 230)
                {
					npc.ai[1] = 0;
					npc.ai[0] = 1;
				}
			}
		}
		public override bool CheckActive() { return false; }
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.15f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			float androframe = 8f / (float)Main.npcFrameCount[npc.type];

			//Wings
			Texture2D wingtexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/AssaultDroneGlow"));

			int wingtextureheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), wingtexturesquare, wingtexturealpha, npc.rotation, Utils.Size(wingtexturesquare) / 2f, npc.scale, npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
	}
}