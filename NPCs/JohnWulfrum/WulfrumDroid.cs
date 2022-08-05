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
using CalamityMod;

namespace CalValPlus.NPCs.JohnWulfrum
{

	public class WulfrumDroid : ModNPC

	{
		int laser = 0;
		bool alt = false;
		public override string Texture => "CalamityMod/Projectiles/Summon/WulfrumDroid";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wulfrum Droid");
			Main.npcFrameCount[NPC.type] = 12;
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 26; //324
			NPC.height = 24; //216
			NPC.defense = 10;
			NPC.lifeMax = 120;
			NPC.aiStyle = -1; //new\
			AIType = -1; //new
			NPC.knockBackResist = 0.6f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.DR_NERD(0.1f);
		}

		public override void AI()
		{
			NPC.spriteDirection = -NPC.direction;
			NPC.TargetClosest();
			if (NPC.ai[0] == 1)
			{
				NPC.damage = 25;
				Vector2 position = NPC.Center;
				Vector2 targetPosition = Main.player[NPC.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				float speed = 4f;
				NPC.velocity.X = direction.X * speed;
				NPC.velocity.Y = direction.Y * speed * 0.5f;
			}
			if (NPC.ai[0] == 2)
			{
				NPC.ai[1]++;
				NPC.ai[2]++;
				int num412 = 1;
				/*if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
				{
					num412 = -1;
				}*/
				float num413 = 25f;
				float num414 = 1.2f;
				float distanceX = 140f;
				float yoffset = 0f;

				if (NPC.ai[3] == 0f)
				{
					distanceX = 180f;
					yoffset = 140f;
					num412 = 1;
				}
				if (NPC.ai[3] == 1f)
				{
					distanceX = -180f;
					yoffset = 140f;

				}
				if (NPC.ai[3] == 2f)
				{
					distanceX = 180f;
					yoffset = -140f;
					num412 = 1;
				}
				if (NPC.ai[3] == 3f)
				{
					distanceX = -180f;
					yoffset = -140f;
				}
				Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num415 = Main.player[NPC.target].Center.X + (float)(Main.player[NPC.target].width / 2) + (float)(num412 * distanceX) - vector40.X;
				float num416 = Main.player[NPC.target].Center.Y + (float)(Main.player[NPC.target].height / 2) + (float)(num412 * yoffset) - vector40.Y;
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
				if ((NPC.ai[3] == 1f || NPC.ai[3] == 0f) ? NPC.ai[2] >= 120 : NPC.ai[2] >= 90)
				{
					laser++;
					if (laser >= 90)
					{
						if (NPC.CountNPCS(ModContent.NPCType<WulfrumDroid>()) < 3)
						{
							SoundEngine.PlaySound(SoundID.Item43, NPC.position);
						}
						Vector2 position = NPC.Center;
						position.X = NPC.Center.X + (10f * NPC.direction);
						Vector2 targetPosition = Main.player[NPC.target].Center;
						Vector2 direction = targetPosition - position;
						direction.Normalize();
						float speed = 10f;
						float speed2 = 6f;
						int type = ModContent.ProjectileType<WulfrumBoltHostile>();
						int damage = Main.expertMode ? 15 : 20;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
						if (Main.expertMode)
						{
							switch (NPC.ai[3])
							{
								case 0: //Bottom right
									if (alt)
									{
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, -1 * speed2, 0 * speed2, type, damage, 0f, Main.myPlayer);
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 0 * speed2, -1 * speed2, type, damage, 0f, Main.myPlayer);
									}
									break;
								case 1: //Bottom left
									if (!alt)
									{
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 0 * speed2, -1 * speed2, type, damage, 0f, Main.myPlayer);
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 1 * speed2, 0 * speed2, type, damage, 0f, Main.myPlayer);
									}
									break;
								case 2: //Top right
									if (!alt)
									{
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 0 * speed2, 1 * speed2, type, damage, 0f, Main.myPlayer);
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, -1 * speed2, 0 * speed2, type, damage, 0f, Main.myPlayer);
									}
									break;
								case 3: //Top left
									if (alt)
									{
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 1 * speed2, 0 * speed2, type, damage, 0f, Main.myPlayer);
										Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, 0 * speed2, 1 * speed2, type, damage, 0f, Main.myPlayer);
									}
									break;
							}
						}
						if (alt)
                        {
							alt = false;
                        }
						if (!alt)
                        {
							alt = true;
						}
						laser = 0;
					}
                }
				if (NPC.ai[3] == 1f || NPC.ai[3] == 0f)
				{
					if (NPC.ai[1] >= 330)
					{
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[0] = 3;
					}
				}
				if (NPC.ai[3] == 2f || NPC.ai[3] == 3f)
				{
					if (NPC.ai[1] >= 300)
					{
						NPC.ai[1] = 0;
						NPC.ai[2] = 0;
						NPC.ai[0] = 3;
					}
				}
			}
			if (NPC.ai[0] == 3)
            {
				NPC.velocity.Y = -7;
				laser++;
				if (laser == 90)
				{
					SoundEngine.PlaySound(SoundID.Item43, NPC.position);
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (10f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ModContent.ProjectileType<WulfrumBoltHostile>();
					int damage = Main.expertMode ? 15 : 20;
					Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
				}
			}
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1;
			if (NPC.frameCounter > 6)
			{
				NPC.frameCounter = 0.0;
				NPC.frame.Y += frameHeight;
			}
			if (NPC.frame.Y > frameHeight * 5)
			{
				NPC.frame.Y = 0;
			}
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(laser);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			laser = reader.ReadInt32();
		}
	}
}