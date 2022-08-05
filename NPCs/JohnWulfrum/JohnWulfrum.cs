using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;
using CalamityMod;
using Terraria.Localization;

namespace CalValPlus.NPCs.JohnWulfrum
{
	[AutoloadBossHead]

	public class JohnWulfrum : ModNPC

	{
		private bool noai = true;
		private bool supercharge = false;
		int teldir = 0;
		private bool dial1 = false;
		private bool phasetrans = false;
		int runboy = 0;
		int chargedir = 0;
		int choppercounter = 0;
		bool sizeup = false;
		float sizetimer = 0;
		private bool wave1 = false;
		private bool wave2 = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("John Wulfrum");
			Main.npcFrameCount[NPC.type] = 16;
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 56; //324
			NPC.height = 37; //216
			NPC.defense = 6;
			NPC.lifeMax = 3000;
			NPC.aiStyle = -1; //new\
			AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 10, 0);
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.boss = true;
			NPC.DR_NERD(0.1f);
		}
		private void EdgyTalk(string text, Color color, bool combatText = false)
		{
			if (combatText)
			{
				CombatText.NewText(NPC.getRect(), color, text, true);
			}
			else
			{
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(text, color);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey(text), color);
				}
			}
		}

		public override void AI()
		{
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			choppercounter += 7;
			if (choppercounter >= 704)
			{
				choppercounter = 0;
			}
			if (supercharge && Main.rand.Next(5) == 0)
			{
				Dust dust;
				Vector2 position = NPC.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 43, 0f, 0f, 0, new Color(0, 177, 255), 3.5f)];
				dust.noGravity = true;
				dust.noLight = false;
			}
			if (NPC.ai[0] != 1 && NPC.ai[0] != 9)
			{
				NPC.spriteDirection = -NPC.direction;
			}
			if (NPC.alpha > 0)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (Main.player[NPC.target].dead)
			{
				NPC.active = false;
			}
			if (noai)
			{
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				NPC.ai[1]++;
				if (NPC.ai[1] >= 160)
				{
					NPC.TargetClosest();
					float playerXX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
					if (playerXX > 0)
					{
						NPC.direction = 1;
					}
					else
					{
						NPC.direction = -1;
					}
				}
				if (NPC.ai[3] != 2f)
				{
					if (NPC.ai[1] >= 20 && !dial1)
					{
						EdgyTalk("What! Who goes there!?", Color.LightSeaGreen, true);
						dial1 = true;
					}
					if (NPC.ai[1] == 180)
					{
						EdgyTalk("...", Color.LightSeaGreen, true);
					}
					if (NPC.ai[1] == 300)
					{
						EdgyTalk("Are you one of the people who've been destroying my work!?", Color.LightSeaGreen, true);
					}
					if (NPC.ai[1] == 420)
					{
						EdgyTalk("I've spent YEARS working on those robots!", Color.LightSeaGreen, true);
					}
					if (NPC.ai[1] == 510)
					{
						EdgyTalk("I'll need to forcibly close this issue", Color.LightSkyBlue, true);
					}
					if (NPC.ai[1] >= 630)
					{
						NPC.ai[1] = 0;
						NPC.ai[0] = 1;
						noai = false;
					}
				}
				if (NPC.ai[3] == 2 && NPC.ai[1] == 60 && CalValPlusWorld.downedJohnWulfrum)
                {
					EdgyTalk("You want to go again? Have at it!", Color.LightSkyBlue, true);
				}
				if (NPC.ai[3] == 2 && NPC.ai[1] == 60 && !CalValPlusWorld.downedJohnWulfrum)
				{
					dial1 = true;
					NPC.ai[3] = 0;
					NPC.ai[1] = 299;
				}
				if (NPC.ai[3] == 2 && NPC.ai[1] >= 160)
                {
					NPC.ai[1] = 0;
					NPC.ai[3] = 0;
					NPC.ai[0] = 1;
					noai = false;
				}
				if (NPC.life < NPC.lifeMax * 0.99f || playerX > 600 | playerX < -600 || playerY > 600 || playerY < -600)
				{
					EdgyTalk("Fine! I see how it is!", Color.LightSkyBlue, true);
					NPC.ai[1] = 0;
					NPC.ai[3] = 0;
					NPC.ai[0] = 1;
					noai = false;
				}
			}
			if (!noai && !phasetrans && NPC.life <= NPC.lifeMax * 0.5f && Main.expertMode)
			{
				wave1 = false;
				wave2 = false;
				runboy = 0;
				NPC.ai[1] = 0;
				NPC.ai[2] = 0;
				NPC.ai[3] = 0;
				NPC.ai[0] = 10;
				phasetrans = true;
			}
			if (NPC.ai[0] == 1) //Tank
			{
				NPC.damage = 60;
				NPC.spriteDirection = -chargedir;
				NPC.TargetClosest();
				NPC.ai[1]++;
				NPC.ai[2]++;
				NPC.ai[3]++;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				runboy++;
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				if (NPC.ai[2] == 1)
				{
					SoundEngine.PlaySound(SoundID.Item23, NPC.position);
					if (playerX > 0)
					{
						chargedir = 1;
					}
					else
					{
						chargedir = -1;
					}
				}
				int extraboost = supercharge ? 2 : 0;
				float speed = CalamityMod.World.CalamityWorld.revenge ? (6f + extraboost + (NPC.ai[1] / 50)) : (4f + extraboost + (NPC.ai[1] / 50));
				NPC.velocity.X = chargedir * speed;
				int num858 = 80;
				int num859 = 20;
				Vector2 position4 = new Vector2(NPC.Center.X - (float)(num858 / 2), NPC.position.Y + (float)NPC.height - (float)num859);
				bool flag50 = false;
				if (NPC.position.X < Main.player[NPC.target].position.X && NPC.position.X + (float)NPC.width > Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width && NPC.position.Y + (float)NPC.height < Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height - 16f)
				{
					flag50 = true;
				}
				if (flag50)
				{
					NPC.velocity.Y += 0.5f;
				}
				if (Collision.SolidCollision(position4, num858, num859))
				{
					if (NPC.velocity.Y > 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y > -0.2)
					{
						NPC.velocity.Y -= 0.025f;
					}
					else
					{
						NPC.velocity.Y -= 0.2f;
					}
					if (NPC.velocity.Y < -4f)
					{
						NPC.velocity.Y = -4f;
					}
				}
				else
				{
					if (NPC.velocity.Y < 0f)
					{
						NPC.velocity.Y = 0f;
					}
					if ((double)NPC.velocity.Y < 0.1)
					{
						NPC.velocity.Y += 0.025f;
					}
					else
					{
						NPC.velocity.Y += 0.5f;
					}
				}
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
				}
				if (runboy == (supercharge ? 110 : 150) && NPC.ai[3] < 295) //Charge for 2.5 seconds then switch directions
				{
					SoundEngine.PlaySound(SoundID.Item23, NPC.position);
					chargedir = chargedir * -1;
					runboy = 0;
				}
				if (((playerX > 1200 || playerY < -360 || playerX < -1200) && Main.expertMode) || ((playerX > 1200 || playerY < -260 || playerX < -1200) && CalamityMod.World.CalamityWorld.revenge))
				{
					NPC.velocity.Y -= 0.8f;
					runboy = 0;
					NPC.ai[0] = 8;
				}
				if (NPC.ai[1] >= 300 || NPC.ai[3] >= 900)
				{
					runboy = 0;
					NPC.ai[2] = 0;
					NPC.ai[1] = 0;
					NPC.ai[0] = 2;
				}
			}
			if (NPC.ai[0] == 2) //Bow + Helipack
			{
				NPC.damage = 0;
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				if (playerY < 50f)
				{
					NPC.velocity.Y -= 0.1f;
				}
				if (playerY > 100f)
				{
					NPC.velocity.Y += 0.1f;
				}
				if (NPC.velocity.Y > 2f)
				{
					NPC.velocity.Y = 2f;
				}
				if (NPC.velocity.Y < -4f)
				{
					NPC.velocity.Y = -4f;
				}

				if (playerX < 0f)
				{
					NPC.velocity.X -= 0.1f;
				}
				if (playerX > 0f)
				{
					NPC.velocity.X += 0.1f;
				}
				if (NPC.velocity.X > 6f)
				{
					NPC.velocity.X = 6f + (playerX * 0.001f);
				}
				if (NPC.velocity.X < -6f)
				{
					NPC.velocity.X = -6f + (playerX * 0.001f);
				}
				NPC.ai[1]++;
				NPC.ai[2]++;
				if (CalamityMod.World.CalamityWorld.death)
				{
					NPC.ai[2]++;
				}
				if (supercharge ? NPC.ai[2] >= 30 : NPC.ai[2] >= 70)
				{
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (10f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ProjectileID.WoodenArrowHostile;
					int superboost = supercharge ? 5 : 0;
					int damage = Main.expertMode ? 10 + superboost : 15 + superboost;
					Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
					NPC.ai[2] = 0;
				}
				if (NPC.ai[1] >= 360) //Lasts for 6 seconds
				{
					wave1 = false;
					wave2 = false;
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[0] = 3;
				}
			}
			if (NPC.ai[0] == 3) //Drones
			{
				NPC.noGravity = true;
				NPC.noTileCollide = false;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0;
				NPC.ai[1]++;
				NPC.ai[2]++;
				//Normal
				if (CalamityMod.World.CalamityWorld.revenge || (Main.expertMode && supercharge))
				{
					if (NPC.ai[2] >= 40 && !wave1) //Spawn drone every second
					{
						SoundEngine.PlaySound(SoundID.Item15, NPC.position);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 0f, 255);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 1f, 255);
						wave1 = true;
					}
					if (NPC.ai[2] >= 70 && !wave2) //Spawn drone every second
					{
						SoundEngine.PlaySound(SoundID.Item15, NPC.position);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 2f, 255);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 3f, 255);
						wave2 = true;
					}

				}
				else if (Main.expertMode || (!Main.expertMode && supercharge))
				{
					if (NPC.ai[2] >= 40 && !wave1) //Spawn drone every second
					{
						SoundEngine.PlaySound(SoundID.Item15, NPC.position);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 2f, 255);
						wave1 = true;
					}
					if (NPC.ai[2] >= 70 && NPC.alpha <= 0 && !wave2) //Spawn drone every second
					{
						SoundEngine.PlaySound(SoundID.Item15, NPC.position);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, 3f, 255);
						wave2 = true;
					}

				}
				else
				{
					if (NPC.ai[2] >= 60 && !wave1) //Spawn drone every second
					{
						SoundEngine.PlaySound(SoundID.Item15, NPC.position);
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, Main.rand.Next(2,4), 255);
						wave1 = true;
					}
				}
				
				if (NPC.ai[1] >= 120) //Go invisible
				{
					NPC.alpha += 4;
				}
				if (NPC.alpha >= 255)
				{
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[0] = 4;
				}
			}
			if (NPC.ai[0] == 4) //Stealth deactivate
			{
				NPC.ai[1]++;
				NPC.ai[2]++;
				NPC.TargetClosest();
				NPC.damage = 0;
				NPC.position.X = Main.player[NPC.target].Center.X - 120;
				NPC.position.Y = Main.player[NPC.target].Center.Y - 16;
				//210 and 300
				if ((NPC.ai[2] == 67 | NPC.ai[2] == 157) && (CalamityMod.World.CalamityWorld.revenge || (Main.expertMode && supercharge)))
                {
					SoundEngine.PlaySound(SoundID.Item43, Main.player[NPC.target].Center);
				}
				if (!NPC.AnyNPCs(ModContent.NPCType<WulfrumDroid>()) || CalamityMod.World.CalamityWorld.death ? NPC.ai[1] >= 240 : NPC.ai[1] >= 300)
				{
					NPC.alpha -= 2;
					if (NPC.alpha <= 0)
					{
						NPC.ai[0] = 5;
						for (int x = 0; x < 20; x++)
						{
							SoundEngine.PlaySound(SoundID.Item96, NPC.position);
							Dust dust;
							Vector2 position = NPC.Center;
							position.X = NPC.Center.X - 4;
							dust = Terraria.Dust.NewDustPerfect(position, 43, new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), 0, new Color(0, 177, 255), 1.718605f);
							dust.noGravity = true;
							NPC.ai[1] = 0;
						}
					}
				}
			}
			if (NPC.ai[0] == 5) //Jump and toss out knives
			{
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				if (NPC.ai[1] <= 2)
				{
					NPC.velocity.Y = -4;
					NPC.velocity.X = -6;
				}
				if (NPC.ai[1] >= 20)
				{
					NPC.ai[1] = 0;
					NPC.ai[0] = 6;
				}
			}
			if (NPC.ai[0] == 6)
			{
				if (NPC.velocity.X < 0)
				{
					NPC.velocity.X += 0.1f;
				}
				if (NPC.velocity.Y < 0)
				{
					NPC.velocity.X += 0.1f;
				}
				NPC.alpha = 0;
				NPC.TargetClosest();
				NPC.noGravity = false;
				NPC.noTileCollide = false;
				NPC.ai[1]++;
				NPC.ai[2]++;
				int ranshotspeed = (CalamityMod.World.CalamityWorld.revenge ? 68 : 85);
				if (NPC.ai[2] >= ranshotspeed)
				{
					SoundEngine.PlaySound(SoundID.Item1, NPC.position);
					Vector2 position = NPC.Center;
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					int type = ModContent.ProjectileType<Projectiles.WulfrumKnifeHostile>();
					int superboost = supercharge ? 5 : 0;
					int damage = Main.expertMode ? 10 + superboost : 15 + superboost;
					int spacer = CalamityMod.World.CalamityWorld.revenge || supercharge ? Main.rand.Next(3, 10) * NPC.direction : 5 * NPC.direction;
					for (int spacing = 0; spacing < 10; spacing++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, (NPC.direction * spacing * (CalamityMod.World.CalamityWorld.revenge ? 2.5f :3)) - spacer, -9, type, damage, 0f, Main.myPlayer);
					}
					NPC.ai[2] = 0;
				}
				if (NPC.ai[1] >= 300)
				{
					NPC.ai[2] = 0;
					NPC.ai[1] = 0;
					NPC.ai[0] = 7;
				}
			}
			if (NPC.ai[0] == 7) //Fire wulfrum bolts
			{
				NPC.ai[3]++;
				if (NPC.ai[3] == 1)
				{
					if (teldir == -1)
					{
						teldir = 1;
					}
					else if (teldir == 1)
					{
						teldir = -1;
					}
					else
					{
						if (Main.rand.Next(2) == 0)
						{
							teldir = -1;
						}
						else
						{
							teldir = 1;
						}
					}
					Vector2 dustpos = NPC.Center;
					for (int dusttimer = 0; dusttimer < 10; dusttimer++)
					{
						int dusty = Dust.NewDust(NPC.Center, NPC.width, NPC.height, 61, 0f, 0f, 100, default, 3f);
						Main.dust[dusty].noGravity = true;
					}
				}
				if (NPC.ai[3] == 2)
				{
					SoundEngine.PlaySound(SoundID.Item8, NPC.position);
					NPC.position.X = Main.player[NPC.target].Center.X + 300 * teldir;
					NPC.position.Y = Main.player[NPC.target].Center.Y - 80;
					Vector2 dustpos = NPC.Center;
					for (int dusttimer = 0; dusttimer < 10; dusttimer++)
					{
						int dusty = Dust.NewDust(NPC.Center, NPC.width, NPC.height, 61, 0f, 0f, 100, default, 3f);
						Main.dust[dusty].noGravity = true;
					}
				}
				if (NPC.ai[3] == 3)
				{
					for (int dusttimer = 0; dusttimer < 10; dusttimer++)
					{
						int dustf = Dust.NewDust(NPC.Center, NPC.width, NPC.height, 61, 0f, 0f, 100, default, 3f);
						Main.dust[dustf].noGravity = true;
					}
				}
				if (NPC.ai[3] >= 210)
				{
					NPC.ai[3] = 0;
				}
				NPC.velocity.X = 0;
				NPC.TargetClosest();
				NPC.ai[1]++;
				if (NPC.ai[1] >= 30 || Main.expertMode)
				{
					NPC.ai[2]++;
					int shotspeed = (CalamityMod.World.CalamityWorld.revenge ? 90 : 80);
					if (NPC.ai[2] >= shotspeed)
					{
						SoundEngine.PlaySound(SoundID.Item43, NPC.position);
						Vector2 position = NPC.Center;
						position.X = NPC.Center.X + (10f * NPC.direction);
						Vector2 targetPosition = Main.player[NPC.target].Center;
						Vector2 direction2 = targetPosition - position;
						Vector2 direction;
						direction.X = NPC.direction;
						direction.Y = 0;
						direction2.Normalize();
						direction.Normalize();
						float speedX = 6f;
						float speed = 8f;
						int type = ModContent.ProjectileType<Projectiles.WulfrumBoltHostile>();
						int superboost = supercharge ? 5 : 0;
						int damage = Main.expertMode ? 15 + superboost : 20 + superboost;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speedX, type, damage, 0f, Main.myPlayer);
						if ((Main.expertMode && Main.rand.Next(4) == 0) || CalamityMod.World.CalamityWorld.revenge || supercharge)
						{
							Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, direction.X * speedX, -3f, type, damage, 0f, Main.myPlayer);
							Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, direction.X * speedX, 3, type, damage, 0f, Main.myPlayer);
						}
						NPC.ai[2] = 0;
						if (CalamityMod.World.CalamityWorld.death)
						{
							Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction2 * speed, type, damage, 0f, Main.myPlayer);
						}
					}
				}
				if (NPC.ai[1] >= 410)
				{
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[3] = 0;
					NPC.ai[0] = 1;
				}
			}
			if (NPC.ai[0] == 8) //Initial helipack catch up
			{
				NPC.damage = 0;
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				if (playerY < 20)
				{
					NPC.velocity.Y -= 0.3f;
				}
				else
				{
					NPC.velocity.Y += 0.01f;
				}
				if (NPC.velocity.Y > 10f)
				{
					NPC.velocity.Y = 10f;
				}
				if (NPC.velocity.Y < -10f)
				{
					NPC.velocity.Y = -10f;
				}

				if (playerX < 0f)
				{
					NPC.velocity.X -= 0.2f;
				}
				if (playerX > 0f)
				{
					NPC.velocity.X += 0.2f;
				}
				if (NPC.velocity.X > 25f)
				{
					NPC.velocity.X = 25f + (playerX * 0.001f);
				}
				if (NPC.velocity.X < -25f)
				{
					NPC.velocity.X = -25f + (playerX * 0.001f);
				}
				NPC.ai[1]++;
				NPC.ai[3]++;
				if (playerX < 680 && playerY > 40 && playerX > -680)
				{
					SoundEngine.PlaySound(SoundID.Item23, NPC.position);
					NPC.velocity.Y = 0;
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[0] = 9;
				}
				if (NPC.ai[3] >= 360)
				{
					NPC.ai[2] = 0;
					NPC.ai[1] = 0;
					NPC.ai[0] = 2;
				}
			}
			if (NPC.ai[0] == 9) //Tank enraged
			{
				//Orbital strike!!!
				Vector2 position = NPC.Center;
				Vector2 targetPosition = Main.player[NPC.target].Center;
				Vector2 direction = targetPosition - position;
				direction.Normalize();
				NPC.damage = 60;
				NPC.spriteDirection = -chargedir;
				NPC.TargetClosest();
				NPC.ai[1]++;
				NPC.ai[2]++;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				if (NPC.ai[2] == 1)
				{
					if (playerX > 0)
					{
						chargedir = 1;
					}
					else
					{
						chargedir = -1;
					}
					SoundEngine.PlaySound(SoundID.Item23, NPC.position);
					float speed = CalamityMod.World.CalamityWorld.revenge ? 16f : 12f;
					NPC.velocity.X = direction.X * speed;
					NPC.velocity.Y = direction.Y * speed;
				}
				if (NPC.ai[1] == 150) //Charge for 2.5 seconds then switch directions
				{
					SoundEngine.PlaySound(SoundID.Item23, NPC.position);
					chargedir = chargedir * -1;
				}
				if (playerX > 1600 || playerY < -360 || playerX < -1600 || NPC.ai[1] >= 70)
				{
					NPC.velocity.X = 0f;
					NPC.velocity.Y -= 0.8f;
					NPC.ai[0] = 8;
				}
			}
			if (NPC.ai[0] == 10) //SUPAHCHARGE
			{
				NPC.ai[1]++;
				NPC.noGravity = false;
				NPC.noTileCollide = false;
				NPC.dontTakeDamage = true;
				NPC.velocity.X = 0;
				NPC.velocity.Y += 0.1f;
				if (((NPC.collideX || NPC.collideY) && NPC.ai[1] < 180) || (NPC.ai[1] >= 180))
				{
					NPC.ai[3]++;
					if (NPC.ai[2] <= 100)
					{
						if (NPC.ai[3] >= 35)
						{
							SoundEngine.PlaySound(SoundID.Item43, NPC.position);
							for (int x = 0; x < 20; x++)
							{
								Dust dust;
								Vector2 position = NPC.Center;
								position.X = NPC.Center.X - 4;
								dust = Terraria.Dust.NewDustPerfect(position, 43, new Vector2(Main.rand.Next(-30, 30), Main.rand.Next(-30, 30)), 0, new Color(0, 177, 255), 1.718605f);
								dust.noGravity = true;
							}
							NPC.ai[3] = 0;
						}
					}
					if (NPC.ai[2] > 100)
					{
						if (NPC.ai[3] >= 20)
						{
							SoundEngine.PlaySound(SoundID.Item43, NPC.position);
							for (int x = 0; x < 20; x++)
							{
								Dust dust;
								Vector2 position = NPC.Center;
								position.X = NPC.Center.X - 4;
								dust = Terraria.Dust.NewDustPerfect(position, 43, new Vector2(Main.rand.Next(-30, 30), Main.rand.Next(-30, 30)), 0, new Color(0, 177, 255), 1.718605f);
								dust.noGravity = true;
							}
							NPC.ai[3] = 0;
						}
					}
					if (NPC.ai[2] <= 160)
					{
						NPC.ai[2]++;
						Dust dust;
						Vector2 position = NPC.Center;
						position.Y = NPC.Center.Y + (NPC.height / 2);
						position.X = NPC.Center.X - 4;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(2.5f, 0f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(2.5f, 2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(2.5f, -2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(-2.5f, 0f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(-2.5f, 2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(-2.5f, -2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(0f, 2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
						dust = Terraria.Dust.NewDustPerfect(position, 61, new Vector2(0f, -2.5f), 0, new Color(255, 255, 255), 1.918605f);
						dust.noGravity = true;
					}
				}
				if (NPC.ai[2] >= 160)
				{
					for (int x = 0; x < 40; x++)
					{
						Dust dust;
						Vector2 position = NPC.Center;
						position.X = NPC.Center.X - 4;
						dust = Terraria.Dust.NewDustPerfect(position, 43, new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), 0, new Color(0, 177, 255), 4.718605f);
						dust.noGravity = true;
					}
					SoundEngine.PlaySound(SoundID.Item117, NPC.position);
					supercharge = true;
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[3] = 0;
					NPC.ai[0] = 1;
				}
			}
		}
		public override void FindFrame(int frameHeight)
		{
			if (NPC.ai[0] == 1) //Charging on land
			{
				NPC.frameCounter += 1.0;
				if ((NPC.frameCounter > 6.0 && !supercharge) || (supercharge && NPC.frameCounter > 4.0))
				{
					NPC.frame.Y += frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 1 || NPC.frame.Y > frameHeight * 4)
				{
					NPC.frame.Y = frameHeight * 1;
				}
			}
			if (NPC.ai[0] == 2 || NPC.ai[0] == 8) //Bow
			{
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 6.0)
				{
					NPC.frame.Y += frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 6 || NPC.frame.Y > frameHeight * 9)
				{
					NPC.frame.Y = 6 * frameHeight;
				}
			}
			if (NPC.ai[0] == 3) //Drones
			{
				NPC.frameCounter += 1.0;
				if (NPC.frameCounter > 6.0)
				{
					NPC.frame.Y += frameHeight;
					NPC.frameCounter = 0.0;
				}
				if (NPC.frame.Y < frameHeight * 10 || NPC.frame.Y > frameHeight * 13)
				{
					NPC.frame.Y = 10 * frameHeight;
				}
			}
			if (NPC.ai[0] == 7) //Staff
			{
				NPC.frame.Y = 14 * frameHeight;
			}
			if (NPC.ai[0] == 9) //Charge in air
			{
				NPC.frame.Y = 5 * frameHeight;
			}
			if (NPC.ai[0] == 10) //Dragonball
			{
				NPC.frame.Y = 15 * frameHeight;
			}
			if (noai || NPC.ai[0] == 4 || NPC.ai[0] == 5 || NPC.ai[0] == 6) //Idle
			{
				NPC.frame.Y = 0 * frameHeight;
			}
		}
		public override void OnKill()
		{
			Mod calam = ModLoader.GetMod("CalamityMod");
			Mod calval = ModLoader.GetMod("CalValEX");
			CalValPlusWorld.downedJohnWulfrum = true;
			NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<JohnWulfrumSentry>(), 0, 0f, 0f, 0f, 0f, 255);
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (NPC.dontTakeDamage)
			{
				//float androframe = 11f / (float)Main.npcFrameCount[npc.type];\
				if (sizeup)
				{
					sizetimer += 0.01f;
				}
				else
				{
					sizetimer -= 0.01f;
				}
				if (sizetimer > 0.5f)
				{
					sizeup = false;
				}
				else if (sizetimer < 0.1f)
				{
					sizeup = true;
				}
				Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/JohnWulfrum/WulfrumShield").Value);

				int wingtextureheight = (int)((float)(choppercounter / NPC.frame.Height)) * (wingtexture.Height / 11);

				Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight, wingtexture.Width, wingtexture.Height / 11);
				Color wingtexturealpha = NPC.GetAlpha(drawColor);
				spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f + (10 * NPC.spriteDirection), NPC.gfxOffY - 10), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale + sizetimer, SpriteEffects.None, 0f);
			}

		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(noai);
			writer.Write(supercharge);
			writer.Write(dial1);
			writer.Write(phasetrans);
			writer.Write(runboy);
			writer.Write(chargedir);
			writer.Write(choppercounter);
			writer.Write(sizeup);
			writer.Write(sizetimer);
			writer.Write(teldir);
			writer.Write(wave1);
			writer.Write(wave2);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			supercharge = reader.ReadBoolean();
			dial1 = reader.ReadBoolean();
			phasetrans = reader.ReadBoolean();
			noai = reader.ReadBoolean();
			sizeup = reader.ReadBoolean();
			wave1 = reader.ReadBoolean();
			wave2 = reader.ReadBoolean();
			teldir = reader.ReadInt32();
			runboy = reader.ReadInt32();
			sizetimer = reader.ReadInt32();
			chargedir = reader.ReadInt32();
			choppercounter = reader.ReadInt32();
		}
	}
}
