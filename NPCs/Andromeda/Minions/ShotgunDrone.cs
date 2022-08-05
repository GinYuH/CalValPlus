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
		int playdir = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blast Drone");
			Main.npcFrameCount[NPC.type] = 8;
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 76; //324
			NPC.height = 98; //216
			NPC.defense = 10;
			NPC.lifeMax = 40000;
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
			//npc.spriteDirection = npc.direction;
			if (NPC.timeLeft <= 120 && !Main.player[NPC.target].dead)
            {
				NPC.timeLeft = 121;
            }
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
			if (Main.npc[CalValPlusGlobalNPC.androalive].life >= Main.npc[CalValPlusGlobalNPC.androalive].lifeMax * 0.6)
			{
				if (NPC.ai[0] == 1)
				{
					if (NPC.life <= NPC.lifeMax * 0.3f)
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 2;
					}
					NPC.ai[1]++;
					if (NPC.ai[1] >= 140f)
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 2;
					}
					float num818 = (float)Math.Sqrt(num816 * num816 + num817 * num817);
					if (num818 > 90f)
					{
						num818 = 18f / num818;
						num816 *= num818;
						num817 *= num818;
						NPC.velocity.X = (NPC.velocity.X * 15f + num816) / 16f;
						NPC.velocity.Y = (NPC.velocity.Y * 15f + num817) / 16f;
						return;
					}
					if (Math.Abs(num816) > 900f)
					{
						NPC.velocity.X *= 1.5f;
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < 8f)
					{
						NPC.velocity.Y *= 1.05f;
						NPC.velocity.X *= 1.05f;
					}
					if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) > 8f)
					{
						NPC.velocity.Y *= 0.0005f;
						NPC.velocity.X *= 0.0005f;
					}
				}
				if (NPC.ai[0] == 2)
				{
					NPC.ai[1]++;
					if (NPC.ai[1] < 2)
					{
						if (Main.player[NPC.target].velocity.X < 0)
						{
							playdir = -1;
						}
						else
						{
							playdir = 1;
						}
					}
					float num413 = 25f;
					float num414 = 1.2f;
					float distanceX = 140f;
					float yoffset = 0f;

					if (NPC.ai[3] == 0f)
					{
						distanceX = 340f;
						yoffset = 0f;
					}
					else if (NPC.ai[3] == 1f)
					{
						distanceX = 440f;
						yoffset = -80f;
					}
					else if (NPC.ai[3] == 2f)
					{
						distanceX = 380f;
						yoffset = 80f;
					}
					else
					{
						distanceX = 480f;
						yoffset = 80f;
					}
					Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
					float num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(playdir * distanceX) - vector40.X;
					float num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) + yoffset - vector40.Y;
					float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
					num417 = num413 / num417;
					num415 *= num417;
					num416 *= num417;
					if (NPC.velocity.X < num415)
					{
						NPC.velocity.X += num414;
						if (NPC.velocity.X < 0f && num415 > 0f)
						{
							NPC.velocity.X += num414;
						}
					}
					else if (NPC.velocity.X > num415)
					{
						NPC.velocity.X -= num414;
						if (NPC.velocity.X > 0f && num415 < 0f)
						{
							NPC.velocity.X -= num414;
						}
					}
					if (NPC.velocity.Y < num416)
					{
						NPC.velocity.Y += num414;
						if (NPC.velocity.Y < 0f && num416 > 0f)
						{
							NPC.velocity.Y += num414;
						}
					}
					else if (NPC.velocity.Y > num416)
					{
						NPC.velocity.Y -= num414;
						if (NPC.velocity.Y > 0f && num416 < 0f)
						{
							NPC.velocity.Y -= num414;
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
					if (NPC.ai[1] >= 120)
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 3;
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
				if (NPC.ai[0] == 3)
				{
					NPC.velocity.X = 0;
					NPC.velocity.Y = 0;
					NPC.ai[1]++;
					Vector2 position = NPC.Center;
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 5f;
					float shootrandom = Main.rand.Next(-5, 5);
					int type = ProjectileID.MartianWalkerLaser;
					int damage = 80;
					//Vector2 shooty;
					//shooty.X = (direction.X * speed) + shootrandom;
					//shooty.Y = (direction.Y * speed) + shootrandom;
					if ((NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient) && ((NPC.ai[1] == 15 && NPC.ai[3] == 0f) || (NPC.ai[1] == 30 && NPC.ai[3] == 1f) || (NPC.ai[1] == 45 && NPC.ai[3] == 2f)))
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * speed), 0, type, damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * speed) + 1, 1, type, damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * speed) + 1, 2, type, damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * speed) - 1, -1, type, damage, 0f, Main.myPlayer);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * speed) - 1, -2, type, damage, 0f, Main.myPlayer);
					}
					if ((NPC.ai[1] >= 25 && NPC.ai[3] == 0f) || (NPC.ai[1] >= 40 && NPC.ai[3] == 1f) || (NPC.ai[1] >= 55 && NPC.ai[3] == 2f))
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 1;
					}
				}
			}
			else
			{
				NPC.dontTakeDamage = true;
				if (NPC.ai[0] != 5)
				{
					NPC.ai[1]++;
					Vector2 position = NPC.Center;
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 13f;
					NPC.velocity.X = direction.X * speed;
					NPC.velocity.Y = direction.Y * speed * 0.5f;

					if (NPC.ai[1] >= 240)
					{
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[0] = 5;
					}

				}
				if (NPC.ai[0] == 5)
				{
					NPC.velocity *= 0.6f;
					NPC.ai[1]++;
					NPC.ai[2]++;
					Vector2 speed;
					speed.X = 0;
					speed.Y = 0;

					if (NPC.ai[2] >= 120)
					{
						NPC.dontTakeDamage = false;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, speed, ProjectileID.InfernoHostileBlast, 120, 0f, 255);
						CheckDead();
						NPC.life = 0;
						return;
					}
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
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(noai);
			writer.Write(playdir);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			noai = reader.ReadBoolean();
			playdir = reader.ReadInt32();
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 8f / (float)Main.npcFrameCount[NPC.type];

			//Wings
			Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/ShotgunDroneGlow").Value);

			int wingtextureheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = (Main.npc[CalValPlusGlobalNPC.androalive].life <= Main.npc[CalValPlusGlobalNPC.androalive].lifeMax * 0.6) ? Color.Red : Color.White;
			spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY - 1), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale, NPC.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
	}
}