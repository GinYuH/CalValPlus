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

	public class ShotgunDrone : ModNPC

	{
		private bool noai = true;
		int lasertimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blast Drone");
			Main.npcFrameCount[npc.type] = 8;
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.npcSlots = 0f;
			npc.width = 76; //324
			npc.height = 98; //216
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
			//npc.spriteDirection = npc.direction;
			if (npc.timeLeft <= 120 && !Main.player[npc.target].dead)
            {
				npc.timeLeft = 121;
            }
			if (!NPC.AnyNPCs(ModContent.NPCType<Andromeda>()) && !NPC.AnyNPCs(ModContent.NPCType<ItNPC>()))
            {
				npc.active = false;
            }
			npc.TargetClosest();
			if (CalValPlusGlobalNPC.androalive < 0)
			{
				npc.active = false;
				npc.netUpdate = true;
			}
			npc.spriteDirection = -npc.direction;
			if (noai)
			{
				npc.ai[1]++;
				if (npc.ai[1] >= 60)
				{
					npc.ai[1] = 0;
					npc.ai[0] = 1;
					noai = false;
				}
			}
			Vector2 vector97 = new Vector2(npc.Center.X, npc.Center.Y);
			float num816 = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - vector97.X;
			float num817 = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - vector97.Y;
			if (npc.ai[0] == 1)
			{
				npc.ai[1]++;
				if (npc.ai[1] >= 140f)
				{
					npc.ai[1] = 0;
					npc.ai[0] = 2;
				}
				float num818 = (float)Math.Sqrt(num816 * num816 + num817 * num817);
				if (num818 > 90f)
				{
					num818 = 18f / num818;
					num816 *= num818;
					num817 *= num818;
					npc.velocity.X = (npc.velocity.X * 15f + num816) / 16f;
					npc.velocity.Y = (npc.velocity.Y * 15f + num817) / 16f;
					return;
				}
				if (Math.Abs(num816) > 900f)
				{
					npc.velocity.X *= 1.5f;
                }
				if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 8f)
				{
					npc.velocity.Y *= 1.05f;
					npc.velocity.X *= 1.05f;
				}
				if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > 8f)
				{
					npc.velocity.Y *= 0.0005f;
					npc.velocity.X *= 0.0005f;
				}
			}
			if (npc.ai[0] == 2)
			{
				npc.ai[1]++;
				int num412 = 1;
				if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
				{
					num412 = -1;
				}
				float num413 = 25f;
				float num414 = 1.2f;
				float distanceX = 140f;
				float yoffset = 0f;
				
				if (npc.ai[3] == 0f)
                {
					distanceX = 340f;
					yoffset = 0f;
                }
				else if (npc.ai[3] == 1f)
                {
					distanceX = 440f;
					yoffset = -80f;
                }
				else if (npc.ai[3] == 2f)
				{
					distanceX = 380f;
					yoffset = 80f;
				}
				else
                {
					distanceX = 480f;
					yoffset = 80f;
				}
				Vector2 vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float num415 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num412 * distanceX) - vector40.X;
				float num416 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) + yoffset - vector40.Y;
				float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
				num417 = num413 / num417;
				num415 *= num417;
				num416 *= num417;
				if (npc.velocity.X < num415)
				{
					npc.velocity.X += num414;
					if (npc.velocity.X < 0f && num415 > 0f)
					{
						npc.velocity.X += num414;
					}
				}
				else if (npc.velocity.X > num415)
				{
					npc.velocity.X -= num414;
					if (npc.velocity.X > 0f && num415 < 0f)
					{
						npc.velocity.X -= num414;
					}
				}
				if (npc.velocity.Y < num416)
				{
					npc.velocity.Y += num414;
					if (npc.velocity.Y < 0f && num416 > 0f)
					{
						npc.velocity.Y += num414;
					}
				}
				else if (npc.velocity.Y > num416)
				{
					npc.velocity.Y -= num414;
					if (npc.velocity.Y > 0f && num416 < 0f)
					{
						npc.velocity.Y -= num414;
					}
				}
				/*Vector2 position = npc.Center;
				Vector2 targetPosition = Main.player[npc.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				float speed = 5f;
				float shootrandom = Main.rand.Next(-5, 5);
				int type = ProjectileID.MartianWalkerLaser;
				int damage = npc.damage;
				//Vector2 shooty;
				//shooty.X = (direction.X * speed) + shootrandom;
				//shooty.Y = (direction.Y * speed) + shootrandom;
				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] == Main.rand.Next(120, 240))
				{
					Projectile.NewProjectile(position.X, position.Y, (direction.X * speed), (direction.Y * speed), type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (direction.X * speed) + 1, (direction.Y * speed) + 1, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (direction.X * speed) + 1, (direction.Y * speed) - 1, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (direction.X * speed) - 1, (direction.Y * speed) + 1, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (direction.X * speed) - 1, (direction.Y * speed) - 1, type, damage, 0f, Main.myPlayer);
				}*/
				if (npc.ai[1] >= 120)
				{
					npc.ai[1] = 0;
					npc.ai[0] = 3;
				}
				/*float Xdist = Math.Abs(npc.Center.X - Main.player[npc.target].Center.X);
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
				}*/

			}
			if (npc.ai[0] == 3)
			{
				npc.velocity.X = 0;
				npc.velocity.Y = 0;
				npc.ai[1]++;
				Vector2 position = npc.Center;
				Vector2 targetPosition = Main.player[npc.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				float speed = 5f;
				float shootrandom = Main.rand.Next(-5, 5);
				int type = ProjectileID.MartianWalkerLaser;
				int damage = 80;
				//Vector2 shooty;
				//shooty.X = (direction.X * speed) + shootrandom;
				//shooty.Y = (direction.Y * speed) + shootrandom;
				if ((npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient) && ((npc.ai[1] == 15 && npc.ai[3] == 0f) || (npc.ai[1] == 30 && npc.ai[3] == 1f) || (npc.ai[1] == 45 && npc.ai[3] == 2f)))
				{
					Projectile.NewProjectile(position.X, position.Y, (npc.direction * speed), 0, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (npc.direction * speed) + 1, 1, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (npc.direction * speed) + 1, 2, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (npc.direction * speed) - 1, -1, type, damage, 0f, Main.myPlayer);
					Projectile.NewProjectile(position.X, position.Y, (npc.direction * speed) - 1, -2, type, damage, 0f, Main.myPlayer);
				}
				if ((npc.ai[1] >= 25 && npc.ai[3] == 0f) || (npc.ai[1] >= 40 && npc.ai[3] == 1f) || (npc.ai[1] >= 55 && npc.ai[3] == 2f))
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
			Texture2D wingtexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/ShotgunDroneGlow"));

			int wingtextureheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), wingtexturesquare, Color.White, npc.rotation, Utils.Size(wingtexturesquare) / 2f, npc.scale, npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
	}
}