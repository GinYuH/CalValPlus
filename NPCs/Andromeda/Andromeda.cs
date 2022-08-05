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

namespace CalValPlus.NPCs.Andromeda
{
	public class Andromeda : ModNPC

	{
		int charging = 0;
		int framer = 21;
		int bottomoffset = 0;
		int face = 0;
		int wingdelay = 0;
		int wingrot = 7;
		int venttimer = 0;
		int ventframe = 0;
		int dronespawn = 0;
		int dronerespawn = 0;
		int dronepocalypse = 0;
		int invincibletimer = 0;
		int ventbuffer = 0;
		private bool wingdelayt = true;
		private bool wingup = true;
		private bool moveup = true;
		private bool statprobe = true;
		private bool invincible = false;
		private bool hasbeeninvincible = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Andromeda");
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 3f;
			NPC.width = 172; //324
			NPC.height = 136; //216
			NPC.defense = 10;
			NPC.lifeMax = 5500000;
			NPC.boss = true;
			NPC.aiStyle = -1; //new
			Main.npcFrameCount[NPC.type] = 1; //new
			AIType = -1; //new
			AnimationType = 10; //new
			NPC.knockBackResist = 0f;
			//npc.alpha = 230;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			//music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AndromedaOne");
			NPC.dontTakeDamage = false;
			NPC.netAlways = true;
			NPC.DR_NERD(0.5f);
		}
		public override void AI()
		{
			CalValPlusGlobalNPC.androalive = NPC.whoAmI;
			//Bottom section Animation
			if (framer >= 80)
            {
				moveup = false;
            }
			else if (framer <= 0)
            {
				moveup = true;
            }

			if (moveup)
            {
				framer++;
            }
			else if (!moveup)
            {
				framer--;
            }
			bottomoffset = 360 - framer;

			if (wingrot >= 7)
            {
				wingup = false;
            }
			else if (wingrot <= -24)
            {
				wingup = true;
            }
			if (wingup)
			{
				wingrot++;
			}
			else if (!wingup)
			{
				wingrot--;
			}

			wingdelay++;
			if (wingdelay >= 1)
			{
				wingdelay = 0;
			}

			//Vent sliding
			if (invincibletimer < 601 && invincibletimer > 1)
            {
				ventbuffer = 520;
            }
			else
            {
				ventbuffer = 0;
            }
			/*if (invincibletimer < 601 && invincibletimer > 1 && venttimer >= 170 + ventbuffer)
			{
				venttimer--;
			}*/
				venttimer++;
			if (venttimer < 20)
            {
				ventframe = 0;
            }
			else if (venttimer >= 10 && venttimer < 20)
            {
				ventframe = 1;
            }
			else if (venttimer >= 20 && venttimer < 30)
			{
				ventframe = 2;
			}
			else if (venttimer >= 30 && venttimer < 80 + ventbuffer)
			{
				ventframe = 3;
			}
			else if (venttimer >= 80 + ventbuffer && venttimer < 100 + ventbuffer)
			{
				ventframe = 2;
			}
			else if (venttimer >= 100 + ventbuffer && venttimer < 120 + ventbuffer)
			{
				ventframe = 1;
			}
			else if (venttimer >= 120 + ventbuffer)
			{
				ventframe = 0;
			}
			/*if (invincible == false && npc.life <= npc.lifeMax * 0.6f)
            {
				ventframe = 0;
            }*/

			//Spawn stationary turrets
			if (statprobe)
			{
				dronespawn++;
				/*NPC.NewNPC((int)npc.Center.X + 300, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X + 340, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 1f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X + 260, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 2f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 300, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 3f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 340, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 4f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 260, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 5f, 0f, 0f, 0f, 255);*/
				if (dronespawn == 5)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 357, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("ShotgunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 357, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("ShotgunDrone").Type, 0, 5f, 0f, 0f, 1f, 255);
				}
				if (dronespawn == 35)
                {
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 357, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("MachineGunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 357, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("MachineGunDrone").Type, 0, 5f, 0f, 0f, 1f, 255);
				}
				if (dronespawn == 60)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 357, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("ShotgunDrone").Type, 0, 5f, 0f, 0f, 2f, 255);
					statprobe = false;
					dronespawn = 0;
				}
			}
			//Setup phases
			NPC.localAI[0] = 1f;
			if (NPC.life <= NPC.lifeMax * 0.6f && !hasbeeninvincible)
			{
				NPC.dontTakeDamage = true;
				invincible = true;
				hasbeeninvincible = true;
			}
			if (invincible)
            {
				NPC.velocity *= 0.99f;
				if (invincibletimer < 601)
				{
					dronepocalypse++;
				}
				invincibletimer++;
				if (invincibletimer >= 1000)
                {
					NPC.dontTakeDamage = false;
					invincible = false;
					invincibletimer = 0;
                }
				if (invincibletimer == 2)
                {
					venttimer = 0;
				}
            }


			//AI, juicy YuH coded AI

			//Positioning for turrets
			//Left mega cannon
			Vector2 leftpos;
			leftpos.X = NPC.Center.X - 320;
			leftpos.Y = NPC.Center.Y + 600 - bottomoffset;

			//Right mega cannon
			Vector2 rightpos;
			rightpos.X = NPC.Center.X + 320;
			rightpos.Y = NPC.Center.Y + 600 - bottomoffset;

			//Top right mega cannon
			Vector2 toprightpos;
			toprightpos.X = NPC.Center.X + 130;
			toprightpos.Y = NPC.Center.Y - 495;

			//Top left mega cannon
			Vector2 topleftpos;
			topleftpos.X = NPC.Center.X - 130;
			topleftpos.Y = NPC.Center.Y - 495;

			// Debug dusts

			/*Dust dusttl;
			dusttl = Main.dust[Terraria.Dust.NewDust(topleftpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dusttr;
			dusttr = Main.dust[Terraria.Dust.NewDust(toprightpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dustl;
			dustl = Main.dust[Terraria.Dust.NewDust(leftpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dustr;
			dustr = Main.dust[Terraria.Dust.NewDust(rightpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];*/

			//Respawn drones
			if (NPC.life >= NPC.lifeMax * 0.6f)
			{
				dronerespawn++;
				if (dronerespawn == 90)
				{
					venttimer = 0;
				}
				if (dronerespawn == 120 && NPC.CountNPCS(ModContent.NPCType<Minions.ShotgunDrone>()) < 12 && NPC.CountNPCS(ModContent.NPCType<Minions.KamikazeDrone>()) < 4 && NPC.CountNPCS(ModContent.NPCType<Minions.MachineGunDrone>()) < 4)
                {
					int spawnpos = 377;

					int spawnchoice = Main.rand.Next(3);
					if (spawnchoice == 0)
					{
						spawnpos = -377;
					}
					else
					{
						spawnpos = 377;
					}
					int dronechoice = Main.rand.Next(3);
					if (dronechoice == 0)
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("KamikazeDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
					}
					else if (dronechoice == 1)
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("MachineGunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
					}
					else
					{
						NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("ShotgunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
					}
				}
			}
			if (dronerespawn >= 150)
			{
				dronerespawn = 0;
			}
			if (dronepocalypse == 20)
			{
				SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("CalValPlus/Sounds/PlasmaCasterFire"), NPC.Center);
				int spawnpos = 377;

				int spawnchoice = Main.rand.Next(3);
				if (spawnchoice == 0)
                {
					spawnpos = -377;
                }
				else
                {
					spawnpos = 377;
                }
				int dronechoice = Main.rand.Next(3);
				if (dronechoice == 0)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("KamikazeDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
				}
				else if (dronechoice == 1)
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("MachineGunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
				}
				else
				{
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + spawnpos, (int)NPC.Center.Y + 14, Mod.Find<ModNPC>("ShotgunDrone").Type, 0, 5f, 0f, 0f, 0f, 255);
				}
				dronepocalypse = 0;
			}

			//Movement 

			if (NPC.localAI[0] == 1f)
			{
				NPC.localAI[1]++;
				NPC.TargetClosest();
				/*float num893 = 0.45f;
				float num894 = 7f;
				npc.velocity.X += num893;
				if (npc.velocity.X > num894)
				{c
					npc.velocity.X = num894;
				}
				if (npc.velocity.X < 0f - num894)
				{
					npc.velocity.X = 0f - num894;
				}*/
				if (Main.player[NPC.target].dead)
                {
					NPC.ai[0] = -1;
					NPC.active = false;
					NPC.velocity.Y = 150f;
                }
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				if (playerY < 50f)
				{
					NPC.velocity.Y -= 0.1f;
				}
				if (playerY > 50f)
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
				if (NPC.velocity.X > 12f)
				{
					NPC.velocity.X = 12f + (playerX * 0.0001f);
				}
				if (NPC.velocity.X < -12f)
				{
					NPC.velocity.X = -12f + (playerX * 0.0001f);
				}

				//npc.velocity.Y = 0;
				//npc.velocity.X = 0;

				//Attacks

				int attackbase = 180;
				if (NPC.life <= NPC.lifeMax * 0.6f)
                {
					NPC.localAI[1]++;
                }
				Player player = Main.player[NPC.target];
				if (player.HasBuff(ModLoader.GetMod("CalamityMod").Find<ModBuff>("AndromedaBuff").Type) || player.HasBuff(ModLoader.GetMod("CalamityMod").Find<ModBuff>("AndromedaSmallBuff").Type))
				{
					NPC.localAI[1]++;
				}

				if (!invincible)
				{
					//if (npc.localAI[1] == attackbase || npc.localAI[1] == attackbase + 10 || npc.localAI[1] == attackbase + 20 || npc.localAI[1] == attackbase + 30 || npc.localAI[1] == attackbase + 40 || npc.localAI[1] == attackbase + 50 || npc.localAI[1] == attackbase + 60 || npc.localAI[1] == attackbase + 70 || npc.localAI[1] == attackbase + 80)
						if (NPC.localAI[1] == attackbase || NPC.localAI[1] == attackbase + 40 || NPC.localAI[1] == attackbase + 80)
						{
						//Gauss blasts
						SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("CalValPlus/Sounds/GaussWeaponFire"), NPC.Center);
						for (int x = 0; x < 4; x++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromAI(), rightpos.X, rightpos.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ModContent.ProjectileType<AndroMissile>(), 80, 0f, 255);
							Projectile.NewProjectile(NPC.GetSource_FromAI(), leftpos.X, leftpos.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ModContent.ProjectileType<AndroMissile>(), 80, 0f, 255);
						}
					}
					if (NPC.localAI[1] == attackbase * 3)
					{
						SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("CalValPlus/Sounds/PlasmaBolt"), NPC.Center);
						for (int x = 0; x < 4; x++)
						{
							Projectile.NewProjectile(NPC.GetSource_FromAI(), leftpos.X, leftpos.Y, -20, Main.rand.Next(-20, 20), ModContent.ProjectileType<AndromedaDeathrayLeft>(), 80, 0f, 255);
							Projectile.NewProjectile(NPC.GetSource_FromAI(), rightpos.X, rightpos.Y, 20, Main.rand.Next(-20, 20), ModContent.ProjectileType<AndromedaDeathrayRight>(), 80, 0f, 255);
						}
					}
				}
				if (NPC.localAI[1] >= attackbase * 3)
                {
					NPC.localAI[1] = 0;
				}

				//Faces
				if (NPC.localAI[1] < attackbase && !invincible)
                {
					face = 0;
                }
				else if (NPC.localAI[1] >= attackbase || invincible)
                {
					face = 3;
                }
				else
                {
					face = 0;
                }
			}
		}
		public override bool CheckActive() { return false; }
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[NPC.type];
			
			//Wings
			//Top
			Texture2D wingtexturetop = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingTop").Value);
			Texture2D wingtexturetopglow = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingTopGlow").Value);

			int wingtexturetopheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexturetop.Height / 1);

			Rectangle wingtexturetopsquare = new Rectangle(0, wingtexturetopheight, wingtexturetop.Width, wingtexturetop.Height / 1);
			Color wingtexturetopalpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturetop, NPC.Center - Main.screenPosition + new Vector2(0f - 400, NPC.gfxOffY - 40), wingtexturetopsquare, wingtexturetopalpha, NPC.rotation + (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturetop, NPC.Center - Main.screenPosition + new Vector2(0f + 400, NPC.gfxOffY - 40), wingtexturetopsquare, wingtexturetopalpha, NPC.rotation - (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturetopglow, NPC.Center - Main.screenPosition + new Vector2(0f - 400, NPC.gfxOffY - 40), wingtexturetopsquare, Color.White, NPC.rotation + (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturetopglow, NPC.Center - Main.screenPosition + new Vector2(0f + 400, NPC.gfxOffY - 40), wingtexturetopsquare, Color.White, NPC.rotation - (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			//Midle
			Texture2D wingtexturemiddle = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingMiddle").Value);
			Texture2D wingtexturemiddleglow = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingMiddleGlow").Value);

			int wingtexturemiddleheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexturemiddle.Height / 1);

			Rectangle wingtexturemiddlesquare = new Rectangle(0, wingtexturemiddleheight, wingtexturemiddle.Width, wingtexturemiddle.Height / 1);
			Color wingtexturemiddlealpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturemiddle, NPC.Center - Main.screenPosition + new Vector2(0f - 360, NPC.gfxOffY - 20), wingtexturemiddlesquare, wingtexturemiddlealpha, NPC.rotation + (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturemiddle, NPC.Center - Main.screenPosition + new Vector2(0f + 360, NPC.gfxOffY - 20), wingtexturemiddlesquare, wingtexturemiddlealpha, NPC.rotation - (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturemiddleglow, NPC.Center - Main.screenPosition + new Vector2(0f - 360, NPC.gfxOffY - 20), wingtexturemiddlesquare, Color.White, NPC.rotation + (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturemiddleglow, NPC.Center - Main.screenPosition + new Vector2(0f + 360, NPC.gfxOffY - 20), wingtexturemiddlesquare, Color.White, NPC.rotation - (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			//Bottom
			Texture2D wingtexturelower = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingLower").Value);
			Texture2D wingtexturelowerglow = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingLowerGlow").Value);

			int wingtexturelowerheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexturelower.Height / 1);

			Rectangle wingtexturelowersquare = new Rectangle(0, wingtexturelowerheight, wingtexturelower.Width, wingtexturelower.Height / 1);
			Color wingtextureloweralpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturelower, NPC.Center - Main.screenPosition + new Vector2(0f - 360, NPC.gfxOffY), wingtexturelowersquare, wingtextureloweralpha, NPC.rotation + (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturelower, NPC.Center - Main.screenPosition + new Vector2(0f + 360, NPC.gfxOffY), wingtexturelowersquare, wingtextureloweralpha, NPC.rotation - (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturelowerglow, NPC.Center - Main.screenPosition + new Vector2(0f - 360, NPC.gfxOffY), wingtexturelowersquare, Color.White, NPC.rotation + (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturelowerglow, NPC.Center - Main.screenPosition + new Vector2(0f + 360, NPC.gfxOffY), wingtexturelowersquare, Color.White, NPC.rotation - (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			//Curved
			Texture2D wingtexturecurve = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingCurve").Value);
			Texture2D wingtexturecurveglow = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaWingCurveGlow").Value);

			int wingtexturecurveheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexturecurve.Height / 1);

			Rectangle wingtexturecurvesquare = new Rectangle(0, wingtexturecurveheight, wingtexturecurve.Width, wingtexturecurve.Height / 1);
			Color wingtexturecurvealpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturecurve, NPC.Center - Main.screenPosition + new Vector2(0f - 340, NPC.gfxOffY - 20), wingtexturecurvesquare, wingtexturecurvealpha, NPC.rotation + (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturecurve, NPC.Center - Main.screenPosition + new Vector2(0f + 340, NPC.gfxOffY - 20), wingtexturecurvesquare, wingtexturecurvealpha, NPC.rotation - (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturecurveglow, NPC.Center - Main.screenPosition + new Vector2(0f - 340, NPC.gfxOffY - 20), wingtexturecurvesquare, Color.White, NPC.rotation + (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturecurveglow, NPC.Center - Main.screenPosition + new Vector2(0f + 340, NPC.gfxOffY - 20), wingtexturecurvesquare, Color.White, NPC.rotation - (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, NPC.scale, SpriteEffects.None, 0f);


			//Top
			Texture2D androbottom = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaBottom").Value);

			int androbottomheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (androbottom.Height / 1);

			Rectangle androbottomsquare = new Rectangle(0, androbottomheight, androbottom.Width, androbottom.Height / 1);
			Color androbottomalpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(androbottom, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY + 320 + framer), androbottomsquare, androbottomalpha, NPC.rotation, Utils.Size(androbottomsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);

			//Mega Busters
			Texture2D guntexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/AndromedaGun").Value);
			Texture2D gunglowtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/AndromedaGunGlow").Value);

			int gunheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (guntexture.Height / 1);

			Rectangle gunsquare = new Rectangle(0, gunheight, guntexture.Width, guntexture.Height / 1);
			spriteBatch.Draw(guntexture, NPC.Center - Main.screenPosition + new Vector2(0f - 270, NPC.gfxOffY + 220 + framer), gunsquare, wingtexturetopalpha, NPC.rotation, Utils.Size(gunsquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(guntexture, NPC.Center - Main.screenPosition + new Vector2(0f + 270, NPC.gfxOffY + 220 + framer), gunsquare, wingtexturetopalpha, NPC.rotation, Utils.Size(gunsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(gunglowtexture, NPC.Center - Main.screenPosition + new Vector2(0f - 270, NPC.gfxOffY + 220 + framer), gunsquare, Color.White, NPC.rotation, Utils.Size(gunsquare) / 2f, NPC.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(gunglowtexture, NPC.Center - Main.screenPosition + new Vector2(0f + 270, NPC.gfxOffY + 220 + framer), gunsquare, Color.White, NPC.rotation, Utils.Size(gunsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);

			//Top
			Texture2D androbottom2 = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaBottomGlow").Value);

			int androbottomheight2 = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (androbottom2.Height / 1);

			Rectangle androbottomsquare2 = new Rectangle(0, androbottomheight2, androbottom2.Width, androbottom2.Height / 1);
			spriteBatch.Draw(androbottom2, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY + 320 + framer), androbottomsquare2, Color.White, NPC.rotation, Utils.Size(androbottomsquare2) / 2f, NPC.scale, SpriteEffects.None, 0f);

			//Top
			Texture2D androtoper = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaTop").Value);

			int androtopheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (androtoper.Height / 1);

			Rectangle androtopsquare = new Rectangle(0, androtopheight, androtoper.Width, androtoper.Height / 1);
			Color androtopalpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(androtoper, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY - 182), androtopsquare, androtopalpha, NPC.rotation, Utils.Size(androtopsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);

			//Vent
			Texture2D venttexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/Vent").Value);

			//int ventheight = (int)((float)(npc.frame.Y / npc.frame.Height) * ventframe) * (venttexture.Height / 4);

			Rectangle ventsquare = venttexture.Frame(1, 4, 0, ventframe);
			Vector2 ventorigin = new Vector2(venttexture.Width / 2f, venttexture.Height / 2f / 4f);
			Color ventalpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(venttexture, NPC.Center - Main.screenPosition + new Vector2(0f - 374, NPC.gfxOffY), ventsquare, ventalpha, NPC.rotation, ventorigin, NPC.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(venttexture, NPC.Center - Main.screenPosition + new Vector2(0f + 374, NPC.gfxOffY), ventsquare, ventalpha, NPC.rotation, ventorigin, NPC.scale, SpriteEffects.FlipHorizontally, 0f);

			//Top
			Texture2D androtop = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/AndromedaTopGlow").Value);

			int androheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (androtop.Height / 1);

			Rectangle androsquare = new Rectangle(0, androheight, androtop.Width, androtop.Height / 1);
			spriteBatch.Draw(androtop, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY - 182), androsquare, Color.White, NPC.rotation, Utils.Size(androsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
			//Screen
			/*Texture2D screentexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Andromeda"));

			int screentexturenheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (screentexture.Height / 1);

			Rectangle screentexturesquare = new Rectangle(0, screentexturenheight, screentexture.Width, screentexture.Height / 1);
			spriteBatch.Draw(screentexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), screentexturesquare, drawColor, npc.rotation, Utils.Size(screentexturesquare) / 2f, npc.scale, SpriteEffects.None, 0f);*/

			return true;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[NPC.type];
			//Face
			Texture2D facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/NeutralAlt").Value);
			if (face == 1)
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/Neutral").Value);
			}
			else if (face == 2)
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/Cmon").Value);
			}
			else if (face == 3)
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/Exclamation").Value);
			}
			else if (face == 4)
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/Cheeky").Value);
			}
			else if (face == 5)
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/Death").Value);
			}
			else
			{
				facetexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/FacialExpressions/NeutralAlt").Value);
			}

			int facetexturenheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (facetexture.Height / 1);

			Rectangle facetexturesquare = new Rectangle(0, facetexturenheight, facetexture.Width, facetexture.Height / 1);
			spriteBatch.Draw(facetexture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), facetexturesquare, Color.White, NPC.rotation, Utils.Size(facetexturesquare) / 2f, NPC.scale, SpriteEffects.None, 0f);

			
			/*Texture2D activetex = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/ActiveCannon"));

			int activetexheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (activetex.Height / 1);

			Rectangle activetexsquare = new Rectangle(0, activetexheight, activetex.Width, activetex.Height / 1);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f + 300, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f + 340, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f + 260, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f - 300, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f - 340, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(activetex, npc.Center - Main.screenPosition + new Vector2(0f - 360, npc.gfxOffY - 30), activetexsquare, drawColor, npc.rotation, Utils.Size(activetexsquare) / 2f, npc.scale, SpriteEffects.None, 0f);*/
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(framer);
			writer.Write(charging);
			writer.Write(bottomoffset);
			writer.Write(face);
			writer.Write(wingdelay);
			writer.Write(venttimer);
			writer.Write(ventframe);
			writer.Write(dronespawn);
			writer.Write(dronerespawn);
			writer.Write(dronepocalypse);
			writer.Write(invincibletimer);
			writer.Write(ventbuffer);
			writer.Write(wingdelayt);
			writer.Write(wingup);
			writer.Write(moveup);
			writer.Write(statprobe);
			writer.Write(invincible);
			writer.Write(hasbeeninvincible);
	}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			wingdelayt = reader.ReadBoolean();
			wingup = reader.ReadBoolean();
			moveup = reader.ReadBoolean();
			statprobe = reader.ReadBoolean();
			invincible = reader.ReadBoolean();
			hasbeeninvincible = reader.ReadBoolean();
			framer = reader.ReadInt32();
			charging = reader.ReadInt32();
			bottomoffset = reader.ReadInt32();
			face = reader.ReadInt32();
			wingdelay = reader.ReadInt32();
			wingrot = reader.ReadInt32();
			venttimer = reader.ReadInt32();
			ventframe = reader.ReadInt32();
			dronespawn = reader.ReadInt32();
			dronerespawn = reader.ReadInt32();
			dronepocalypse = reader.ReadInt32();
			invincibletimer = reader.ReadInt32();
			ventbuffer = reader.ReadInt32();
	}
		public override void BossLoot(ref string name, ref int potionType)
		{
			Mod calam = ModLoader.GetMod("CalamityMod");
			potionType = calam.Find<ModItem>("OmegaHealingPotion").Type;
		}
		public override void OnKill()
		{
			Mod calam = ModLoader.GetMod("CalamityMod");
			Mod calval = ModLoader.GetMod("CalValEX");
			//Main.rand.Next(30, 41)
			Item.NewItem(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, calval.Find<ModItem>("AndroombaGBC").Type, 1);
			Item.NewItem(NPC.GetSource_Death(), (int)NPC.position.Y, NPC.width, NPC.height, calam.Find<ModItem>("Baguette").Type, Main.rand.Next(30, 41));
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			Vector2 gorespeed;
			gorespeed.X = NPC.velocity.X;
			gorespeed.Y = NPC.velocity.Y;
			Vector2 tower = new Vector2(NPC.Center.X + 30, NPC.Center.Y - 300);
			Vector2 bustergore = new Vector2(NPC.Center.X - 180, NPC.Center.Y + 200);
			Vector2 bustergore2 = new Vector2(NPC.Center.X - 80, NPC.Center.Y - 180);
			Vector2 undergore = new Vector2(NPC.Center.X - 90, NPC.Center.Y - 20);
			Vector2 pillar = new Vector2(NPC.Center.X + 10, NPC.Center.Y - 95);
			Vector2 towerdown = new Vector2(NPC.Center.X - 20, NPC.Center.Y + 80);
			Vector2 grapple = new Vector2(NPC.Center.X + 50, NPC.Center.Y + 150);
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_FromAI(),tower, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda1").Type, 1f); // Tower
				Gore.NewGore(NPC.GetSource_FromAI(), bustergore, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda2").Type, 1f); //Cannons
				Gore.NewGore(NPC.GetSource_FromAI(), bustergore2, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda2").Type, 1f); 
				Gore.NewGore(NPC.GetSource_FromAI(), undergore, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda3").Type, 1f); //Base next to screen
				Gore.NewGore(NPC.GetSource_FromAI(), towerdown, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda4").Type, 1f); //Below tower
				Gore.NewGore(NPC.GetSource_FromAI(), towerdown, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda4").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), pillar, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda5").Type, 1f); // Small pillar
				Gore.NewGore(NPC.GetSource_FromAI(), towerdown, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda6").Type, 1f); //Below tower chunks
				Gore.NewGore(NPC.GetSource_FromAI(), towerdown, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda6").Type, 1f);
				Gore.NewGore(NPC.GetSource_FromAI(), grapple, NPC.velocity, Mod.Find<ModGore>("Gores/Andromeda7").Type, 1f); //Lower grapple area
			}
		}
	}
}