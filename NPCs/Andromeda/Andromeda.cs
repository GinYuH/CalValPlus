using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;
using CalamityMod.CalPlayer;

namespace CalValPlus.NPCs.Andromeda
{
	public class Andromeda : ModNPC

	{
		int charging = 0;
		int attacktimer = 0;
		int framer = 21;
		int bottomoffset = 0;
		int face = 0;
		int wingdelay = 0;
		int wingrot = 7;
		private bool wingdelayt = true;
		private bool wingup = true;
		private bool moveup = true;
		private bool statprobe = true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Andromeda");
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.npcSlots = 3f;
			npc.width = 172; //324
			npc.height = 136; //216
			npc.defense = 10;
			npc.lifeMax = 5500000;
			npc.boss = true;
			npc.aiStyle = -1; //new
			Main.npcFrameCount[npc.type] = 1; //new
			aiType = -1; //new
			animationType = 10; //new
			npc.knockBackResist = 0f;
			//npc.alpha = 230;
			npc.value = Item.buyPrice(0, 10, 0, 0);
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.lavaImmune = true;
			npc.behindTiles = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AndromedaOne");
			npc.dontTakeDamage = false;
			npc.netAlways = true;
		}
		public override void AI()
		{
			CalValPlusGlobalNPC.androalive = npc.whoAmI;
			Mod clamMod =
				ModLoader.GetMod(
					"CalamityMod");
			clamMod.Call(ModContent.NPCType<Andromeda>(), 0.5f);
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

			//Spawn stationary turrets
			if (statprobe)
			{
				/*NPC.NewNPC((int)npc.Center.X + 300, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X + 340, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 1f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X + 260, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 2f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 300, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 3f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 340, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 4f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 260, (int)npc.Center.Y - 30, mod.NPCType("ActiveCannon"), 0, 5f, 0f, 0f, 0f, 255);*/
				NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 200, mod.NPCType("ShotgunDrone"), 0, 5f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X - 200, (int)npc.Center.Y, mod.NPCType("ShotgunDrone"), 0, 5f, 0f, 0f, 1f, 255);
				NPC.NewNPC((int)npc.Center.X + 200, (int)npc.Center.Y, mod.NPCType("ShotgunDrone"), 0, 5f, 0f, 0f, 2f, 255);
				NPC.NewNPC((int)npc.Center.X + 400, (int)npc.Center.Y - 300, mod.NPCType("MachineGunDrone"), 0, 5f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)npc.Center.X + -400, (int)npc.Center.Y - 300, mod.NPCType("MachineGunDrone"), 0, 5f, 0f, 0f, 1f, 255);
				statprobe = false;
			}
			//Setup phases
			if (npc.life <= npc.lifeMax * 0.3f)
			{
				npc.localAI[0] = 1f; //Change later
			}
			else
			{
				npc.localAI[0] = 1f;
			}

			//AI, juicy YuH coded AI

			//Positioning for turrets
			//Left mega cannon
			Vector2 leftpos;
			leftpos.X = npc.Center.X - 320;
			leftpos.Y = npc.Center.Y + 600 - bottomoffset;

			//Right mega cannon
			Vector2 rightpos;
			rightpos.X = npc.Center.X + 320;
			rightpos.Y = npc.Center.Y + 600 - bottomoffset;

			//Top right mega cannon
			Vector2 toprightpos;
			toprightpos.X = npc.Center.X + 130;
			toprightpos.Y = npc.Center.Y - 495;

			//Top left mega cannon
			Vector2 topleftpos;
			topleftpos.X = npc.Center.X - 130;
			topleftpos.Y = npc.Center.Y - 495;

			// Debug dusts

			/*Dust dusttl;
			dusttl = Main.dust[Terraria.Dust.NewDust(topleftpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dusttr;
			dusttr = Main.dust[Terraria.Dust.NewDust(toprightpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dustl;
			dustl = Main.dust[Terraria.Dust.NewDust(leftpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];
			Dust dustr;
			dustr = Main.dust[Terraria.Dust.NewDust(rightpos, 0, 0, DustID.Shadowflame, 1f, 1f, 0, new Color(109, 255, 0), 2f)];*/

			//Movement 
			
			if (npc.localAI[0] == 1f)
			{
				npc.TargetClosest();
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
				if (Main.player[npc.target].dead)
                {
					npc.velocity.Y *= 1.1f;
                }
				float playerY = Main.player[npc.target].position.Y - (npc.position.Y + npc.height);
				float playerX = Main.player[npc.target].position.X - (npc.position.X + npc.width);
				if (playerY < 50f)
				{
					npc.velocity.Y -= 0.1f;
				}
				if (playerY > 50f)
				{
					npc.velocity.Y += 0.1f;
				}
				if (npc.velocity.Y > 2f)
				{
					npc.velocity.Y = 2f;
				}
				if (npc.velocity.Y < -4f)
				{
					npc.velocity.Y = -4f;
				}

				if (playerX < 0f)
				{
					npc.velocity.X -= 0.1f;
				}
				if (playerX > 0f)
				{
					npc.velocity.X += 0.1f;
				}
				if (npc.velocity.X > 12f)
				{
					npc.velocity.X = 12f + (playerX * 0.0001f);
				}
				if (npc.velocity.X < -12f)
				{
					npc.velocity.X = -12f + (playerX * 0.0001f);
				}

				//npc.velocity.Y = 0;
				//npc.velocity.X = 0;

				//Attacks

				attacktimer++;
				int attackbase = 180;
				if (npc.life <= npc.lifeMax * 0.3f)
                {
					attacktimer++;
                }
				Player player = Main.player[npc.target];
				if (player.HasBuff(ModLoader.GetMod("CalamityMod").BuffType("AndromedaBuff")) || player.HasBuff(ModLoader.GetMod("CalamityMod").BuffType("AndromedaSmallBuff")))
				{
					attacktimer++;
				}

				if (attacktimer == attackbase || attacktimer == attackbase + 10 || attacktimer == attackbase + 20 || attacktimer == attackbase + 30 || attacktimer == attackbase + 40 || attacktimer == attackbase + 50 || attacktimer == attackbase + 60 || attacktimer == attackbase + 70 || attacktimer == attackbase + 80)
				{
					//Gauss blasts
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/GaussWeaponFire"));
					for (int x = 0; x < 4; x++)
					{
						Projectile.NewProjectile(rightpos.X, rightpos.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ModContent.ProjectileType<AndroMissile>(), 80, 0f, 255);
						Projectile.NewProjectile(leftpos.X, leftpos.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ModContent.ProjectileType<AndroMissile>(), 80, 0f, 255);
						//Projectile.NewProjectile(rightpos.X, rightpos.Y, 10, Main.rand.Next(-10, 10), ProjectileID.SaucerLaser, 80, 0f, 255);
						//Projectile.NewProjectile(leftpos.X, leftpos.Y, -10, Main.rand.Next(-10, 10), ProjectileID.SaucerLaser, 80, 0f, 255);
					}
				}
				if (attacktimer == attackbase * 2)
				{
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/PlasmaBolt"));
						for (int x = 0; x < 4; x++)
						{
							Projectile.NewProjectile(leftpos.X, leftpos.Y, -20, Main.rand.Next(-20, 20), ModContent.ProjectileType<AndromedaDeathrayLeft>(), 80, 0f, 255);
							Projectile.NewProjectile(rightpos.X, rightpos.Y, 20, Main.rand.Next(-20, 20), ModContent.ProjectileType<AndromedaDeathrayRight>(), 80, 0f, 255);
							//Projectile.NewProjectile(rightpos.X, rightpos.Y, 10, Main.rand.Next(-10, 10), ProjectileID.SaucerLaser, 80, 0f, 255);
							//Projectile.NewProjectile(leftpos.X, leftpos.Y, -10, Main.rand.Next(-10, 10), ProjectileID.SaucerLaser, 80, 0f, 255);
						}
					attacktimer = 0;
				}

				//Faces
				if (attacktimer < attackbase)
                {
					face = 0;
                }
				else if (attacktimer >= attackbase)
                {
					face = 3;
                }
				else
                {
					face = 0;
                }
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[npc.type];
			
			//Wings
			//Top
			Texture2D wingtexturetop = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingTop"));
			Texture2D wingtexturetopglow = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingTopGlow"));

			int wingtexturetopheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexturetop.Height / 1);

			Rectangle wingtexturetopsquare = new Rectangle(0, wingtexturetopheight, wingtexturetop.Width, wingtexturetop.Height / 1);
			Color wingtexturetopalpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturetop, npc.Center - Main.screenPosition + new Vector2(0f - 400, npc.gfxOffY - 40), wingtexturetopsquare, wingtexturetopalpha, npc.rotation + (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturetop, npc.Center - Main.screenPosition + new Vector2(0f + 400, npc.gfxOffY - 40), wingtexturetopsquare, wingtexturetopalpha, npc.rotation - (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturetopglow, npc.Center - Main.screenPosition + new Vector2(0f - 400, npc.gfxOffY - 40), wingtexturetopsquare, Color.White, npc.rotation + (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturetopglow, npc.Center - Main.screenPosition + new Vector2(0f + 400, npc.gfxOffY - 40), wingtexturetopsquare, Color.White, npc.rotation - (wingrot * 0.015f), Utils.Size(wingtexturetopsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			//Midle
			Texture2D wingtexturemiddle = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingMiddle"));
			Texture2D wingtexturemiddleglow = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingMiddleGlow"));

			int wingtexturemiddleheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexturemiddle.Height / 1);

			Rectangle wingtexturemiddlesquare = new Rectangle(0, wingtexturemiddleheight, wingtexturemiddle.Width, wingtexturemiddle.Height / 1);
			Color wingtexturemiddlealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturemiddle, npc.Center - Main.screenPosition + new Vector2(0f - 360, npc.gfxOffY - 20), wingtexturemiddlesquare, wingtexturemiddlealpha, npc.rotation + (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturemiddle, npc.Center - Main.screenPosition + new Vector2(0f + 360, npc.gfxOffY - 20), wingtexturemiddlesquare, wingtexturemiddlealpha, npc.rotation - (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturemiddleglow, npc.Center - Main.screenPosition + new Vector2(0f - 360, npc.gfxOffY - 20), wingtexturemiddlesquare, Color.White, npc.rotation + (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturemiddleglow, npc.Center - Main.screenPosition + new Vector2(0f + 360, npc.gfxOffY - 20), wingtexturemiddlesquare, Color.White, npc.rotation - (wingrot * 0.01f), Utils.Size(wingtexturemiddlesquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			//Bottom
			Texture2D wingtexturelower = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingLower"));
			Texture2D wingtexturelowerglow = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingLowerGlow"));

			int wingtexturelowerheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexturelower.Height / 1);

			Rectangle wingtexturelowersquare = new Rectangle(0, wingtexturelowerheight, wingtexturelower.Width, wingtexturelower.Height / 1);
			Color wingtextureloweralpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturelower, npc.Center - Main.screenPosition + new Vector2(0f - 360, npc.gfxOffY), wingtexturelowersquare, wingtextureloweralpha, npc.rotation + (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturelower, npc.Center - Main.screenPosition + new Vector2(0f + 360, npc.gfxOffY), wingtexturelowersquare, wingtextureloweralpha, npc.rotation - (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturelowerglow, npc.Center - Main.screenPosition + new Vector2(0f - 360, npc.gfxOffY), wingtexturelowersquare, Color.White, npc.rotation + (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturelowerglow, npc.Center - Main.screenPosition + new Vector2(0f + 360, npc.gfxOffY), wingtexturelowersquare, Color.White, npc.rotation - (wingrot * 0.0075f), Utils.Size(wingtexturelowersquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			//Curved
			Texture2D wingtexturecurve = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingCurve"));
			Texture2D wingtexturecurveglow = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaWingCurveGlow"));

			int wingtexturecurveheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexturecurve.Height / 1);

			Rectangle wingtexturecurvesquare = new Rectangle(0, wingtexturecurveheight, wingtexturecurve.Width, wingtexturecurve.Height / 1);
			Color wingtexturecurvealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexturecurve, npc.Center - Main.screenPosition + new Vector2(0f - 340, npc.gfxOffY - 20), wingtexturecurvesquare, wingtexturecurvealpha, npc.rotation + (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturecurve, npc.Center - Main.screenPosition + new Vector2(0f + 340, npc.gfxOffY - 20), wingtexturecurvesquare, wingtexturecurvealpha, npc.rotation - (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(wingtexturecurveglow, npc.Center - Main.screenPosition + new Vector2(0f - 340, npc.gfxOffY - 20), wingtexturecurvesquare, Color.White, npc.rotation + (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(wingtexturecurveglow, npc.Center - Main.screenPosition + new Vector2(0f + 340, npc.gfxOffY - 20), wingtexturecurvesquare, Color.White, npc.rotation - (wingrot * 0.0013f), Utils.Size(wingtexturecurvesquare) / 2f, npc.scale, SpriteEffects.None, 0f);


			//Top
			Texture2D androbottom = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaBottom"));

			int androbottomheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (androbottom.Height / 1);

			Rectangle androbottomsquare = new Rectangle(0, androbottomheight, androbottom.Width, androbottom.Height / 1);
			Color androbottomalpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(androbottom, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY + 320 + framer), androbottomsquare, androbottomalpha, npc.rotation, Utils.Size(androbottomsquare) / 2f, npc.scale, SpriteEffects.None, 0f);

			//Mega Busters
			Texture2D guntexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/AndromedaGun"));
			Texture2D gunglowtexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/AndromedaGunGlow"));

			int gunheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (guntexture.Height / 1);

			Rectangle gunsquare = new Rectangle(0, gunheight, guntexture.Width, guntexture.Height / 1);
			spriteBatch.Draw(guntexture, npc.Center - Main.screenPosition + new Vector2(0f - 270, npc.gfxOffY + 220 + framer), gunsquare, wingtexturetopalpha, npc.rotation, Utils.Size(gunsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(guntexture, npc.Center - Main.screenPosition + new Vector2(0f + 270, npc.gfxOffY + 220 + framer), gunsquare, wingtexturetopalpha, npc.rotation, Utils.Size(gunsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(gunglowtexture, npc.Center - Main.screenPosition + new Vector2(0f - 270, npc.gfxOffY + 220 + framer), gunsquare, Color.White, npc.rotation, Utils.Size(gunsquare) / 2f, npc.scale, SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(gunglowtexture, npc.Center - Main.screenPosition + new Vector2(0f + 270, npc.gfxOffY + 220 + framer), gunsquare, Color.White, npc.rotation, Utils.Size(gunsquare) / 2f, npc.scale, SpriteEffects.None, 0f);

			//Top
			Texture2D androbottom2 = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaBottomGlow"));

			int androbottomheight2 = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (androbottom2.Height / 1);

			Rectangle androbottomsquare2 = new Rectangle(0, androbottomheight2, androbottom2.Width, androbottom2.Height / 1);
			spriteBatch.Draw(androbottom2, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY + 320 + framer), androbottomsquare2, Color.White, npc.rotation, Utils.Size(androbottomsquare2) / 2f, npc.scale, SpriteEffects.None, 0f);

			//Top
			Texture2D androtoper = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaTop"));

			int androtopheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (androtoper.Height / 1);

			Rectangle androtopsquare = new Rectangle(0, androtopheight, androtoper.Width, androtoper.Height / 1);
			Color androtopalpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(androtoper, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY - 182), androtopsquare, androtopalpha, npc.rotation, Utils.Size(androtopsquare) / 2f, npc.scale, SpriteEffects.None, 0f);

			//Top
			Texture2D androtop = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/AndromedaTopGlow"));

			int androheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (androtop.Height / 1);

			Rectangle androsquare = new Rectangle(0, androheight, androtop.Width, androtop.Height / 1);
			spriteBatch.Draw(androtop, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY - 182), androsquare, Color.White, npc.rotation, Utils.Size(androsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
			//Screen
			/*Texture2D screentexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Andromeda"));

			int screentexturenheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (screentexture.Height / 1);

			Rectangle screentexturesquare = new Rectangle(0, screentexturenheight, screentexture.Width, screentexture.Height / 1);
			spriteBatch.Draw(screentexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), screentexturesquare, drawColor, npc.rotation, Utils.Size(screentexturesquare) / 2f, npc.scale, SpriteEffects.None, 0f);*/

			return true;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[npc.type];
			//Face
			Texture2D facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/NeutralAlt"));
			if (face == 1)
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/Neutral"));
			}
			else if (face == 2)
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/Cmon"));
			}
			else if (face == 3)
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/Exclamation"));
			}
			else if (face == 4)
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/Cheeky"));
			}
			else if (face == 5)
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/Death"));
			}
			else
			{
				facetexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/FacialExpressions/NeutralAlt"));
			}

			int facetexturenheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (facetexture.Height / 1);

			Rectangle facetexturesquare = new Rectangle(0, facetexturenheight, facetexture.Width, facetexture.Height / 1);
			spriteBatch.Draw(facetexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), facetexturesquare, Color.White, npc.rotation, Utils.Size(facetexturesquare) / 2f, npc.scale, SpriteEffects.None, 0f);

			
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
		public override void BossLoot(ref string name, ref int potionType)
		{
			Mod calam = ModLoader.GetMod("CalamityMod");
			potionType = calam.ItemType("OmegaHealingPotion");
		}
		public override void NPCLoot()
		{
			Mod calam = ModLoader.GetMod("CalamityMod");
			Mod calval = ModLoader.GetMod("CalValEX");
			//Main.rand.Next(30, 41)
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, calval.ItemType("AndroombaGBC"), 1);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, calam.ItemType("Baguette"), Main.rand.Next(30, 41));
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			Vector2 gorespeed;
			gorespeed.X = npc.velocity.X;
			gorespeed.Y = npc.velocity.Y;
			Vector2 tower = new Vector2(npc.Center.X + 30, npc.Center.Y - 300);
			Vector2 bustergore = new Vector2(npc.Center.X - 180, npc.Center.Y + 200);
			Vector2 bustergore2 = new Vector2(npc.Center.X - 80, npc.Center.Y - 180);
			Vector2 undergore = new Vector2(npc.Center.X - 90, npc.Center.Y - 20);
			Vector2 pillar = new Vector2(npc.Center.X + 10, npc.Center.Y - 95);
			Vector2 towerdown = new Vector2(npc.Center.X - 20, npc.Center.Y + 80);
			Vector2 grapple = new Vector2(npc.Center.X + 50, npc.Center.Y + 150);
			if (npc.life <= 0)
			{
				Gore.NewGore(tower, gorespeed, mod.GetGoreSlot("Gores/Andromeda1"), 1f); //Tower
				Gore.NewGore(bustergore, gorespeed, mod.GetGoreSlot("Gores/Andromeda2"), 1f); //Mega Buster
				Gore.NewGore(bustergore2, gorespeed, mod.GetGoreSlot("Gores/Andromeda2"), 1f); //Mega Buster
				Gore.NewGore(undergore, gorespeed, mod.GetGoreSlot("Gores/Andromeda3"), 1f); //Base next to screen
				Gore.NewGore(towerdown, gorespeed, mod.GetGoreSlot("Gores/Andromeda4"), 1f); //Below tower
				Gore.NewGore(towerdown, gorespeed, mod.GetGoreSlot("Gores/Andromeda4"), 1f); //Below tower
				Gore.NewGore(pillar, gorespeed, mod.GetGoreSlot("Gores/Andromeda5"), 1f); //Small pillar
				Gore.NewGore(towerdown, gorespeed, mod.GetGoreSlot("Gores/Andromeda6"), 1f); //Below tower chunk
				Gore.NewGore(towerdown, gorespeed, mod.GetGoreSlot("Gores/Andromeda6"), 1f); //Below tower chunk
				Gore.NewGore(grapple, gorespeed, mod.GetGoreSlot("Gores/Andromeda7"), 1f); //Lower grapple area
			}
		}
	}
}